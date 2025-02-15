using System.Xml.Serialization;

namespace SOC.Classes.Lua
{
    public class LuaText : LuaValue
    {
        [XmlAttribute] public string Text { get; set; }
        public override string Value => $"\"{Text}\"";

        public LuaText() : base(TemplateRestrictionType.TEXT) { }
        public LuaText(string text) : base(TemplateRestrictionType.TEXT)
        {
            Text = text;
        }
    }
}
