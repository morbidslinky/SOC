using SOC.QuestObjects.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SOC.UI
{
    public abstract class QuestObjectBox : UserControl
    {

        public abstract QuestObject getQuestObject();

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // QuestObjectBox
            // 
            this.Name = "QuestObjectBox";
            this.ResumeLayout(false);

        }
    }
}
