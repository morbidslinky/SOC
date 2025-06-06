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
        [XmlAttribute] public string TableVariable {  get; set; }

        [XmlArray("TableKeyPath")]
        [XmlArrayItem("Key")] 
        public LuaValue[] IdentifierKeys { get; set; }

        public TemplateRestrictionType EvaluatesTo { get; set; }
        public override string TokenValue => GetIdentifier();

        public LuaTableIdentifier() : base(TemplateRestrictionType.TABLE_IDENTIFIER) { }
        public LuaTableIdentifier(string tableVariable, TemplateRestrictionType evaluatesToType = TemplateRestrictionType.NIL, params LuaValue[] identifierPath) : base(TemplateRestrictionType.TABLE_IDENTIFIER)
        {
            TableVariable = tableVariable;
            EvaluatesTo = evaluatesToType;
            IdentifierKeys = identifierPath;
        }

        public string GetIdentifier()
        {
            StringBuilder luaBuilder = new StringBuilder();
            luaBuilder.Append(TableVariable);
            foreach (var key in IdentifierKeys)
            {
                if (key is LuaString luaString)
                    luaBuilder.Append(LuaTable.IsValidLuaIdentifier(luaString.Value) ? $".{luaString.Value}" : $"[{luaString.TokenValue}]");
                else
                    luaBuilder.Append($"[{key}]");
            }
            return luaBuilder.ToString();
        }

        public string GetAssignmentLua(LuaVariable identifierVariable)
        {
            if (identifierVariable != null && identifierVariable.GetVarName() == TableVariable)
            {
                if (TryGetAssignedValue(identifierVariable, out LuaValue luaValue, out bool isMarkedForExtrusion))
                {
                    if (isMarkedForExtrusion)
                    {
                        StringBuilder luaBuilder = new StringBuilder();
                        luaBuilder.Append(identifierVariable.GetVarName());
                        foreach (var key in IdentifierKeys)
                        {
                            if (key is LuaString luaString)
                                luaBuilder.Append(LuaTable.IsValidLuaIdentifier(luaString.Value) ? $".{luaString.Value}" : $"[{luaString.TokenValue}]");
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
