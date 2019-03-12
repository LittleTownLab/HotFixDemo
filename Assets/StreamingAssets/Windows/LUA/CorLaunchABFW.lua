--加载AB包

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
			print("CorLaunchABFW.lua/(回调函数) / FinishWork()/加载指定包资源成功！")
		else
			print("### 发生错误 ：CorLaunchABFW.lua/(回调函数) / FinishWork()/加载对象为nil , 请检查！")
		end
end

return
{
	yield_return = util.async_to_sync(LoadABPackage)
}
