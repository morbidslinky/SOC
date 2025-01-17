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

namespace SOC.QuestObjects.Camera
{
    public partial class CameraBox : QuestObjectBox
    {
        public int ID;

        public CameraBox(Camera qObject)
        {
            InitializeComponent();
            ID = qObject.ID;
            groupBox_main.Text = qObject.GetObjectName();

            checkBox_target.Checked = qObject.isTarget;
            checkBox_weapon.Checked = qObject.weapon;

            textBox_xcoord.Text = qObject.position.coords.xCoord;
            textBox_ycoord.Text = qObject.position.coords.yCoord;
            textBox_zcoord.Text = qObject.position.coords.zCoord;
            textBox_rot.Text = qObject.position.rotation.GetDegreeRotY();

        }

        public override QuestObject getQuestObject()
        {
            return new Camera(this);
        }

    }
}
