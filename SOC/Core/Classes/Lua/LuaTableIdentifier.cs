using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace SOC.Classes.Lua
{
    public class LuaTableIdentifier : LuaValue
    {
        [XmlAttribute] public string IdentifierVariableName {  get; set; }

        [XmlArray("TableKeys")]
        [XmlArrayItem("Key")] 
        public LuaValue[] IdentifierKeys { get; set; }
        public override string Value => GetIdentifier();

        public LuaTableIdentifier() : base() { }
        public LuaTableIdentifier(string identifierVariableName, params LuaValue[] identifierPath) : base(TemplateRestrictionType.TABLE_IDENTIFIER)
        {
            IdentifierVariableName = identifierVariableName;
            IdentifierKeys = identifierPath;
        }

        public string GetIdentifier()
        {
            StringBuilder luaBuilder = new StringBuilder();
            luaBuilder.Append(IdentifierVariableName);
            foreach (var key in IdentifierKeys)
            {
                if (key is LuaText luaString)
                    luaBuilder.Append(LuaTable.IsValidLuaIdentifier(luaString.Text) ? $".{luaString.Text}" : $"[{luaString.Value}]");
                else
                    luaBuilder.Append($"[{key}]");
            }
            return luaBuilder.ToString();
        }

        public string GetAssignmentLua(LuaVariable identifierVariable)
        {
            if (identifierVariable != null && identifierVariable.GetVarName() == IdentifierVariableName)
            {
                if (TryGetAssignedValue(identifierVariable, out LuaValue luaValue, out bool isMarkedForExtrusion))
                {
                    if (isMarkedForExtrusion)
                    {
                        StringBuilder luaBuilder = new StringBuilder();
                        luaBuilder.Append(identifierVariable.GetVarName());
                        foreach (var key in IdentifierKeys)
                        {
                            if (key is LuaText luaString)
                                luaBuilder.Append(LuaTable.IsValidLuaIdentifier(luaString.Text) ? $".{luaString.Text}" : $"[{luaString.Value}]");
                            else
                                luaBuilder.Append($"[{key}]");
                        }
                        luaBuilder.AppendLine($" = {luaValue}\n");
                        return luaBuilder.ToString();
                    }
                }
            }
            return "";
        }

        public bool TryGetAssignedValue(LuaVariable identifierVariable, out LuaValue value, out bool extrude)
        {
            LuaValue rootValue = identifierVariable.GetAssignedValue();
            if (rootValue is LuaTable table)
            {
                return table.TryGet(IdentifierKeys, out value, out extrude);
            }

            value = new LuaNil(); extrude = false;
            return false;
        }
    }

}
