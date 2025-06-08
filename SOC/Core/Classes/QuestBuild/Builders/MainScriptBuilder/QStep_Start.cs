namespace SOC.Classes.Lua
{
    public class QStep_Start
    {
        public LuaTable Table = new LuaTable();
        public LuaFunctionBuilder OnEnter = new LuaFunctionBuilder();

        public QStep_Start()
        {
            OnEnter.AppendLuaValue(Create.FunctionCall(Create.TableIdentifier("TppQuest", "SetNextQuestStep"), Create.String("QStep_Main")));
        }

        public LuaTableEntry Get()
        {
            Table.Add(Create.TableEntry("OnEnter", OnEnter.ToFunction()));
            return Create.TableEntry("QStep_Start", Table, true);
        }
    }
}
