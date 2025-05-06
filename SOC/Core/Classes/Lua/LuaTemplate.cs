using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace SOC.Classes.Lua
{
    /// <summary>
    /// Embed |[1-based index|RESTRICTION]| into the template string to set placeholders that will be populated when the lua is written to a file from a LuaFunction, assuming the function's populationValues are aligned with the template placeholders.
    /// Example: "This is a string: |[1|STRING]|, and this is a variable: |[2|VARIABLE]|, and this is an error: |[foo|bar]|."
    /// Accepted placeholder restrictions: BOOLEAN, STRING, NUMBER, FUNCTION, FUNCTION_CALL, TABLE, TABLE_IDENTIFIER, VARIABLE, ASSIGN_VARIABLE, NIL 
    /// </summary>
    public class LuaTemplate
    {
        [XmlElement]
        public string Template {  get; set; }
        public LuaTemplate() { }
        
        public LuaTemplate(string templateString)
        {
            Template = templateString;
        }

        public static string GetTemplateRestrictionTypeString(LuaValue v, bool ifVariableIsAssign = false)
        {
            string restrictionType = "";
            switch (v)
            {
                case LuaBoolean boolean:
                    restrictionType = "BOOLEAN";
                    break;
                case LuaFunction func:
                    restrictionType = "FUNCTION";
                    break;
                case LuaFunctionCall call:
                    restrictionType = "FUNCTION_CALL";
                    break;
                case LuaNil nil:
                    restrictionType = "NIL";
                    break;
                case LuaNumber num:
                    restrictionType = "NUMBER";
                    break;
                case LuaTable table:
                    restrictionType = "TABLE";
                    break;
                case LuaTableIdentifier identifier:
                    restrictionType = "TABLE_IDENTIFIER";
                    break;
                case LuaString text:
                    restrictionType = "STRING";
                    break;
                case LuaVariable var:
                    restrictionType = (ifVariableIsAssign ? "ASSIGN_VARIABLE" : "VARIABLE");
                    break;
                default:
                    restrictionType = "unknown value type";
                    break;
            }
            return restrictionType;
        }

        internal static bool TryTokenize(string templateString, out List<LuaTemplateToken> tokenList)
        {
            bool isValid = true;
            tokenList = new List<LuaTemplateToken>();
            int start = 0;

            while (start < templateString.Length)
            {
                int open = templateString.IndexOf("|[", start);
                if (open == -1)
                {
                    tokenList.Add(new LuaTemplatePlainText(templateString.Substring(start)));
                    break;
                }

                int close = templateString.IndexOf("]|", open);
                if (close == -1) break;

                tokenList.Add(new LuaTemplatePlainText(templateString.Substring(start, open - start)));

                string placeholderToken = templateString.Substring(open + 2, close - open - 2).Trim();
                isValid = LuaTemplatePlaceholder.TryParse(placeholderToken, out LuaTemplatePlaceholder placeholder) && isValid;
                tokenList.Add(placeholder);

                start = close + 2;
            }

            return isValid;
        }

        public static bool TryGetPlaceholderTokens(string templateString, out List<LuaTemplatePlaceholder> placeholderTokens)
        {
            if (TryTokenize(templateString, out List<LuaTemplateToken> allTokens))
            {
                placeholderTokens = allTokens.OfType<LuaTemplatePlaceholder>().ToList();
                return true;
            }

            placeholderTokens = new List<LuaTemplatePlaceholder>();
            return false;
        }

        public string Populate(params LuaValue[] populationData)
        {
            StringBuilder populatedTemplate = new StringBuilder();
            TryTokenize(Template, out List<LuaTemplateToken> tokens);

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
        public LuaValue.TemplateRestrictionType AllowedType { get; set; }

        public LuaTemplatePlaceholder(string placeholderString) {
            Index = -1; AllowedType = LuaValue.TemplateRestrictionType.NIL; PlaceholderString = placeholderString;
        }
        
        public LuaTemplatePlaceholder(string placeholder, int index, LuaValue.TemplateRestrictionType allowedType)
        {
            Index = index; AllowedType = allowedType; PlaceholderString = placeholder;
        }

        public static bool TryParse(string placeholderToken, out LuaTemplatePlaceholder placeholder)
        {
            placeholder = new LuaTemplatePlaceholder(placeholderToken);

            string[] splitPlaceholderTokens = placeholderToken.Split('|');
            if (splitPlaceholderTokens.Length != 2)
                return false;
            if (!int.TryParse(splitPlaceholderTokens[0].Trim(), out int parseInt))
                return false;

            placeholder.Index = parseInt;

            string typeRestriction = splitPlaceholderTokens[1];

            switch (typeRestriction.Trim().ToUpper())
            {
                case "BOOLEAN":
                    placeholder.AllowedType = LuaValue.TemplateRestrictionType.BOOLEAN;
                    break;
                case "STRING":
                    placeholder.AllowedType  = LuaValue.TemplateRestrictionType.STRING;
                    break;
                case "NUMBER":
                    placeholder.AllowedType = LuaValue.TemplateRestrictionType.NUMBER;
                    break;
                case "FUNCTION":
                    placeholder.AllowedType = LuaValue.TemplateRestrictionType.FUNCTION;
                    break;
                case "FUNCTION_CALL":
                    placeholder.AllowedType = LuaValue.TemplateRestrictionType.FUNCTION_CALL;
                    break;
                case "TABLE":
                    placeholder.AllowedType = LuaValue.TemplateRestrictionType.TABLE;
                    break;
                case "TABLE_IDENTIFIER":
                    placeholder.AllowedType = LuaValue.TemplateRestrictionType.TABLE_IDENTIFIER;
                    break;
                case "VARIABLE":
                    placeholder.AllowedType = LuaValue.TemplateRestrictionType.VARIABLE;
                    break;
                case "ASSIGN_VARIABLE":
                    placeholder.AllowedType = LuaValue.TemplateRestrictionType.ASSIGN_VARIABLE;
                    break;
                case "NIL":
                    placeholder.AllowedType = LuaValue.TemplateRestrictionType.NIL;
                    break;
                default:
                    placeholder.AllowedType = LuaValue.TemplateRestrictionType.TEMPLATE_ERROR;
                    return false;
            }

            return true;
        }

        public string Populate(LuaValue[] luaValues)
        {
            if (Index == -1 || AllowedType == LuaValue.TemplateRestrictionType.TEMPLATE_ERROR)
                return $"--[[ERROR: Invalid placeholder ({PlaceholderString})]]";
            if (!TryGetValueString(luaValues, out string luaString))
                return $"--[[ERROR: Valid placeholder ({PlaceholderString}), but invalid data ({luaString})]]";
            return luaString;
        }

        internal bool TryGetValueString(LuaValue[] luaValues, out string valueString)
        {
            if (Index - 1 >= luaValues.Length || luaValues[Index - 1] == null)
            {
                valueString = $"Missing population data at 1-based index '{Index - 1}'";
                return false;
            }
            LuaValue luaValueAtIndex = luaValues[Index - 1];

            if (luaValueAtIndex is LuaVariable variable)
            {
                if (AllowedType == LuaValue.TemplateRestrictionType.VARIABLE || variable.GetAssignedValue().Type == AllowedType)
                {
                    valueString = variable.ToString();
                    return true;
                } 
                else if (AllowedType == LuaValue.TemplateRestrictionType.ASSIGN_VARIABLE)
                {
                    valueString = variable.GetAssignmentLua();
                    return true;
                }
            }
            else if (luaValueAtIndex.Type == AllowedType)
            {
                valueString = luaValueAtIndex.ToString();
                return true;
            }

            valueString = luaValueAtIndex.ToString();
            return false;
        }
    }
}
