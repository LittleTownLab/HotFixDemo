using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

namespace HotUpdateModel
{
    [Hotfix]
	public class TestHotFix1 : MonoBehaviour 
	{
        LuaEnv luaEnv = new LuaEnv();
        //public int tick;

        private void Start()
        {
            Debug.Log("测试lua中的热补丁技术");

            InvokeRepeating("InvokeInCsharp", 1f, 2f);
        }

        public void InvokeInCsharp()
        {
            Debug.Log("在C#中执行的方法");
        }

        public void InvokeInLua()
        {
            Debug.Log("准备lua调用");
            luaEnv.DoString(@"xlua.hotfix(
                                           CS.HotUpdateModel.TestHotFix1, 'InvokeInCsharp', function()
                                           print('this is run funciotn in lua') 
                                           end
                                         )");
        }
    }
}