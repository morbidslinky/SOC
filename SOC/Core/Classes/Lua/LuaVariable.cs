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
        [XmlAttribute] public string Name { get; set; }
        [XmlElement] public LuaValue AssignedTo { get; set; }
        public override string Value => GetVarName();

        public LuaVariable() : base(TemplateRestrictionType.VARIABLE) { }
        public LuaVariable(string name, LuaValue assignedTo = null): base(TemplateRestrictionType.VARIABLE)
        {
            Name = name;
            AssignedTo = assignedTo == null ? new LuaNil() : assignedTo;
        }

        public string GetVarName()
        {
        return Name; 
        }

        public string GetAssignmentLua()
        {
            StringBuilder luaBuilder = new StringBuilder();
            luaBuilder.AppendLine($"{Name} = {AssignedTo}\n");
            AppendTableIdentifiersMarkedForExtrusion(luaBuilder);

            return luaBuilder.ToString();
        }

        private void AppendTableIdentifiersMarkedForExtrusion(StringBuilder luaBuilder)
        {
            if (AssignedTo is LuaTable table)
                foreach (var path in table.GetTablePaths(true))
                {
                    var varIdentifier = new LuaTableIdentifier(this, path.ToArray());
                    luaBuilder.AppendLine(varIdentifier.GetAssignmentLua(this));
                }
        }

        public LuaValue GetAssignedValue()
        {
            LuaValue rootValue = AssignedTo;
            while (rootValue is LuaVariable variable)
            {
                rootValue = variable.AssignedTo;
            }
            return rootValue;
        }
    }

}
