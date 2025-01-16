using SOC.QuestObjects.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SOC.QuestObjects.UAV
{
    public class UAVsMetadata : ObjectsMetadata
    {
        public UAVsMetadata() { }

        public UAVsMetadata(UAVControl control)
        {
            objectiveType = control.comboBox_ObjType.Text;
        }

        [XmlAttribute]
        public string objectiveType { get; set; } = "KILLREQUIRED";
    }
}
