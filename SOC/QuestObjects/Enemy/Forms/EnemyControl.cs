using System.Collections.Generic;
using System.Windows.Forms;

namespace SOC.QuestObjects.Enemy
{
    public partial class EnemyControl : UserControl
    {
        public EnemyControl()
        {
            InitializeComponent();
            comboBox_ObjType.SelectedIndex = 0;
            Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
        }

        internal void SetMetadata(EnemiesMetadata meta, List<string> subtypes)
        {
            comboBox_ObjType.Text = meta.objectiveType;

            comboBox_Subtype.Items.Clear();
            comboBox_Subtype.Items.AddRange(subtypes.ToArray());
            if (comboBox_Subtype.Items.Contains(meta.subtype))
                comboBox_Subtype.Text = meta.subtype;
            else if (comboBox_Subtype.Items.Count > 0)
                comboBox_Subtype.SelectedIndex = 0;
        }
    }
}
