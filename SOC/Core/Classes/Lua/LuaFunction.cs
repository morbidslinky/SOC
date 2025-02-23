using System;
using System.Collections.Generic;
using System.Deployment.Internal;
using System.Dynamic;
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
        public override string Value => GetLuaFunctionValue();

        public LuaFunction() : base(TemplateRestrictionType.FUNCTION) { }

        public LuaFunction(LuaTemplate template, LuaValue[] populationValues, string[] parameters) : base(TemplateRestrictionType.FUNCTION)
        {
            Template = template;
            PopulationValues = populationValues;
            Parameters = parameters;
        }

        public string GetLuaFunctionValue()
        {
            return $"function({string.Join(", ", Parameters)})\n{Template.Populate(PopulationValues)}\nend";
        }

        public void WriteToLua(string filename)
        {
            string populatedTemplate = Template.Populate(PopulationValues);

            var directoryPath = Path.GetDirectoryName(filename);
            if (!string.IsNullOrEmpty(directoryPath))
                Directory.CreateDirectory(directoryPath);
            using (StreamWriter fileWriter = new StreamWriter(filename))
            {
                fileWriter.Write(LuaLexer.FormatIndentions(populatedTemplate));
            }
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

        public static LuaTableEntry ToTableEntry(string functionName, string[] functionParameters, string functionBody)
        {
            LuaTableEntry entry = new LuaTableEntry();
            return Lua.TableEntry(functionName, Lua.Function(functionBody, functionParameters));
        }
    }

    public class LuaFunctionBuilder
    {
        List<FunctionToken> Values = new List<FunctionToken>();
        List<string> Parameters = new List<string>();

        public void AppendPlainText(string plainText)
        {
            Values.Add(new FunctionTokenPlainText(plainText));
        }

        public void AppendLuaValue(LuaValue luaValue)
        {
            Values.Add(new FunctionTokenValue(luaValue));
        }

        public void AppendAssignment(LuaVariable LHS, LuaValue RHS)
        {
            Values.Add(new FunctionTokenAssignment(Lua.TableEntry(LHS, RHS)));
        }

        public void AppendAssignment(LuaTableIdentifier LHS, LuaValue RHS)
        {
            Values.Add(new FunctionTokenAssignment(Lua.TableEntry(LHS, RHS)));
        }

        public LuaFunction ToFunction()
        {
            StringBuilder template = new StringBuilder();
            List<LuaValue> templateValues = new List<LuaValue>();

            int index = 0;
            foreach (FunctionToken token in Values)
            {
                if (token is FunctionTokenPlainText t)
                {
                    template.Append($"{t.PlainText} ");
                }
                else if (token is FunctionTokenValue v)
                {
                    template.AppendLine($"|[{index}|{LuaTemplate.GetTemplateRestrictionTypeString(v.Value, v.isAssign)}]| ");
                    templateValues.Add(v.Value);
                    index++;
                }
                else if (token is FunctionTokenAssignment e)
                {
                    template.AppendLine($"|[{index}|{LuaTemplate.GetTemplateRestrictionTypeString(e.Entry.Key)}]| = |[{index + 1}|{LuaTemplate.GetTemplateRestrictionTypeString(e.Entry.Value)}]|");
                    templateValues.Add(e.Entry.Key);
                    templateValues.Add(e.Entry.Value);
                    index += 2;
                }
            }

            return new LuaFunction(new LuaTemplate(template.ToString()), templateValues.ToArray(), Parameters.ToArray());
        }

        public LuaTableEntry ToTableEntry(string functionName, bool extrude = false)
        {
            LuaTableEntry tableEntry = new LuaTableEntry();
            return Lua.TableEntry(functionName, ToFunction(), extrude);
        }

        public LuaVariable ToVariable(string functionName)
        {
            LuaVariable var = new LuaVariable(functionName);
            var.AssignedTo = ToFunction();
            return var;
        }

    }

    internal abstract class FunctionToken { }

    internal class FunctionTokenPlainText : FunctionToken
    {
        public string PlainText;
        public FunctionTokenPlainText(string plainText)
        {
            PlainText = plainText;
        }
    }

    internal class FunctionTokenValue : FunctionToken
    {
        public LuaValue Value;
        public bool isAssign;
        public FunctionTokenValue(LuaValue value)
        {
            Value = value;
            isAssign = false;
        }

        public FunctionTokenValue(LuaVariable var, bool isAssignment)
        {
            Value = var;
            isAssign = isAssignment;
        }
    }

    internal class FunctionTokenAssignment : FunctionToken
    {
        public LuaTableEntry Entry;
        public FunctionTokenAssignment(LuaTableEntry entry)
        {
            Entry = entry;
        }
    }
}