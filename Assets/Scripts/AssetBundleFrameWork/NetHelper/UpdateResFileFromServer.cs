using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ABFW;
using System.IO;

namespace HotUpdateModel
{
    public class UpdateResFileFromServer : MonoBehaviour
    {

        public bool EnableSelf = true;
        private string _DownLoadPath = string.Empty;
        private string _ServerURL = PathTools.SERVER_URL;

        private void Awake()
        {
            if (EnableSelf)
            {
                _DownLoadPath = PathTools.GetABOutPath();

                StartCoroutine(DownLoadResAndCheckUpdate(_ServerURL));
            }
            else
            {
                Debug.Log("### WARING : " + GetType() + "此脚本已禁用，停止从服务器下载更新服务");
                BroadcastMessage(ABDefine.ReceiveInfoStartRuning, SendMessageOptions.DontRequireReceiver);
            }
        }

        IEnumerator DownLoadResAndCheckUpdate(string serverUrl)
        {
            /* 1： 下载“校验文件”到客户端 */
            /* 2： 根据 "校验文件”， 客户端逐条读取资源文件， 然后与本客户端相同的资源文件进行MD5编码对比。*/
            /* 3： 热更客户端没有服务器（增加）文件， 直接下载服务器端文件即可。*/
            /* 4： 客户端存在与服务器相同的文件名，但MD5编码对比不一致，说明服务器端对应的资源文件发生了更新，则客户端下载最新的资源文件。         */


            if (string.IsNullOrEmpty(serverUrl))
            {
                Debug.LogError(GetType() + " sever url is error!");

                yield break;
            }



            string fileURL = serverUrl + ABDefine.ProjectVerifyFile;

            WWW www = new WWW(fileURL);

            yield return www;

            if (www.error != null && !string.IsNullOrEmpty(www.error))
            {
                Debug.LogError("www error " + www.error);
                yield break;
            }

            if (!Directory.Exists(_DownLoadPath))
            {
                Directory.CreateDirectory(_DownLoadPath);
            }
            Debug.Log("_DownLoadPath = " + _DownLoadPath);

            File.WriteAllBytes(_DownLoadPath + ABDefine.ProjectVerifyFile, www.bytes);

            string strServerFileText = www.text;
            string[] lines = strServerFileText.Split('\n');

            for (int i = 0; i < lines.Length; i++)
            {
                if (string.IsNullOrEmpty(lines[i]))
                {
                    continue;
                }

                string[] fileAndMD5 = lines[i].Split('|');
                string strServerFileName = fileAndMD5[0].Trim();
                string strServerMD5 = fileAndMD5[1].Trim();

                string strLocalFile = _DownLoadPath + "/" + strServerFileName;

                if (!File.Exists(strLocalFile))
                {
                    Debug.Log(strLocalFile);

                    string dir = Path.GetDirectoryName(strLocalFile);
                    if (!string.IsNullOrEmpty(dir))
                    {
                        Directory.CreateDirectory(dir);
                    }

                    yield return StartCoroutine(DownLoadFileByWWW(serverUrl + "/" + strServerFileName, strLocalFile));
                }
                else
                {
                    string strLocalMD5 = Helps.GetMD5Vlues(strLocalFile);

                    if (!strLocalMD5.Equals(strServerMD5))
                    {
                        File.Delete(strLocalFile);
                        yield return StartCoroutine(DownLoadFileByWWW(serverUrl + "/" + strServerFileName, strLocalFile));
                        Debug.Log("download res file from server " + strServerFileName);
                    }
                }
            }

            yield return new WaitForEndOfFrame();

            Debug.Log("更新完成！！！");

            BroadcastMessage(ABDefine.ReceiveInfoStartRuning, SendMessageOptions.DontRequireReceiver);
        }

        IEnumerator DownLoadFileByWWW(string wwwURL, string localFileAddress)
        {
            WWW www = new WWW(wwwURL);

            yield return www;

            if (www.error != null && !string.IsNullOrEmpty(www.error))
            {
                Debug.LogError("www error " + www.error);
                yield break;
            }
            Debug.Log("localFileAddress = " + localFileAddress);

            File.WriteAllBytes(localFileAddress, www.bytes);
        }
    }
}