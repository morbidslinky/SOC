using SOC.Classes.Common;
using SOC.Core.Classes.InfiniteHeaven;
using SOC.QuestObjects.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SOC.QuestObjects.Camera
{
    public class Camera : QuestObject
    {
        public Camera() { }

        public Camera(Position pos, int numId)
        {
            position = pos; ID = numId;
        }

        public Camera(CameraBox box)
        {
            ID = box.ID;
            isTarget = box.checkBox_target.Checked;
            weapon = box.checkBox_weapon.Checked;
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
            return "Camera_" + ID;
        }

        [XmlAttribute]
        public int ID { get; set; } = 0;

        [XmlElement]
        public bool isTarget { get; set; } = false;

        [XmlElement]
        public bool weapon { get; set; } = false;

        [XmlElement]
        public Position position = new Position(new Coordinates(), new Rotation());
    }
}
