using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HotUpdateModel
{
    public class StartGame : MonoBehaviour, IStartGame
    {
        public void ReceiveInfoStartRuning()
        {
            print("game start!");

            LuaHelper.GetInstance().DoString("require 'LauchABFW'");
            LuaHelper.GetInstance().CallLuaFunction("LauchABFW", "StartABFW");
        }
    }
}