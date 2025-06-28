using System;
using System.Collections.Generic;
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
        public LuaVariable[] Parameters { get; set; }

        [XmlIgnore]
        public LuaTemplate Body { get; set; }

        [XmlElement("Template")]
        public string Template
        {
            get => Body?.Template;
            set => Body = new LuaTemplate(value);
        }

        [XmlArray("PopulationValues")]
        [XmlArrayItem("Value")]
        public LuaValue[] PopulationValues { get; set; } = new LuaValue[0];

        public override string TokenValue => GetLuaFunctionValue();

        public LuaFunction() : base(TemplateRestrictionType.FUNCTION) {
            Parameters = Array.Empty<LuaVariable>();
        }

        public LuaFunction(LuaTemplate template, LuaValue[] populationValues, LuaVariable[] parameters) : base(TemplateRestrictionType.FUNCTION)
        {
            Body = template;
            PopulationValues = populationValues;
            Parameters = parameters;
        }

        public string GetLuaFunctionValue()
        {
            return $"function({string.Join(", ", Parameters.Select(p => p.Value))})\n{Body.Populate(PopulationValues)}\nend";
        }

        public void WriteToLua(string filename)
        {
            string populatedTemplate = Body.Populate(PopulationValues);

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
    }
}