using SOC.Classes.Lua;

namespace SOC.Classes.Lua
{
    public class QStep_Main
    {
        public StrCode32Table StrCode32Table = new StrCode32Table();

        LuaTable QStep_Main_Table = new LuaTable();

        public LuaFunctionBuilder OnEnterFunction = new LuaFunctionBuilder();
        public LuaFunctionBuilder OnLeaveFunction = new LuaFunctionBuilder();

        public QStep_Main()
        {
            OnEnterFunction.AppendLuaValue(Lua.FunctionCall(Lua.TableIdentifier("Fox", "Log"), Lua.String("QStep_Main OnEnter")));
            OnLeaveFunction.AppendLuaValue(Lua.FunctionCall(Lua.TableIdentifier("Fox", "Log"), Lua.String("QStep_Main OnLeave")));
        }

        public LuaTableEntry Get(string strCode32TableVariableName)
        {
            QStep_Main_Table.Add(
                Lua.TableEntry("Messages", Lua.Function("return |[1|FUNCTION_CALL]|", Lua.FunctionCall("StrCode32Table", StrCode32Table.ToStrCode32Table(strCode32TableVariableName))), false),
                Lua.TableEntry("OnEnter", OnEnterFunction.ToFunction()),
                Lua.TableEntry("OnLeave", OnLeaveFunction.ToFunction())
            );

            return Lua.TableEntry("QStep_Main", QStep_Main_Table, true);
        }
    }
}

// as much as I want to refactor & redesign all of this to display on the script page if the user selects a target, there's really not a lot of value for the user beyond "oh that's cool".
// Maybe revisit if I want objects to add their own event scripts?
public static class QStep_Main_TargetMessages 
{
    static readonly Script PlayerPickUpWeapon = new Script(
        new StrCode32("Player", Lua.String("OnPickUpWeapon")),
        LuaFunction.ToTableEntry(
            "CheckTargetDynamic", 
            StrCode32.DefaultParameters,
            $"local isClearType = qvars.CheckQuestAllTargetDynamic(\"PickUpDormant\", {StrCode32.DefaultParameters[1]}); TppQuest.ClearWithSave(isClearType); ")
        );

    static readonly Script PlayerPickUpPlaced = new Script(
        new StrCode32("Player", Lua.String("OnPickUpPlaced")),
        LuaFunction.ToTableEntry(
            "CheckTargetDynamic", 
            StrCode32.DefaultParameters,
            $"if TppPlaced.IsQuestBlock({StrCode32.DefaultParameters[2]}) then local isClearType = qvars.CheckQuestAllTargetDynamic(\"PickUpActive\", {StrCode32.DefaultParameters[1]}); TppQuest.ClearWithSave(isClearType); end; "));

    static readonly Script PlacedActivatePlaced = new Script(
        new StrCode32("Placed", Lua.String("OnActivatePlaced")),
        LuaFunction.ToTableEntry(
            "CheckTargetDynamic", 
            StrCode32.DefaultParameters,
            $"if TppPlaced.IsQuestBlock({StrCode32.DefaultParameters[1]}) then local isClearType = qvars.CheckQuestAllTargetDynamic(\"Activate\", {StrCode32.DefaultParameters[0]}); TppQuest.ClearWithSave(isClearType); end; "));

    static readonly Script GameObjectDead = new Script(
        new StrCode32("GameObject", Lua.String("Dead")),
        LuaFunction.ToTableEntry(
            "CheckTargetDynamic", 
            StrCode32.DefaultParameters,
            $"local isClearType = qvars.CheckQuestAllTargetDynamic(\"Dead\", {StrCode32.DefaultParameters[0]}, {StrCode32.DefaultParameters[2]}); TppQuest.ClearWithSave(isClearType); "));

    static readonly Script GameObjectFultonInfo = new Script(
        new StrCode32("GameObject", Lua.String("FultonInfo")),
        LuaFunction.ToTableEntry(
            "CheckTargetDynamic", 
            StrCode32.DefaultParameters,
            $"if mvars.fultonInfo ~= TppDefine.QUEST_CLEAR_TYPE.NONE then TppQuest.ClearWithSave(mvars.fultonInfo) end; mvars.fultonInfo = TppDefine.QUEST_CLEAR_TYPE.NONE; "));

    static readonly Script GameObjectFulton = new Script(
        new StrCode32("GameObject", Lua.String("Fulton")),
        LuaFunction.ToTableEntry(
            "CheckTargetDynamic", 
            StrCode32.DefaultParameters,
            $"local isClearType = qvars.CheckQuestAllTargetDynamic(\"Fulton\", {StrCode32.DefaultParameters[0]}, {StrCode32.DefaultParameters[1]}); TppQuest.ClearWithSave(isClearType); "));

    static readonly Script GameObjectFultonFailed = new Script(
        new StrCode32("GameObject", Lua.String("FultonFailed")),
        LuaFunction.ToTableEntry(
            "CheckTargetDynamic", 
            StrCode32.DefaultParameters,
            $"if {StrCode32.DefaultParameters[3]} == TppGameObject.FULTON_FAILED_TYPE_ON_FINISHED_RISE then local isClearType = qvars.CheckQuestAllTargetDynamic(\"FultonFailed\", {StrCode32.DefaultParameters[0]}, {StrCode32.DefaultParameters[1]}); TppQuest.ClearWithSave(isClearType); end;  "));

    static readonly Script GameObjectPlacedIntoHeli = new Script(
        new StrCode32("GameObject", Lua.String("PlacedIntoVehicle")),
        LuaFunction.ToTableEntry(
            "CheckTargetDynamic", 
            StrCode32.DefaultParameters,
            $"if Tpp.IsHelicopter({StrCode32.DefaultParameters[1]}) then local isClearType = qvars.CheckQuestAllTargetDynamic(\"InHelicopter\", {StrCode32.DefaultParameters[0]}); TppQuest.ClearWithSave(isClearType); end; "));

    static readonly Script GameObjectVehicleBroken = new Script(
        new StrCode32("GameObject", Lua.String("VehicleBroken")),
        LuaFunction.ToTableEntry(
            "CheckTargetDynamic", 
            StrCode32.DefaultParameters,
            $"if {StrCode32.DefaultParameters[1]} == StrCode32(\"End\") then local isClearType = qvars.CheckQuestAllTargetDynamic(\"VehicleBroken\", {StrCode32.DefaultParameters[0]}); TppQuest.ClearWithSave(isClearType); end; "));

    static readonly Script GameObjectLostControl = new Script(
        new StrCode32("GameObject", Lua.String("LostControl")),
        LuaFunction.ToTableEntry(
            "CheckTargetDynamic", 
            StrCode32.DefaultParameters,
            $"if {StrCode32.DefaultParameters[1]} == StrCode32(\"End\") then local isClearType = qvars.CheckQuestAllTargetDynamic(\"LostControl\", {StrCode32.DefaultParameters[0]}); TppQuest.ClearWithSave(isClearType); end; "));

    public static readonly Script[] allCommonMessages = { PlayerPickUpWeapon, PlayerPickUpPlaced, PlacedActivatePlaced, GameObjectDead, GameObjectFultonInfo, GameObjectFulton, GameObjectFultonFailed, GameObjectPlacedIntoHeli, GameObjectVehicleBroken, GameObjectLostControl };

    public static readonly Script[] genericTargetMessages = { GameObjectDead, GameObjectFultonInfo, GameObjectFulton, GameObjectFultonFailed, GameObjectPlacedIntoHeli, GameObjectVehicleBroken, GameObjectLostControl };

    public static readonly Script[] dormantItemTargetMessages = { PlayerPickUpWeapon };

    public static readonly Script[] activeItemTargetMessages = { PlayerPickUpPlaced, PlacedActivatePlaced };

    public static readonly Script[] mechaCaptureTargetMessages = { GameObjectDead, GameObjectFultonInfo, GameObjectFulton, GameObjectFultonFailed, GameObjectVehicleBroken };

    public static readonly Script[] mechaNoCaptureTargetMessages = { GameObjectDead, GameObjectVehicleBroken, GameObjectLostControl };

    public static readonly Script[] animalTargetMessages = { GameObjectDead, GameObjectFultonInfo, GameObjectFulton, GameObjectFultonFailed };
}

public static class StaticObjectiveFunctions
{
    public static readonly LuaTableEntry IsTargetSetMessageIdForItem = LuaFunction.ToTableEntry(
        "IsTargetSetMessageIdForItem", 
        new string[] { "gameId", "messageId", "checkAnimalId" },
        @"if messageId == ""PickUpDormant"" then
            for i, targetInfo in pairs(qvars.ObjectiveTypeList.targetItemList) do
              if gameId == targetInfo.equipId and targetInfo.messageId == ""None"" and targetInfo.active == false then
                targetInfo.messageId = messageId
                return true, true
              end
            end
          elseif messageId == ""PickUpActive"" or messageId == ""Activate"" then
            for i, targetInfo in pairs(qvars.ObjectiveTypeList.targetItemList) do
              if gameId == targetInfo.equipId and targetInfo.messageId == ""None"" and targetInfo.active == true then
                targetInfo.messageId = messageId
                return true, true
              end
            end
          end
          return false, false");

    public static readonly LuaTableEntry TallyItemTargets = LuaFunction.ToTableEntry(
        "TallyItemTargets", 
        new string[] { "totalTargets", "objectiveCompleteCount", "objectiveFailedCount" },
        @"for i, targetInfo in pairs(qvars.ObjectiveTypeList.targetItemList) do
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
          return totalTargets, objectiveCompleteCount, objectiveFailedCount");

    public static readonly LuaTableEntry IsTargetSetMessageIdForGenericEnemy = LuaFunction.ToTableEntry(
        "IsTargetSetMessageIdForGenericEnemy", 
        new string[] { "gameId", "messageId", "checkAnimalId" },
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
          return false, false");

    public static readonly LuaTableEntry TallyGenericTargets = LuaFunction.ToTableEntry(
        "TallyGenericTargets", 
        new string[] { "totalTargets", "objectiveCompleteCount", "objectiveFailedCount" },
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
          return totalTargets, objectiveCompleteCount, objectiveFailedCount");

    public static readonly LuaTableEntry IsTargetSetMessageIdForAnimal = LuaFunction.ToTableEntry(
        "IsTargetSetMessageIdForAnimal", 
        new string[] { "gameId", "messageId", "checkAnimalId" },
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

    public static readonly LuaTableEntry TallyAnimalTargets = LuaFunction.ToTableEntry(
        "TallyAnimalTargets", 
        new string[] { "totalTargets", "objectiveCompleteCount", "objectiveFailedCount" },
        @"local dynamicQuestType = qvars.ObjectiveTypeList.animalObjective
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
          return totalTargets, objectiveCompleteCount, objectiveFailedCount");

    public static readonly LuaTableEntry CheckQuestAllTargetDynamicFunction = LuaFunction.ToTableEntry(
        "CheckQuestAllTargetDynamic", 
        new string[] { "messageId", "gameId", "checkAnimalId" },
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