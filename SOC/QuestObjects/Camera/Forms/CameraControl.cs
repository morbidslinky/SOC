using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SOC.QuestObjects.Camera
{
    public partial class CameraControl : UserControl
    {
        public CameraControl()
        {
            InitializeComponent();
            Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
        }

        public void SetMetadata(CamerasMetadata meta)
        {
            comboBox_ObjType.Text = meta.objectiveType;
        }
    }
}
