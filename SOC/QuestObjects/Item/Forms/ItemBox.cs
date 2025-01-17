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

namespace SOC.QuestObjects.Item
{
    public partial class ItemBox : QuestObjectBox
    {
        public int ID;

        public ItemBox(Item qObject)
        {
            InitializeComponent();
            ID = qObject.ID;
            groupBox_main.Text = qObject.GetObjectName();

            textBox_xcoord.Text = qObject.position.coords.xCoord;
            textBox_ycoord.Text = qObject.position.coords.yCoord;
            textBox_zcoord.Text = qObject.position.coords.zCoord;

            textBox_xrot.Text = qObject.position.rotation.xRot;
            textBox_yrot.Text = qObject.position.rotation.yRot;
            textBox_zrot.Text = qObject.position.rotation.zRot;
            textBox_wrot.Text = qObject.position.rotation.wRot;

            comboBox_item.Items.AddRange(ItemNames.itemNames);
            comboBox_item.Text = qObject.item;

            comboBox_count.Items.AddRange(new object[] {
                "1","4","8","12","16"
            });
            comboBox_count.Text = qObject.count;

            checkBox_boxed.Checked = qObject.isBoxed;
            checkBox_target.Checked = qObject.isTarget;
        }

        public override QuestObject getQuestObject()
        {
            return new Item(this);
        }

        private void comboBox_item_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_item.Text.Contains("EQP_WP_"))
            {
                comboBox_count.Text = "0";
                comboBox_count.Enabled = false;
                checkBox_target.Enabled = true;
            }
            else
            {
                int count = 1;
                int.TryParse(comboBox_count.Text, out count);

                comboBox_count.Text = count.ToString();
                comboBox_count.Enabled = true;
                checkBox_target.Checked = false;
                checkBox_target.Enabled = false;
            }
        }
    }
}
