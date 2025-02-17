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
        public SetupDetails setupDetails;
        public ObjectsDetails objectsDetails;

        List<string> functionList = new List<string>();

        OpeningVariables openingVariables = new OpeningVariables();
        AuxiliaryCode auxiliaryCode = new AuxiliaryCode();
        OnAllocate onAllocate = new OnAllocate();
        Messages messages = new Messages();
        OnInitialize onInitialize = new OnInitialize();
        QuestTable questTable = new QuestTable();
        OnUpdate onUpdate = new OnUpdate();
        OnTerminate onTerminate = new OnTerminate();
        QStep_Start qStep_start = new QStep_Start();
        public QStep_Main qStep_main = new QStep_Main();
        CheckQuestMethodsList checkQuestMethodList = new CheckQuestMethodsList();
        ObjectiveTypesList objectiveTypesList = new ObjectiveTypesList();

        public MainScriptBuilder()
        {
            
        }
        public MainScriptBuilder(SetupDetails _setupDetails, ObjectsDetails _objectsDetails)
        {
            setupDetails = _setupDetails; objectsDetails = _objectsDetails;
        }
        
        public void AddToOpeningVariables(string variableName, string value = "")
        {
            openingVariables.Add(variableName, value);
        }

        public void AddToAuxiliary(LuaFunctionOldFormat function)
        {
            auxiliaryCode.Add(function.ToLua());
        }

        public void AddToAuxiliary(string localVar)
        {
            auxiliaryCode.Add(localVar);
        }

        public void AddToOnTerminate(string call)
        {
            if (!onAllocate.contains(call))
            {
                onAllocate.AddOnTerminate(call);
            }
        }

        public void AddToQStep_Start_OnEnter(params string[] functionCalls)
        {
            foreach (string functionCall in functionCalls)
                qStep_start.AddToOnEnter(functionCall);
        }

        public void AddToQStep_Start_OnEnter(params LuaFunctionOldFormat[] auxiliaryFunctions)
        {
            foreach (LuaFunctionOldFormat function in auxiliaryFunctions)
                qStep_start.AddToOnEnter($"InfCore.PCall(this.{function.FunctionName})");
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

        public void AddToOnUpdate(string code)
        {
            onUpdate.Add(code);
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
                if (!qStep_main.Contains(message))
                    qStep_main.Add(message);
        }

        public string GetMainLuaFormatted()
        {
            StringBuilder functionBuilder = new StringBuilder();

            functionBuilder.Append(openingVariables.ToLua(this)); // local variables declared before the quest table
            functionBuilder.Append(questTable.ToLua(this)); // the quest table, which lists information on every lua quest object for the sideop
            functionBuilder.Append(auxiliaryCode.ToLua(this)); // functions and variables used for setting up quest objects and other misc. purposes
            functionBuilder.Append(onAllocate.ToLua(this));// onallocate. namely contains OnTerminate logic 
            functionBuilder.Append(messages.ToLua(this)); // quest messages, not to be confused with qStep_main messages
            functionBuilder.Append(onInitialize.ToLua(this));
            functionBuilder.Append(onUpdate.ToLua(this)); // contains calls to frequent checks
            functionBuilder.Append(onTerminate.ToLua(this));
            functionBuilder.Append(qStep_start.ToLua(this)); // calls auxiliary setup functions
            functionBuilder.Append(qStep_main.ToLua(this)); // contains a long list of messages which listen for quest updates

            if (questTable.HasTargets())
            {
                functionBuilder.Append(objectiveTypesList.ToLua(this)); // contains logic for how a quest update is determined as well as what object has what objective type
                functionBuilder.Append(checkQuestMethodList.ToLua(this)); // determines what and how to tally up quest objectives
                functionBuilder.Append(CheckQuestAllTargetDynamic.function.ToLua());
            }

            functionBuilder.Append(" return this");

            return functionBuilder.ToString();
        }

        public void Build(string mainLuaFilePath)
        {
            /*
            string LuaScriptPath = Path.Combine(dir, setupDetails.FpkName + "_fpkd", "Assets/tpp/level/mission2/quest/ih");
            string LuaScriptFile = Path.Combine(LuaScriptPath, setupDetails.FpkName + ".lua");

            Directory.CreateDirectory(LuaScriptPath);

            File.WriteAllText(LuaScriptFile, BuildMain(setupDetails, objectsDetails));

            MainLuaBuilder mainLua = new MainLuaBuilder(setupDetails, objectsDetails);
            mainLua.AddToOpeningVariables("this", "{}");
            mainLua.AddToOpeningVariables("quest_step", "{}");
            mainLua.AddToOpeningVariables("StrCode32", "Fox.StrCode32");
            mainLua.AddToOpeningVariables("StrCode32Table", "Tpp.StrCode32Table");
            mainLua.AddToOpeningVariables("GetGameObjectId", "GameObject.GetGameObjectId");
            mainLua.AddToOpeningVariables("ELIMINATE", "TppDefine.QUEST_TYPE.ELIMINATE");
            mainLua.AddToOpeningVariables("RECOVERED", "TppDefine.QUEST_TYPE.RECOVERED");
            mainLua.AddToOpeningVariables("KILLREQUIRED", "9");

            string cpNameString = setupDetails.CPName;
            if (setupDetails.CPName == "NONE")
            {
                cpNameString = $"InfMain.GetClosestCp{{{setupDetails.coords.xCoord},{setupDetails.coords.yCoord},{setupDetails.coords.zCoord}}}";
            }
            else
            {
                cpNameString = $@"""{setupDetails.CPName}""";
            }

            mainLua.AddToOpeningVariables("CPNAME", cpNameString);
            mainLua.AddToOpeningVariables("DISTANTCP", $@"""{QuestObjects.Enemy.EnemyInfo.ChooseDistantCP(setupDetails.CPName, setupDetails.locationID)}""");
            mainLua.AddToOpeningVariables("questTrapName", $@"""trap_preDeactiveQuestArea_{setupDetails.loadArea}""");

            mainLua.AddToQuestTable("questType = ELIMINATE");
            mainLua.AddToQuestTable("soldierSubType = SUBTYPE");

            foreach (ObjectsDetail detail in objectsDetails.details)
            {
                detail.AddToMainLua(mainLua);
            }

            return mainLua.GetMainLuaFormatted();
            */
        }
    }

    class OpeningVariables
    {
        Dictionary<string, string> variableDictionary = new Dictionary<string, string>();

        public string ToLua(MainScriptBuilder mainLua)
        {
            return GetVariablesFormatted();
        }

        public void Add(string name, string value)
        {
            if (variableDictionary.Keys.Contains(name))
                variableDictionary[name] = value;
            else
                variableDictionary.Add(name, value);
        }

        private string GetVariablesFormatted()
        {
            StringBuilder variablesBuilder = new StringBuilder();
            foreach (KeyValuePair<string, string> variable in variableDictionary)
            {
                if (variable.Value == "")
                    variablesBuilder.Append($@"local {variable.Key}
");
                else
                    variablesBuilder.Append($@"local {variable.Key} = {variable.Value}
");
            }
            return variablesBuilder.ToString();
        }
    }
    class AuxiliaryCode
    {
        List<string> auxiliaryCodes = new List<string>();

        public void Add(string code)
        {
            auxiliaryCodes.Add(code);
        }

        public string ToLua(MainScriptBuilder mainLua)
        {

            foreach (StrCodeBlock strCode in mainLua.qStep_main.strCodes)
            {
                foreach (StrCodeMsgBlock msgBlock in strCode.msgBlocks)
                {
                    foreach (LuaFunctionOldFormat luaFunction in msgBlock.functions)
                    {
                        this.Add(luaFunction.ToLua());
                    }
                }
            }
            StringBuilder auxCodeBuilder = new StringBuilder();
            foreach (string auxCode in auxiliaryCodes)
                auxCodeBuilder.Append($@"
{auxCode}");
            return auxCodeBuilder.ToString();
        }
    }

    class OnAllocate
    {
        List<string> onTerminateCalls = new List<string>();

        public void AddOnTerminate(string call)
        {
            onTerminateCalls.Add(call);
        }

        public bool contains(string call)
        {
            return onTerminateCalls.Contains(call);
        }

        public string ToLua(MainScriptBuilder mainLua)
        {
            return $@"
function this.OnAllocate()
  TppQuest.RegisterQuestStepList{{
    ""QStep_Start"",
    ""QStep_Main"",
    nil
  }}
  TppEnemy.OnAllocateQuestFova(this.QUEST_TABLE)
  TppQuest.RegisterQuestStepTable(quest_step)
  TppQuest.RegisterQuestSystemCallbacks{{
    OnActivate = function()
      TppEnemy.OnActivateQuest(this.QUEST_TABLE)
      TppAnimal.OnActivateQuest(this.QUEST_TABLE)
    end,
    OnDeactivate = function()
      TppEnemy.OnDeactivateQuest(this.QUEST_TABLE)
      TppAnimal.OnDeactivateQuest(this.QUEST_TABLE)
    end,
    OnOutOfAcitveArea = function() 
    end,
    OnTerminate = function(){BuildOnTerminate()}
    end,
  }}
  mvars.fultonInfo = NONE
end";
        }

        private string BuildOnTerminate()
        {
            AddOnTerminate("TppEnemy.OnTerminateQuest(this.QUEST_TABLE)");
            AddOnTerminate("TppAnimal.OnTerminateQuest(this.QUEST_TABLE)");

            StringBuilder terminateBuilder = new StringBuilder();
            foreach (string call in onTerminateCalls)
            {
                terminateBuilder.Append($@"
      {call}");
            }

            return terminateBuilder.ToString();
        }
    }
    class Messages
    {
        public string ToLua(MainScriptBuilder mainLua)
        {
            return @"
this.Messages = function()
  return
    StrCode32Table {
      Block = {
        {
          msg = ""StageBlockCurrentSmallBlockIndexUpdated"",
          func = function() end,
        },
      },
    }
end";
        }
    }
    class OnInitialize
    {
        public string ToLua(MainScriptBuilder mainLua)
        {
            return @"
function this.OnInitialize()
	TppQuest.QuestBlockOnInitialize( this )
end";
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

    class OnUpdate
    {
        List<string> onUpdateList = new List<string>();

        public string ToLua(MainScriptBuilder mainLua)
        {
            StringBuilder onUpdateBuilder = new StringBuilder(@"
function this.OnUpdate()
  TppQuest.QuestBlockOnUpdate(this)");

            foreach (string code in onUpdateList)
            {
                onUpdateBuilder.Append($@"
  {code}");
            }
            onUpdateBuilder.Append(@"
end");

            return onUpdateBuilder.ToString();
        }

        public void Add(string code)
        {
            onUpdateList.Add(code);
        }
    }
    class OnTerminate
    {
        public string ToLua(MainScriptBuilder mainLua)
        {
            return @"
function this.OnTerminate()
	TppQuest.QuestBlockOnTerminate(this)
end";
        }
    }
    public class QStep_Start
    {
        List<string> OnEnterList = new List<string>();

        public void AddToOnEnter(string code)
        {
            OnEnterList.Add(code);
        }

        public string ToLua(MainScriptBuilder mainLua)
        {
            return $@"
quest_step.QStep_Start = {{
  OnEnter = function(){GetEnterListFormatted()}
    TppQuest.SetNextQuestStep(""QStep_Main"")
  end,
}}";
        }

        private string GetEnterListFormatted()
        {
            StringBuilder EnterListBuilder = new StringBuilder();
            foreach (string code in OnEnterList)
            {
                EnterListBuilder.Append($@"
    {code}");
            }
            return EnterListBuilder.ToString();
        }
    }

    public class QStep_Main
    {
        public List<StrCodeBlock> strCodes = new List<StrCodeBlock>();

        public void Add(StrCodeBlock _codeBlock)
        {
            foreach (StrCodeBlock codeBlock in strCodes)
            {
                if (codeBlock.Equals(_codeBlock))
                {
                    codeBlock.Add(_codeBlock.msgBlocks);
                    return;
                }
            }
            strCodes.Add(_codeBlock);
        }

        public bool Contains(StrCodeBlock _codeBlock)
        {
            var existingMsgFunctionPairs = new HashSet<(StrCodeMsgBlock, LuaFunctionOldFormat)>();

            foreach (StrCodeBlock codeBlock in strCodes)
            {
                foreach (StrCodeMsgBlock msgBlock in codeBlock.msgBlocks)
                {
                    foreach (LuaFunctionOldFormat luaFunction in msgBlock.functions)
                    {
                        existingMsgFunctionPairs.Add((msgBlock, luaFunction));
                    }
                }
            }

            foreach (StrCodeMsgBlock _msgBlock in _codeBlock.msgBlocks)
            {
                foreach (LuaFunctionOldFormat _luaFunction in _msgBlock.functions)
                {
                    if (existingMsgFunctionPairs.Contains((_msgBlock, _luaFunction)))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public string ToLua(MainScriptBuilder mainLua)
        {
            return $@"
quest_step.QStep_Main = {{
  Messages = function( self )
    return StrCode32Table {{
        {string.Join(",", strCodes.Select(code => code.ToLua()))}
      }}
  end,
  OnEnter = function() end,
  OnLeave = function() end,
}}";
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

        public StrCodeBlock(string _strCode, string _name, string[] _msgArgs, params LuaFunctionOldFormat[] _functions)
        {
            strCode = _strCode; msgBlocks.Add(new StrCodeMsgBlock(_name, _msgArgs, _functions));
        }

        public StrCodeBlock(string _strCode, string _name, string _sender, string[] _msgArgs, params LuaFunctionOldFormat[] _functions)
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

        public string ToLua()
        {
            return $@"{strCode} = {{
            {string.Join(", ", msgBlocks.Select(msg => $"{{{msg.ToLua()}}}"))}
            }}";
        }
    }

    public class StrCodeMsgBlock
    {
        string msg;
        string sender;
        string[] msgArgs;
        public List<LuaFunctionOldFormat> functions;

        public StrCodeMsgBlock(string _name, string[] _msgArgs)
        {
            msg = _name; sender = ""; msgArgs = _msgArgs; functions = new List<LuaFunctionOldFormat>();
        }

        public StrCodeMsgBlock(string _name, string _sender, string[] _msgArgs)
        {
            msg = _name; sender = _sender; msgArgs = _msgArgs; functions = new List<LuaFunctionOldFormat>();
        }

        public StrCodeMsgBlock(string _name, string[] _msgArgs, LuaFunctionOldFormat[] _functions)
        {
            msg = _name; sender = ""; msgArgs = _msgArgs; functions = _functions.ToList();
        }

        public StrCodeMsgBlock(string _name, string _sender, string[] _msgArgs, LuaFunctionOldFormat[] _functions)
        {
            msg = _name; sender = _sender; msgArgs = _msgArgs; functions = _functions.ToList();
        }

        public void AddFunctionCalls(List<LuaFunctionOldFormat> calls)
        {
            functions.AddRange(calls);
        }

        public string ToLua()
        {
            return $@"
            msg = ""{msg}"", {(sender == "" ? "" : $@"
            sender = {sender}, ")}
            func = function({string.Join(", ", msgArgs)})
              {string.Join(" ", functions.Select(func => func.Call(msgArgs)))}
            end";
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
                checkQuestBuilder.Append($@"{pair.TargetMessageMethod.ToLua()}
{pair.TallyMethod.ToLua()}
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
        public LuaFunctionOldFormat TargetMessageMethod, TallyMethod;

        public CheckQuestMethodsPair(MainScriptBuilder mainLua, LuaFunctionOldFormat a, LuaFunctionOldFormat b, string targetTableName, LuaFunctionOldFormat check, string objective)
        {
            TargetMessageMethod = a; TallyMethod = b;
            mainLua.AddToCheckQuestMethod(this);
            mainLua.AddToObjectiveTypes(targetTableName, new GenericTargetPair(check, objective));
        }

        public CheckQuestMethodsPair(MainScriptBuilder mainLua, LuaFunctionOldFormat a, LuaFunctionOldFormat b, string oneLineObjective)
        {
            TargetMessageMethod = a; TallyMethod = b;
            mainLua.AddToCheckQuestMethod(this);
            mainLua.AddToObjectiveTypes(oneLineObjective);
        }

        public CheckQuestMethodsPair(MainScriptBuilder mainLua, LuaFunctionOldFormat a, LuaFunctionOldFormat b)
        {
            TargetMessageMethod = a; TallyMethod = b;
            mainLua.AddToCheckQuestMethod(this);
        }

        public string GetTableFormat()
        {
            return $"{{IsTargetSetMessageMethod = this.{TargetMessageMethod.FunctionName}, TallyMethod = this.{TallyMethod.FunctionName}}}";
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
{pair.checkMethod.ToLua()}");
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
        public LuaFunctionOldFormat checkMethod;
        public string ObjectiveType;

        public GenericTargetPair(LuaFunctionOldFormat check, string type)
        {
            checkMethod = check; ObjectiveType = type;
        }

        public string GetTableFormat()
        {
            return $"{{Check = this.{checkMethod.FunctionName}, Type = {ObjectiveType}}}";
        }
    }

    public static class QStep_MainCommonMessages
    {

        static readonly StrCodeBlock PlayerPickUpWeapon = new StrCodeBlock(
            "Player",
            "OnPickUpWeapon",
            new string[] { "playerIndex", "playerIndex" },
            new LuaFunctionOldFormat(
                "PlayerPickUpWeaponClearCheck",
                new string[] { "playerIndex", "playerIndex" },
                " local isClearType = this.CheckQuestAllTargetDynamic(\"PickUpDormant\", equipId); TppQuest.ClearWithSave(isClearType); "));

        static readonly StrCodeBlock PlayerPickUpPlaced = new StrCodeBlock(
            "Player",
            "OnPickUpPlaced",
            new string[] { "playerGameObjectId", "equipId", "index", "isPlayer" },
            new LuaFunctionOldFormat(
                "PlayerPickUpPlacedClearCheck",
                new string[] { "playerGameObjectId", "equipId", "index", "isPlayer" },
                " if TppPlaced.IsQuestBlock(index) then local isClearType = this.CheckQuestAllTargetDynamic(\"PickUpActive\", equipId); TppQuest.ClearWithSave(isClearType); end; "));

        static readonly StrCodeBlock PlacedActivatePlaced = new StrCodeBlock(
            "Placed",
            "OnActivatePlaced",
            new string[] { "equipId", "index" },
            new LuaFunctionOldFormat(
                "PlacedActivatePlacedClearCheck",
                new string[] { "equipId", "index" },
                " if TppPlaced.IsQuestBlock(index) then local isClearType = this.CheckQuestAllTargetDynamic(\"Activate\", equipId); TppQuest.ClearWithSave(isClearType); end; "));

        static readonly StrCodeBlock GameObjectDead = new StrCodeBlock(
            "GameObject",
            "Dead",
            new string[] { "gameObjectId", "gameObjectId01", "animalId" },
            new LuaFunctionOldFormat(
                "GameObjectDeadClearCheck",
                new string[] { "gameObjectId", "gameObjectId01", "animalId" },
                " local isClearType = this.CheckQuestAllTargetDynamic(\"Dead\",gameObjectId, animalId); TppQuest.ClearWithSave(isClearType); "));

        static readonly StrCodeBlock GameObjectFultonInfo = new StrCodeBlock(
            "GameObject",
            "FultonInfo",
            new string[] { "gameObjectId" },
            new LuaFunctionOldFormat(
                "GameObjectFultonInfoClearCheck",
                new string[] { "gameObjectId" },
                " if mvars.fultonInfo ~= NONE then TppQuest.ClearWithSave(mvars.fultonInfo) end; mvars.fultonInfo = NONE; "));

        static readonly StrCodeBlock GameObjectFulton = new StrCodeBlock(
            "GameObject",
            "Fulton",
            new string[] { "gameObjectId", "animalId" },
            new LuaFunctionOldFormat(
                "GameObjectFultonClearCheck",
                new string[] { "gameObjectId", "animalId" },
                " local isClearType = this.CheckQuestAllTargetDynamic(\"Fulton\", gameObjectId, animalId); TppQuest.ClearWithSave(isClearType); "));

        static readonly StrCodeBlock GameObjectFultonFailed = new StrCodeBlock(
            "GameObject",
            "FultonFailed",
            new string[] { "gameObjectId", "locatorName", "locatorNameUpper", "failureType" },
            new LuaFunctionOldFormat(
                "GameObjectFultonFailedClearCheck",
                new string[] { "gameObjectId", "locatorName", "locatorNameUpper", "failureType" },
                " if failureType == TppGameObject.FULTON_FAILED_TYPE_ON_FINISHED_RISE then local isClearType = this.CheckQuestAllTargetDynamic(\"FultonFailed\", gameObjectId, locatorName); TppQuest.ClearWithSave(isClearType); end;  "));

        static readonly StrCodeBlock GameObjectPlacedIntoHeli = new StrCodeBlock(
            "GameObject",
            "PlacedIntoVehicle",
            new string[] { "gameObjectId", "vehicleGameObjectId" },
            new LuaFunctionOldFormat(
                "GameObjectPlacedIntoHeliClearCheck",
                new string[] { "gameObjectId", "vehicleGameObjectId" },
                " if Tpp.IsHelicopter(vehicleGameObjectId) then local isClearType = this.CheckQuestAllTargetDynamic(\"InHelicopter\", gameObjectId); TppQuest.ClearWithSave(isClearType); end; "));

        static readonly StrCodeBlock GameObjectVehicleBroken = new StrCodeBlock(
            "GameObject",
            "VehicleBroken",
            new string[] { "gameObjectId", "state" },
            new LuaFunctionOldFormat(
                "GameObjectVehicleBrokenClearCheck",
                new string[] { "gameObjectId", "state" },
                " if state == StrCode32(\"End\") then local isClearType = this.CheckQuestAllTargetDynamic(\"VehicleBroken\", gameObjectId); TppQuest.ClearWithSave(isClearType); end; "));

        static readonly StrCodeBlock GameObjectLostControl = new StrCodeBlock(
            "GameObject",
            "LostControl",
            new string[] { "gameObjectId", "state" },
            new LuaFunctionOldFormat(
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
        static readonly LuaFunctionOldFormat IsTargetSetMessageIdForItem = new LuaFunctionOldFormat("IsTargetSetMessageIdForItem", new string[] { "gameId", "messageId", "checkAnimalId" },
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

        static readonly LuaFunctionOldFormat TallyItemTargets = new LuaFunctionOldFormat("TallyItemTargets", new string[] { "totalTargets", "objectiveCompleteCount", "objectiveFailedCount" },
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

        public CheckQuestItem(MainScriptBuilder mainLua, LuaFunctionOldFormat checkFunction, string objectiveType) : base(mainLua, IsTargetSetMessageIdForItem, TallyItemTargets, "itemTargets", checkFunction, objectiveType) { }
    }
    class CheckQuestGenericEnemy : CheckQuestMethodsPair
    {

        static readonly LuaFunctionOldFormat IsTargetSetMessageIdForGenericEnemy = new LuaFunctionOldFormat("IsTargetSetMessageIdForGenericEnemy", new string[] { "gameId", "messageId", "checkAnimalId" },
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

        static readonly LuaFunctionOldFormat TallyGenericTargets = new LuaFunctionOldFormat("TallyGenericTargets", new string[] { "totalTargets", "objectiveCompleteCount", "objectiveFailedCount" },
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

        public CheckQuestGenericEnemy(MainScriptBuilder mainLua, LuaFunctionOldFormat checkFunction, string objectiveType) : base(mainLua, IsTargetSetMessageIdForGenericEnemy, TallyGenericTargets, "genericTargets", checkFunction, objectiveType) { }

        public CheckQuestGenericEnemy(MainScriptBuilder mainLua) : base(mainLua, IsTargetSetMessageIdForGenericEnemy, TallyGenericTargets) { }
    }
    class CheckQuestAnimal : CheckQuestMethodsPair
    {
        static readonly LuaFunctionOldFormat IsTargetSetMessageIdForAnimal = new LuaFunctionOldFormat("IsTargetSetMessageIdForAnimal", new string[] { "gameId", "messageId", "checkAnimalId" },
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

        static readonly LuaFunctionOldFormat TallyAnimalTargets = new LuaFunctionOldFormat("TallyAnimalTargets", new string[] { "totalTargets", "objectiveCompleteCount", "objectiveFailedCount" },
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
        public static readonly LuaFunctionOldFormat function = new LuaFunctionOldFormat("CheckQuestAllTargetDynamic", new string[] { "messageId", "gameId", "checkAnimalId" }, "" +
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
    public class LuaFunctionOldFormat
    {
        public string FunctionName { get; set; }
        public string[] Parameters { get; set; }
        public string Function { get; set; }
        public LuaFunctionOldFormat(string functionName, string[] parameters, string function)
        {
            FunctionName = functionName;
            Parameters = parameters ?? Array.Empty<string>();
            Function = function;
        }
        public string Call(string[] args)
        {
            return $@"this.{FunctionName}({string.Join(", ", args)})";
        }
        public override bool Equals(object obj)
        {
            if (!(obj is LuaFunctionOldFormat other))
                return false;
            return this.ToLua() == other.ToLua();
        }
        public override int GetHashCode()
        {
            return this.ToLua().GetHashCode();
        }
        public string ToLua()
        {
            return $@"this.{FunctionName} = function({string.Join(", ", Parameters)}) {Function} end";
        }
    }
}
