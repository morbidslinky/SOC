namespace SOC.Classes.Lua
{
    public class OnUpdate
    {
        public LuaFunctionBuilder Function = new LuaFunctionBuilder();

        public OnUpdate()
        {
            Function.AppendLuaValue(Lua.FunctionCall(Lua.TableIdentifier("TppQuest", "QuestBlockOnUpdate"), Lua.Variable("this")));
        }

        public LuaTableEntry Get()
        {
            return Function.ToTableEntry("OnUpdate", true);
        }
    }
}
