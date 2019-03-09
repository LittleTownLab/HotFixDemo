using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;
using System.IO;
using ABFW;

namespace HotUpdateModel
{
	public class LuaHelper
	{
        private static LuaHelper _Instance;
        private LuaEnv _luaEnv = new LuaEnv();
        private Dictionary<string, byte[]> _DicLuaFileArray = new Dictionary<string, byte[]>();

        private LuaHelper()
        {
            _luaEnv.AddLoader(customLoader);
        }

        public static LuaHelper GetInstance()
        {
            if(_Instance == null)
            {
                _Instance = new LuaHelper();
            }

            return _Instance ;
        }

        public void DoString(string chunk, string chunkName = "chunk", LuaTable env = null)
        {
            _luaEnv.DoString(chunk, chunkName, env);
        }

        public object[] CallLuaFunction(string luaScriptName, string luaMethodName, params object[] args)
        {
            LuaTable luaTab = _luaEnv.Global.Get<LuaTable>(luaScriptName);
            LuaFunction luaFun = luaTab.Get<LuaFunction>(luaMethodName);

            return luaFun.Call(args);
        }

        private byte[] customLoader(ref string fileName)
        {
            string luaPath = PathTools.GetABOutPath() + PathTools.LUA_DEPLOY_PATH;

            if(_DicLuaFileArray.ContainsKey(fileName))
            {
                return _DicLuaFileArray[fileName];
            }
            else
            {
                return ProcessDIR(new DirectoryInfo(luaPath), fileName);
            }
        }

        private byte[] ProcessDIR(FileSystemInfo fileSysInfo, string fileName)
        {
            DirectoryInfo dirInf = fileSysInfo as DirectoryInfo;
            FileSystemInfo[] files = dirInf.GetFileSystemInfos();

            foreach (FileSystemInfo item in files)
            {
                FileInfo fileInfo = item as FileInfo;

                //dir
                if (fileInfo == null)
                {
                    ProcessDIR(item, fileName);
                }
                //file
                else
                {
                    string tmpName = item.Name.Split('.')[0];

                    if(item.Extension == ".meta" || tmpName != fileName)
                    {
                        continue;
                    }

                    byte[] bytes = File.ReadAllBytes(fileInfo.FullName);

                    _DicLuaFileArray.Add(fileName, bytes);

                    return bytes;
                }
            }

            return null;
        }
	}
}