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
    /// Embed |[index,restriction]| into the template string to set placeholders that will be populated when the lua is written to a file from a LuaFunction, assuming the function's populationValues are aligned with the template placeholders.
    /// Example: "This is a string: |[0,string]|, and this is a variable: |[1,variable]|, and this is an error: |[foo,bar]|."
    /// Accepted placeholder restrictions: boolean, string, number, function, function_call, table, table_identifier, variable, assign_variable, nil 
    /// </summary>
    public class LuaTemplate
    {
        [XmlElement]public string Template {  get; set; }
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
                    restrictionType = "boolean";
                    break;
                case LuaFunction func:
                    restrictionType = "function";
                    break;
                case LuaFunctionCall call:
                    restrictionType = "function_call";
                    break;
                case LuaNil nil:
                    restrictionType = "nil";
                    break;
                case LuaNumber num:
                    restrictionType = "number";
                    break;
                case LuaTable table:
                    restrictionType = "table";
                    break;
                case LuaTableIdentifier identifier:
                    restrictionType = "table_identifier";
                    break;
                case LuaText text:
                    restrictionType = "string";
                    break;
                case LuaVariable var:
                    restrictionType = (ifVariableIsAssign ? "assign_variable" : "variable");
                    break;
                default:
                    restrictionType = "unknown value type";
                    break;
            }
            return restrictionType;
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
                int open = templateString.IndexOf("|[", start);
                if (open == -1)
                {
                    tokenBuilder.Add(new LuaTemplatePlainText(templateString.Substring(start)));
                    break;
                }

                int close = templateString.IndexOf("]|", open);
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

            switch (typeRestriction.Trim().ToLower())
            {
                case "boolean":
                    placeholder.AllowedType = LuaValue.TemplateRestrictionType.BOOLEAN;
                    break;
                case "string":
                    placeholder.AllowedType  = LuaValue.TemplateRestrictionType.TEXT;
                    break;
                case "number":
                    placeholder.AllowedType = LuaValue.TemplateRestrictionType.NUMBER;
                    break;
                case "function":
                    placeholder.AllowedType = LuaValue.TemplateRestrictionType.FUNCTION;
                    break;
                case "function_call":
                    placeholder.AllowedType = LuaValue.TemplateRestrictionType.FUNCTION_CALL;
                    break;
                case "table":
                    placeholder.AllowedType = LuaValue.TemplateRestrictionType.TABLE;
                    break;
                case "table_identifier":
                    placeholder.AllowedType = LuaValue.TemplateRestrictionType.TABLE_IDENTIFIER;
                    break;
                case "variable":
                    placeholder.AllowedType = LuaValue.TemplateRestrictionType.VARIABLE;
                    break;
                case "assign_variable":
                    placeholder.AllowedType = LuaValue.TemplateRestrictionType.ASSIGN_VARIABLE;
                    break;
                case "nil":
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
            if (Index >= luaValues.Length || luaValues[Index] == null)
            {
                valueString = $"Missing population data At index '{Index}'";
                return false;
            }
            LuaValue luaValueAtIndex = luaValues[Index];

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
