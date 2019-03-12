--����AB��

local string loc_ScenesName = nil
local string loc_PackageName = nil
local string loc_AssetName = nil


local util = require("xlua.util")

local abMgrClass = CS.ABFW.AssetBundleMgr
local abMgrObj = abMgrClass.GetInstance()

local function LoadABPackage(scenesName, packageName, assetName)

		loc_ScenesName = scenesName
		loc_PackageName = packageName
		loc_AssetName = assetName

		abMgrObj:LoadAssetBundlePackage(loc_ScenesName, loc_PackageName, FinishWork)

end

function FinishWork()
		local assetObj = abMgrObj:LoadAsset(loc_ScenesName,loc_PackageName,loc_AssetName,false)

		if(assetObj) then
			CS.UnityEngine.Object.Instantiate(assetObj)
			print("CorLaunchABFW.lua/(�ص�����) / FinishWork()/����ָ������Դ�ɹ���")
		else
			print("### �������� ��CorLaunchABFW.lua/(�ص�����) / FinishWork()/���ض���Ϊnil , ���飡")
		end
end

return
{
	yield_return = util.async_to_sync(LoadABPackage)
}
