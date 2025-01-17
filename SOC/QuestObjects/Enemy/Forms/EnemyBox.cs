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
using SOC.Classes.Common;

namespace SOC.QuestObjects.Enemy
{
    public partial class EnemyBox : QuestObjectBox
    {
        private static int armorCount = 0;
        private static int balaclavaCount = 0;
        public static void ResetFovaCounts()
        {
            armorCount = 0;
            balaclavaCount = 0;
        }

        public EnemyBox(Enemy qObject, List<string> routes, List<string> bodies)
        {
            InitializeComponent();
            string soldierName = qObject.GetObjectName();
            if (!soldierName.Contains("sol_quest"))
            {
                RelabelCPSoldier();
                comboBox_sneakroute.Items.Add("DEFAULT");
                comboBox_cautionroute.Items.Add("DEFAULT");
            }
            groupBox_main.Text = soldierName;

            checkBox_spawn.Checked = qObject.spawn;
            checkBox_target.Checked = qObject.isTarget;
            checkBox_balaclava.Checked = qObject.balaclava;
            checkBox_zombie.Checked = qObject.zombie;
            
            comboBox_sneakroute.Items.AddRange(routes.ToArray());
            SetComboBox(comboBox_sneakroute, qObject.dRoute);

            comboBox_cautionroute.Items.AddRange(routes.ToArray());
            SetComboBox(comboBox_cautionroute, qObject.cRoute);

            comboBox_power.Items.AddRange(EnemyInfo.powerSetting);
            listBox_power.Items.AddRange(qObject.powers);

            checkBox_armor.Checked = qObject.armored;

            comboBox_body.Items.AddRange(bodies.ToArray());
            SetComboBox(comboBox_body, qObject.body);

            comboBox_skill.Items.AddRange(NPCMtbsInfo.skills);
            SetComboBox(comboBox_skill, qObject.skill);

            comboBox_staff.Items.AddRange(NPCMtbsInfo.Staff_Type_ID);
            SetComboBox(comboBox_staff, qObject.staffType);

            UpdateSpawn(true);
        }

        public override QuestObject getQuestObject()
        {
            return new Enemy(this);
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

        private void RelabelCPSoldier()
        {
            checkBox_spawn.Text = "Customize";
        }

        private void checkBox_spawn_CheckedChanged(object sender, EventArgs e)
        {
            UpdateSpawn();
        }

        private void UpdateSpawn(bool initialSetup = false)
        {
            if (checkBox_spawn.Checked)
            {
                checkBox_balaclava.Enabled = true;
                checkBox_armor.Enabled = true;
                label_skill.Enabled = true;
                label_sneakroute.Enabled = true;
                label_staff.Enabled = true;
                listBox_power.Enabled = true;
                listBox_power.SelectedIndex = listBox_power.Items.Count - 1;
                checkBox_target.Enabled = true;
                comboBox_body.Enabled = true;
                comboBox_cautionroute.Enabled = true;
                comboBox_power.Enabled = true;
                comboBox_skill.Enabled = true;
                comboBox_sneakroute.Enabled = true;
                comboBox_staff.Enabled = true;
                label_body.Enabled = true;
                label_cautionroute.Enabled = true;
                label_power.Enabled = true;
                checkBox_zombie.Enabled = true;

                if(listBox_power.Items.Count > 0)
                    button_removepower.Enabled = true;

                button_SwapRoutes.Enabled = true;
                button_SneakToCaution.Enabled = true;
                button_CautionToSneak.Enabled = true;

                if (checkBox_armor.Checked)
                    ArmorChecked();

                if (checkBox_balaclava.Checked)
                    BalaclavaChecked();
            }
            else
            {
                checkBox_armor.Enabled = false;
                if (checkBox_armor.Checked && !initialSetup)
                    armorCount--;

                checkBox_balaclava.Enabled = false;
                if (checkBox_balaclava.Checked && !initialSetup)
                    balaclavaCount--;

                label_skill.Enabled = false;
                label_sneakroute.Enabled = false;
                label_staff.Enabled = false;
                listBox_power.SelectedIndex = -1;
                listBox_power.Enabled = false;
                checkBox_target.Enabled = false;
                comboBox_body.Enabled = false;
                comboBox_cautionroute.Enabled = false;
                comboBox_power.Enabled = false;
                comboBox_skill.Enabled = false;
                comboBox_sneakroute.Enabled = false;
                comboBox_staff.Enabled = false;
                label_body.Enabled = false;
                label_cautionroute.Enabled = false;
                label_power.Enabled = false;
                checkBox_zombie.Enabled = false;
                button_removepower.Enabled = false;
                button_SwapRoutes.Enabled = false;
                button_SneakToCaution.Enabled = false;
                button_CautionToSneak.Enabled = false;
            }

        }

        private void SwapRoute_Button_Clicked(object sender, EventArgs e)
        {
            string cRoute = comboBox_cautionroute.Text;
            comboBox_cautionroute.Text = comboBox_sneakroute.Text;
            comboBox_sneakroute.Text = cRoute;
            groupBox_main.Focus();
        }

        private void SneakToCaution_Button_Clicked(object sender, EventArgs e)
        {
            comboBox_cautionroute.Text = comboBox_sneakroute.Text;
            groupBox_main.Focus();
        }

        private void CautionToSneak_Button_Clicked(object sender, EventArgs e)
        {
            comboBox_sneakroute.Text = comboBox_cautionroute.Text;
            groupBox_main.Focus();
        }
        
        private void listBox_power_selectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox_power.SelectedIndex > -1)
                button_removepower.Enabled = true;
            else
                button_removepower.Enabled = false;
            groupBox_main.Focus();
        }

        private void comboBox_power_selectedIndexChanged(object sender, EventArgs e)
        {
            if (!listBox_power.Items.Contains(comboBox_power.Text))
            {
                listBox_power.Items.Add(comboBox_power.Text);
                listBox_power.SelectedIndex = listBox_power.Items.Count - 1;
                groupBox_main.Focus();
            }
        }

        private void button_removepower_Click(object sender, EventArgs e)
        {

            if (listBox_power.Text.Equals("QUEST_ARMOR"))
            {
                checkBox_armor.Checked = false;
            }
            else if (listBox_power.SelectedIndex != -1)
            {
                listBox_power.Items.RemoveAt(listBox_power.SelectedIndex);
                listBox_power.SelectedIndex = listBox_power.Items.Count - 1;
            }
            groupBox_main.Focus();
        }

        private void checkBox_armor_Click(object sender, EventArgs e)
        {
            if (checkBox_armor.Checked)
                ArmorChecked();
            else
                ArmorUnchecked();
            Console.WriteLine(armorCount);
        }

        private void ArmorChecked()
        {
            if (armorCount >= EnemyInfo.MAXQUESTFOVA)
            {
                MessageBox.Show("Heavy Armor can only be applied to 8 soldiers maximum.", "Game Limitation Exceeded", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                checkBox_armor.Checked = false;
                if (listBox_power.Items.Contains("QUEST_ARMOR"))
                {
                    listBox_power.Items.Remove("QUEST_ARMOR");
                    listBox_power.SelectedIndex = listBox_power.Items.Count - 1;
                }
            }
            else
            {
                armorCount++;
                if (!listBox_power.Items.Contains("QUEST_ARMOR"))
                {
                    listBox_power.Items.Add("QUEST_ARMOR");
                    listBox_power.SelectedIndex = listBox_power.Items.Count - 1;
                }
                comboBox_body.Enabled = false;
                checkBox_balaclava.Enabled = false;
                label_body.Enabled = false;
            }
        }

        private void ArmorUnchecked()
        {
            armorCount--;
            comboBox_body.Enabled = true;
            checkBox_balaclava.Enabled = true;
            label_body.Enabled = true;
            listBox_power.Items.Remove("QUEST_ARMOR");
            listBox_power.SelectedIndex = listBox_power.Items.Count - 1;
        }

        private void checkBox_balaclava_Click(object sender, EventArgs e)
        {
            if (checkBox_balaclava.Checked)
                BalaclavaChecked();
            else
                BalaclavaUnchecked();
            Console.WriteLine(balaclavaCount);
        }

        private void BalaclavaChecked()
        {
            if (balaclavaCount >= EnemyInfo.MAXQUESTFOVA)
            {
                MessageBox.Show("Balaclavas can only be applied to 8 soldiers maximum.", "Game Limitation Exceeded", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                checkBox_balaclava.Checked = false;
            }
            else
            {
                balaclavaCount++;
                checkBox_armor.Enabled = false;
            }
        }

        private void BalaclavaUnchecked()
        {
            balaclavaCount--;
            checkBox_armor.Enabled = true;
        }
    }
}
