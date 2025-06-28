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
            OnEnterFunction.AppendLuaValue(Create.FunctionCall(Create.TableIdentifier("Fox", "Log"), Create.String("QStep_Main OnEnter")));
            OnLeaveFunction.AppendLuaValue(Create.FunctionCall(Create.TableIdentifier("Fox", "Log"), Create.String("QStep_Main OnLeave")));
        }

        public LuaTableEntry Get(string strCode32TableVariableName)
        {
            QStep_Main_Table.Add(
                Create.TableEntry("Messages", Create.Function("return |[1|FUNCTION_CALL]|", Create.FunctionCall("StrCode32Table", StrCode32Table.ToStrCode32Table(strCode32TableVariableName))), false),
                Create.TableEntry("OnEnter", OnEnterFunction.ToFunction()),
                Create.TableEntry("OnLeave", OnLeaveFunction.ToFunction())
            );

            return Create.TableEntry("QStep_Main", QStep_Main_Table, true);
        }
    }
}

public static class QStep_Main_TargetMessages 
{
    static readonly Script PlayerPickUpWeapon = new Script(
        new StrCode32("Player", Create.String("OnPickUpWeapon")),
        Create.FunctionAsTableEntry(
            "CheckTargetDynamic", 
            StrCode32.DefaultParameters,
            $"qvars.CheckQuestAllTargetDynamic(\"PickUpDormant\", {StrCode32.DefaultParameters[1]})")
        );

    static readonly Script PlayerPickUpPlaced = new Script(
        new StrCode32("Player", Create.String("OnPickUpPlaced")),
        Create.FunctionAsTableEntry(
            "CheckTargetDynamic", 
            StrCode32.DefaultParameters,
            $"if TppPlaced.IsQuestBlock({StrCode32.DefaultParameters[2]}) then \nqvars.CheckQuestAllTargetDynamic(\"PickUpActive\", {StrCode32.DefaultParameters[1]}) \nend"));

    static readonly Script PlacedActivatePlaced = new Script(
        new StrCode32("Placed", Create.String("OnActivatePlaced")),
        Create.FunctionAsTableEntry(
            "CheckTargetDynamic", 
            StrCode32.DefaultParameters,
            $"if TppPlaced.IsQuestBlock({StrCode32.DefaultParameters[1]}) then \nqvars.CheckQuestAllTargetDynamic(\"Activate\", {StrCode32.DefaultParameters[0]}) \nend"));

    static readonly Script GameObjectDead = new Script(
        new StrCode32("GameObject", Create.String("Dead")),
        Create.FunctionAsTableEntry(
            "CheckTargetDynamic", 
            StrCode32.DefaultParameters,
            $"qvars.CheckQuestAllTargetDynamic(\"Dead\", {StrCode32.DefaultParameters[0]}, {StrCode32.DefaultParameters[2]})"));

    static readonly Script GameObjectFulton = new Script(
        new StrCode32("GameObject", Create.String("Fulton")),
        Create.FunctionAsTableEntry(
            "CheckTargetDynamic", 
            StrCode32.DefaultParameters,
            $"qvars.CheckQuestAllTargetDynamic(\"Fulton\", {StrCode32.DefaultParameters[0]}, {StrCode32.DefaultParameters[1]})"));

    static readonly Script GameObjectFultonFailed = new Script(
        new StrCode32("GameObject", Create.String("FultonFailed")),
        Create.FunctionAsTableEntry(
            "CheckTargetDynamic", 
            StrCode32.DefaultParameters,
            $"if {StrCode32.DefaultParameters[3]} == TppGameObject.FULTON_FAILED_TYPE_ON_FINISHED_RISE then \nqvars.CheckQuestAllTargetDynamic(\"FultonFailed\", {StrCode32.DefaultParameters[0]}, {StrCode32.DefaultParameters[1]}) \nend"));

    static readonly Script GameObjectPlacedIntoHeli = new Script(
        new StrCode32("GameObject", Create.String("PlacedIntoVehicle")),
        Create.FunctionAsTableEntry(
            "CheckTargetDynamic", 
            StrCode32.DefaultParameters,
            $"if Tpp.IsHelicopter({StrCode32.DefaultParameters[1]}) then \nqvars.CheckQuestAllTargetDynamic(\"InHelicopter\", {StrCode32.DefaultParameters[0]}) \nend"));

    static readonly Script GameObjectVehicleBroken = new Script(
        new StrCode32("GameObject", Create.String("VehicleBroken")),
        Create.FunctionAsTableEntry(
            "CheckTargetDynamic", 
            StrCode32.DefaultParameters,
            $"if {StrCode32.DefaultParameters[1]} == StrCode32(\"End\") then \nqvars.CheckQuestAllTargetDynamic(\"VehicleBroken\", {StrCode32.DefaultParameters[0]}) \nend"));

    static readonly Script GameObjectLostControl = new Script(
        new StrCode32("GameObject", Create.String("LostControl")),
        Create.FunctionAsTableEntry(
            "CheckTargetDynamic", 
            StrCode32.DefaultParameters,
            $"if {StrCode32.DefaultParameters[1]} == StrCode32(\"End\") then \nqvars.CheckQuestAllTargetDynamic(\"LostControl\", {StrCode32.DefaultParameters[0]}) \nend"));

    public static readonly Script[] allCommonMessages = { PlayerPickUpWeapon, PlayerPickUpPlaced, PlacedActivatePlaced, GameObjectDead, GameObjectFulton, GameObjectFultonFailed, GameObjectPlacedIntoHeli, GameObjectVehicleBroken, GameObjectLostControl };

    public static readonly Script[] genericTargetMessages = { GameObjectDead, GameObjectFulton, GameObjectFultonFailed, GameObjectPlacedIntoHeli, GameObjectVehicleBroken, GameObjectLostControl };

    public static readonly Script[] dormantItemTargetMessages = { PlayerPickUpWeapon };

    public static readonly Script[] activeItemTargetMessages = { PlayerPickUpPlaced, PlacedActivatePlaced };

    public static readonly Script[] mechaCaptureTargetMessages = { GameObjectDead, GameObjectFulton, GameObjectFultonFailed, GameObjectVehicleBroken };

    public static readonly Script[] mechaNoCaptureTargetMessages = { GameObjectDead, GameObjectVehicleBroken, GameObjectLostControl };

    public static readonly Script[] animalTargetMessages = { GameObjectDead, GameObjectFulton, GameObjectFultonFailed };
}

public static class StaticObjectiveFunctions
{
    public static readonly LuaTableEntry IsTargetSetMessageIdForItem = Create.FunctionAsTableEntry(
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

    public static readonly LuaTableEntry TallyItemTargets = Create.FunctionAsTableEntry(
        "TallyItemTargets", 
        new string[] {"objectiveCompleteCount", "objectiveFailedCount", "targetGameId", "targetMessageId" },
        @"local targetContribution = 0
		
		for i, targetInfo in pairs(qvars.ObjectiveTypeList.targetItemList) do 
			local dynamicQuestType = ""RECOVERED"" 
			for _, ObjectiveTypeInfo in ipairs(qvars.ObjectiveTypeList.itemTargets) do 
				if ObjectiveTypeInfo.Check(targetInfo) then 
					dynamicQuestType = ObjectiveTypeInfo.Type 
					break 
				end 
			end 
			local messageId = targetInfo.messageId 
			
			if messageId ~= ""None"" then 
				local contributesPositive = false
				local contributesNegative = false
				
				if dynamicQuestType == ""RECOVERED"" then 
					if (messageId == ""PickUpDormant"" or messageId == ""PickUpActive"") then 
						objectiveCompleteCount = objectiveCompleteCount + 1 
						contributesPositive = true
					elseif (messageId == ""Activate"") then 
						objectiveFailedCount = objectiveFailedCount + 1 
						contributesNegative = true
					end 
					
				elseif dynamicQuestType == ""ELIMINATE"" then 
					if (messageId == ""PickUpActive"" or messageId == ""Activate"") then 
						objectiveCompleteCount = objectiveCompleteCount + 1 
						contributesPositive = true
					end 
					
				elseif dynamicQuestType == ""KILLREQUIRED"" then 
					if (messageId == ""Activate"") then 
						objectiveCompleteCount = objectiveCompleteCount + 1 
						contributesPositive = true
					elseif (messageId == ""PickUpActive"") then 
						objectiveFailedCount = objectiveFailedCount + 1 
						contributesNegative = true
					end 
				end
				
				if targetInfo.equipId == targetGameId and messageId == targetMessageId then
					if contributesPositive then
						targetContribution = 1
					elseif contributesNegative then
						targetContribution = -1
					end
				end
			end 
		end 
		return objectiveCompleteCount, objectiveFailedCount, targetContribution");

    public static readonly LuaTableEntry IsTargetSetMessageIdForGenericEnemy = Create.FunctionAsTableEntry(
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

    public static readonly LuaTableEntry TallyGenericTargets = Create.FunctionAsTableEntry(
        "TallyGenericTargets", 
        new string[] { "objectiveCompleteCount", "objectiveFailedCount", "targetGameId", "targetMessageId" },
        @"local targetContribution = 0
		
		for gameId, targetInfo in pairs(mvars.ene_questTargetList) do 
			local dynamicQuestType = ""ELIMINATE"" 
			local isTarget = targetInfo.isTarget or false 
			local messageId = targetInfo.messageId 
			
			if isTarget == true then 
				if qvars.ObjectiveTypeList.genericTargets ~= nil then 
					for _, ObjectiveTypeInfo in ipairs(qvars.ObjectiveTypeList.genericTargets) do 
						if ObjectiveTypeInfo.Check(gameId) then 
							dynamicQuestType = ObjectiveTypeInfo.Type 
							break 
						end 
					end 
				end 
				
				if messageId ~= ""None"" then 
					local contributesPositive = false
					local contributesNegative = false
					
					if dynamicQuestType == ""RECOVERED"" then 
						if (messageId == ""Fulton"") or (messageId == ""InHelicopter"") then 
							objectiveCompleteCount = objectiveCompleteCount + 1 
							contributesPositive = true
						elseif (messageId == ""FultonFailed"") or (messageId == ""Dead"") or (messageId == ""VehicleBroken"") or (messageId == ""LostControl"") then 
							objectiveFailedCount = objectiveFailedCount + 1 
							contributesNegative = true
						end 
						
					elseif dynamicQuestType == ""ELIMINATE"" then 
						if (messageId == ""Fulton"") or (messageId == ""InHelicopter"") or (messageId == ""FultonFailed"") or (messageId == ""Dead"") or (messageId == ""VehicleBroken"") or (messageId == ""LostControl"") then 
							objectiveCompleteCount = objectiveCompleteCount + 1 
							contributesPositive = true
						end 
						
					elseif dynamicQuestType == ""KILLREQUIRED"" then 
						if (messageId == ""FultonFailed"") or (messageId == ""Dead"") or (messageId == ""VehicleBroken"") or (messageId == ""LostControl"") then 
							objectiveCompleteCount = objectiveCompleteCount + 1 
							contributesPositive = true
						elseif (messageId == ""Fulton"") or (messageId == ""InHelicopter"") then 
							objectiveFailedCount = objectiveFailedCount + 1 
							contributesNegative = true
						end 
					end
					
					if gameId == targetGameId and messageId == targetMessageId then
						if contributesPositive then
							targetContribution = 1
						elseif contributesNegative then
							targetContribution = -1
						end
					end
				end
			end 
		end 
		return objectiveCompleteCount, objectiveFailedCount, targetContribution");

    public static readonly LuaTableEntry IsTargetSetMessageIdForAnimal = Create.FunctionAsTableEntry(
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

    public static readonly LuaTableEntry TallyAnimalTargets = Create.FunctionAsTableEntry(
        "TallyAnimalTargets", 
        new string[] { "objectiveCompleteCount", "objectiveFailedCount", "targetGameId", "targetMessageId" },
        @"local targetContribution = 0
		local dynamicQuestType = qvars.ObjectiveTypeList.animalObjective 
		
		for animalId, targetInfo in pairs(mvars.ani_questTargetList) do 
			local messageId = targetInfo.messageId 
			
			if messageId ~= ""None"" then 
				local contributesPositive = false
				local contributesNegative = false
				
				if dynamicQuestType == ""RECOVERED"" then 
					if (messageId == ""Fulton"") then 
						objectiveCompleteCount = objectiveCompleteCount + 1 
						contributesPositive = true
					elseif (messageId == ""FultonFailed"") or (messageId == ""Dead"") then 
						objectiveFailedCount = objectiveFailedCount + 1 
						contributesNegative = true
					end 
					
				elseif dynamicQuestType == ""ELIMINATE"" then 
					if (messageId == ""Fulton"") or (messageId == ""FultonFailed"") or (messageId == ""Dead"") then 
						objectiveCompleteCount = objectiveCompleteCount + 1 
						contributesPositive = true
					end 
					
				elseif dynamicQuestType == ""KILLREQUIRED"" then 
					if (messageId == ""FultonFailed"") or (messageId == ""Dead"") then 
						objectiveCompleteCount = objectiveCompleteCount + 1 
						contributesPositive = true
					elseif (messageId == ""Fulton"") then 
						objectiveFailedCount = objectiveFailedCount + 1 
						contributesNegative = true
					end 
				end
				
				local animalGameId = GetGameObjectId(animalId)
				if animalGameId == targetGameId and messageId == targetMessageId then
					if contributesPositive then
						targetContribution = 1
					elseif contributesNegative then
						targetContribution = -1
					end
				end
			end 
		end 
		return objectiveCompleteCount, objectiveFailedCount, targetContribution");

    public static readonly LuaTableEntry CheckQuestAllTargetDynamicFunction = Create.FunctionAsTableEntry(
        "CheckQuestAllTargetDynamic", 
        new string[] { "messageId", "gameId", "checkAnimalId" },
        @"local currentQuestName = TppQuest.GetCurrentQuestName() 
		if TppQuest.IsEnd(currentQuestName) then 
			return
		end 
	
		local inTargetList = false 
		local intendedTarget = true 
		for IsTargetSetMethod, _ in pairs(qvars.CheckQuestMethodPairs) do 
			inTargetList, intendedTarget = IsTargetSetMethod(gameId, messageId, checkAnimalId) 
			if inTargetList == true then 
				break 
			end 
		end 
	
		if inTargetList == false or intendedTarget == false then 
			return
		end 
	
		local objectiveCompleteCount = 0 
		local objectiveFailedCount = 0 
		local objectiveContributionType = 0
	
		for _, TallyMethod in pairs(qvars.CheckQuestMethodPairs) do 
			local completeCount, failedCount, contribution = TallyMethod(objectiveCompleteCount, objectiveFailedCount, gameId, messageId) 
			objectiveCompleteCount = completeCount
			objectiveFailedCount = failedCount
		
			if contribution ~= 0 then
				objectiveContributionType = contribution
			end
		end 
	
		local targetMsg = ""OnTargetDeath"" 
		if (messageId == ""PickUpDormant"") or (messageId == ""PickUpActive"") or (messageId == ""Fulton"") or (messageId == ""InHelicopter"") then 
			targetMsg = ""OnTargetExtraction"" 
		end 
		Mission.SendMessage(""Mission"", targetMsg, gameId, objectiveContributionType, objectiveCompleteCount, objectiveFailedCount)", true);

}