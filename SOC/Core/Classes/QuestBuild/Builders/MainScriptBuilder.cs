using SOC.Classes.Common;
using SOC.QuestObjects.Common;

namespace SOC.Classes.Lua
{
    public class MainScriptBuilder
    {
        public SetupDetails SetupDetails;
        public ObjectsDetails ObjectsDetails;

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

            QStep_Main.StrCode32Table.AddCommonDefinitions( // TODO create a qvars table first, add these, and merge it with the CommonDefinitions table?
                Lua.TableEntry("DISTANTCP", QuestObjects.Enemy.EnemyInfo.ChooseDistantCP(setupDetails.CPName, setupDetails.locationID)),
                Lua.TableEntry("questTrapName", $"trap_preDeactiveQuestArea_{setupDetails.loadArea}")
            );

            if (setupDetails.CPName == "NONE")
            {
                QStep_Main.StrCode32Table.AddCommonDefinitions(Lua.TableEntry("CPNAME", 
                    Lua.FunctionCall(
                        Lua.TableIdentifier("InfMain", "GetClosestCp"),
                        Lua.Table(setupDetails.coords.xCoord, setupDetails.coords.yCoord, setupDetails.coords.zCoord))));
            }
            else
            {
                QStep_Main.StrCode32Table.AddCommonDefinitions(Lua.TableEntry("CPNAME", setupDetails.CPName));
            }


            QUEST_TABLE.AddOrSet(
                Lua.TableEntry("questType", Lua.TableIdentifier("TppDefine", "QUEST_TYPE", "ELIMINATE"))
            );

            foreach (ObjectsDetail detail in objectsDetails.details)
            {
                detail.AddToMainLua(this);
            }

            quest_step.AddOrSet(
                QStep_Start.Get(),
                QStep_Main.Get(QStep_Main_MessagesDefVariable.Name)
            );

            @this.AddOrSet(
                Lua.TableEntry("QUEST_TABLE", QUEST_TABLE, true),
                Lua.TableEntry("quest_step", quest_step, true),
                OnAllocate.Get(),
                Messages.Get(),
                OnInitialize.Get(),
                OnUpdate.Get(),
                OnTerminate.Get()
            );

            QStep_Main_MessagesDefVariable.AssignedTo = QStep_Main.StrCode32Table.GetStrCode32DefinitionsTable(QStep_Main_MessagesDefVariable.Name);
            Quest_MessagesDefVariable.AssignedTo = Messages.GetMessagesDefs();
            CommonDefinitionsVariable.AssignedTo = QStep_Main.StrCode32Table.GetCommonDefinitionsTable();
        }

        public void Build(string mainLuaFilePath)
        {
            var mainScript = Lua.Function(
                "local StrCode32 = Fox.StrCode32 \n" +
                "local StrCode32Table = Tpp.StrCode32Table \n" +
                "local GetGameObjectId = GameObject.GetGameObjectId \n\n" +
                "local |[0|assign_variable]| " +
                "local |[1|assign_variable]| " +
                "local |[2|assign_variable]| " +
                "local |[3|assign_variable]| " +
                "return |[3|variable]|",
                    CommonDefinitionsVariable,
                    Quest_MessagesDefVariable,
                    QStep_Main_MessagesDefVariable,
                    Lua.Variable("this", @this)
                );

            mainScript.WriteToLua(mainLuaFilePath);
        }
    }
}
