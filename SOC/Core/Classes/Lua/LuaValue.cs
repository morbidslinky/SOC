
using System.IO;
using System.Xml.Serialization;

namespace SOC.Classes.Lua
{

    [XmlInclude(typeof(LuaNil))]
    [XmlInclude(typeof(LuaString))]
    [XmlInclude(typeof(LuaBoolean))]
    [XmlInclude(typeof(LuaNumber))]
    [XmlInclude(typeof(LuaTable))]
    [XmlInclude(typeof(LuaTableIdentifier))]
    [XmlInclude(typeof(LuaVariable))]
    [XmlInclude(typeof(LuaFunction))]
    [XmlInclude(typeof(LuaFunctionCall))]
    public abstract class LuaValue
    {
        [XmlIgnore] public TemplateRestrictionType Type { get; set; }
        [XmlIgnore] public abstract string TokenValue { get; }

        protected LuaValue() { }
        protected LuaValue(TemplateRestrictionType type) { Type = type; }

        public override string ToString() => TokenValue;
        public bool Matches(object v) => (v != null && v is LuaValue luaV && luaV.Type == Type && luaV.TokenValue == TokenValue);

        public enum TemplateRestrictionType
        {
            NIL,
            STRING,
            NUMBER,
            BOOLEAN,
            TABLE,
            TABLE_IDENTIFIER,
            FUNCTION,
            FUNCTION_CALL,
            VARIABLE,
            ASSIGN_VARIABLE,
            TEMPLATE_ERROR
        }
    }
}