using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SOC.UI;
using SOC.QuestObjects.Common;

namespace SOC.QuestObjects.Vehicle
{
    public partial class VehicleBox : QuestObjectBox
    {
        public int ID;

        public VehicleBox(Vehicle qObject, VehiclesMetadata meta)
        {
            InitializeComponent();
            ID = qObject.ID;
            groupBox_main.Text = qObject.GetObjectName();

            textBox_xcoord.Text = qObject.position.coords.xCoord;
            textBox_ycoord.Text = qObject.position.coords.yCoord;
            textBox_zcoord.Text = qObject.position.coords.zCoord;
            textBox_rot.Text = qObject.position.rotation.GetDegreeRotY();

            checkBox_target.Checked = qObject.isTarget;

            comboBox_vehicle.Items.AddRange(new string[] 
            {
                "TT77 NOSOROG","M84A MAGLOADER", "ZHUK BR-3", "ZHUK RS-ZO","STOUT IFV-SC","STOUT IFV-FS"
            });
            comboBox_vehicle.Text = qObject.vehicle;

            comboBox_class.Items.AddRange(new string[] 
            {
                "DEFAULT", "DARK_GRAY", "OXIDE_RED"
            });
            comboBox_class.Text = qObject.vehicleClass;
        }

        public override QuestObject getQuestObject()
        {
            return new Vehicle(this);
        }
    }
}
