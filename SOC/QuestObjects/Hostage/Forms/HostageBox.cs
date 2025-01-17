using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SOC.Classes.Common;
using SOC.Core.Classes.InfiniteHeaven;
using SOC.Forms;
using SOC.QuestObjects.Common;
using SOC.UI;

namespace SOC.QuestObjects.Hostage
{
    public partial class HostageBox : QuestObjectBox // Note: inherit from UserControl to show box in designer
    {
        public int ID;

        public HostageBox(Hostage qObject, HostageMetadata meta)
        {
            InitializeComponent();
            ID = qObject.ID;
            groupBox_main.Text = qObject.GetObjectName();

            textBox_xcoord.Text = qObject.position.coords.xCoord;
            textBox_ycoord.Text = qObject.position.coords.yCoord;
            textBox_zcoord.Text = qObject.position.coords.zCoord;
            textBox_rot.Text = qObject.position.rotation.GetDegreeRotY();

            checkBox_target.Checked = qObject.isTarget;
            checkBox_untied.Checked = qObject.isUntied;
            checkBox_injured.Checked = qObject.isInjured;

            comboBox_scared.Items.AddRange(new string[] {
                "NORMAL", "ALWAYS", "NEVER"
            });
            comboBox_scared.Text = qObject.scared;

            comboBox_lang.Text = qObject.language; // Note: h_combobox_lang has the male languages prelisted in the designer
            RefreshLanguage(meta.hostageBodyName);

            comboBox_staff.Items.AddRange(NPCMtbsInfo.Staff_Type_ID);
            comboBox_staff.Text = qObject.staffType;

            comboBox_skill.Items.AddRange(NPCMtbsInfo.skills);
            comboBox_skill.Text = qObject.skill;
        }

        public override QuestObject getQuestObject()
        {
            return new Hostage(this);
        }

        public void RefreshLanguage(string body) 
        {
            if (body.ToUpper().Contains("FEMALE"))
            {
                comboBox_lang.Items.Clear();
                comboBox_lang.Items.Add("english");
                comboBox_lang.SelectedIndex = 0;
            }
            else
            {
                int languageindex = comboBox_lang.SelectedIndex; 
                comboBox_lang.Items.Clear();
                comboBox_lang.Items.AddRange(new string[] { "english", "russian", "pashto", "kikongo", "afrikaans" });
                comboBox_lang.SelectedIndex = languageindex;

            }
        }
    }
}
