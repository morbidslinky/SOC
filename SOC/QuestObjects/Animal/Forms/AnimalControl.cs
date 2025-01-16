using System.Windows.Forms;

namespace SOC.QuestObjects.Animal
{
    public partial class AnimalControl : UserControl
    {
        public AnimalControl()
        {
            InitializeComponent();
            comboBox_ObjType.SelectedIndex = 0;
            Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
        }

        internal void SetMetadata(AnimalsMetadata meta)
        {
            comboBox_ObjType.Text = meta.objectiveType;
        }
    }
}
