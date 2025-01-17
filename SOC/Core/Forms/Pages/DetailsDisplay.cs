using SOC.Classes.Common;
using SOC.QuestObjects.Common;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace SOC.UI
{
    public partial class DetailsDisplay : UserControl
    {
        ObjectsDetails objectsDetails;

        public DetailsDisplay(ObjectsDetails _managers)
        {
            InitializeComponent();
            Dock = DockStyle.Fill;
            objectsDetails = _managers;
            flowPanelDetails.Controls.AddRange(objectsDetails.GetModulePanels());
        }

        public void RefreshObjectPanels(SetupDetails setupDetails)
        {
            objectsDetails.RefreshAllPanels(setupDetails);
        }

        private void flowPanelDetails_Layout(object sender, LayoutEventArgs e)
        {
            labelFlowHeight.Height = flowPanelDetails.Height - 18;
        }
    }
}
