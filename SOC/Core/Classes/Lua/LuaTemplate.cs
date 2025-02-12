using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOC.Classes.Lua
{
    internal class LuaTemplate
    {
        public static string ParseTemplate(string template, params LuaValue[] populationData)
        {
            StringBuilder populatedTemplate = new StringBuilder();
            int start = 0;

            while (start < template.Length)
            {
                int open = template.IndexOf("<<", start);
                if (open == -1)
                {
                    populatedTemplate.Append(template.Substring(start));
                    break;
                }

                int close = template.IndexOf(">>", open);
                if (close == -1) break;
                populatedTemplate.Append(template.Substring(start, open - start));
                string placeholderToken = template.Substring(open + 2, close - open - 2).Trim();

                populatedTemplate.Append(LuaTemplatePlaceholder.Parse(placeholderToken, populationData));

                start = close + 2;
            }
            return populatedTemplate.ToString();
        }
    }

    internal class LuaTemplatePlaceholder
    {
        int Index;
        LuaValue.ValueType[] AllowedTypes;

        public static string Parse(string placeholderToken, LuaValue[] luaValues)
        {
            LuaTemplatePlaceholder placeholder = new LuaTemplatePlaceholder();
            if (!placeholder.TryParsePlaceholder(placeholderToken))
                return $"--[[Invalid Placeholder ({placeholderToken})]]";
            if (!placeholder.TryGetValidKeyValue(luaValues, out LuaValue luaValue))
                return $"--[[Valid Placeholder ({placeholderToken}), Invalid Data ({luaValue},{(luaValue is LuaVariable v ? v.GetAssignedValue() : luaValue).Type.ToString().ToLower()})]]";
            return luaValue.Value;
        }

        internal bool TryParsePlaceholder(string placeholderToken)
        {
            string[] commaSeparatedSplit = placeholderToken.Split(',');
            if (commaSeparatedSplit.Length != 2)
                return false;
            if (!int.TryParse(commaSeparatedSplit[0].Trim(), out int parseInt))
                return false;

            Index = parseInt;

            string[] typeRestrictions = commaSeparatedSplit[1].Split('|');
            AllowedTypes = new LuaValue.ValueType[typeRestrictions.Length];

            for (int i = 0; i < typeRestrictions.Length; i++)
            {
                switch (typeRestrictions[i].Trim().ToLower())
                {
                    case "bool":
                    case "boolean":
                        AllowedTypes[i] = LuaValue.ValueType.Boolean;
                        break;
                    case "text":
                    case "string":
                        AllowedTypes[i] = LuaValue.ValueType.Text;
                        break;
                    case "num":
                    case "number":
                        AllowedTypes[i] = LuaValue.ValueType.Number;
                        break;
                    case "func":
                    case "function":
                        AllowedTypes[i] = LuaValue.ValueType.Function;
                        break;
                    case "table":
                        AllowedTypes[i] = LuaValue.ValueType.Table;
                        break;
                    case "var":
                    case "variable":
                        AllowedTypes[i] = LuaValue.ValueType.Variable;
                        break;
                    case "any":
                    case "value":
                        AllowedTypes[i] = LuaValue.ValueType.ANY;
                        break;
                    case "nil":
                        AllowedTypes[i] = LuaValue.ValueType.NIL;
                        break;
                    default:
                        return false;
                }
            }

            return true;
        }

        internal bool TryGetValidKeyValue(LuaValue[] luaValues, out LuaValue luaKeyValue)
        {
            if (Index >= luaValues.Length || luaValues[Index] == null)
            {
                luaKeyValue = new LuaNil();
                return false;
            }

            LuaValue luaValueAtIndex = luaValues[Index];
            if (luaValueAtIndex is LuaVariable variable)
            {
                luaValueAtIndex = variable.GetAssignedValue();
            }

            bool isValid = AllowedTypes.Contains(luaValueAtIndex.Type) || AllowedTypes.Contains(LuaValue.ValueType.ANY);
            luaKeyValue = luaValues[Index];
            return isValid;
        }
    }
}
