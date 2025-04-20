using SOC.Classes.Lua;
using System;
using System.Collections.Generic;
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
        public List<StrCode32Script> QStep_Main = new List<StrCode32Script>();

        [XmlArray("Variables")]
        [XmlArrayItem("UserVariable")]
        public List<LuaTableEntry> VariableDeclarations = new List<LuaTableEntry>();
    }
}
