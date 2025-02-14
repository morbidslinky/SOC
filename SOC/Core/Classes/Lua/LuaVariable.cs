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
        [XmlAttribute] public bool DeclareLocally { get; set; }
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

        public string AssignTo(LuaValue luaValue)
        {
            StringBuilder luaBuilder = new StringBuilder();
            AssignedTo = luaValue;

            luaBuilder.AppendLine($"{(IsLocalAndFirstTimeDeclared() ? "local " : "")}{Name} = {AssignedTo}\n");
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
