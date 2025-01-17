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

namespace SOC.QuestObjects.ActiveItem
{
    public partial class ActiveItemBox : QuestObjectBox
    {
        public int ID;

        public ActiveItemBox(ActiveItem qObject)
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

            comboBox_activeItem.Items.AddRange(ActiveItemNames.activeItems);
            comboBox_activeItem.Text = qObject.activeItem;

            checkBox_target.Checked = qObject.isTarget;
        }

        public override QuestObject getQuestObject()
        {
            return new ActiveItem(this);
        }
    }
}
