using SOC.QuestObjects.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SOC.QuestObjects.ActiveItem
{
    public class ActiveItemsMetadata : ObjectsMetadata
    {
        public ActiveItemsMetadata() { }

        public ActiveItemsMetadata(ActiveItemControl control)
        {
            objectiveType = control.comboBox_ObjType.Text;
        }

        [XmlAttribute]
        public string objectiveType { get; set; } = "ELIMINATE";
    }
}
