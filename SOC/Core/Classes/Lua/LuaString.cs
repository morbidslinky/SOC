using System.Xml.Serialization;

namespace SOC.Classes.Lua
{
    public class LuaString : LuaValue
    {
        [XmlAttribute] public string Value { get; set; }
        public override string TokenValue => $"\"{Value}\"";

        public LuaString() : base(TemplateRestrictionType.STRING) { }
        public LuaString(string text) : base(TemplateRestrictionType.STRING)
        {
            Value = text;
        }
    }
}
