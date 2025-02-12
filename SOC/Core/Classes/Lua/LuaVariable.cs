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
        [XmlIgnore] internal bool OneTimeLocalDeclaration;
        [XmlElement] public LuaValue AssignedTo { get; set; }
        public override string Value => Name;

        public LuaVariable() : base(ValueType.Variable) { }
        public LuaVariable(string name, bool declareLocally): base(ValueType.Variable)
        {
            Name = name; 
            DeclareLocally = declareLocally;
            OneTimeLocalDeclaration = true;
            AssignedTo = new LuaNil();
        }

        public void AssignTo(LuaValue luaValue)
        {
            while (luaValue is LuaVariable variable)
            {
                luaValue = variable.GetAssignedValue();
            }
            AssignedTo = luaValue;
        }

        public void GetAssignmentLua(StringBuilder luaBuilder)
        {
            if (IsLocalAndFirstTimeDeclared())
            {
                luaBuilder.AppendLine($"local {Name} = {AssignedTo}");
                return;
            } 
            luaBuilder.AppendLine($"{Name} = {AssignedTo}");
        }

        public LuaValue GetAssignedValue()
        {
            return AssignedTo;
        }

        public void GetReturnLua(StringBuilder luaBuilder)
        {
            luaBuilder.AppendLine($"return {Name}");
        }

        private bool IsLocalAndFirstTimeDeclared()
        {
            if (DeclareLocally && OneTimeLocalDeclaration)
            {
                OneTimeLocalDeclaration = false;
                return true;
            }
            return false;
        }
    }

}
