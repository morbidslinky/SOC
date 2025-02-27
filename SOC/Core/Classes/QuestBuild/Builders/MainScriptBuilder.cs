using SOC.Classes.Common;
using SOC.QuestObjects.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOC.Classes.Lua
{
    public class MainScriptBuilder
    {
        public SetupDetails SetupDetails;
        public ObjectsDetails ObjectsDetails;

        QuestTable questTable = new QuestTable(); // TODO
        CheckQuestMethodsList checkQuestMethodList = new CheckQuestMethodsList(); // TODO
        ObjectiveTypesList objectiveTypesList = new ObjectiveTypesList(); // TODO


        public OnUpdate OnUpdate = new OnUpdate();
        public OnAllocate OnAllocate = new OnAllocate();
        public Messages Messages = new Messages();
        public OnInitialize OnInitialize = new OnInitialize();
        public OnTerminate OnTerminate = new OnTerminate();
        public QStep_Start QStep_Start = new QStep_Start();
        public QStep_Main QStep_Main = new QStep_Main(); 

        public LuaTable qvars = new LuaTable();
        public LuaTable @this = new LuaTable();
        public LuaTable quest_step = new LuaTable();

        public MainScriptBuilder(SetupDetails setupDetails, ObjectsDetails objectsDetails)
        {
            SetupDetails = setupDetails; ObjectsDetails = objectsDetails;

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

            foreach (ObjectsDetail detail in objectsDetails.details)
            {
                detail.AddToMainLua(this);
            }

            @this.AddOrSet(
                OnAllocate.Get(),
                Messages.Get(),
                OnInitialize.Get(),
                OnUpdate.Get(),
                OnTerminate.Get()
            );

            quest_step.AddOrSet(
                QStep_Start.Get(),
                QStep_Main.Get()
            );

            /*
            AddToQuestTable("questType = ELIMINATE");
            AddToQuestTable("soldierSubType = SUBTYPE");
            */
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

        public void AddToQuestTable(params object[] tableItems)
        {
            foreach(object tableItem in tableItems)
            {
                if (tableItem is Table)
                    questTable.Add(tableItem as Table);
                else if (tableItem is string)
                    questTable.Add(tableItem as string);
            }
        }

        public void AddToTargetList(string targetName)
        {
            questTable.AddTarget(targetName);
        }

        public void AddBaseQStep_MainMsgs(params StrCodeBlock[] messages)
        {
            foreach (StrCodeBlock message in messages)
                if (!QStep_Main.Contains(message))
                {
                    foreach (StrCodeMsgBlock block in message.msgBlocks)
                    {
                        foreach (LuaTableEntry function in block.functions)
                        {
                            function.ExtrudeForAssignmentVariable = true;
                            qvars.AddOrSet(function);
                        }
                    }
                    QStep_Main.AddToMessagesStrCode32Table(message);
                }
        }

        public void Build(string mainLuaFilePath)
        {
            var mainScript = Lua.Function("local |[0|assign_variable]| local |[1|assign_variable]| local |[2|assign_variable]| return |[1|variable]|",
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
        public List<StrCodeBlock> StrCode32TableEntries = new List<StrCodeBlock>();
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
            LuaTable StrCode32Table = new LuaTable();
            foreach (StrCodeBlock block in StrCode32TableEntries)
            {
                StrCode32Table.AddOrSet(block.Get());
            }

            QStep_Main_Table.AddOrSet(
                Lua.TableEntry("Messages", Lua.Function("return |[0|function_call]|", Lua.FunctionCall("StrCode32Table", StrCode32Table)), false),
                Lua.TableEntry("OnEnter", OnEnterFunction.ToFunction()),
                Lua.TableEntry("OnLeave", OnLeaveFunction.ToFunction())
            );

            return Lua.TableEntry("QStep_Main", QStep_Main_Table, true);
        }

        public void AddToMessagesStrCode32Table(StrCodeBlock _codeBlock)
        {
            foreach (StrCodeBlock codeBlock in StrCode32TableEntries)
            {
                if (codeBlock.Equals(_codeBlock))
                {
                    codeBlock.Add(_codeBlock.msgBlocks);
                    return;
                }
            }
            StrCode32TableEntries.Add(_codeBlock);
        }

        public bool Contains(StrCodeBlock _codeBlock)
        {
            var existingMsgFunctionPairs = new HashSet<(StrCodeMsgBlock, LuaTableEntry)>();

            foreach (StrCodeBlock codeBlock in StrCode32TableEntries)
            {
                foreach (StrCodeMsgBlock msgBlock in codeBlock.msgBlocks)
                {
                    foreach (LuaTableEntry luaFunction in msgBlock.functions)
                    {
                        existingMsgFunctionPairs.Add((msgBlock, luaFunction));
                    }
                }
            }

            foreach (StrCodeMsgBlock _msgBlock in _codeBlock.msgBlocks)
            {
                foreach (LuaTableEntry _luaFunction in _msgBlock.functions)
                {
                    if (existingMsgFunctionPairs.Contains((_msgBlock, _luaFunction)))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

    }

    public class StrCodeBlock
    {
        public string strCode;
        public List<StrCodeMsgBlock> msgBlocks = new List<StrCodeMsgBlock>();

        public StrCodeBlock(string _strCode, StrCodeMsgBlock _msgBlock)
        {
            strCode = _strCode; msgBlocks.Add(_msgBlock);
        }

        public StrCodeBlock(string _strCode, List<StrCodeMsgBlock> _msgBlocks)
        {
            strCode = _strCode; msgBlocks.AddRange(_msgBlocks);
        }

        public StrCodeBlock(string _strCode, string _name, string[] _msgArgs, params LuaTableEntry[] _functions)
        {
            strCode = _strCode; msgBlocks.Add(new StrCodeMsgBlock(_name, _msgArgs, _functions));
        }

        public StrCodeBlock(string _strCode, string _name, string _sender, string[] _msgArgs, params LuaTableEntry[] _functions)
        {
            strCode = _strCode; msgBlocks.Add(new StrCodeMsgBlock(_name, _sender, _msgArgs, _functions));
        }

        public void Add(List<StrCodeMsgBlock> _msgBlocks)
        {
            foreach (StrCodeMsgBlock msg in _msgBlocks)
            {
                this.Add(msg);
            }
        }

        public void Add(StrCodeMsgBlock _msgBlock)
        {
            foreach (StrCodeMsgBlock msgBlock in msgBlocks)
            {
                if (msgBlock.Equals(_msgBlock))
                {
                    msgBlock.AddFunctionCalls(_msgBlock.functions);
                    return;
                }
            }
            msgBlocks.Add(_msgBlock);
        }

        public bool Equals(StrCodeBlock _code)
        {
            return strCode.Equals(_code.strCode);
        }

        public LuaTableEntry Get()
        {
            LuaTable StrCodeTable = new LuaTable();

            foreach (StrCodeMsgBlock msgBlock in msgBlocks)
            {
                StrCodeTable.AddOrSet(Lua.TableEntry(msgBlock.Get()));
            }

            return Lua.TableEntry(strCode, StrCodeTable);
        }
    }

    public class StrCodeMsgBlock
    {
        string msg;
        string sender;
        string[] msgArgs;
        public List<LuaTableEntry> functions;

        public StrCodeMsgBlock(string _name, string[] _msgArgs)
        {
            msg = _name; sender = ""; msgArgs = _msgArgs; functions = new List<LuaTableEntry>();
        }

        public StrCodeMsgBlock(string _name, string _sender, string[] _msgArgs)
        {
            msg = _name; sender = _sender; msgArgs = _msgArgs; functions = new List<LuaTableEntry>();
        }

        public StrCodeMsgBlock(string _name, string[] _msgArgs, LuaTableEntry[] _functions)
        {
            msg = _name; sender = ""; msgArgs = _msgArgs; functions = _functions.ToList();
        }

        public StrCodeMsgBlock(string _name, string _sender, string[] _msgArgs, LuaTableEntry[] _functions)
        {
            msg = _name; sender = _sender; msgArgs = _msgArgs; functions = _functions.ToList();
        }

        public void AddFunctionCalls(List<LuaTableEntry> calls)
        {
            functions.AddRange(calls);
        }

        public LuaTable Get()
        {
            LuaTable MsgSenderFuncTuple = new LuaTable();
            MsgSenderFuncTuple.AddOrSet(
                Lua.TableEntry("msg", msg));

            if (sender != "")
            {
                MsgSenderFuncTuple.AddOrSet(
                    Lua.TableEntry("sender", sender));
            }

            LuaFunctionBuilder entryFunctionBuilder = new LuaFunctionBuilder();
            entryFunctionBuilder.AppendParameter(msgArgs);

            foreach (LuaTableEntry func in functions)
            {
                entryFunctionBuilder.AppendLuaValue(Lua.FunctionCall(Lua.TableIdentifier("qvars", func.Key), msgArgs.Select(argString => Lua.Variable(argString)).ToArray()));
            }

            MsgSenderFuncTuple.AddOrSet(Lua.TableEntry("func", entryFunctionBuilder.ToFunction()));

            return MsgSenderFuncTuple;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is StrCodeMsgBlock other))
                return false;

            return msg.Equals(other.msg) && sender.Equals(other.sender);
        }

        public override int GetHashCode()
        {
            return msg.GetHashCode() + sender.GetHashCode();
        }
    }

    class QuestTable
    {
        List<Table> Tables = new List<Table>();
        List<string> variables = new List<string>();
        List<string> targetNames = new List<string>();

        public void Add(Table table)
        {
            Table existingTable = Find(table.GetName());
            if (existingTable != null)
            {
                foreach (string entry in table.GetEntries())
                {
                    existingTable.Add(entry);
                }
            }
            else
                Tables.Add(table);
        }

        public void Add(string variable)
        {
            variables.Add(variable);
        }

        public void AddTarget(string targetName)
        {
            if (!targetNames.Contains(targetName))
                targetNames.Add(targetName);
        }

        public bool HasTargets()
        {
            return targetNames.Count > 0;
        }

        public Table Find(string tableName)
        {
            foreach (Table table in Tables)
            {
                if (table.GetName() == tableName)
                {
                    return table;
                }
            }
            return null;
        }

        public string ToLua(MainScriptBuilder mainLua)
        {
            return GetQuestTableFormatted();
        }

        private string GetQuestTableFormatted()
        {
            StringBuilder questTableBuilder = new StringBuilder(@"
this.QUEST_TABLE = {");
            foreach (string var in variables)
            {
                questTableBuilder.Append($@"
    {var},");
            }
            foreach (Table table in Tables)
            {
                questTableBuilder.Append(table.GetTableFormatted());
            }
            questTableBuilder.Append(GetTargetList());
            questTableBuilder.Append(@"
}");

            return questTableBuilder.ToString();
        }

        private string GetTargetList()
        {
            Table targetList = new Table("targetList");
            foreach (string targetName in targetNames)
                targetList.Add($@"""{targetName}""");

            return targetList.GetTableFormatted();
        }
    }

    public class Table
    {
        string Name;
        List<string> Entries = new List<string>();

        public Table(string name)
        {
            Name = name;
        }

        public void Add(string entry)
        {
            Entries.Add(entry);
        }

        public string GetName()
        {
            return Name;
        }

        public string[] GetEntries()
        {
            return Entries.ToArray();
        }

        public string GetTableFormatted()
        {
            StringBuilder tableBuilder = new StringBuilder($@"
    {Name} = {{");
            foreach (string entry in Entries)
            {
                tableBuilder.Append($@"{entry}, ");
            }
            tableBuilder.Append(@"
    },");
            return tableBuilder.ToString();
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

        static readonly StrCodeBlock PlayerPickUpWeapon = new StrCodeBlock(
            "Player",
            "OnPickUpWeapon",
            new string[] { "playerIndex", "playerIndex" },
            LuaFunction.ToTableEntry(
                "PlayerPickUpWeaponClearCheck",
                new string[] { "playerIndex", "playerIndex" },
                " local isClearType = this.CheckQuestAllTargetDynamic(\"PickUpDormant\", equipId); TppQuest.ClearWithSave(isClearType); "));

        static readonly StrCodeBlock PlayerPickUpPlaced = new StrCodeBlock(
            "Player",
            "OnPickUpPlaced",
            new string[] { "playerGameObjectId", "equipId", "index", "isPlayer" },
            LuaFunction.ToTableEntry(
                "PlayerPickUpPlacedClearCheck",
                new string[] { "playerGameObjectId", "equipId", "index", "isPlayer" },
                " if TppPlaced.IsQuestBlock(index) then local isClearType = this.CheckQuestAllTargetDynamic(\"PickUpActive\", equipId); TppQuest.ClearWithSave(isClearType); end; "));

        static readonly StrCodeBlock PlacedActivatePlaced = new StrCodeBlock(
            "Placed",
            "OnActivatePlaced",
            new string[] { "equipId", "index" },
            LuaFunction.ToTableEntry(
                "PlacedActivatePlacedClearCheck",
                new string[] { "equipId", "index" },
                " if TppPlaced.IsQuestBlock(index) then local isClearType = this.CheckQuestAllTargetDynamic(\"Activate\", equipId); TppQuest.ClearWithSave(isClearType); end; "));

        static readonly StrCodeBlock GameObjectDead = new StrCodeBlock(
            "GameObject",
            "Dead",
            new string[] { "gameObjectId", "gameObjectId01", "animalId" },
            LuaFunction.ToTableEntry(
                "GameObjectDeadClearCheck",
                new string[] { "gameObjectId", "gameObjectId01", "animalId" },
                " local isClearType = this.CheckQuestAllTargetDynamic(\"Dead\",gameObjectId, animalId); TppQuest.ClearWithSave(isClearType); "));

        static readonly StrCodeBlock GameObjectFultonInfo = new StrCodeBlock(
            "GameObject",
            "FultonInfo",
            new string[] { "gameObjectId" },
            LuaFunction.ToTableEntry(
                "GameObjectFultonInfoClearCheck",
                new string[] { "gameObjectId" },
                " if mvars.fultonInfo ~= NONE then TppQuest.ClearWithSave(mvars.fultonInfo) end; mvars.fultonInfo = NONE; "));

        static readonly StrCodeBlock GameObjectFulton = new StrCodeBlock(
            "GameObject",
            "Fulton",
            new string[] { "gameObjectId", "animalId" },
            LuaFunction.ToTableEntry(
                "GameObjectFultonClearCheck",
                new string[] { "gameObjectId", "animalId" },
                " local isClearType = this.CheckQuestAllTargetDynamic(\"Fulton\", gameObjectId, animalId); TppQuest.ClearWithSave(isClearType); "));

        static readonly StrCodeBlock GameObjectFultonFailed = new StrCodeBlock(
            "GameObject",
            "FultonFailed",
            new string[] { "gameObjectId", "locatorName", "locatorNameUpper", "failureType" },
            LuaFunction.ToTableEntry(
                "GameObjectFultonFailedClearCheck",
                new string[] { "gameObjectId", "locatorName", "locatorNameUpper", "failureType" },
                " if failureType == TppGameObject.FULTON_FAILED_TYPE_ON_FINISHED_RISE then local isClearType = this.CheckQuestAllTargetDynamic(\"FultonFailed\", gameObjectId, locatorName); TppQuest.ClearWithSave(isClearType); end;  "));

        static readonly StrCodeBlock GameObjectPlacedIntoHeli = new StrCodeBlock(
            "GameObject",
            "PlacedIntoVehicle",
            new string[] { "gameObjectId", "vehicleGameObjectId" },
            LuaFunction.ToTableEntry(
                "GameObjectPlacedIntoHeliClearCheck",
                new string[] { "gameObjectId", "vehicleGameObjectId" },
                " if Tpp.IsHelicopter(vehicleGameObjectId) then local isClearType = this.CheckQuestAllTargetDynamic(\"InHelicopter\", gameObjectId); TppQuest.ClearWithSave(isClearType); end; "));

        static readonly StrCodeBlock GameObjectVehicleBroken = new StrCodeBlock(
            "GameObject",
            "VehicleBroken",
            new string[] { "gameObjectId", "state" },
            LuaFunction.ToTableEntry(
                "GameObjectVehicleBrokenClearCheck",
                new string[] { "gameObjectId", "state" },
                " if state == StrCode32(\"End\") then local isClearType = this.CheckQuestAllTargetDynamic(\"VehicleBroken\", gameObjectId); TppQuest.ClearWithSave(isClearType); end; "));

        static readonly StrCodeBlock GameObjectLostControl = new StrCodeBlock(
            "GameObject",
            "LostControl",
            new string[] { "gameObjectId", "state" },
            LuaFunction.ToTableEntry(
                "GameObjectLostControlClearCheck",
                new string[] { "gameObjectId", "state" },
                " if state == StrCode32(\"End\") then local isClearType = this.CheckQuestAllTargetDynamic(\"LostControl\", gameObjectId); TppQuest.ClearWithSave(isClearType); end; "));

        public static readonly StrCodeBlock[] allCommonMessages = { PlayerPickUpWeapon, PlayerPickUpPlaced, PlacedActivatePlaced, GameObjectDead, GameObjectFultonInfo, GameObjectFulton, GameObjectFultonFailed, GameObjectPlacedIntoHeli, GameObjectVehicleBroken, GameObjectLostControl };

        public static readonly StrCodeBlock[] genericTargetMessages = { GameObjectDead, GameObjectFultonInfo, GameObjectFulton, GameObjectFultonFailed, GameObjectPlacedIntoHeli, GameObjectVehicleBroken, GameObjectLostControl };

        public static readonly StrCodeBlock[] dormantItemTargetMessages = { PlayerPickUpWeapon };

        public static readonly StrCodeBlock[] activeItemTargetMessages = { PlayerPickUpPlaced, PlacedActivatePlaced };

        public static readonly StrCodeBlock[] mechaCaptureTargetMessages = { GameObjectDead, GameObjectFultonInfo, GameObjectFulton, GameObjectFultonFailed, GameObjectVehicleBroken };

        public static readonly StrCodeBlock[] mechaNoCaptureTargetMessages = { GameObjectDead, GameObjectVehicleBroken, GameObjectLostControl };

        public static readonly StrCodeBlock[] animalTargetMessages = { GameObjectDead, GameObjectFultonInfo, GameObjectFulton, GameObjectFultonFailed };
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
