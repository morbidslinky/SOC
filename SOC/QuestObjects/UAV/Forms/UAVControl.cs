using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SOC.QuestObjects.UAV
{
    public partial class UAVControl : UserControl
    {
        public UAVControl()
        {
            InitializeComponent();
            Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
        }

        public void SetMetadata(UAVsMetadata meta)
        {
            comboBox_ObjType.Text = meta.objectiveType;
        }
    }
}
