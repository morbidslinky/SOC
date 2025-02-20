using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;

namespace SOC.Classes.Lua
{
    public class LuaFunctionCall : LuaValue
    {
        [XmlAttribute] public string FunctionVariableName { get; set; }

        [XmlArray("FunctionArguments")]
        [XmlArrayItem("Argument")]
        public LuaValue[] Arguments { get; set; }
        public override string Value => GetFunctionCall();

        public LuaFunctionCall() : base() { }
        public LuaFunctionCall(string functionVariableName, params LuaValue[] args) : base(TemplateRestrictionType.CALL_FUNCTION)
        {
            FunctionVariableName = functionVariableName;
            Arguments = args;
        }

        public string GetFunctionCall()
        {
            return $"{FunctionVariableName}({string.Join(", ", Arguments.Select(arg => arg.ToString()))})";
        }
    }
}