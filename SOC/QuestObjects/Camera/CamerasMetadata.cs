using SOC.QuestObjects.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SOC.QuestObjects.Camera
{
    public class CamerasMetadata : ObjectsMetadata
    {
        public CamerasMetadata() { }

        public CamerasMetadata(CameraControl control)
        {
            objectiveType = control.comboBox_ObjType.Text;
        }

        [XmlAttribute]
        public string objectiveType { get; set; } = "KILLREQUIRED";
    }
}
