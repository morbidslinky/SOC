using SOC.Classes.Common;
using SOC.QuestObjects.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SOC.Classes.Lua
{
    public class MainScriptBuilder
    {
        public SetupDetails SetupDetails;
        public ObjectsDetails ObjectsDetails;

        CheckQuestMethodsList checkQuestMethodList = new CheckQuestMethodsList(); // TODO
        ObjectiveTypesList objectiveTypesList = new ObjectiveTypesList(); // TODO


        public OnUpdate OnUpdate = new OnUpdate();
        public OnAllocate OnAllocate = new OnAllocate();
        public Messages Messages = new Messages();
        public OnInitialize OnInitialize = new OnInitialize();
        public OnTerminate OnTerminate = new OnTerminate();
        public QStep_Start QStep_Start = new QStep_Start();
        public QStep_Main QStep_Main = new QStep_Main();

        public LuaTable QUEST_TABLE = new LuaTable();
        public LuaTable qvars = new LuaTable();
        public LuaTable @this = new LuaTable();
        public LuaTable quest_step = new LuaTable();

        public LuaTable CommonFunctions = new LuaTable();


        public MainScriptBuilder(SetupDetails setupDetails, ObjectsDetails objectsDetails)
        {
            SetupDetails = setupDetails; ObjectsDetails = objectsDetails;

            /*TODO
            XmlSerializer serializer = new XmlSerializer(typeof(LuaTable));
            using (StreamReader reader = new StreamReader("StaticScriptFunctions.xml"))
            {
                CommonFunctions = (LuaTable)serializer.Deserialize(reader);
            }
            */

            qvars.AddOrSet(
                Lua.TableEntry("StrCode32", Lua.TableIdentifier("Fox", Lua.Text("StrCode32"))),
                Lua.TableEntry("StrCode32Table", Lua.TableIdentifier("Tpp", Lua.Text("StrCode32Table"))),
                Lua.TableEntry("GetGameObjectId", Lua.TableIdentifier("GameObject", Lua.Text("GetGameObjectId"))),
                Lua.TableEntry("ELIMINATE", Lua.TableIdentifier("TppDefine", Lua.Text("QUEST_TYPE"), Lua.Text("ELIMINATE"))),
                Lua.TableEntry("RECOVERED", Lua.TableIdentifier("TppDefine", Lua.Text("QUEST_TYPE"), Lua.Text("RECOVERED"))),
                Lua.TableEntry("KILLREQUIRED", 9),
                Lua.TableEntry("DISTANTCP", QuestObjects.Enemy.EnemyInfo.ChooseDistantCP(setupDetails.CPName, setupDetails.locationID)),
                Lua.TableEntry("questTrapName", $"trap_preDeactiveQuestArea_{setupDetails.loadArea}")
            );

            if (setupDetails.CPName == "NONE")
            {
                qvars.AddOrSet(Lua.TableEntry("CPNAME", 
                    Lua.FunctionCall(
                        Lua.TableIdentifier("InfMain", "GetClosestCp"),
                        Lua.Table(setupDetails.coords.xCoord, setupDetails.coords.yCoord, setupDetails.coords.zCoord))));
            }
            else
            {
                qvars.AddOrSet(Lua.TableEntry("CPNAME", setupDetails.CPName));
            }

            QUEST_TABLE.AddOrSet(
                Lua.TableEntry("questType", Lua.TableIdentifier("TppDefine", "QUEST_TYPE", "ELIMINATE"))
            );

            foreach (ObjectsDetail detail in objectsDetails.details)
            {
                detail.AddToMainLua(this);
            }

            @this.AddOrSet(
                OnAllocate.Get(),
                Messages.Get(),
                OnInitialize.Get(),
                OnUpdate.Get(),
                OnTerminate.Get(),
                Lua.TableEntry("QUEST_TABLE", QUEST_TABLE)
            );

            quest_step.AddOrSet(
                QStep_Start.Get(),
                QStep_Main.Get()
            );
        }

        public void AddToCheckQuestMethod(CheckQuestMethodsPair methodsPair)
        {
            if (!checkQuestMethodList.Contains(methodsPair))
                checkQuestMethodList.Add(methodsPair);
        }

        public void AddToObjectiveTypes(string tableName, GenericTargetPair objectivePair)
        {
            objectiveTypesList.Add(tableName, objectivePair);
        }

        public void AddToObjectiveTypes(string oneLineObjective)
        {
            if (!objectiveTypesList.oneLineObjectiveTypes.Contains(oneLineObjective))
                objectiveTypesList.oneLineObjectiveTypes.Add(oneLineObjective);
        }

        public void Build(string mainLuaFilePath)
        {
            var mainScript = Lua.Function("local |[0|assign_variable]| local |[1|assign_variable]| local |[2|assign_variable]| local |[3|assign_variable]| return |[2|variable]|",
                    QStep_Main.GetStrCode32DefinitionsVariable(),
                    Lua.Variable("qvars",qvars),
                    Lua.Variable("this", @this),
                    Lua.Variable("quest_step", quest_step)
                );
            mainScript.WriteToLua(mainLuaFilePath);
            mainScript.WriteToXml(mainLuaFilePath + ".xml");
        }
    }

    public class OnAllocate
    {
        public LuaFunctionBuilder OnActivate = new LuaFunctionBuilder();
        public LuaFunctionBuilder OnDeactivate = new LuaFunctionBuilder();
        public LuaFunctionBuilder OnOutOfAcitveArea = new LuaFunctionBuilder();
        public LuaFunctionBuilder OnTerminateQuest = new LuaFunctionBuilder();

        public OnAllocate() {
            OnActivate.AppendLuaValue(Lua.FunctionCall(
                        Lua.TableIdentifier("TppEnemy", "OnActivateQuest"),
                        Lua.TableIdentifier("this", "QUEST_TABLE")));
            OnActivate.AppendLuaValue(Lua.FunctionCall(
                        Lua.TableIdentifier("TppAnimal", "OnActivateQuest"),
                        Lua.TableIdentifier("this", "QUEST_TABLE")));

            OnDeactivate.AppendLuaValue(Lua.FunctionCall(
                        Lua.TableIdentifier("TppEnemy", "OnDeactivateQuest"),
                        Lua.TableIdentifier("this", "QUEST_TABLE")));
            OnDeactivate.AppendLuaValue(Lua.FunctionCall(
                        Lua.TableIdentifier("TppAnimal", "OnDeactivateQuest"),
                        Lua.TableIdentifier("this", "QUEST_TABLE")));

            OnTerminateQuest.AppendLuaValue(Lua.FunctionCall(
                        Lua.TableIdentifier("TppEnemy", "OnTerminateQuest"),
                        Lua.TableIdentifier("this", "QUEST_TABLE")));
            OnTerminateQuest.AppendLuaValue(Lua.FunctionCall(
                        Lua.TableIdentifier("TppAnimal", "OnTerminateQuest"),
                        Lua.TableIdentifier("this", "QUEST_TABLE")));
        }
        public LuaTableEntry Get()
        {
            var registerQuestSystemCallbacks = Lua.Table(
                OnActivate.ToTableEntry("OnActivate"),
                OnDeactivate.ToTableEntry("OnDeactivate"),
                OnOutOfAcitveArea.ToTableEntry("OnOutOfAcitveArea"),
                OnTerminateQuest.ToTableEntry("OnTerminateQuest")
            );

            LuaFunctionBuilder OnAllocate = new LuaFunctionBuilder();
            OnAllocate.AppendLuaValue(Lua.FunctionCall(
                        Lua.TableIdentifier("TppQuest", "RegisterQuestStepList"),
                        Lua.Table(new LuaValue[] { Lua.Text("QStep_Start"), Lua.Text("QStep_Main"), Lua.Nil() })));
            OnAllocate.AppendLuaValue(Lua.FunctionCall(
                        Lua.TableIdentifier("TppEnemy", "OnAllocateQuestFova"),
                        Lua.TableIdentifier("this", "QUEST_TABLE")));
            OnAllocate.AppendLuaValue(Lua.FunctionCall(
                        Lua.TableIdentifier("TppQuest", "RegisterQuestStepTable"),
                        Lua.Variable("quest_step")));
            OnAllocate.AppendLuaValue(Lua.FunctionCall(
                        Lua.TableIdentifier("TppQuest", "RegisterQuestSystemCallbacks"),
                        registerQuestSystemCallbacks));

            return OnAllocate.ToTableEntry("OnAllocate", true);
        }
    }

    public class Messages
    {
        LuaTable StrCode32Table = new LuaTable();
        
        public Messages()
        {
            StrCode32Table.AddOrSet(Lua.TableEntry(
                "Block", Lua.Table(
                    Lua.TableEntry("msg", "StageBlockCurrentSmallBlockIndexUpdated"),
                    Lua.TableEntry("func", Lua.Function("")))
                )
            );
        }

        public LuaTableEntry Get()
        {
            return Lua.TableEntry("Messages", Lua.Function("return |[0|function_call]|", Lua.FunctionCall("StrCode32Table", StrCode32Table)), true);
        }
    }

    public class OnInitialize
    {
        public LuaFunctionBuilder Function = new LuaFunctionBuilder();

        public OnInitialize()
        {
            Function.AppendLuaValue(Lua.FunctionCall(Lua.TableIdentifier("TppQuest", "QuestBlockOnInitialize"), Lua.Variable("this")));
        }

        public LuaTableEntry Get()
        {
            return Function.ToTableEntry("OnInitialize", true);
        }
    }

    public class OnUpdate
    {
        public LuaFunctionBuilder Function = new LuaFunctionBuilder();

        public OnUpdate()
        {
            Function.AppendLuaValue(Lua.FunctionCall(Lua.TableIdentifier("TppQuest", "QuestBlockOnUpdate"), Lua.Variable("this")));
        }

        public LuaTableEntry Get()
        {
            return Function.ToTableEntry("OnUpdate", true);
        }
    }

    public class OnTerminate
    {
        public LuaFunctionBuilder Function = new LuaFunctionBuilder();

        public OnTerminate()
        {
            Function.AppendLuaValue(Lua.FunctionCall(Lua.TableIdentifier("TppQuest", "QuestBlockOnTerminate"), Lua.Variable("this")));
        }

        public LuaTableEntry Get()
        {
            return Function.ToTableEntry("OnTerminate", true);
        }
    }

    public class QStep_Start
    {
        public LuaFunctionBuilder Function = new LuaFunctionBuilder();

        public QStep_Start()
        {
            var OnEnter = new LuaFunctionBuilder();
            OnEnter.AppendLuaValue(Lua.FunctionCall(Lua.TableIdentifier("TppQuest", "SetNextQuestStep"), Lua.Text("QStep_Main")));
            Function.AppendLuaValue(Lua.Table(Lua.TableEntry("OnEnter", OnEnter.ToFunction())));
        }

        public LuaTableEntry Get()
        {
            return Function.ToTableEntry("QStep_Start", true);
        }
    }

    public class QStep_Main
    {
        public StrCode32Table StrCode32Table = new StrCode32Table();
        public LuaVariable StrCode32TableVariable = new LuaVariable("QStep_Main_StrCode32_Defs");

        LuaTable QStep_Main_Table = new LuaTable();

        public LuaFunctionBuilder OnEnterFunction = new LuaFunctionBuilder();
        public LuaFunctionBuilder OnLeaveFunction = new LuaFunctionBuilder();

        public QStep_Main()
        {
            OnEnterFunction.AppendLuaValue(Lua.FunctionCall(Lua.TableIdentifier("Fox", "Log"), Lua.Text("QStep_Main OnEnter")));
            OnLeaveFunction.AppendLuaValue(Lua.FunctionCall(Lua.TableIdentifier("Fox", "Log"), Lua.Text("QStep_Main OnLeave")));
        }

        public LuaTableEntry Get()
        {
            QStep_Main_Table.AddOrSet(
                Lua.TableEntry("Messages", Lua.Function("return |[0|function_call]|", Lua.FunctionCall("StrCode32Table", StrCode32Table.ToStrCode32Table(StrCode32TableVariable))), false),
                Lua.TableEntry("OnEnter", OnEnterFunction.ToFunction()),
                Lua.TableEntry("OnLeave", OnLeaveFunction.ToFunction())
            );

            return Lua.TableEntry("QStep_Main", QStep_Main_Table, true);
        }

        public LuaVariable GetStrCode32DefinitionsVariable()
        {
            StrCode32Table.AssignFunctionDefinitions(StrCode32TableVariable);
            return StrCode32TableVariable;
        }
    }

    public class CheckQuestMethodsList
    {
        List<CheckQuestMethodsPair> CheckQuestMethods = new List<CheckQuestMethodsPair>();

        public void Add(CheckQuestMethodsPair pair)
        {
            CheckQuestMethods.Add(pair);
        }

        public bool Contains(CheckQuestMethodsPair methodsPair)
        {
            return (CheckQuestMethods.Exists(pair => pair.TallyMethod.Equals(methodsPair.TallyMethod) || pair.TargetMessageMethod.Equals(methodsPair.TargetMessageMethod)));
        }

        public string ToLua(MainScriptBuilder mainLua)
        {

            return $@"{GetCheckFunctions()}
{GetList()}
";
        }

        private string GetCheckFunctions()
        {
            StringBuilder checkQuestBuilder = new StringBuilder();
            foreach (CheckQuestMethodsPair pair in CheckQuestMethods)
            {
                checkQuestBuilder.Append($@"{pair.TargetMessageMethod.Value}
{pair.TallyMethod.Value}
");
            }
            return checkQuestBuilder.ToString();
        }

        private string GetList()
        {
            StringBuilder checkQuestBuilder = new StringBuilder();
            checkQuestBuilder.Append(@"local CheckQuestMethodList = {");

            foreach (CheckQuestMethodsPair pair in CheckQuestMethods)
            {
                checkQuestBuilder.Append($@"
  {pair.GetTableFormat()},");
            }
            checkQuestBuilder.Append(@"
}");
            return checkQuestBuilder.ToString();
        }
    }

    public abstract class CheckQuestMethodsPair
    {
        public LuaTableEntry TargetMessageMethod, TallyMethod;

        public CheckQuestMethodsPair(MainScriptBuilder mainLua, LuaTableEntry a, LuaTableEntry b, string targetTableName, LuaTableEntry check, string objective)
        {
            TargetMessageMethod = a; TallyMethod = b;
            mainLua.AddToCheckQuestMethod(this);
            mainLua.AddToObjectiveTypes(targetTableName, new GenericTargetPair(check, objective));
        }

        public CheckQuestMethodsPair(MainScriptBuilder mainLua, LuaTableEntry a, LuaTableEntry b, string oneLineObjective)
        {
            TargetMessageMethod = a; TallyMethod = b;
            mainLua.AddToCheckQuestMethod(this);
            mainLua.AddToObjectiveTypes(oneLineObjective);
        }

        public CheckQuestMethodsPair(MainScriptBuilder mainLua, LuaTableEntry a, LuaTableEntry b)
        {
            TargetMessageMethod = a; TallyMethod = b;
            mainLua.AddToCheckQuestMethod(this);
        }

        public string GetTableFormat()
        {
            return $"{{IsTargetSetMessageMethod = qvars.{TargetMessageMethod.Key}, TallyMethod = qvars.{TallyMethod.Value}}}";
        }
    }

    public class ObjectiveTypesList
    {
        public List<GenericTargetTable> targetTables = new List<GenericTargetTable>();
        public List<string> oneLineObjectiveTypes = new List<string>();

        public string ToLua(MainScriptBuilder mainLua)
        {
            return $@"{GetObjectiveFunctions()}
{GetObjectiveTypesList()}
";
        }

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

        private string GetObjectiveFunctions()
        {
            StringBuilder functionsBuilder = new StringBuilder();
            foreach (GenericTargetTable table in targetTables)
            {
                foreach (GenericTargetPair pair in table.GetTargetPairs())
                {
                    functionsBuilder.Append($@"
{pair.checkMethod.Value}");
                }
            }
            return functionsBuilder.ToString();
        }

        private string GetObjectiveTypesList()
        {
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

    public static class QStep_MainCommonMessages
    {
        static readonly StrCode32Script PlayerPickUpWeapon = new StrCode32Script(
            new StrCode32Event("Player", "OnPickUpWeapon", "", "playerIndex", "playerIndex"),
            LuaFunction.ToTableEntry(
                "PlayerPickUpWeaponClearCheck",
                new string[] { "playerIndex", "playerIndex" },
                " local isClearType = this.CheckQuestAllTargetDynamic(\"PickUpDormant\", equipId); TppQuest.ClearWithSave(isClearType); ")
            );

        static readonly StrCode32Script PlayerPickUpPlaced = new StrCode32Script(
            new StrCode32Event("Player", "OnPickUpPlaced", "", "playerGameObjectId", "equipId", "index", "isPlayer"),
            LuaFunction.ToTableEntry(
                "PlayerPickUpPlacedClearCheck",
                new string[] { "playerGameObjectId", "equipId", "index", "isPlayer" },
                " if TppPlaced.IsQuestBlock(index) then local isClearType = this.CheckQuestAllTargetDynamic(\"PickUpActive\", equipId); TppQuest.ClearWithSave(isClearType); end; "));

        static readonly StrCode32Script PlacedActivatePlaced = new StrCode32Script(
            new StrCode32Event("Placed", "OnActivatePlaced", "", "equipId", "index"),
            LuaFunction.ToTableEntry(
                "PlacedActivatePlacedClearCheck",
                new string[] { "equipId", "index" },
                " if TppPlaced.IsQuestBlock(index) then local isClearType = this.CheckQuestAllTargetDynamic(\"Activate\", equipId); TppQuest.ClearWithSave(isClearType); end; "));

        static readonly StrCode32Script GameObjectDead = new StrCode32Script(
            new StrCode32Event("GameObject", "Dead", "", "gameObjectId", "gameObjectId01", "animalId"),
            LuaFunction.ToTableEntry(
                "GameObjectDeadClearCheck",
                new string[] { "gameObjectId", "gameObjectId01", "animalId" },
                " local isClearType = this.CheckQuestAllTargetDynamic(\"Dead\",gameObjectId, animalId); TppQuest.ClearWithSave(isClearType); "));

        static readonly StrCode32Script GameObjectFultonInfo = new StrCode32Script(
            new StrCode32Event("GameObject", "FultonInfo", "", "gameObjectId"),
            LuaFunction.ToTableEntry(
                "GameObjectFultonInfoClearCheck",
                new string[] { "gameObjectId" },
                " if mvars.fultonInfo ~= NONE then TppQuest.ClearWithSave(mvars.fultonInfo) end; mvars.fultonInfo = NONE; "));

        static readonly StrCode32Script GameObjectFulton = new StrCode32Script(
            new StrCode32Event("GameObject", "Fulton", "", "gameObjectId", "animalId"),
            LuaFunction.ToTableEntry(
                "GameObjectFultonClearCheck",
                new string[] { "gameObjectId", "animalId" },
                " local isClearType = this.CheckQuestAllTargetDynamic(\"Fulton\", gameObjectId, animalId); TppQuest.ClearWithSave(isClearType); "));

        static readonly StrCode32Script GameObjectFultonFailed = new StrCode32Script(
            new StrCode32Event("GameObject", "FultonFailed", "", "gameObjectId", "locatorName", "locatorNameUpper", "failureType"),
            LuaFunction.ToTableEntry(
                "GameObjectFultonFailedClearCheck",
                new string[] { "gameObjectId", "locatorName", "locatorNameUpper", "failureType" },
                " if failureType == TppGameObject.FULTON_FAILED_TYPE_ON_FINISHED_RISE then local isClearType = this.CheckQuestAllTargetDynamic(\"FultonFailed\", gameObjectId, locatorName); TppQuest.ClearWithSave(isClearType); end;  "));

        static readonly StrCode32Script GameObjectPlacedIntoHeli = new StrCode32Script(
            new StrCode32Event("GameObject", "PlacedIntoVehicle", "", "gameObjectId", "vehicleGameObjectId"),
            LuaFunction.ToTableEntry(
                "GameObjectPlacedIntoHeliClearCheck",
                new string[] { "gameObjectId", "vehicleGameObjectId" },
                " if Tpp.IsHelicopter(vehicleGameObjectId) then local isClearType = this.CheckQuestAllTargetDynamic(\"InHelicopter\", gameObjectId); TppQuest.ClearWithSave(isClearType); end; "));

        static readonly StrCode32Script GameObjectVehicleBroken = new StrCode32Script(
            new StrCode32Event("GameObject", "VehicleBroken", "", "gameObjectId", "state"),
            LuaFunction.ToTableEntry(
                "GameObjectVehicleBrokenClearCheck",
                new string[] { "gameObjectId", "state" },
                " if state == StrCode32(\"End\") then local isClearType = this.CheckQuestAllTargetDynamic(\"VehicleBroken\", gameObjectId); TppQuest.ClearWithSave(isClearType); end; "));

        static readonly StrCode32Script GameObjectLostControl = new StrCode32Script(
            new StrCode32Event("GameObject", "LostControl", "", "gameObjectId", "state"),
            LuaFunction.ToTableEntry(
                "GameObjectLostControlClearCheck",
                new string[] { "gameObjectId", "state" },
                " if state == StrCode32(\"End\") then local isClearType = this.CheckQuestAllTargetDynamic(\"LostControl\", gameObjectId); TppQuest.ClearWithSave(isClearType); end; "));

        public static readonly StrCode32Script[] allCommonMessages = { PlayerPickUpWeapon, PlayerPickUpPlaced, PlacedActivatePlaced, GameObjectDead, GameObjectFultonInfo, GameObjectFulton, GameObjectFultonFailed, GameObjectPlacedIntoHeli, GameObjectVehicleBroken, GameObjectLostControl };

        public static readonly StrCode32Script[] genericTargetMessages = { GameObjectDead, GameObjectFultonInfo, GameObjectFulton, GameObjectFultonFailed, GameObjectPlacedIntoHeli, GameObjectVehicleBroken, GameObjectLostControl };

        public static readonly StrCode32Script[] dormantItemTargetMessages = { PlayerPickUpWeapon };

        public static readonly StrCode32Script[] activeItemTargetMessages = { PlayerPickUpPlaced, PlacedActivatePlaced };

        public static readonly StrCode32Script[] mechaCaptureTargetMessages = { GameObjectDead, GameObjectFultonInfo, GameObjectFulton, GameObjectFultonFailed, GameObjectVehicleBroken };

        public static readonly StrCode32Script[] mechaNoCaptureTargetMessages = { GameObjectDead, GameObjectVehicleBroken, GameObjectLostControl };

        public static readonly StrCode32Script[] animalTargetMessages = { GameObjectDead, GameObjectFultonInfo, GameObjectFulton, GameObjectFultonFailed };
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
