using System.Linq;
using System.Xml.Serialization;

namespace SOC.Classes.Lua
{
    public class LuaMultiAssignment : LuaValue
    {
        [XmlArray("Variables")]
        [XmlArrayItem("Variable")]
        public LuaVariable[] Variables { get; set; }

        [XmlArray("Values")]
        [XmlArrayItem("Value")]
        public LuaValue[] Values { get; set; }

        [XmlElement]
        public bool DeclareLocally { get; set; }

        public override string Value => GetLua();

        public LuaMultiAssignment() : base(ValueType.Variable) { }

        public LuaMultiAssignment(LuaVariable[] variables, LuaValue[] values, bool declareLocally)
            : base(ValueType.Variable)
        {
            Variables = variables;
            Values = values;
            DeclareLocally = declareLocally;
        }

        public string GetLua()
        {
            string maybeLocal = DeclareLocally ? "local " : "";
            string lhs = string.Join(", ", Variables.Select(v => v.Name));
            string rhs = string.Join(", ", Values.Select(v => v.ToString()));
            return $"{maybeLocal}{lhs} = {rhs}";
        }
    }
}