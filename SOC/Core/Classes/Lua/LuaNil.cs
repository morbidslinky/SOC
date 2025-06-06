
namespace SOC.Classes.Lua
{
    public class LuaNil : LuaValue
    {
        public override string TokenValue => "nil";

        public LuaNil() : base(TemplateRestrictionType.NIL) { }
    }
}
