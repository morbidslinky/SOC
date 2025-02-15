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
        [XmlElement]public string Template {  get; set; }
        public LuaTemplate() { }
        public LuaTemplate(string templateString)
        {
            Template = templateString;
        }

        public static bool TryParse(string templateString, out LuaTemplate luaTemplate)
        {
            luaTemplate = new LuaTemplate();
            bool isValid = TryTokenize(templateString, out _);
            luaTemplate.Template = templateString;
            return isValid;
        }

        internal static bool TryTokenize(string templateString, out LuaTemplateToken[] tokens)
        {
            tokens = new LuaTemplateToken[0];
            bool isValid = true;
            List<LuaTemplateToken> tokenBuilder = new List<LuaTemplateToken>();
            int start = 0;

            while (start < templateString.Length)
            {
                int open = templateString.IndexOf("<<", start);
                if (open == -1)
                {
                    tokenBuilder.Add(new LuaTemplatePlainText(templateString.Substring(start)));
                    break;
                }

                int close = templateString.IndexOf(">>", open);
                if (close == -1) break;

                tokenBuilder.Add(new LuaTemplatePlainText(templateString.Substring(start, open - start)));

                string placeholderToken = templateString.Substring(open + 2, close - open - 2).Trim();
                isValid = LuaTemplatePlaceholder.TryParse(placeholderToken, out LuaTemplatePlaceholder placeholder) && isValid;
                tokenBuilder.Add(placeholder);

                start = close + 2;
            }

            tokens = tokenBuilder.ToArray();
            return isValid;
        }

        public string Populate(params LuaValue[] populationData)
        {
            StringBuilder populatedTemplate = new StringBuilder();
            TryTokenize(Template, out LuaTemplateToken[] tokens);

            foreach (LuaTemplateToken token in tokens)
            {
                if (token is LuaTemplatePlainText textToken)
                    populatedTemplate.Append(textToken.PlainText);
                else if (token is LuaTemplatePlaceholder placeholder)
                    populatedTemplate.Append(placeholder.Populate(populationData));
            }
            return populatedTemplate.ToString();
        }
    }

    public abstract class LuaTemplateToken { }

    public class LuaTemplatePlainText : LuaTemplateToken 
    {
        public string PlainText { get; set; }

        public LuaTemplatePlainText() { }

        public LuaTemplatePlainText(string plainText)
        {
            PlainText = plainText;
        }
    }

    public class LuaTemplatePlaceholder : LuaTemplateToken
    {
        public string PlaceholderString { get; set; }
        public int Index { get; set; }
        public LuaValue.ValueType[] AllowedTypes { get; set; }

        public LuaTemplatePlaceholder(string placeholderString) {
            Index = -1; AllowedTypes = new LuaValue.ValueType[0]; PlaceholderString = placeholderString;
        }
        
        public LuaTemplatePlaceholder(string placeholder, int index, LuaValue.ValueType[] allowedTypes)
        {
            Index = index; AllowedTypes = allowedTypes; PlaceholderString = placeholder;
        }

        public static bool TryParse(string placeholderToken, out LuaTemplatePlaceholder placeholder)
        {
            placeholder = new LuaTemplatePlaceholder(placeholderToken);

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
                        placeholder.AllowedTypes = new LuaValue.ValueType[0];
                        return false;
                }
            }

            return true;
        }

        public string Populate(LuaValue[] luaValues)
        {
            if (Index == -1 || AllowedTypes.Length == 0)
                return $"--[[ERROR: Invalid Placeholder ({PlaceholderString})]]";
            if (!TryGetValidKeyValue(luaValues, out LuaValue luaValue))
                return $"--[[ERROR: Valid Placeholder ({PlaceholderString}), Invalid Data ({(luaValue is LuaVariable v ? $"{v.Name}, {v.GetAssignedValue()}" : luaValue.Value).ToLower()})]]";
            return luaValue.ToString();
        }

        internal bool TryGetValidKeyValue(LuaValue[] luaValues, out LuaValue luaKeyValue)
        {
            if (Index >= luaValues.Length || luaValues[Index] == null)
            {
                luaKeyValue = new LuaNil();
                return false;
            }

            LuaValue luaValueAtIndex = luaValues[Index];
            if (luaValueAtIndex is LuaVariable variable && !AllowedTypes.Contains(LuaValue.ValueType.Variable))
            {
                luaValueAtIndex = variable.GetAssignedValue();
            }

            bool isValid = AllowedTypes.Contains(luaValueAtIndex.Type) || AllowedTypes.Contains(LuaValue.ValueType.ANY);
            luaKeyValue = luaValues[Index];
            return isValid;
        }
    }
}
