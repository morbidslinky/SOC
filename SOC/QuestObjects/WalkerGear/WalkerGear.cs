using SOC.Classes.Common;
using SOC.Core.Classes.InfiniteHeaven;
using SOC.QuestObjects.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SOC.QuestObjects.WalkerGear
{
    public class WalkerGear : QuestObject
    {
        public WalkerGear() { }

        public WalkerGear(Position pos, int id)
        {
            position = pos; ID = id;
        }

        public WalkerGear(WalkerBox box)
        {
            ID = box.ID;

            isTarget = box.checkBox_target.Checked;
            pilot = box.comboBox_pilot.Text;
            paint = box.comboBox_paint.Text;
            weapon = box.comboBox_weapon.Text;
            position = new Position(new Coordinates(box.textBox_xcoord.Text, box.textBox_ycoord.Text, box.textBox_zcoord.Text), new Rotation(box.textBox_rot.Text));
        }

        [XmlElement]
        public bool isTarget { get; set; } = false;

        [XmlAttribute]
        public int ID { get; set; } = 0;

        [XmlElement]
        public string pilot { get; set; } = "NONE";

        [XmlElement]
        public string paint { get; set; } = "SOVIET";

        [XmlElement]
        public string weapon { get; set; } = "WG_MACHINEGUN";

        [XmlElement]
        public Position position = new Position(new Coordinates(), new Rotation());

        public override Position GetPosition()
        {
            return position;
        }

        public override void SetPosition(Position pos)
        {
            position = pos;
        }

        public override int GetID()
        {
            return ID;
        }

        public override string GetObjectName()
        {
            return "WalkerGear_" + ID;
        }
    }
}
