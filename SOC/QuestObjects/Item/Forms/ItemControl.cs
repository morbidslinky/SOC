using System.Windows.Forms;

namespace SOC.QuestObjects.Item
{
    public partial class ItemControl : UserControl
    {
        public ItemControl()
        {
            InitializeComponent();
            comboBox_ObjType.SelectedIndex = 0;
            Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
        }

        internal void SetMetadata(ItemsMetadata meta)
        {
            comboBox_ObjType.Text = meta.objectiveType;
        }
    }
}
