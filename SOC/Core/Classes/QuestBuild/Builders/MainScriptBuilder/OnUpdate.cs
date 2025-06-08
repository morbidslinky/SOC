namespace SOC.Classes.Lua
{
    public class OnUpdate
    {
        public LuaFunctionBuilder Function = new LuaFunctionBuilder();

        public OnUpdate()
        {
            Function.AppendLuaValue(Create.FunctionCall(Create.TableIdentifier("TppQuest", "QuestBlockOnUpdate"), Create.Variable("this")));
        }

        public LuaTableEntry Get()
        {
            return Function.ToTableEntry("OnUpdate", true);
        }
    }
}
