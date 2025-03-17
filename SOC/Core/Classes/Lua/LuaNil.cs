
namespace SOC.Classes.Lua
{
    public class LuaNil : LuaValue
    {
        public override string Value => "nil";

        public LuaNil() : base(TemplateRestrictionType.NIL) { }
    }
}
