using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;
using UnityEngine.UI;

namespace HotUpdateModel
{
    [Hotfix]
	public class UIMgr : MonoBehaviour 
	{
        public Text TxtNumber;
        private int _CountDownNum = 0;

        private void Start()
        {
            _CountDownNum = 0;
        }

        void Update()
        {
            if(Time.frameCount % 60 == 0)
            {
                ++_CountDownNum;
                TxtNumber.text = _CountDownNum.ToString();
            }
        }

    }
}