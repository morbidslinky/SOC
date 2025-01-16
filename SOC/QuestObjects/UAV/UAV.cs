using SOC.Classes.Common;
using SOC.Core.Classes.InfiniteHeaven;
using SOC.QuestObjects.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SOC.QuestObjects.UAV
{
    public class UAV : QuestObject
    {
        public UAV() { }

        public UAV(Position pos, int numId)
        {
            position = pos; ID = numId;
        }

        public UAV(UAVBox box)
        {
            ID = box.ID;
            isTarget = box.checkBox_target.Checked;

            weapon = box.comboBox_weapon.Text;
            defenseGrade = box.comboBox_defense.Text;

            aRoute = box.comboBox_aRoute.Text;
            dRoute = box.comboBox_dRoute.Text;
            docile = box.checkBox_docile.Checked;
            position = new Position(new Coordinates(box.textBox_xcoord.Text, box.textBox_ycoord.Text, box.textBox_zcoord.Text), new Rotation(box.textBox_rot.Text));
        }

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
            return "UAV_" + ID;
        }

        [XmlAttribute]
        public int ID { get; set; } = 0;

        [XmlElement]
        public bool isTarget { get; set; } = false;

        [XmlElement]
        public bool docile { get; set; } = false;

        [XmlElement]
        public string aRoute { get; set; } = "NONE";

        [XmlElement]
        public string dRoute { get; set; } = "NONE";

        [XmlElement]
        public string weapon { get; set; } = "DEVELOP_LEVEL_LMG_0";

        [XmlElement]
        public string defenseGrade { get; set; } = "DEFAULT";

        [XmlElement]
        public Position position = new Position(new Coordinates(), new Rotation());
    }
}
