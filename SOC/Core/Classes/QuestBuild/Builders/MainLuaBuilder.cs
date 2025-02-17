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
    public class MainLuaBuilder
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

        public MainLuaBuilder()
        {
            
        }
        public MainLuaBuilder(SetupDetails _setupDetails, ObjectsDetails _objectsDetails)
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

        public void Build(string buildArchivePath, SetupDetails setupDetails, ObjectsDetails objectsDetails)
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
}
