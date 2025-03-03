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

        public CheckQuestMethodsList checkQuestMethodList = new CheckQuestMethodsList(); // TODO
        public ObjectiveTypesList objectiveTypesList = new ObjectiveTypesList(); // TODO

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
/*
            @this.AddOrSet(checkQuestMethodList.GetCheckFunctions());
            @this.AddOrSet(checkQuestMethodList.GetCheckQuestMethodList());
            @this.AddOrSet(objectiveTypesList.GetObjectiveFunctions());
            @this.AddOrSet(objectiveTypesList.GetObjectiveTypesList());
*/
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
}
