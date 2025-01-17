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

namespace SOC.QuestObjects.UAV
{
    public partial class UAVBox : QuestObjectBox
    {
        public int ID;

        public UAVBox(UAV qObject, List<string> routes)
        {
            InitializeComponent();
            ID = qObject.ID;
            groupBox_main.Text = qObject.GetObjectName();

            checkBox_target.Checked = qObject.isTarget;
            checkBox_docile.Checked = qObject.docile;

            textBox_xcoord.Text = qObject.position.coords.xCoord;
            textBox_ycoord.Text = qObject.position.coords.yCoord;
            textBox_zcoord.Text = qObject.position.coords.zCoord;
            textBox_rot.Text = qObject.position.rotation.GetDegreeRotY();

            comboBox_dRoute.Items.Add("NONE");
            comboBox_dRoute.Items.AddRange(routes.ToArray());
            SetComboBox(comboBox_dRoute, qObject.dRoute);

            comboBox_aRoute.Items.Add("NONE");
            comboBox_aRoute.Items.AddRange(routes.ToArray());
            SetComboBox(comboBox_aRoute, qObject.aRoute);

            comboBox_defense.Text = qObject.defenseGrade;
            comboBox_weapon.Text = qObject.weapon;
        }

        public override QuestObject getQuestObject()
        {
            return new UAV(this);
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

    }
}
