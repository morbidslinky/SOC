using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;

namespace SOC.Classes.Lua
{
    public class LuaFunction : LuaValue
    {

        [XmlArray("Parameters")]
        [XmlArrayItem("Parameters")]
        public string[] Parameters { get; set; }
        [XmlElement] public string Body { get; set; }
        public override string Value => GetFormattedLuaFunction();

        public LuaFunction() : base(ValueType.Function) { }

        public LuaFunction(string body, params string[] parameters) : base(ValueType.Function)
        {
            Body = body;
            Parameters = parameters;
        }

        private string GetFormattedLuaFunction()
        {
            return $@"function({string.Join(", ", Parameters)})
{Body}
end";
        }
    }
}