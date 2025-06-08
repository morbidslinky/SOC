namespace SOC.Classes.Lua
{
    public class OnTerminate
    {
        public LuaFunctionBuilder Function = new LuaFunctionBuilder();

        public OnTerminate()
        {
            Function.AppendLuaValue(Create.FunctionCall(Create.TableIdentifier("TppQuest", "QuestBlockOnTerminate"), Create.Variable("this")));
        }

        public LuaTableEntry Get()
        {
            return Function.ToTableEntry("OnTerminate", true);
        }
    }
}
