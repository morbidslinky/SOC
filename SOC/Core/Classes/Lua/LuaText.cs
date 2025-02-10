using System.Xml.Serialization;

namespace SOC.Classes.Lua
{
    public class LuaText : LuaValue
    {
        [XmlElement] public string Text { get; set; }
        public override string Value => $"\"{Text}\"";

        public LuaText() : base(ValueType.Text) { }
        public LuaText(string text) : base(ValueType.Text)
        {
            Text = text;
        }
    }
}
