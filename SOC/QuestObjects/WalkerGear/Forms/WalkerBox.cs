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

namespace SOC.QuestObjects.WalkerGear
{
    public partial class WalkerBox : QuestObjectBox
    {
        public int ID;

        public WalkerBox(WalkerGear qObject)
        {
            InitializeComponent();
            ID = qObject.ID;
            groupBox_main.Text = qObject.GetObjectName();

            textBox_xcoord.Text = qObject.position.coords.xCoord;
            textBox_ycoord.Text = qObject.position.coords.yCoord;
            textBox_zcoord.Text = qObject.position.coords.zCoord;
            textBox_rot.Text = qObject.position.rotation.GetDegreeRotY();

            checkBox_target.Checked = qObject.isTarget;

            comboBox_pilot.Items.AddRange(new object[] {
                "NONE",
                "sol_quest_0000",
                "sol_quest_0001",
                "sol_quest_0002",
                "sol_quest_0003",
                "sol_quest_0004",
                "sol_quest_0005",
                "sol_quest_0006",
                "sol_quest_0007"
            });
            comboBox_pilot.Text = qObject.pilot;

            comboBox_paint.Items.AddRange(new object[] {
                "SOVIET",
                "ROGUE_COYOTE",
                "CFA",
                "ZRS",
                "DDOGS"
            });
            comboBox_paint.Text = qObject.paint;

            comboBox_weapon.Items.AddRange(new object[] {
                "WG_MACHINEGUN",
                "WG_MISSILE"
            });
            comboBox_weapon.Text = qObject.weapon;
        }

        public override QuestObject getQuestObject()
        {
            return new WalkerGear(this);
        }
    }
}
