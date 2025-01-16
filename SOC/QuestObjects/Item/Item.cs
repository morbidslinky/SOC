using SOC.Classes.Common;
using SOC.Core.Classes.InfiniteHeaven;
using SOC.QuestObjects.Common;
using System.Xml.Serialization;

namespace SOC.QuestObjects.Item
{
    public class Item : QuestObject
    {

        public Item() { }

        public Item(Position pos, int index)
        {
            position = pos; ID = index;
        }

        public Item(ItemBox box)
        {
            ID = box.ID;

            isTarget = box.checkBox_target.Checked;
            isBoxed = box.checkBox_boxed.Checked;
            count = box.comboBox_count.Text;
            item = box.comboBox_item.Text;
            position = new Position(new Coordinates(box.textBox_xcoord.Text, box.textBox_ycoord.Text, box.textBox_zcoord.Text), new Rotation(box.textBox_xrot.Text, box.textBox_yrot.Text, box.textBox_zrot.Text, box.textBox_wrot.Text));
        }

        public override void SetPosition(Position pos)
        {
            position = pos;
        }

        public override Position GetPosition()
        {
            return position;
        }

        public override int GetID()
        {
            return ID;
        }

        public override string GetObjectName()
        {
            return "Item_" + ID;
        }

        [XmlElement]
        public bool isBoxed { get; set; } = false;

        [XmlElement]
        public bool isTarget { get; set; } = false;

        [XmlAttribute]
        public int ID { get; set; } = 0;

        [XmlElement]
        public string count { get; set; } = "1";

        [XmlElement]
        public string item { get; set; } = "EQP_SWP_Magazine";

        [XmlElement]
        public Position position = new Position(new Coordinates(), new Rotation());
    }
}
