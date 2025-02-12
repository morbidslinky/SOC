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
        [XmlArrayItem("Parameter")]
        public string[] Parameters { get; set; }

        [XmlArray("PopulationValues")]
        [XmlArrayItem("Value")]
        public LuaValue[] PopulationValues { get; set; }
        public LuaTemplate Template { get; set; }
        public override string Value => GetFormattedLuaFunction();

        public LuaFunction() : base(ValueType.Function) { }

        public LuaFunction(LuaTemplate template, LuaValue[] populationValues, params string[] parameters) : base(ValueType.Function)
        {
            Template = template;
            PopulationValues = populationValues;
            Parameters = parameters;
        }

        public LuaFunction(LuaTemplate template, params string[] parameters) : base(ValueType.Function)
        {
            Template = template;
            PopulationValues = new LuaValue[0];
            Parameters = parameters;
        }

        public LuaFunction(LuaTemplate template, params LuaValue[] populationValues) : base(ValueType.Function)
        {
            Template = template;
            PopulationValues = populationValues;
            Parameters = new string[0];
        }

        private string GetFormattedLuaFunction()
        {
            return $@"function({string.Join(", ", Parameters)})
{Template.Populate(PopulationValues)}
end";
        }
    }
}