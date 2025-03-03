namespace SOC.Classes.Lua
{
    public class OnTerminate
    {
        public LuaFunctionBuilder Function = new LuaFunctionBuilder();

        public OnTerminate()
        {
            Function.AppendLuaValue(Lua.FunctionCall(Lua.TableIdentifier("TppQuest", "QuestBlockOnTerminate"), Lua.Variable("this")));
        }

        public LuaTableEntry Get()
        {
            return Function.ToTableEntry("OnTerminate", true);
        }
    }
}
