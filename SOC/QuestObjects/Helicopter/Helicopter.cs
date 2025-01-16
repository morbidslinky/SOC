using SOC.Core.Classes.InfiniteHeaven;
using SOC.QuestObjects.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SOC.QuestObjects.Helicopter
{
    public class Helicopter : QuestObject
    {
        public Helicopter() { }

        public Helicopter(int index) // (Position pos, int index)
        {
            //position = pos;
            ID = index;
        }

        public Helicopter(HelicopterBox box)
        {
            ID = box.ID;

            isSpawn = box.checkBox_spawn.Checked;
            isTarget = box.checkBox_target.Checked;
            dRoute = box.comboBox_dRoute.Text;
            cRoute = box.comboBox_cRoute.Text;
            heliClass = box.comboBox_class.Text;
            //position = new Position(new Coordinates(box.textBox_xcoord.Text, box.textBox_ycoord.Text, box.textBox_zcoord.Text), new Rotation(box.textBox_rot.Text));
        }

        [XmlElement]
        public bool isSpawn { get; set; } = false;

        [XmlElement]
        public bool isTarget { get; set; } = false;

        [XmlAttribute]
        public int ID { get; set; } = 0;

        [XmlElement]
        public string dRoute { get; set; } = "NONE";

        [XmlElement]
        public string cRoute { get; set; } = "NONE";

        [XmlElement]
        public string heliClass { get; set; } = "DEFAULT";
        /*
        [XmlElement]
        public Position position { get; set; } = new Position(new Coordinates(), new Rotation());
        */
        public override int GetID()
        {
            return ID;
        }

        public override string GetObjectName()
        {
            if (ID == 0)
                return "EnemyHeli"; // TppReinforceBlock.REINFORCE_HELI_NAME
            else
                return "Helicopter_" + ID;
        }

        public override Position GetPosition()
        {
            return new Position();
        }

        public override void SetPosition(Position pos)
        {
            return;
        }
    }
}
