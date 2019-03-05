using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using ABFW;
using System.IO;
using System;

namespace HotUpdateModel
{
    public class CreateVerifyFiles
    {
        [MenuItem("AssetBundelTools/Create Verify File")]
        public static void CreateFileMethod()
        {
            string abOutPath = PathTools.GetABOutPath();
            string verifyFileOutPath = abOutPath + "/ProjectVerifyFile.txt";
            List<string> fileList = new List<string>();

            //Debug.Log("verifyFileOutPath = " + verifyFileOutPath);
            Debug.Log("abOutPath = " + abOutPath);

            if (File.Exists(verifyFileOutPath))
            {
                File.Delete(verifyFileOutPath);
            }

            ListFile(new DirectoryInfo(abOutPath), ref fileList);

            //foreach (var item in fileList)
            //{
            //    Debug.Log(item);
            //}
            WriteVerifyFile(verifyFileOutPath, abOutPath, fileList);
        }

        private static void ListFile(FileSystemInfo fileSysInfo, ref List<string> fileList)
        {
            Debug.Log("enter------------------ListFile-----------------enter");

            DirectoryInfo dirInfo = fileSysInfo as DirectoryInfo;
            FileSystemInfo[] fileSysInfos = dirInfo.GetFileSystemInfos();

            //Debug.Log("fileSysInfo " + fileSysInfo);
            //Debug.Log("dirInfo " + dirInfo);

            //foreach (var item in fileSysInfos)
            //{
            //    Debug.Log("item " + item.FullName);
            //}

            //return;

            foreach (FileSystemInfo item in fileSysInfos)
            {
                Debug.Log("\n---- item -----\n" + item.FullName);
                FileInfo fileInfo = item as FileInfo;

                if(fileInfo != null)
                {
                    Debug.Log("fie name : " + fileInfo.FullName);

                    string strFileFullName = fileInfo.FullName.Replace("\\", "/");

                    string fileExt = Path.GetExtension(strFileFullName);

                    if(fileExt.EndsWith(".meta") || fileExt.EndsWith(".bak"))
                    {
                        continue;
                    }
     
                    fileList.Add(strFileFullName);
                }
                else
                {
                    Debug.Log("dir name : " + item.FullName);
                    ListFile(item, ref fileList);
                }
            }            

            Debug.Log("out------------------ListFile-----------------out");
        }

        private static void WriteVerifyFile(string verifyFileOutPath, string abOutPath, List<string> fileLists)
        {
            using (FileStream fs = new FileStream(verifyFileOutPath, FileMode.CreateNew))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    for (int i = 0; i < fileLists.Count; i++)
                    {
                        string strFileName = fileLists[i];
                        string strFileMD4 = Helps.GetMD5Vlues(strFileName);
                        string strTureName = strFileName.Replace(abOutPath + "/", string.Empty);

                        sw.WriteLine(strTureName + "|" + strFileMD4);
                    }
                }

            }

            AssetDatabase.Refresh();
            Debug.Log("校验文件生成！");
        }
    }
}