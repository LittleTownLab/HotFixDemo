Test_ProjectHotFixListInfo = {}
local this = Test_ProjectHotFixListInfo

function this:HotFixScriptsMethod()

xlua.private_accessible(CS.HotUpdateModel.UIMgr)
xlua.hotfix(CS.HotUpdateModel.UIMgr, 'Start',
				function(self)
					self._CountDownNum = 10
				end

)

xlua.private_accessible(CS.HotUpdateModel.UIMgr)
xlua.hotfix(CS.HotUpdateModel.UIMgr, 'Update',
				function(self)
					if(CS.UnityEngine.Time.frameCount%60 == 0) then
						self._CountDownNum = self._CountDownNum - 1;
						self.TxtNumber.text = tostring(self._CountDownNum);
					end
				end

)

end
