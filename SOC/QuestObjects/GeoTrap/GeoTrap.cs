using SOC.Classes.Common;
using SOC.Core.Classes.InfiniteHeaven;
using SOC.QuestObjects.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SOC.QuestObjects.GeoTrap
{
    public class GeoTrap : QuestObject
    {
        public GeoTrap() { }

        public GeoTrap(GeoTrapBox box)
        {
            ID = box.ID;

            geoTrap = box.comboBox_geotrap.Text;

            if (box.radioButton_box.Checked)
                type = "box";
            else
                type = "sphere";

            xScale = box.textBox_xscale.Text;
            zScale = box.textBox_zscale.Text;
            yScale = box.textBox_yscale.Text;

            position = new Position(new Coordinates(box.textBox_xcoord.Text, box.textBox_ycoord.Text, box.textBox_zcoord.Text), new Rotation(box.textBox_rot.Text));
        }

        public GeoTrap(Position pos, int num)
        {
            position = pos; ID = num;
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
            return "Shape_" + ID;
        }

        [XmlAttribute]
        public int ID { get; set; } = 0;

        [XmlElement]
        public string geoTrap { get; set; } = "GeoTrap_0";

        [XmlElement]
        public string type { get; set; } = "box";

        [XmlElement]
        public string xScale { get; set; } = "1";

        [XmlElement]
        public string zScale { get; set; } = "1";

        [XmlElement]
        public string yScale { get; set; } = "1";

        [XmlElement]
        public Position position { get; set; } = new Position(new Coordinates(), new Rotation());
    }
}
