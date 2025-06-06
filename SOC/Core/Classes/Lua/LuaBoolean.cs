using System.Xml.Serialization;

namespace SOC.Classes.Lua
{
    public class LuaBoolean : LuaValue
    {
        [XmlAttribute] public bool Value { get; set; }
        public override string TokenValue => Value ? "true" : "false";

        public LuaBoolean() : base(TemplateRestrictionType.BOOLEAN) { }
        public LuaBoolean(bool value) : base(TemplateRestrictionType.BOOLEAN)
        {
            Value = value;
        }
    }
}
