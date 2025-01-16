using System;
using System.Windows.Forms;

namespace SOC.QuestObjects.Vehicle
{
    public partial class VehicleControl : UserControl
    {
        public VehicleControl()
        {
            InitializeComponent();
            comboBox_ObjType.SelectedIndex = 0;
            Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
        }

        internal void SetMetadata(VehiclesMetadata meta)
        {
             comboBox_ObjType.Text = meta.ObjectiveType;
        }
    }
}
