using SOC.Classes.Common;
using SOC.Core.Classes.InfiniteHeaven;
using SOC.QuestObjects.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SOC.QuestObjects.Model
{
    public class Model : QuestObject
    {

        public Model() { }

        public Model(ModelBox box)
        {
            ID = box.ID;

            collision = box.checkBox_collision.Checked;
            model = box.comboBox_model.Text;
            position = new Position(new Coordinates(box.textBox_xcoord.Text, box.textBox_ycoord.Text, box.textBox_zcoord.Text), new Rotation(box.textBox_xrot.Text, box.textBox_yrot.Text, box.textBox_zrot.Text, box.textBox_wrot.Text));
        }

        public Model(Position pos, int index)
        {
            position = pos; ID = index;
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
            return "Model_" + ID;
        }

        [XmlElement]
        public bool collision { get; set; } = true; // default to true, yeah?

        [XmlAttribute]
        public int ID { get; set; } = 0;

        [XmlElement]
        public string model { get; set; } = "";

        [XmlElement]
        public Position position = new Position(new Coordinates(), new Rotation());

    }
}
