using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SOC.Classes.Lua
{
    [XmlRoot("LuaValueList")]
    public class LuaValueList
    {
        [XmlArray("Values")]
        [XmlArrayItem("Value")]
        public List<LuaValue> Values { get; set; }

        public LuaValueList()
        {
            Values = new List<LuaValue>();
        }

        public static void SaveToXml(LuaValueList obj, string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(LuaValueList));
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, obj);
            }
        }

        public static LuaValueList LoadFromXml(string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(LuaValueList));
            using (StreamReader reader = new StreamReader(filePath))
            {
                return (LuaValueList)serializer.Deserialize(reader);
            }
        }
    }
}
