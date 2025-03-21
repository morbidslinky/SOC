﻿
using System.IO;
using System.Xml.Serialization;

namespace SOC.Classes.Lua
{

    [XmlInclude(typeof(LuaNil))]
    [XmlInclude(typeof(LuaText))]
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
        [XmlIgnore] public abstract string Value { get; }

        protected LuaValue() { }
        protected LuaValue(TemplateRestrictionType type) { Type = type; }

        public override string ToString() => Value;

        public enum TemplateRestrictionType
        {
            NIL,
            TEXT,
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