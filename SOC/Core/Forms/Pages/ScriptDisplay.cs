using SOC.Classes.Common;
using SOC.QuestObjects.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SOC.Core.Forms.Pages
{
    public partial class ScriptDisplay : UserControl
    {
        private ObjectsDetails objectsDetails;

        public ScriptDisplay()
        {
            InitializeComponent();
        }

        public ScriptDisplay(ObjectsDetails objectsDetails)
        {
            this.objectsDetails = objectsDetails;
        }

        internal void RefreshScriptPanels(SetupDetails setupDetails, List<ObjectsDetail> objectsDetails)
        {
            //throw new NotImplementedException();
        }
    }
}
