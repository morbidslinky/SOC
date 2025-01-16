using SOC.Classes.Common;
using SOC.Core.Classes.InfiniteHeaven;
using SOC.QuestObjects.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SOC.QuestObjects.Hostage
{
    public class Hostage : QuestObject
    {
        public Hostage() { }

        public Hostage(Position pos, int numId)
        {
            position = pos; ID = numId;
        }

        public Hostage(HostageBox box)
        {
            ID = box.ID;

            isTarget = box.checkBox_target.Checked;
            isUntied = box.checkBox_untied.Checked;
            isInjured = box.checkBox_injured.Checked;
            skill = box.comboBox_skill.Text;
            staffType = box.comboBox_staff.Text;
            scared = box.comboBox_scared.Text;
            language = box.comboBox_lang.Text;
            position = new Position(new Coordinates(box.textBox_xcoord.Text, box.textBox_ycoord.Text, box.textBox_zcoord.Text), new Rotation(box.textBox_rot.Text));
        }

        public override string GetObjectName()
        {
            return "Hostage_" + ID;
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

        [XmlElement]
        public bool isTarget { get; set; } = false;

        [XmlElement]
        public bool isUntied { get; set; } = false;

        [XmlElement]
        public bool isInjured { get; set; } = false;

        [XmlAttribute]
        public int ID { get; set; } = 0;

        [XmlElement]
        public string skill { get; set; } = "NONE";

        [XmlElement]
        public string staffType { get; set; } = "NONE";

        [XmlElement]
        public string scared { get; set; } = "NORMAL";

        [XmlElement]
        public string language { get; set; } = "english";

        [XmlElement]
        public Position position { get; set; } = new Position(new Coordinates(), new Rotation());
    }
}
