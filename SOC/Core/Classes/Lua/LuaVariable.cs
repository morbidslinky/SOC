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
        [XmlAttribute] public string Value { get; set; }
        [XmlElement] public LuaValue AssignedTo { get; set; }
        public override string TokenValue => GetVarName();

        public LuaVariable() : base(TemplateRestrictionType.VARIABLE) { }
        public LuaVariable(string value, LuaValue assignedTo = null): base(TemplateRestrictionType.VARIABLE)
        {
            Value = value;
            AssignedTo = assignedTo == null ? new LuaNil() : assignedTo;
        }

        public string GetVarName()
        {
        return Value; 
        }

        public string GetAssignmentLua()
        {
            StringBuilder luaBuilder = new StringBuilder();
            luaBuilder.AppendLine($"{Value} = {AssignedTo}\n");
            AppendTableIdentifiersMarkedForExtrusion(luaBuilder);

            return luaBuilder.ToString();
        }

        private void AppendTableIdentifiersMarkedForExtrusion(StringBuilder luaBuilder)
        {
            if (AssignedTo is LuaTable table)
                foreach (var path in table.GetTablePaths(true))
                {
                    var varIdentifier = Lua.TableIdentifier(this, path.ToArray());
                    luaBuilder.Append(varIdentifier.GetAssignmentLua(this));
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
