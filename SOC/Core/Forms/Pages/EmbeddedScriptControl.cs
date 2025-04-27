using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SOC.UI
{
    public partial class EmbeddedScriptControl : UserControl
    {
        public EmbeddedScriptControl(UnEventedScriptNode scriptNode)
        {
            InitializeComponent();
            Dock = DockStyle.Fill;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void EmbeddedScriptControl_Load(object sender, EventArgs e)
        {

        }
    }
}
