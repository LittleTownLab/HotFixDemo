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

            // Debug.Log("verifyFileOutPath = " + verifyFileOutPath);

            if(File.Exists(verifyFileOutPath))
            {
                File.Delete(verifyFileOutPath);
            }

            ListFile(new DirectoryInfo(abOutPath), ref fileList);
        }

        public static void ListFile(FileSystemInfo fileSysInfo, ref List<string> fileList)
        {
            DirectoryInfo dirInfo = fileSysInfo as DirectoryInfo;

            FileSystemInfo[] fileSysInfos = dirInfo.GetFileSystemInfos();

            foreach(FileSystemInfo item in fileSysInfos)
            {
                FileInfo fileInfo = item as FileInfo;

                if(fileInfo != null)
                {
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
                    ListFile(fileInfo, ref fileList);
                }
            }
        }
    }
}