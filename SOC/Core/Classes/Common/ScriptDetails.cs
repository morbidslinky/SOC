using SOC.Classes.Lua;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SOC.Classes.Common
{
    [XmlType("ScriptDetails")]
    public class ScriptDetails
    {
        [XmlArray("QStep_Main")]
        [XmlArrayItem("UserScript")]
        public List<Script> QStep_Main = new List<Script>();

        [XmlArray("Variables")]
        [XmlArrayItem("UserVariable")]
        public List<LuaTableEntry> VariableDeclarations = new List<LuaTableEntry>();

        [XmlIgnore]
        public List<ChoiceKeyValues> QuestChoosableValueSetsCache = new List<ChoiceKeyValues>();

        public ScriptDetails () { }

        public ScriptDetails (List<Script> qstep_main, List<LuaTableEntry> variables)
        {
            QStep_Main = qstep_main;
            VariableDeclarations = variables;
        }

        public void WriteToXml(string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ScriptDetails));
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, this);
            }
        }

        public static ScriptDetails LoadFromXml(string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ScriptDetails));
            using (StreamReader reader = new StreamReader(filePath))
            {
                return (ScriptDetails)serializer.Deserialize(reader);
            }
        }
    }
}
