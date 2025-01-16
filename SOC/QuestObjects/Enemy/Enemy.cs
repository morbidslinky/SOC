using SOC.Core.Classes.InfiniteHeaven;
using SOC.QuestObjects.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SOC.QuestObjects.Enemy
{
    public class Enemy : QuestObject
    {
        public Enemy() { }

        public Enemy(string enemyName)
        {
            name = enemyName;
        }

        [XmlAttribute]
        public string name { get; set; } = "sol_quest_0000";

        [XmlAttribute]
        public bool spawn { get; set; } = false;

        [XmlElement]
        public bool isTarget { get; set; } = false;

        [XmlElement]
        public bool balaclava { get; set; } = false;

        [XmlElement]
        public bool zombie { get; set; } = false;

        [XmlElement]
        public bool armored { get; set; } = false;

        [XmlElement]
        public string body { get; set; } = "DEFAULT";

        [XmlElement]
        public string cRoute { get; set; } = "NONE";

        [XmlElement]
        public string dRoute { get; set; } = "NONE";

        [XmlElement]
        public string skill { get; set; } = "NONE";

        [XmlElement]
        public string staffType { get; set; } = "NONE";

        [XmlArray]
        public string[] powers { get; set; } = new string[0];

        public Enemy(EnemyBox box)
        {
            name = box.groupBox_main.Text;

            spawn = box.checkBox_spawn.Checked;
            isTarget = box.checkBox_target.Checked;
            balaclava = box.checkBox_balaclava.Checked;
            zombie = box.checkBox_zombie.Checked;

            cRoute = box.comboBox_cautionroute.Text;
            dRoute = box.comboBox_sneakroute.Text;
            skill = box.comboBox_skill.Text;
            staffType = box.comboBox_staff.Text;

            armored = box.checkBox_armor.Checked;
            body = box.comboBox_body.Text;
            powers = box.listBox_power.Items.OfType<string>().ToArray();
        }

        public override Position GetPosition()
        {
            return new Position();
        }

        public override void SetPosition(Position pos)
        {
            return;
        }

        public override int GetID()
        {
            return 0;
        }

        public override string GetObjectName()
        {
            return name;
        }
    }
}
