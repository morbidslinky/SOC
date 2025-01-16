using SOC.Classes.Common;
using SOC.QuestObjects.Common;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace SOC.UI
{
    public partial class DetailDisplay : UserControl
    {
        ObjectsDetails managers;

        public DetailDisplay(ObjectsDetails _managers)
        {
            InitializeComponent();
            Dock = DockStyle.Fill;
            managers = _managers;
            flowPanelDetails.Controls.AddRange(managers.GetModulePanels());
        }

        public void RefreshObjectPanels(SetupDetails setupDetails)
        {
            managers.RefreshAllPanels(setupDetails);
        }

        private void flowPanelDetails_Layout(object sender, LayoutEventArgs e)
        {
            labelFlowHeight.Height = flowPanelDetails.Height - 18;
        }
    }
}
