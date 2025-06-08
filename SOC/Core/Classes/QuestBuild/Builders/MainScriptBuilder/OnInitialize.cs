namespace SOC.Classes.Lua
{
    public class OnInitialize
    {
        public LuaFunctionBuilder Function = new LuaFunctionBuilder();

        public OnInitialize()
        {
            Function.AppendLuaValue(Create.FunctionCall(Create.TableIdentifier("TppQuest", "QuestBlockOnInitialize"), Create.Variable("this")));
        }

        public LuaTableEntry Get()
        {
            return Function.ToTableEntry("OnInitialize", true);
        }
    }
}
