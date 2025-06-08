using SOC.Classes.Common;
using SOC.QuestObjects.Common;

namespace SOC.Classes.Lua
{
    public class MainScriptBuilder
    {
        public Quest Quest;

        public OnUpdate OnUpdate = new OnUpdate();
        public OnAllocate OnAllocate = new OnAllocate();
        public Messages Messages = new Messages();
        public OnInitialize OnInitialize = new OnInitialize();
        public OnTerminate OnTerminate = new OnTerminate();
        public QStep_Start QStep_Start = new QStep_Start();
        public QStep_Main QStep_Main = new QStep_Main();

        public LuaTable QUEST_TABLE = new LuaTable();
        public LuaTable @this = new LuaTable();
        public LuaTable quest_step = new LuaTable();

        public LuaVariable QStep_Main_MessagesDefVariable = new LuaVariable("QStep_Defs");
        public LuaVariable Quest_MessagesDefVariable = new LuaVariable(Messages.TABLE_VAR_NAME);
        public LuaVariable CommonDefinitionsVariable = new LuaVariable("qvars");


        public MainScriptBuilder(Quest quest)
        {
            Quest = quest;

            QStep_Main.StrCode32Table.AddCommonDefinitions(Quest.ScriptDetails.VariableDeclarations.ToArray());
            QStep_Main.StrCode32Table.Add(Quest.ScriptDetails.QStep_Main);

            QStep_Main.StrCode32Table.AddCommonDefinitions(
                Create.TableEntry("DISTANTCP", QuestObjects.Enemy.EnemyInfo.ChooseDistantCP(Quest.SetupDetails.CPName, Quest.SetupDetails.locationID)),
                Create.TableEntry("questTrapName", $"trap_preDeactiveQuestArea_{Quest.SetupDetails.loadArea}")
            );

            if (Quest.SetupDetails.CPName == "NONE")
            {
                QStep_Main.StrCode32Table.AddCommonDefinitions(Create.TableEntry("CPNAME", 
                    Create.FunctionCall(
                        Create.TableIdentifier("InfMain", "GetClosestCp"),
                        Create.Table(Quest.SetupDetails.coords.xCoord, Quest.SetupDetails.coords.yCoord, Quest.SetupDetails.coords.zCoord))));
            }
            else
            {
                QStep_Main.StrCode32Table.AddCommonDefinitions(Create.TableEntry("CPNAME", Quest.SetupDetails.CPName));
            }


            QUEST_TABLE.Add(
                Create.TableEntry("questType", Create.TableIdentifier("TppDefine", "QUEST_TYPE", "ELIMINATE"))
            );

            foreach (ObjectsDetail detail in Quest.ObjectsDetails.Details)
            {
                detail.AddToMainLua(this);
            }

            quest_step.Add(
                QStep_Start.Get(),
                QStep_Main.Get(QStep_Main_MessagesDefVariable.Value)
            );

            @this.Add(
                Create.TableEntry("QUEST_TABLE", QUEST_TABLE, true),
                Create.TableEntry("quest_step", quest_step, true),
                OnAllocate.Get(),
                Messages.Get(),
                OnInitialize.Get(),
                OnUpdate.Get(),
                OnTerminate.Get()
            );

            QStep_Main_MessagesDefVariable.AssignedTo = QStep_Main.StrCode32Table.GetStrCode32DefinitionsTable(QStep_Main_MessagesDefVariable.Value);
            Quest_MessagesDefVariable.AssignedTo = Messages.GetMessagesDefs();
            CommonDefinitionsVariable.AssignedTo = QStep_Main.StrCode32Table.GetCommonDefinitionsTable();
        }

        public void Build(string mainLuaFilePath)
        {
            var mainScript = Create.Function(
                "local StrCode32 = Fox.StrCode32 \n" +
                "local StrCode32Table = Tpp.StrCode32Table \n" +
                "local GetGameObjectId = GameObject.GetGameObjectId \n\n" +
                "local |[1|ASSIGN_VARIABLE]| " +
                "local |[2|ASSIGN_VARIABLE]| " +
                "local |[3|ASSIGN_VARIABLE]| " +
                "local |[4|ASSIGN_VARIABLE]| " +
                "return |[4|VARIABLE]|",
                    CommonDefinitionsVariable,
                    Quest_MessagesDefVariable,
                    QStep_Main_MessagesDefVariable,
                    Create.Variable("this", @this)
                );

            mainScript.WriteToLua(mainLuaFilePath);
        }
    }
}
