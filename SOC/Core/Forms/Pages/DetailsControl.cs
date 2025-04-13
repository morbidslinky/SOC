using SOC.Classes.Common;
using SOC.QuestObjects.Common;
using System.Windows.Forms;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Linq;

namespace SOC.UI
{
    public partial class DetailsControl : UserControl
    {
        Quest Quest;

        public DetailsControl(Quest quest)
        {
            InitializeComponent();
            Dock = DockStyle.Fill;
            Quest = quest;
            flowPanelDetails.Controls.AddRange(GetDetailPanels());
        }

        private UserControl[] GetDetailPanels()
        {
            return Quest.ObjectsDetails.Details.Select(detail => detail.GetControlPanel().detailControl).ToArray();
        }

        private void flowPanelDetails_Layout(object sender, LayoutEventArgs e)
        {
            labelFlowHeight.Height = flowPanelDetails.Height - 18;
        }

        public void SyncQuestDataToUserInput()
        {
            Quest.ObjectsDetails.Details = Quest.ObjectsDetails.Details.Select(detail => detail.GetControlPanel().GetDetailFromControl()).ToList();
            Quest.RefreshAllStubTexts();
        }
    }
}
