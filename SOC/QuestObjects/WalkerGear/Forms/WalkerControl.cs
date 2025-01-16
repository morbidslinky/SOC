using System.Windows.Forms;

namespace SOC.QuestObjects.WalkerGear
{
    public partial class WalkerControl : UserControl
    {
        public WalkerControl()
        {
            InitializeComponent();
            comboBox_ObjType.SelectedIndex = 0;
            Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
        }

        public void SetMetadata(WalkerGearsMetadata meta)
        {
            comboBox_ObjType.Text = meta.objectiveType;
        }
    }
}
