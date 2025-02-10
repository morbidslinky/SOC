using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SOC.Classes.Lua
{
    public class LuaVariable : LuaValue
    {
        [XmlElement] public string Name { get; set; }
        [XmlElement] public bool DeclareLocally { get; set; }
        [XmlElement] public LuaValue AssignedTo { get; set; }
        public override string Value => Name;

        public LuaVariable() : base(ValueType.Variable) { }
        public LuaVariable(string name, bool declareLocally): base(ValueType.Variable)
        {
            Name = name; 
            DeclareLocally = declareLocally; 
            AssignedTo = new LuaNil();
        }

        public void AssignTo(LuaValue luaValue)
        {
            AssignedTo = luaValue;
        }

        public string GetAssignmentLua()
        {
            if (IsLocalAndFirstTimeDeclared())
            {
                return $"local {Name} = {AssignedTo}";
            }
            return $"{Name} = {AssignedTo}";
        }

        public string GetCallLua(string[] args, out LuaValue[] functionReturns)
        {
            if (AssignedTo is LuaFunction function)
            {
                functionReturns = function.Returns;
                return $"{Name}({string.Join(", ", args)})";
            }
            functionReturns = new LuaValue[0];
            return $"-- VARIABLE CALL FUNCTION ERROR: {AssignedTo} IS NOT A FUNCTION";
        }

        public LuaValue GetAssignedValue()
        {
            return AssignedTo;
        }

        private bool IsLocalAndFirstTimeDeclared()
        {
            if (DeclareLocally)
            {
                DeclareLocally = false;
                return true;
            }
            return false;
        }
    }

}
