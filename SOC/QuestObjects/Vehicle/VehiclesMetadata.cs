using SOC.QuestObjects.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SOC.QuestObjects.Vehicle
{
    public class VehiclesMetadata : ObjectsMetadata
    {

        public VehiclesMetadata() { }

        public VehiclesMetadata(VehicleControl control)
        {
            ObjectiveType = control.comboBox_ObjType.Text;
        }

        [XmlAttribute]
        public string ObjectiveType { get; set; } = "ELIMINATE";
    }
}
