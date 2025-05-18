using SOC.Classes.Common;
using SOC.Classes.Lua;
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
        private bool _isUpdatingControls = false;
        ScriptControl ParentControl;
        ScriptNode ScriptNode;

        public EmbeddedScriptControl(ScriptControl parentControl)
        {
            InitializeComponent();
            Dock = DockStyle.Fill;
            ParentControl = parentControl;
        }

        public override string ToString() => ScriptNode.ToString();

        public UserControl Menu(ScriptNode scriptNode)
        {
            ScriptNode = scriptNode;
            ParentControl.SetMenuText(ToString(), ScriptNode.Identifier.Text);
            UpdateMenu();
            return this;
        }

        private void UpdateMenu()
        {
            var selectedScript = ScriptNode.ConvertToScript();
            _isUpdatingControls = true;

            comboBoxStrCodes.Items.Clear();
            comboBoxStrCodes.Items.AddRange(ScriptControl.MessageClassListMapping.Keys.ToArray());
            comboBoxStrCodes.Text = selectedScript.CodeEvent.StrCode32.Text;
            comboBoxStrMsgs.Text = selectedScript.CodeEvent.msg.Text;
            comboBoxStrSenders.Text = selectedScript.CodeEvent.sender.Text;

            textBoxDescription.Text = selectedScript.Description;

            listBoxPreconditions.Items.Clear();
            listBoxPreconditions.Items.AddRange(selectedScript.Preconditionals.ToArray());

            listBoxOperations.Items.Clear();
            listBoxOperations.Items.AddRange(selectedScript.Operationals.ToArray());

            _isUpdatingControls = false;
        }

        private void comboBoxStrCodes_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedCode = comboBoxStrCodes.SelectedItem.ToString();
            comboBoxStrMsgs.Items.Clear();

            if (ScriptControl.MessageClassListMapping.ContainsKey(selectedCode))
            {
                comboBoxStrMsgs.Items.AddRange(ScriptControl.MessageClassListMapping[selectedCode].ToArray());
            }

            if (comboBoxStrMsgs.Items.Count > 0)
            {
                comboBoxStrMsgs.SelectedIndex = 0;
            }

            MoveSelectedScript();
        }

        private void comboBoxStrMsgs_SelectedIndexChanged(object sender, EventArgs e)
        {
            MoveSelectedScript();
        }

        private void comboBoxStrSenders_SelectedIndexChanged(object sender, EventArgs e)
        {
            MoveSelectedScript();
        }

        private void MoveSelectedScript()
        {
            if (_isUpdatingControls) return;
            string selectedMsg = comboBoxStrMsgs.SelectedItem.ToString();
            string selectedCode = comboBoxStrCodes.SelectedItem.ToString();
            string selectedSender = "";//comboBoxStrSenders.SelectedItem.ToString(); not implemented yet. needs to grab (useful) senders from sideop details

            var script = ScriptNode.getEvent();
            if (script.msg.Text != selectedMsg || script.sender.Text != selectedSender || script.StrCode32.Text != selectedCode)
            {
                var movedNode = ScriptNode.GetStrCode32TableNode().MoveScript(ScriptNode, selectedCode, selectedMsg, selectedSender);
                ParentControl.treeViewScripts.SelectedNode = movedNode;
                ParentControl.RedrawScriptDependents();
                movedNode.ExpandAll();
            }
        }

        private void textBoxDescription_TextChanged(object sender, EventArgs e)
        {
            _isUpdatingControls = true;
            ScriptNode.UpdateDescription(textBoxDescription.Text);
            _isUpdatingControls = false;
        }

        private void buttonSaveScript_Click(object sender, EventArgs e)
        {
            SaveScript(ScriptNode.ConvertToScript());
        }

        public static void SaveScript(Script script)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "Xml File|*.xml";
            saveFile.FileName = script.Identifier.Text;
            DialogResult saveResult = saveFile.ShowDialog();

            if (saveResult == DialogResult.OK)
            {
                script.WriteToXml(saveFile.FileName);
                MessageBox.Show("Done!", "Script Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
