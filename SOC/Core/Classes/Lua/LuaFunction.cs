using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;

namespace SOC.Classes.Lua
{
    public class LuaFunction : LuaValue
    {

        [XmlArray("Parameters")]
        [XmlArrayItem("Parameter")]
        public string[] Parameters { get; set; }
        public LuaTemplate Template { get; set; }
        [XmlArray("PopulationValues")]
        [XmlArrayItem("Value")]
        public LuaValue[] PopulationValues { get; set; }
        public override string Value => GetLuaFunction();

        public LuaFunction() : base(TemplateRestrictionType.FUNCTION) { }

        public LuaFunction(LuaTemplate template, LuaValue[] populationValues, params string[] parameters) : base(TemplateRestrictionType.FUNCTION)
        {
            Template = template;
            PopulationValues = populationValues;
            Parameters = parameters;
        }

        public LuaFunction(LuaTemplate template, params string[] parameters) : base(TemplateRestrictionType.FUNCTION)
        {
            Template = template;
            PopulationValues = new LuaValue[0];
            Parameters = parameters;
        }

        public LuaFunction(LuaTemplate template) : base(TemplateRestrictionType.FUNCTION)
        {
            Template = template;
            PopulationValues = new LuaValue[0];
            Parameters = new string[0];
        }

        public LuaFunction(LuaTemplate template, params LuaValue[] populationValues) : base(TemplateRestrictionType.FUNCTION)
        {
            Template = template;
            PopulationValues = populationValues;
            Parameters = new string[0];
        }

        private string GetLuaFunction()
        {
            return $"function({string.Join(", ", Parameters)})\n{Template.Populate(PopulationValues)}\nend";
        }

        public void WriteToLua(string filename)
        {
            string populatedTemplate = Template.Populate(PopulationValues);

            using (StreamWriter fileWriter = new StreamWriter(filename))
            {
                fileWriter.Write(FormatIndentions(populatedTemplate));
            }
        }

        internal static string FormatIndentions(string unformattedString)
        {
            var tokens = LuaLexer.Tokenize(unformattedString);
            int indentLevel = 0;
            StringBuilder formattedCode = new StringBuilder();

            for (int i = 0; i < tokens.Count; i++)
            {
                string token = tokens[i];
                if (token == "\n")
                {
                    if (i + 1 < tokens.Count && (tokens[i + 1] == "end" || tokens[i + 1] == "end," || tokens[i + 1] == "until" || tokens[i + 1] == "}" || tokens[i + 1] == "},"))
                    {
                        indentLevel--;
                    }

                    if (indentLevel < 0)
                    {

                        indentLevel = 0;
                    }
                    formattedCode.Append("\n" + new string('\t', indentLevel));
                }
                else
                {
                    formattedCode.Append(token + " ");
                }

                if (token == "function()" || token == "if" || token == "for" || token == "while" || token == "repeat" || token == "do" || token == "{")
                {
                    indentLevel++;
                }
            }
            return formattedCode.ToString();
        }

        internal static string Indent(string codeLine, int indents)
        {
            StringBuilder indentBuilder = new StringBuilder();
            for (int i = 0; i < indents; i++)
                indentBuilder.Append("\t");

            return indentBuilder.ToString() + codeLine.Trim();
        }

        public void WriteToXml(string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(LuaFunction));
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, this);
            }
        }

        public static LuaFunction LoadFromXml(string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(LuaFunction));
            using (StreamReader reader = new StreamReader(filePath))
            {
                return (LuaFunction)serializer.Deserialize(reader);
            }
        }
    }
}