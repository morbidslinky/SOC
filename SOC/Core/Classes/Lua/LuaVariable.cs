using SOC.Classes.QuestBuild.Lua;
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
        public LuaVariable(string name, LuaValue assignedTo, bool isGlobal = false, bool assignmentStatmentOnFirstUse = true): base(TemplateRestrictionType.VARIABLE)
        {
            Name = name;
            AssignedTo = assignedTo;
        }

        public string GetVarName()
        {
        return Name; 
        }

        public string GetAssignmentLua()
        {
            StringBuilder luaBuilder = new StringBuilder();
            luaBuilder.AppendLine($"{Name} = {AssignedTo}\n");
            AppendTableValuesMarkedForExtrusion(luaBuilder);

            return luaBuilder.ToString();
        }

        private void AppendTableValuesMarkedForExtrusion(StringBuilder luaBuilder)
        {
            if (AssignedTo is LuaTable table)
                foreach (var path in table.GetTablePaths(true))
                {
                    var pathArray = path.ToArray();
                    if (table.TryGet(pathArray, out LuaValue tableNode, out bool isMarkedForExtrusion))
                        if (isMarkedForExtrusion)
                        {
                            luaBuilder.Append(Name);
                            foreach(var key in pathArray)
                            {
                                if (key is LuaText luaString)
                                    luaBuilder.Append(LuaTable.IsValidLuaIdentifier(luaString.Text) ? $".{luaString.Text}" : $"[{luaString.Value}]");
                                else
                                    luaBuilder.Append($"[{key}]");
                            }
                            luaBuilder.AppendLine($" = {tableNode}\n");
                        }
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
