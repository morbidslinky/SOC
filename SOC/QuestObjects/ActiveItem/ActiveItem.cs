using SOC.Classes.Common;
using SOC.Core.Classes.InfiniteHeaven;
using SOC.QuestObjects.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SOC.QuestObjects.ActiveItem
{
    public class ActiveItem : QuestObject
    {
        public ActiveItem() { }

        public ActiveItem(Position pos, int index)
        {
            position = pos; ID = index;
        }

        public ActiveItem(ActiveItemBox box)
        {
            ID = box.ID;

            isTarget = box.checkBox_target.Checked;
            activeItem = box.comboBox_activeItem.Text;
            position = new Position(new Coordinates(box.textBox_xcoord.Text, box.textBox_ycoord.Text, box.textBox_zcoord.Text), new Rotation(box.textBox_xrot.Text, box.textBox_yrot.Text, box.textBox_zrot.Text, box.textBox_wrot.Text));
        }

        [XmlAttribute]
        public int ID { get; set; } = 0;

        [XmlElement]
        public bool isTarget { get; set; } = false;

        [XmlElement]
        public string activeItem { get; set; } = "EQP_SWP_DMine";

        [XmlElement]
        public Position position { get; set; } = new Position(new Coordinates(), new Rotation());

        public override int GetID()
        {
            return ID;
        }

        public override string GetObjectName()
        {
            return "ActiveItem_" + ID;
        }

        public override Position GetPosition()
        {
            return position;
        }

        public override void SetPosition(Position pos)
        {
            position = pos;
        }
    }
}
