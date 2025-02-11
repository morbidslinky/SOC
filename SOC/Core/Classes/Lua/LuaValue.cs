
using System.IO;
using System.Xml.Serialization;

namespace SOC.Classes.Lua
{

    [XmlInclude(typeof(LuaNil))]
    [XmlInclude(typeof(LuaText))]
    [XmlInclude(typeof(LuaBoolean))]
    [XmlInclude(typeof(LuaNumber))]
    [XmlInclude(typeof(LuaTable))]
    [XmlInclude(typeof(LuaVariable))]
    [XmlInclude(typeof(LuaMultiAssignment))]
    [XmlInclude(typeof(LuaFunction))]
    public abstract class LuaValue
    {
        [XmlAttribute] public ValueType Type { get; set; }
        [XmlIgnore] public abstract string Value { get; }

        protected LuaValue() { }
        protected LuaValue(ValueType type) { Type = type; }

        public override string ToString() => Value;

        public enum ValueType
        {
            NIL,
            Text,
            Number,
            Boolean,
            Table,
            Function,
            Variable,
            ANY
        }
    }
}