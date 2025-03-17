namespace SOC.Classes.Lua
{
    public class OnInitialize
    {
        public LuaFunctionBuilder Function = new LuaFunctionBuilder();

        public OnInitialize()
        {
            Function.AppendLuaValue(Lua.FunctionCall(Lua.TableIdentifier("TppQuest", "QuestBlockOnInitialize"), Lua.Variable("this")));
        }

        public LuaTableEntry Get()
        {
            return Function.ToTableEntry("OnInitialize", true);
        }
    }
}
