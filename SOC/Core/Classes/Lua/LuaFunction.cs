using System;
using System.Xml.Serialization;

namespace SOC.Classes.Lua
{
    public class LuaFunction : LuaValue
    {
        [XmlArray("Parameters")]
        [XmlArrayItem("Parameter")]
        public string[] Parameters { get; set; }
        [XmlArray("Returns")]
        [XmlArrayItem("Return")]
        public LuaValue[] Returns { get; set; }
        [XmlElement] public string FunctionBody { get; set; }
        public override string Value => $@"function({string.Join(", ", Parameters)}) {FunctionBody} end";

        public LuaFunction() : base(ValueType.Function)
        {
            Returns = Array.Empty<LuaValue>();
        }

        public LuaFunction(string[] parameters, string functionBody, params LuaValue[] returns) : base(ValueType.Function)
        {
            Parameters = parameters ?? Array.Empty<string>();
            FunctionBody = functionBody;
            Returns = returns;
        }

        public string GetAnonymousCallLua(string[] args, out LuaValue[] functionReturns)
        {
            functionReturns = Returns;
            return $@"(function({string.Join(", ", Parameters)}) {FunctionBody} end)({args})";
        }
    }
}
