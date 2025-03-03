namespace SOC.Classes.Lua
{
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
}
