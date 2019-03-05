using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using ABFW;

namespace HotUpdateModel
{    
    public class CopyLuaFileToSA
    {
        private static string _LuaDIRPath = Application.dataPath + PathTools.LUA_RESOURCE_PATH;
        private static string _CopyTargetDIR = PathTools.GetABOutPath() + PathTools.LUA_DEPLOY_PATH;

        [MenuItem("AssetBundelTools/CopyLuaFileToSA")]
        public static void CopyLuaFileTo()
        {
            DirectoryInfo dirInfo = new DirectoryInfo(_LuaDIRPath);
            FileInfo[] files = dirInfo.GetFiles();
            //Debug.Log(_CopyTargetDIR);
            //dirInfo.MoveTo(_CopyTargetDIR);

            if(!Directory.Exists(_CopyTargetDIR))
            {
                Directory.CreateDirectory(_CopyTargetDIR);
            }

            foreach (FileInfo infoObj in files)
            {
                File.Copy(infoObj.FullName, _CopyTargetDIR + "/" + infoObj.Name, true);
            }

            AssetDatabase.Refresh();
            Debug.Log("copy lua fles success!");
        }
    }
}