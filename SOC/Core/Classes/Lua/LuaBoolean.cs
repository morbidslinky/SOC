using System.Xml.Serialization;

namespace SOC.Classes.Lua
{
    public class LuaBoolean : LuaValue
    {
        [XmlElement] public bool BooleanValue { get; set; }
        public override string Value => BooleanValue ? "true" : "false";

        public LuaBoolean() : base(ValueType.Boolean) { }
        public LuaBoolean(string value) : base(ValueType.Boolean)
        {
            BooleanValue = value == "1" || (bool.TryParse(value, out bool result) && result);
        }
    }
}
