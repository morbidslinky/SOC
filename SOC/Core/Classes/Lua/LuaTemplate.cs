using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace SOC.Classes.Lua
{
    public class LuaTemplate
    {
        [XmlArray("Tokens")]
        [XmlArrayItem("Token")]
        public LuaTemplateToken[] Tokens { get; set; }
        public LuaTemplate() { }
        public LuaTemplate(LuaTemplateToken[] tokens) {
            Tokens = tokens;
        }

        public static bool TryParse(string templateString, out LuaTemplate luaTemplate)
        {
            luaTemplate = new LuaTemplate();

            List<LuaTemplateToken> tokenList = new List<LuaTemplateToken>();
            int start = 0;

            while (start < templateString.Length)
            {
                int open = templateString.IndexOf("<<", start);
                if (open == -1)
                {
                    tokenList.Add(new LuaTemplatePlainText(templateString.Substring(start)));
                    break;
                }

                int close = templateString.IndexOf(">>", open);
                if (close == -1) break;

                tokenList.Add(new LuaTemplatePlainText(templateString.Substring(start, open - start)));

                string placeholderToken = templateString.Substring(open + 2, close - open - 2).Trim();

                if (LuaTemplatePlaceholder.TryParse(placeholderToken, out LuaTemplatePlaceholder placeholder))
                    tokenList.Add(placeholder);
                else
                    return false;

                start = close + 2;
            }

            luaTemplate.Tokens = tokenList.ToArray();
            return true;
        }

        public string Populate(params LuaValue[] populationData)
        {
            StringBuilder populatedTemplate = new StringBuilder();
            foreach (LuaTemplateToken token in Tokens)
            {
                if (token is LuaTemplatePlainText textToken)
                    populatedTemplate.Append(textToken.PlainText);
                else if (token is LuaTemplatePlaceholder placeholder)
                    populatedTemplate.Append(placeholder.Populate(populationData));
            }
            return populatedTemplate.ToString();
        }
    }

    [XmlInclude(typeof(LuaTemplatePlainText))]
    [XmlInclude(typeof(LuaTemplatePlaceholder))]
    public abstract class LuaTemplateToken { }

    public class LuaTemplatePlainText : LuaTemplateToken 
    {
        [XmlAttribute] public string PlainText { get; set; }

        public LuaTemplatePlainText() { }

        public LuaTemplatePlainText(string plainText)
        {
            PlainText = plainText;
        }
    }

    public class LuaTemplatePlaceholder : LuaTemplateToken
    {
        [XmlAttribute] public int Index { get; set; }

        [XmlArray("AllowedTypes")]
        [XmlArrayItem("AllowedType")]
        public LuaValue.ValueType[] AllowedTypes { get; set; }

        public LuaTemplatePlaceholder() { }
        
        public LuaTemplatePlaceholder(int index, LuaValue.ValueType[] allowedTypes)
        {
            Index = index; AllowedTypes = allowedTypes;
        }

        public static bool TryParse(string placeholderToken, out LuaTemplatePlaceholder placeholder)
        {
            placeholder = new LuaTemplatePlaceholder();

            string[] commaSeparatedSplit = placeholderToken.Split(',');
            if (commaSeparatedSplit.Length != 2)
                return false;
            if (!int.TryParse(commaSeparatedSplit[0].Trim(), out int parseInt))
                return false;

            placeholder.Index = parseInt;

            string[] typeRestrictions = commaSeparatedSplit[1].Split('|');
            placeholder.AllowedTypes = new LuaValue.ValueType[typeRestrictions.Length];

            for (int i = 0; i < typeRestrictions.Length; i++)
            {
                switch (typeRestrictions[i].Trim().ToLower())
                {
                    case "bool":
                    case "boolean":
                        placeholder.AllowedTypes[i] = LuaValue.ValueType.Boolean;
                        break;
                    case "text":
                    case "string":
                        placeholder.AllowedTypes[i] = LuaValue.ValueType.Text;
                        break;
                    case "num":
                    case "number":
                        placeholder.AllowedTypes[i] = LuaValue.ValueType.Number;
                        break;
                    case "func":
                    case "function":
                        placeholder.AllowedTypes[i] = LuaValue.ValueType.Function;
                        break;
                    case "table":
                        placeholder.AllowedTypes[i] = LuaValue.ValueType.Table;
                        break;
                    case "var":
                    case "variable":
                        placeholder.AllowedTypes[i] = LuaValue.ValueType.Variable;
                        break;
                    case "any":
                    case "value":
                        placeholder.AllowedTypes[i] = LuaValue.ValueType.ANY;
                        break;
                    case "nil":
                        placeholder.AllowedTypes[i] = LuaValue.ValueType.NIL;
                        break;
                    default:
                        return false;
                }
            }

            return true;
        }

        public string Populate(LuaValue[] luaValues)
        {
            if (Index == -1)
                return $"--[[ERROR: Invalid Placeholder]]";
            if (!TryGetValidKeyValue(luaValues, out LuaValue luaValue))
                return $"--[[ERROR: Valid Placeholder ({Index},{string.Join(", ", AllowedTypes.Select(t => t.ToString()))}), Invalid Data ({luaValue},{(luaValue is LuaVariable v ? v.GetAssignedValue() : luaValue).Type.ToString().ToLower()})]]";
            return luaValue.Value;
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
