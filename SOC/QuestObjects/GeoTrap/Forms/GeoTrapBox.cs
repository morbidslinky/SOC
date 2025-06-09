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
    public partial class GeoTrapBox : QuestObjectBox
    {
        public int ID; GeoTrapsControlPanel ParentPanel;
        public bool _controlUpdate = false;

        public GeoTrapBox(GeoTrap qObject, GeoTrapsControlPanel parentPanel)
        {
            ParentPanel = parentPanel;
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

            for (int i = 0; i < 100; i++)
            {
                comboBox_geotrap.Items.Add($"GeoTrap_{i}");
            }

            comboBox_geotrap.Text = qObject.geoTrap;

            checkBoxPlayerOnlyTrigger.Checked = qObject.isPlayerOnlyTrigger;

            textBox_xscale.Text = qObject.xScale;
            textBox_yscale.Text = qObject.yScale;
            textBox_zscale.Text = qObject.zScale;
        }

        private void checkBoxPlayerOnlyTrigger_CheckedChanged(object sender, EventArgs e)
        {
            if (_controlUpdate) return;

            _controlUpdate = true;
            ParentPanel.SetPlayerOnlyTriggerForGeoTrap((string)comboBox_geotrap.SelectedItem, checkBoxPlayerOnlyTrigger.Checked);
            _controlUpdate = false;
        }

        private void comboBox_geotrap_SelectedIndexChanged(object sender, EventArgs e)
        {
            _controlUpdate = true;
            checkBoxPlayerOnlyTrigger.Checked = ParentPanel.GetPlayerOnlyTrigger((string)comboBox_geotrap.SelectedItem, this);
            _controlUpdate = false;
        }

        public override QuestObject getQuestObject()
        {
            return new GeoTrap(this);
        }
    }
}
