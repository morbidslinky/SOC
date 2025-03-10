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

        public LuaVariable StrCode32TableDefinitionsVariable = new LuaVariable("StrCode32TableDefinitions");
        public LuaVariable CommonDefinitionsVariable = new LuaVariable("CommonDefinitions");


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
                Lua.TableEntry("ELIMINATE", Lua.TableIdentifier("TppDefine", Lua.Text("QUEST_TYPE"), Lua.Text("ELIMINATE"))),
                Lua.TableEntry("RECOVERED", Lua.TableIdentifier("TppDefine", Lua.Text("QUEST_TYPE"), Lua.Text("RECOVERED"))),
                Lua.TableEntry("KILLREQUIRED", 9),
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

            @this.AddOrSet(
                OnAllocate.Get(),
                Messages.Get(),
                OnInitialize.Get(),
                OnUpdate.Get(),
                OnTerminate.Get(),
                Lua.TableEntry("QUEST_TABLE", QUEST_TABLE, true)
            );

            quest_step.AddOrSet(
                QStep_Start.Get(),
                QStep_Main.Get(StrCode32TableDefinitionsVariable.Name)
            );

            StrCode32TableDefinitionsVariable.AssignedTo = QStep_Main.StrCode32Table.GetStrCode32DefinitionsTable(StrCode32TableDefinitionsVariable.Name);
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
                "return |[2|variable]|",
                    CommonDefinitionsVariable,
                    StrCode32TableDefinitionsVariable,
                    Lua.Variable("this", @this),
                    Lua.Variable("quest_step", quest_step)
                );

            mainScript.WriteToLua(mainLuaFilePath);
            mainScript.WriteToXml(mainLuaFilePath + ".xml");
        }
    }
}
