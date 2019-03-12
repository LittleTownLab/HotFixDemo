using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HotUpdateModel
{
    public class TestLuaStartGame : MonoBehaviour, IStartGame
    {
        public void ReceiveInfoStartRuning()
        {
            Debug.Log("star lua hotFix");
            LuaHelper.GetInstance().DoString("require 'Test_ProjectHotFixListInfo'");
            LuaHelper.GetInstance().CallLuaFunction("Test_ProjectHotFixListInfo", "HotFixScriptsMethod");
        }
    }
}