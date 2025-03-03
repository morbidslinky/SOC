using System.Collections.Generic;
using System.Text;

namespace SOC.Classes.Lua
{
    public class ObjectiveTypesList
    {
        public List<GenericTargetTable> targetTables = new List<GenericTargetTable>();
        public List<string> oneLineObjectiveTypes = new List<string>();

        public void Add(string tableName, GenericTargetPair pair)
        {
            GenericTargetTable insertTable = targetTables.Find(table => table.GetName() == tableName);
            if (insertTable != null)
            {
                insertTable.Add(pair);
            }
            else
            {
                insertTable = new GenericTargetTable(tableName);
                insertTable.Add(pair);
                targetTables.Add(insertTable);
            }
        }

        public LuaTableEntry[] GetObjectiveFunctions()
        {
            List<LuaTableEntry> objectiveFunctions = new List<LuaTableEntry>();

            return objectiveFunctions.ToArray();
            /*
                        StringBuilder functionsBuilder = new StringBuilder();
                        foreach (GenericTargetTable table in targetTables)
                        {
                            foreach (GenericTargetPair pair in table.GetTargetPairs())
                            {
                                functionsBuilder.Append($@"
            {pair.checkMethod.Value}");
                            }
                        }
                        return functionsBuilder.ToString();*/
        }

        public LuaTableEntry GetObjectiveTypesList()
        {
            LuaTable objectiveTypesList = new LuaTable();


            return Lua.TableEntry("ObjectiveTypeList", objectiveTypesList);

            /*
            StringBuilder objectiveListBuilder = new StringBuilder(@"
ObjectiveTypeList = {");

            foreach (GenericTargetTable table in targetTables)
            {
                if (table.GetTargetPairs().Length > 0)
                {
                    objectiveListBuilder.Append(table.GetTableFormatted());
                }
            }

            foreach (string oneLineObjectiveType in oneLineObjectiveTypes)
            {
                objectiveListBuilder.Append($@"
  {oneLineObjectiveType},");
            }
            objectiveListBuilder.Append(@"
}");
            return objectiveListBuilder.ToString();
            */
        }
    }

    public class GenericTargetTable
    {
        string tableName;
        List<GenericTargetPair> genericTargets = new List<GenericTargetPair>();

        public GenericTargetTable(string name)
        {
            tableName = name;
        }

        public string GetName()
        {
            return tableName;
        }

        public GenericTargetPair[] GetTargetPairs()
        {
            return genericTargets.ToArray();
        }

        public void Add(params GenericTargetPair[] pairs)
        {
            foreach (GenericTargetPair pair in pairs)
            {
                if (!genericTargets.Exists(existingPair => existingPair.checkMethod.Equals(pair.checkMethod)))
                {
                    genericTargets.Add(pair);
                }
            }
        }

        public string GetTableFormatted()
        {
            StringBuilder tableBuilder = new StringBuilder($@"
  {tableName} = {{");
            foreach (GenericTargetPair pair in genericTargets)
            {
                tableBuilder.Append($@"
    {pair.GetTableFormat()},");
            }
            tableBuilder.Append(@"
  },");
            return tableBuilder.ToString();
        }
    }
    public class GenericTargetPair
    {
        public LuaTableEntry checkMethod;
        public string ObjectiveType;

        public GenericTargetPair(LuaTableEntry check, string type)
        {
            checkMethod = check; ObjectiveType = type;
        }

        public string GetTableFormat()
        {
            return $"{{Check = qvars.{checkMethod.Key}, Type = {ObjectiveType}}}";
        }
    }

    class CheckQuestItem : CheckQuestMethodsPair
    {
        static readonly LuaTableEntry IsTargetSetMessageIdForItem = LuaFunction.ToTableEntry("IsTargetSetMessageIdForItem", new string[] { "gameId", "messageId", "checkAnimalId" },
    @" if messageId == ""PickUpDormant"" then
    for i, targetInfo in pairs(this.QUEST_TABLE.targetItemList) do
      if gameId == targetInfo.equipId and targetInfo.messageId == ""None"" and targetInfo.active == false then
        targetInfo.messageId = messageId
        return true, true
      end
    end
  elseif messageId == ""PickUpActive"" or messageId == ""Activate"" then
    for i, targetInfo in pairs(this.QUEST_TABLE.targetItemList) do
      if gameId == targetInfo.equipId and targetInfo.messageId == ""None"" and targetInfo.active == true then
        targetInfo.messageId = messageId
        return true, true
      end
    end
  end
  return false, false; ");

        static readonly LuaTableEntry TallyItemTargets = LuaFunction.ToTableEntry("TallyItemTargets", new string[] { "totalTargets", "objectiveCompleteCount", "objectiveFailedCount" },
            @" for i, targetInfo in pairs(this.QUEST_TABLE.targetItemList) do
    local dynamicQuestType = RECOVERED
    for _, ObjectiveTypeInfo in ipairs(ObjectiveTypeList.itemTargets) do
      if ObjectiveTypeInfo.Check(targetInfo) then
        dynamicQuestType = ObjectiveTypeInfo.Type
        break
      end
    end
    local targetMessageId = targetInfo.messageId

    if targetMessageId ~= ""None"" then
        if dynamicQuestType == RECOVERED then
          if (targetMessageId == ""PickUpDormant"" or targetMessageId == ""PickUpActive"") then
            objectiveCompleteCount = objectiveCompleteCount + 1
          elseif (targetMessageId == ""Activate"") then
            objectiveFailedCount = objectiveFailedCount + 1
          end

        elseif dynamicQuestType == ELIMINATE then
          if (targetMessageId == ""PickUpActive"" or targetMessageId == ""Activate"") then
            objectiveCompleteCount = objectiveCompleteCount + 1
          end

        elseif dynamicQuestType == KILLREQUIRED then
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

        public CheckQuestItem(MainScriptBuilder mainLua, LuaTableEntry checkFunction, string objectiveType) : base(mainLua, IsTargetSetMessageIdForItem, TallyItemTargets, "itemTargets", checkFunction, objectiveType) { }
    }
    class CheckQuestGenericEnemy : CheckQuestMethodsPair
    {

        static readonly LuaTableEntry IsTargetSetMessageIdForGenericEnemy = LuaFunction.ToTableEntry("IsTargetSetMessageIdForGenericEnemy", new string[] { "gameId", "messageId", "checkAnimalId" },
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

        static readonly LuaTableEntry TallyGenericTargets = LuaFunction.ToTableEntry("TallyGenericTargets", new string[] { "totalTargets", "objectiveCompleteCount", "objectiveFailedCount" },
            @"for targetGameId, targetInfo in pairs(mvars.ene_questTargetList) do
    local dynamicQuestType = ELIMINATE
    local isTarget = targetInfo.isTarget or false
    local targetMessageId = targetInfo.messageId

    if isTarget == true then
      if ObjectiveTypeList.genericTargets ~= nil then
        for _, ObjectiveTypeInfo in ipairs(ObjectiveTypeList.genericTargets) do
          if ObjectiveTypeInfo.Check(targetGameId) then
            dynamicQuestType = ObjectiveTypeInfo.Type
            break
          end
        end
      end

      if targetMessageId ~= ""None"" then
        if dynamicQuestType == RECOVERED then
          if (targetMessageId == ""Fulton"") or (targetMessageId == ""InHelicopter"") then
            objectiveCompleteCount = objectiveCompleteCount + 1
          elseif (targetMessageId == ""FultonFailed"") or (targetMessageId == ""Dead"") or (targetMessageId == ""VehicleBroken"") or (targetMessageId == ""LostControl"") then
            objectiveFailedCount = objectiveFailedCount + 1
          end

        elseif dynamicQuestType == ELIMINATE then
          if (targetMessageId == ""Fulton"") or (targetMessageId == ""InHelicopter"") or (targetMessageId == ""FultonFailed"") or (targetMessageId == ""Dead"") or (targetMessageId == ""VehicleBroken"") or (targetMessageId == ""LostControl"") then
            objectiveCompleteCount = objectiveCompleteCount + 1
          end

        elseif dynamicQuestType == KILLREQUIRED then
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

        public CheckQuestGenericEnemy(MainScriptBuilder mainLua, LuaTableEntry checkFunction, string objectiveType) : base(mainLua, IsTargetSetMessageIdForGenericEnemy, TallyGenericTargets, "genericTargets", checkFunction, objectiveType) { }

        public CheckQuestGenericEnemy(MainScriptBuilder mainLua) : base(mainLua, IsTargetSetMessageIdForGenericEnemy, TallyGenericTargets) { }
    }
    class CheckQuestAnimal : CheckQuestMethodsPair
    {
        static readonly LuaTableEntry IsTargetSetMessageIdForAnimal = LuaFunction.ToTableEntry("IsTargetSetMessageIdForAnimal", new string[] { "gameId", "messageId", "checkAnimalId" },
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

        static readonly LuaTableEntry TallyAnimalTargets = LuaFunction.ToTableEntry("TallyAnimalTargets", new string[] { "totalTargets", "objectiveCompleteCount", "objectiveFailedCount" },
            @" local dynamicQuestType = ObjectiveTypeList.animalObjective
  for animalId, targetInfo in pairs(mvars.ani_questTargetList) do
    local targetMessageId = targetInfo.messageId

    if targetMessageId ~= ""None"" then
      if dynamicQuestType == RECOVERED then
        if (targetMessageId == ""Fulton"") then
          objectiveCompleteCount = objectiveCompleteCount + 1
        elseif (targetMessageId == ""FultonFailed"") or (targetMessageId == ""Dead"") then
          objectiveFailedCount = objectiveFailedCount + 1
        end

      elseif dynamicQuestType == ELIMINATE then
        if (targetMessageId == ""Fulton"") or (targetMessageId == ""FultonFailed"") or (targetMessageId == ""Dead"") then
          objectiveCompleteCount = objectiveCompleteCount + 1
        end

      elseif dynamicQuestType == KILLREQUIRED then
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

        public CheckQuestAnimal(MainScriptBuilder mainLua, string objectiveType) : base(mainLua, IsTargetSetMessageIdForAnimal, TallyAnimalTargets, "animalObjective = " + objectiveType) { }
    }

    static class CheckQuestAllTargetDynamic
    {
        public static readonly LuaTableEntry function = LuaFunction.ToTableEntry("CheckQuestAllTargetDynamic", new string[] { "messageId", "gameId", "checkAnimalId" }, "" +
            @"local currentQuestName=TppQuest.GetCurrentQuestName()
  if TppQuest.IsEnd(currentQuestName) then
    return TppDefine.QUEST_CLEAR_TYPE.NONE
  end

  local inTargetList = false
  local intendedTarget = true
  for _, CheckMethods in ipairs(CheckQuestMethodList) do
    inTargetList, intendedTarget = CheckMethods.IsTargetSetMessageMethod(gameId, messageId, checkAnimalId)
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
  for _, CheckMethods in ipairs(CheckQuestMethodList) do
    totalTargets, objectiveCompleteCount, objectiveFailedCount = CheckMethods.TallyMethod(totalTargets, objectiveCompleteCount, objectiveFailedCount)
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
