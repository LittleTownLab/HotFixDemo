-- 启动ABFW 框架
-- 通过调动ABFW 核心API， 实现lua中加载unity 中的资源


local string scensName = "mainscenes"
local string packageName = "mainscenes/ui.ab"
local string assetName = "UI_Notice.prefab"

LauchABFW = {}
local this = LauchABFW

local yie_return=require('CorLaunchABFW').yield_return

local co = coroutine.create(
		function()
		yie_return(scensName, packageName, assetName)
		end
)

function LauchABFW:StartABFW()
	assert(coroutine.resume((co)))
end
