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
        [XmlAttribute] public bool Global { get; set; }

        // First time variable is called will return the initial assignment string, and calls after that will return the name. Painful design flaw.
        [XmlAttribute] public bool AssignmentStatementOnFirstUse { get; set; }
        [XmlElement] public LuaValue AssignedTo { get; set; }
        public override string Value => GetVariable();

        public LuaVariable() : base(ValueType.Variable) { }
        public LuaVariable(string name, LuaValue assignedTo, bool isGlobal = false, bool assignmentStatmentOnFirstUse = true): base(ValueType.Variable)
        {
            Name = name;
            AssignedTo = assignedTo;
            Global = isGlobal;
            AssignmentStatementOnFirstUse = assignmentStatmentOnFirstUse;
        }

        public string GetVariable()
        {
            if (AssignmentStatementOnFirstUse)
            {
                AssignmentStatementOnFirstUse = false;
                StringBuilder luaBuilder = new StringBuilder();
                luaBuilder.AppendLine($"{(Global ? "" : "local ")}{Name} = {AssignedTo}\n");
                AppendTableValuesMarkedForExtrusion(luaBuilder);

                return luaBuilder.ToString();
            }
            return Name;
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
