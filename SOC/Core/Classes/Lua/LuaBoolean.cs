using System.Xml.Serialization;

namespace SOC.Classes.Lua
{
    public class LuaBoolean : LuaValue
    {
        [XmlAttribute] public bool BooleanValue { get; set; }
        public override string Value => BooleanValue ? "true" : "false";

        public LuaBoolean() : base(TemplateRestrictionType.BOOLEAN) { }
        public LuaBoolean(bool value) : base(TemplateRestrictionType.BOOLEAN)
        {
            BooleanValue = value;
        }
    }
}
