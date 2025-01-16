using SOC.Classes.Common;
using SOC.Core.Classes.InfiniteHeaven;
using SOC.QuestObjects.Common;
using System.Xml.Serialization;

namespace SOC.QuestObjects.Vehicle
{
    public class Vehicle : QuestObject
    {

        public Vehicle() { }

        public Vehicle(Position pos, int id)
        {
            position = pos; ID = id;
        }

        public Vehicle(VehicleBox box)
        {
            ID = box.ID;

            isTarget = box.checkBox_target.Checked;
            vehicle = box.comboBox_vehicle.Text;
            vehicleClass = box.comboBox_class.Text;
            position = new Position(new Coordinates(box.textBox_xcoord.Text, box.textBox_ycoord.Text, box.textBox_zcoord.Text), new Rotation(box.textBox_rot.Text));
        }

        public override string GetObjectName()
        {
            return "Vehicle_" + ID;
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

        [XmlAttribute]
        public int ID { get; set; } = 0;

        [XmlElement]
        public string vehicle { get; set; } = "TT77 NOSOROG";

        [XmlElement]
        public string vehicleClass { get; set; } = "DEFAULT";

        [XmlElement]
        public Position position = new Position(new Coordinates(), new Rotation());
    }
}
