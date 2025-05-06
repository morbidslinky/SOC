using System.Xml.Serialization;

namespace SOC.Classes.Lua
{
    public class LuaString : LuaValue
    {
        [XmlAttribute] public string Text { get; set; }
        public override string Value => $"\"{Text}\"";

        public LuaString() : base(TemplateRestrictionType.STRING) { }
        public LuaString(string text) : base(TemplateRestrictionType.STRING)
        {
            Text = text;
        }
    }
}
