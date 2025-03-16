using System.Collections.Generic;
using System.Text;

namespace SOC.Classes.Lua
{
    public static class StaticObjectiveFunctions
    {
        public static readonly LuaTableEntry IsTargetSetMessageIdForItem = LuaFunction.ToTableEntry("IsTargetSetMessageIdForItem", new string[] { "gameId", "messageId", "checkAnimalId" },
    @" if messageId == ""PickUpDormant"" then
    for i, targetInfo in pairs(qvars.targetItemList) do
      if gameId == targetInfo.equipId and targetInfo.messageId == ""None"" and targetInfo.active == false then
        targetInfo.messageId = messageId
        return true, true
      end
    end
  elseif messageId == ""PickUpActive"" or messageId == ""Activate"" then
    for i, targetInfo in pairs(qvars.targetItemList) do
      if gameId == targetInfo.equipId and targetInfo.messageId == ""None"" and targetInfo.active == true then
        targetInfo.messageId = messageId
        return true, true
      end
    end
  end
  return false, false; ");

        public static readonly LuaTableEntry TallyItemTargets = LuaFunction.ToTableEntry("TallyItemTargets", new string[] { "totalTargets", "objectiveCompleteCount", "objectiveFailedCount" },
            @" for i, targetInfo in pairs(qvars.targetItemList) do
    local dynamicQuestType = ""RECOVERED""
    for _, ObjectiveTypeInfo in ipairs(qvars.ObjectiveTypeList.itemTargets) do
      if ObjectiveTypeInfo.Check(targetInfo) then
        dynamicQuestType = ObjectiveTypeInfo.Type
        break
      end
    end
    local targetMessageId = targetInfo.messageId

    if targetMessageId ~= ""None"" then
        if dynamicQuestType == ""RECOVERED"" then
          if (targetMessageId == ""PickUpDormant"" or targetMessageId == ""PickUpActive"") then
            objectiveCompleteCount = objectiveCompleteCount + 1
          elseif (targetMessageId == ""Activate"") then
            objectiveFailedCount = objectiveFailedCount + 1
          end

        elseif dynamicQuestType == ""ELIMINATE"" then
          if (targetMessageId == ""PickUpActive"" or targetMessageId == ""Activate"") then
            objectiveCompleteCount = objectiveCompleteCount + 1
          end

        elseif dynamicQuestType == ""KILLREQUIRED"" then
          if (targetMessageId == ""Activate"") then
            objectiveCompleteCount = objectiveCompleteCount + 1
          elseif (targetMessageId == ""PickUpActive"") then
            objectiveFailedCount = objectiveFailedCount + 1
          end
      end
  	end
    totalTargets = totalTargets + 1
  end
  return totalTargets, objectiveCompleteCount, objectiveFailedCount; ");

        public static readonly LuaTableEntry IsTargetSetMessageIdForGenericEnemy = LuaFunction.ToTableEntry("IsTargetSetMessageIdForGenericEnemy", new string[] { "gameId", "messageId", "checkAnimalId" },
            @"if mvars.ene_questTargetList[gameId] then
	local targetInfo = mvars.ene_questTargetList[gameId]
	local intended = true
	if targetInfo.messageId ~= ""None"" and targetInfo.isTarget == true then
	  intended = false
	elseif targetInfo.isTarget == false then
	  intended = false
	end
	targetInfo.messageId = messageId or ""None""
	return true, intended
  end
  return false, false; ");

        public static readonly LuaTableEntry TallyGenericTargets = LuaFunction.ToTableEntry("TallyGenericTargets", new string[] { "totalTargets", "objectiveCompleteCount", "objectiveFailedCount" },
            @"for targetGameId, targetInfo in pairs(mvars.ene_questTargetList) do
    local dynamicQuestType = ""ELIMINATE""
    local isTarget = targetInfo.isTarget or false
    local targetMessageId = targetInfo.messageId

    if isTarget == true then
      if qvars.ObjectiveTypeList.genericTargets ~= nil then
        for _, ObjectiveTypeInfo in ipairs(qvars.ObjectiveTypeList.genericTargets) do
          if ObjectiveTypeInfo.Check(targetGameId) then
            dynamicQuestType = ObjectiveTypeInfo.Type
            break
          end
        end
      end

      if targetMessageId ~= ""None"" then
        if dynamicQuestType == ""RECOVERED"" then
          if (targetMessageId == ""Fulton"") or (targetMessageId == ""InHelicopter"") then
            objectiveCompleteCount = objectiveCompleteCount + 1
          elseif (targetMessageId == ""FultonFailed"") or (targetMessageId == ""Dead"") or (targetMessageId == ""VehicleBroken"") or (targetMessageId == ""LostControl"") then
            objectiveFailedCount = objectiveFailedCount + 1
          end

        elseif dynamicQuestType == ""ELIMINATE"" then
          if (targetMessageId == ""Fulton"") or (targetMessageId == ""InHelicopter"") or (targetMessageId == ""FultonFailed"") or (targetMessageId == ""Dead"") or (targetMessageId == ""VehicleBroken"") or (targetMessageId == ""LostControl"") then
            objectiveCompleteCount = objectiveCompleteCount + 1
          end

        elseif dynamicQuestType == ""KILLREQUIRED"" then
          if (targetMessageId == ""FultonFailed"") or (targetMessageId == ""Dead"") or (targetMessageId == ""VehicleBroken"") or (targetMessageId == ""LostControl"") then
            objectiveCompleteCount = objectiveCompleteCount + 1
          elseif (targetMessageId == ""Fulton"") or (targetMessageId == ""InHelicopter"")  then
            objectiveFailedCount = objectiveFailedCount + 1
          end
        end
      end
      totalTargets = totalTargets + 1
    end
  end
  return totalTargets, objectiveCompleteCount, objectiveFailedCount; ");

        public static readonly LuaTableEntry IsTargetSetMessageIdForAnimal = LuaFunction.ToTableEntry("IsTargetSetMessageIdForAnimal", new string[] { "gameId", "messageId", "checkAnimalId" },
    @"if checkAnimalId ~= nil then
    local databaseId = TppAnimal.GetDataBaseIdFromAnimalId(checkAnimalId)
    local isTarget = false
    for animalId, targetInfo in pairs(mvars.ani_questTargetList) do
      if targetInfo.idType == ""animalId"" then
        if animalId == checkAnimalId then
          targetInfo.messageId = messageId or ""None""
            isTarget = true
          end
        elseif targetInfo.idType == ""databaseId"" then
          if animalId == databaseId then
            targetInfo.messageId = messageId or ""None""
            isTarget = true
          end
        elseif targetInfo.idType == ""targetName"" then
          local animalGameId = GetGameObjectId(animalId)
          if animalGameId == gameId then
            targetInfo.messageId = messageId
            isTarget = true
          end
        end
      end
      return isTarget, true
    end
  return false, false");

        public static readonly LuaTableEntry TallyAnimalTargets = LuaFunction.ToTableEntry("TallyAnimalTargets", new string[] { "totalTargets", "objectiveCompleteCount", "objectiveFailedCount" },
            @" local dynamicQuestType = qvars.ObjectiveTypeList.animalObjective
  for animalId, targetInfo in pairs(mvars.ani_questTargetList) do
    local targetMessageId = targetInfo.messageId

    if targetMessageId ~= ""None"" then
      if dynamicQuestType == ""RECOVERED"" then
        if (targetMessageId == ""Fulton"") then
          objectiveCompleteCount = objectiveCompleteCount + 1
        elseif (targetMessageId == ""FultonFailed"") or (targetMessageId == ""Dead"") then
          objectiveFailedCount = objectiveFailedCount + 1
        end

      elseif dynamicQuestType == ""ELIMINATE"" then
        if (targetMessageId == ""Fulton"") or (targetMessageId == ""FultonFailed"") or (targetMessageId == ""Dead"") then
          objectiveCompleteCount = objectiveCompleteCount + 1
        end

      elseif dynamicQuestType == ""KILLREQUIRED"" then
        if (targetMessageId == ""FultonFailed"") or (targetMessageId == ""Dead"") then
          objectiveCompleteCount = objectiveCompleteCount + 1
        elseif (targetMessageId == ""Fulton"") then
          objectiveFailedCount = objectiveFailedCount + 1
        end
      end
    end
    totalTargets = totalTargets + 1
  end
  return totalTargets, objectiveCompleteCount, objectiveFailedCount; ");

        public static readonly LuaTableEntry CheckQuestAllTargetDynamicFunction = LuaFunction.ToTableEntry("CheckQuestAllTargetDynamic", new string[] { "messageId", "gameId", "checkAnimalId" }, "" +
            @"local currentQuestName=TppQuest.GetCurrentQuestName()
  if TppQuest.IsEnd(currentQuestName) then
    return TppDefine.QUEST_CLEAR_TYPE.NONE
  end

  local inTargetList = false
  local intendedTarget = true
  for IsTargetSetMethod, _ in pairs(qvars.CheckQuestMethodPairs) do 
    inTargetList, intendedTarget = IsTargetSetMethod(gameId, messageId, checkAnimalId) 
    if inTargetList == true then 
        break 
    end 
  end 

  if inTargetList == false then
    return TppDefine.QUEST_CLEAR_TYPE.NONE
  end

  local totalTargets = 0
  local objectiveCompleteCount = 0
  local objectiveFailedCount = 0
  for _, TallyMethod in pairs(qvars.CheckQuestMethodPairs) do 
    totalTargets, objectiveCompleteCount, objectiveFailedCount = TallyMethod(totalTargets, objectiveCompleteCount, objectiveFailedCount) 
  end 

  if totalTargets > 0 then
    if objectiveCompleteCount >= totalTargets then
      return TppDefine.QUEST_CLEAR_TYPE.CLEAR
    elseif objectiveFailedCount > 0 then
      return TppDefine.QUEST_CLEAR_TYPE.FAILURE
    elseif objectiveCompleteCount > 0 then
      if intendedTarget == true then
        local showAnnounceLogId=TppQuest.questCompleteLangIds[TppQuest.GetCurrentQuestName()]
        if showAnnounceLogId then
          TppUI.ShowAnnounceLog(showAnnounceLogId,objectiveCompleteCount,totalTargets)
        end
      end
    end
  end
  return TppDefine.QUEST_CLEAR_TYPE.NONE");
        
    }
}
