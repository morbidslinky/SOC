using SOC.QuestObjects.Common;
using System.Xml.Serialization;

namespace SOC.QuestObjects.Item
{
    public class ItemsMetadata : ObjectsMetadata
    {
        public ItemsMetadata() { }

        public ItemsMetadata(ItemControl itemControl)
        {
            objectiveType = itemControl.comboBox_ObjType.Text;
        }

        [XmlAttribute]
        public string objectiveType { get; set; } = "ELIMINATE";
    }
}
