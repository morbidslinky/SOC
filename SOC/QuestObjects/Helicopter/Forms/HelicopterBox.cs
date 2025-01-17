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

namespace SOC.QuestObjects.Helicopter
{
    public partial class HelicopterBox : QuestObjectBox
    {
        public int ID;

        public HelicopterBox(Helicopter qObject, List<string> routes)
        {
            InitializeComponent();
            ID = qObject.ID;
            groupBox_main.Text = qObject.GetObjectName();

            /*
            textBox_xcoord.Text = qObject.position.coords.xCoord;
            textBox_ycoord.Text = qObject.position.coords.yCoord;
            textBox_zcoord.Text = qObject.position.coords.zCoord;
            textBox_rot.Text = qObject.position.rotation.GetDegreeRotY();
            */

            checkBox_spawn.Checked = qObject.isSpawn;
            ToggleEnable();
            checkBox_target.Checked = qObject.isTarget;
            
            comboBox_class.Text = qObject.heliClass;

            //comboBox_route.Items.Add("NONE");
            comboBox_dRoute.Items.AddRange(routes.ToArray());
            SetComboBox(comboBox_dRoute, qObject.dRoute);

            comboBox_cRoute.Items.AddRange(routes.ToArray());
            SetComboBox(comboBox_cRoute, qObject.cRoute);
        }

        public override QuestObject getQuestObject()
        {
            return new Helicopter(this);
        }

        private void SetComboBox(ComboBox comboBox, string text)
        {
            if (comboBox.Items.Contains(text))
            {
                comboBox.Text = text;
            }
            else if (comboBox.Items.Count > 0)
            {
                comboBox.SelectedIndex = 0;
            }
        }

        private void checkBox_spawn_CheckedChanged(object sender, EventArgs e)
        {
            ToggleEnable();
        }

        private void ToggleEnable()
        {
            bool enable = checkBox_spawn.Checked;

            checkBox_target.Enabled = enable;
            comboBox_class.Enabled = enable;
            comboBox_cRoute.Enabled = enable;
            comboBox_dRoute.Enabled = enable;
        }
    }
}
