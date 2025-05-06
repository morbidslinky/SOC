namespace SOC.Classes.Lua
{
    public class QStep_Start
    {
        public LuaTable Table = new LuaTable();
        public LuaFunctionBuilder OnEnter = new LuaFunctionBuilder();

        public QStep_Start()
        {
            OnEnter.AppendLuaValue(Lua.FunctionCall(Lua.TableIdentifier("TppQuest", "SetNextQuestStep"), Lua.String("QStep_Main")));
        }

        public LuaTableEntry Get()
        {
            Table.Add(Lua.TableEntry("OnEnter", OnEnter.ToFunction()));
            return Lua.TableEntry("QStep_Start", Table, true);
        }
    }
}
