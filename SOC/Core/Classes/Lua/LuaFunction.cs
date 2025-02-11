using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SOC.Classes.Lua
{
    public class LuaFunction : LuaValue
    {
        [XmlElement] public string Template { get; set; }

        [XmlArray("PopulationData")]
        [XmlArrayItem("PopulationData")]
        public LuaValue[] Data { get; set; }

        public override string Value => ParseTemplate();

        public LuaFunction() : base(ValueType.Function) { }

        public LuaFunction(string template, LuaValue[] data) : base(ValueType.Function)
        {
            Template = template;
            Data = data;
        }

        internal string ParseTemplate()
        {
            List<string> tokens = new List<string>();
            int start = 0;

            while (start < Template.Length)
            {
                int open = Template.IndexOf("{{", start);
                if (open == -1)
                {
                    tokens.Add(Template.Substring(start));
                    break;
                }

                int close = Template.IndexOf("}}", open);
                if (close == -1) break;

                tokens.Add(Template.Substring(start, open - start));

                string key = Template.Substring(open + 2, close - open - 2).Trim();
                tokens.Add(TemplatePlaceHolder.TryParsePlaceholder(key, out TemplatePlaceHolder placeholder) 
                    && TemplatePlaceHolder.TryGetValidKey(Data, placeholder, out LuaValue luaValue) ? luaValue.Value : "{{" + key + "}}");

                start = close + 2;
            }

            return string.Join("", tokens);
        }
    }

    internal class TemplatePlaceHolder
    {
        int index { get; set; }
        LuaValue.ValueType[] AllowedTypes { get; set; }

        public static bool TryGetValidKey(LuaValue[] luaValues, TemplatePlaceHolder placeholder, out LuaValue luaValue)
        {
            int lookupIndex = placeholder.index;

            if (lookupIndex >= luaValues.Length || luaValues[lookupIndex] == null)
            {
                luaValue = new LuaNil();
                return false;
            }

            luaValue = luaValues[lookupIndex];
            if (luaValue is LuaVariable var)
            {
                while (var.GetAssignedValue() is LuaVariable nestedVar)
                {
                    var = (LuaVariable)nestedVar.GetAssignedValue();
                }
                return placeholder.AllowedTypes.Contains(var.Type);
            }
            return placeholder.AllowedTypes.Contains(luaValue.Type) || placeholder.AllowedTypes.Contains(LuaValue.ValueType.ANY);
        }

        public static bool TryParsePlaceholder(string token, out TemplatePlaceHolder placeholder) {

            placeholder = new TemplatePlaceHolder();
            string[] commaSeparatedSplit = token.Split(',');
            if (commaSeparatedSplit.Length != 2) { return false; }
            if (!int.TryParse(commaSeparatedSplit[0].Trim(), out int parseInt))
                return false;
            placeholder.index = parseInt;

            string[] typeRestrictions = commaSeparatedSplit[1].Split('|');
            placeholder.AllowedTypes = new LuaValue.ValueType[typeRestrictions.Length];

            for (int i = 0; i < typeRestrictions.Length; i++) {
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

    }

}