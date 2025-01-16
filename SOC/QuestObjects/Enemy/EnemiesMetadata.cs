using SOC.QuestObjects.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SOC.QuestObjects.Enemy
{
    public class EnemiesMetadata : ObjectsMetadata
    {
        public EnemiesMetadata() { }

        public EnemiesMetadata(EnemyControl control)
        {
            objectiveType = control.comboBox_ObjType.Text;
            subtype = control.comboBox_Subtype.Text;
        }

        [XmlAttribute]
        public string objectiveType { get; set; } = "ELIMINATE";

        [XmlAttribute]
        public string subtype { get; set; } = "SOVIET_A";

    }
}
