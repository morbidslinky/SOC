using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOC.Classes.Lua
{
    public class MainLua
    {
        List<string> functionList = new List<string>();

        OpeningVariables openingVariables = new OpeningVariables();
        AuxiliaryCode auxiliaryCode = new AuxiliaryCode();
        OnAllocate onAllocate = new OnAllocate();
        QuestTable questTable = new QuestTable();
        QStep_Start qStep_start = new QStep_Start();
        QStep_Main qStep_main = new QStep_Main();
        CheckQuestMethodsList checkQuestMethodList = new CheckQuestMethodsList();
        ObjectiveTypesList objectiveTypesList = new ObjectiveTypesList();
        OnUpdate onUpdate = new OnUpdate();
        
        public void AddToOpeningVariables(string variableName, string value = "")
        {
            openingVariables.Add(variableName, value);
        }

        public void AddToAuxiliary(LuaFunction function)
        {
            auxiliaryCode.Add(function.FunctionFull);
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

        public void AddToQStep_Start_OnEnter(params LuaFunction[] auxiliaryFunctions)
        {
            foreach (LuaFunction function in auxiliaryFunctions)
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

        public void AddToQStep_Main(params QStep_Message[] messages)
        {
            foreach (QStep_Message message in messages)
                if (!qStep_main.Contains(message))
                    qStep_main.Add(message);
        }

        public string GetMainLuaFormatted()
        {
            openingVariables.BuildComponent(this); // local variables declared before the quest table
            questTable.BuildComponent(this); // the quest table, which lists information on every lua quest object for the sideop
            auxiliaryCode.BuildComponent(this); // functions and variables used for setting up quest objects and other misc. purposes
            onAllocate.BuildComponent(this);// onallocate. namely contains OnTerminate logic 
            new Messages().BuildComponent(this); // quest messages, not to be confused with qStep_main messages
            new OnInitialize().BuildComponent(this);
            onUpdate.BuildComponent(this); // contains calls to frequent checks
            new OnTerminate().BuildComponent(this);
            qStep_start.BuildComponent(this); // calls auxiliary setup functions
            qStep_main.BuildComponent(this); // contains a long list of messages which listen for quest updates
            objectiveTypesList.BuildComponent(this); // contains logic for how a quest update is determined as well as what object has what objective type
            checkQuestMethodList.BuildComponent(this); // determines what and how to tally up quest objectives
            new CheckQuestAllTargetDynamic().BuildComponent(this);
            functionList.Add(@"
return this");

            StringBuilder functionBuilder = new StringBuilder();
            foreach (string function in functionList)
                functionBuilder.Append($@"{function}
");

            return functionBuilder.ToString();
        }

        public void AddCodeToScript(string code)
        {
            functionList.Add(code);
        }
    }
}
