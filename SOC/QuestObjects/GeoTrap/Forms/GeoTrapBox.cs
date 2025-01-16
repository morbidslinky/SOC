using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SOC.QuestObjects.Common;
using SOC.UI;

namespace SOC.QuestObjects.GeoTrap
{
    public partial class GeoTrapBox : QuestBox
    {
        public int ID;

        public GeoTrapBox(GeoTrap qObject)
        {
            InitializeComponent();
            ID = qObject.ID;
            groupBox_main.Text = qObject.GetObjectName();

            textBox_xcoord.Text = qObject.position.coords.xCoord;
            textBox_ycoord.Text = qObject.position.coords.yCoord;
            textBox_zcoord.Text = qObject.position.coords.zCoord;
            textBox_rot.Text = qObject.position.rotation.GetDegreeRotY();

            if (qObject.type == "box")
                radioButton_box.Checked = true;
            else
                radioButton_sphere.Checked = true;

            comboBox_geotrap.Items.AddRange(new string[]
            {
                "GeoTrap_0", "GeoTrap_1", "GeoTrap_2", "GeoTrap_3", "GeoTrap_4", "GeoTrap_5", "GeoTrap_6", "GeoTrap_7", "GeoTrap_8", "GeoTrap_9",
            });
            comboBox_geotrap.Text = qObject.geoTrap;

            textBox_xscale.Text = qObject.xScale;
            textBox_yscale.Text = qObject.yScale;
            textBox_zscale.Text = qObject.zScale;
        }

        public override QuestObject getQuestObject()
        {
            return new GeoTrap(this);
        }
    }
}
