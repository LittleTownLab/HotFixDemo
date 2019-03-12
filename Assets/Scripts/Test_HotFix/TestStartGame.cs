using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ABFW;

namespace HotUpdateModel
{
	public class TestStartGame : MonoBehaviour, IStartGame
	{
        private string _ScenesName = "mainscenes";
        private string _AssetBundleName = "mainscenes/ui.ab";
        private string _AssetName = "UIContDown.prefab";

        public void ReceiveInfoStartRuning()
        {
            LoadUICountDown();
        }

        public void LoadUICountDown()
        {
            AssetBundleMgr.GetInstance().LoadAssetBundlePackage(_ScenesName, _AssetBundleName, LoadAllABComplete);
        }

        void LoadAllABComplete(string abName)
        {
            UnityEngine.Object tmpObj = null;

            tmpObj = AssetBundleMgr.GetInstance().LoadAsset(_ScenesName, _AssetBundleName, _AssetName, false);

            if(tmpObj != null)
            {
                Instantiate(tmpObj);
            }
        }
    }
}