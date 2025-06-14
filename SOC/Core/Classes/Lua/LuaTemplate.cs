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
                    restrictionType = "UNRECOGNIZED";
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
                placeholderTokens = allTokens
                    .OfType<LuaTemplatePlaceholder>()
                    .Distinct()
                    .OrderBy(placeholder => placeholder.Index)
                    .ToList();

                for (int i = placeholderTokens.Count - 1; i >= 0; i--)
                {
                    var currentToken = placeholderTokens[i];
                    if (currentToken.AllowedTypes.Contains(LuaValue.TemplateRestrictionType.ASSIGN_VARIABLE) 
                        && placeholderTokens.Any(token => token.Index == currentToken.Index && token.AllowedTypes.Contains(LuaValue.TemplateRestrictionType.VARIABLE)))
                    {
                        placeholderTokens.Remove(currentToken);
                    }
                }
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

    public class LuaTemplatePlaceholder : LuaTemplateToken, IEquatable<LuaTemplatePlaceholder>
    {
        public string PlaceholderString { get; set; }

        public int Index { get; set; }

        public List<LuaValue.TemplateRestrictionType> AllowedTypes { get; set; }

        public LuaTemplatePlaceholder(string placeholderString) {
            Index = -1; AllowedTypes = new List<LuaValue.TemplateRestrictionType> { LuaValue.TemplateRestrictionType.NIL }; PlaceholderString = placeholderString;
        }

        public bool Equals(LuaTemplatePlaceholder other)
        {
            if (other == null) return false;

            return Index == other.Index
                && string.Equals(PlaceholderString, other.PlaceholderString)
                && new HashSet<LuaValue.TemplateRestrictionType>(AllowedTypes)
                   .SetEquals(other.AllowedTypes);
        }

        public override bool Equals(object obj) => Equals(obj as LuaTemplatePlaceholder);

        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 23 + Index.GetHashCode();
            hash = hash * 23 + (PlaceholderString?.GetHashCode() ?? 0);

            int allowedTypesHash = 0;
            if (AllowedTypes != null)
            {
                foreach (var t in new HashSet<LuaValue.TemplateRestrictionType>(AllowedTypes))
                    allowedTypesHash ^= t.GetHashCode();
            }

            hash = hash * 23 + allowedTypesHash;

            return hash;
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

            string[] typeRestrictions = splitPlaceholderTokens[1].Split(',');
            placeholder.AllowedTypes = new List<LuaValue.TemplateRestrictionType>();

            foreach (string restriction in typeRestrictions)
            {
                string trimmed = restriction.Trim().ToUpper();
                if (Enum.TryParse<LuaValue.TemplateRestrictionType>(trimmed, out var parsedType))
                {
                    placeholder.AllowedTypes.Add(parsedType);
                }
                else
                {
                    placeholder.AllowedTypes = new List<LuaValue.TemplateRestrictionType> { LuaValue.TemplateRestrictionType.TEMPLATE_ERROR };
                    return false;
                }
            }

            return true;
        }

        public string Populate(LuaValue[] luaValues)
        {
            if (Index == -1 || AllowedTypes.Contains(LuaValue.TemplateRestrictionType.TEMPLATE_ERROR))
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

            return Allows(luaValueAtIndex, out valueString);
        }

        public bool Allows(LuaValue value, out string valueString)
        {
            foreach (var allowed in AllowedTypes)
            {
                if (value is LuaVariable variable)
                {
                    if (allowed == LuaValue.TemplateRestrictionType.VARIABLE ||
                        (allowed == LuaValue.TemplateRestrictionType.ASSIGN_VARIABLE) ||
                        (variable.GetAssignedValue().Type == allowed))
                    {
                        valueString = allowed == LuaValue.TemplateRestrictionType.ASSIGN_VARIABLE
                            ? variable.GetAssignmentLua()
                            : variable.ToString();
                        return true;
                    }
                }
                else if (value is LuaFunctionCall function &&
                         (allowed == LuaValue.TemplateRestrictionType.FUNCTION_CALL || function.EvaluatesTo == allowed))
                {
                    valueString = function.ToString();
                    return true;
                }
                else if (value is LuaTableIdentifier tableIdentifier &&
                         (allowed == LuaValue.TemplateRestrictionType.TABLE_IDENTIFIER || tableIdentifier.EvaluatesTo == allowed))
                {
                    valueString = tableIdentifier.ToString();
                    return true;
                }
                else if (value.Type == allowed)
                {
                    valueString = value.ToString();
                    return true;
                }
            }

            valueString = value.ToString();
            return false;
        }
    }
}
