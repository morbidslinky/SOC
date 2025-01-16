using SOC.QuestObjects.Common;
using System.Xml.Serialization;

namespace SOC.QuestObjects.Hostage
{
    public class HostageMetadata : ObjectsMetadata
    {
        public HostageMetadata() { }

        public HostageMetadata(HostageControl control)
        {
            hostageBodyName = control.comboBox_Body.Text;
            canInterrogate = control.checkBox_intrgt.Checked;
            objectiveType = control.comboBox_ObjType.Text;
        }

        [XmlAttribute]
        public string objectiveType { get; set; } = "ELIMINATE";

        [XmlAttribute]
        public string hostageBodyName { get; set; } = "AFGH_HOSTAGE";

        [XmlAttribute]
        public bool canInterrogate { get; set; } = false;
    }
}
