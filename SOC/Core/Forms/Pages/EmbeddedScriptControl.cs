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

        private TreeView TreeViewScripts;

        public EmbeddedScriptControl(TreeView treeViewScripts)
        {
            InitializeComponent();
            Dock = DockStyle.Fill;
            TreeViewScripts = treeViewScripts;
        }

        public void UpdateFromScript(UnEventedScriptNode selectedScriptNode)
        {
            var selectedScript = selectedScriptNode.ConvertToScript();
            _isUpdatingControls = true;

            comboBoxStrCodes.Items.Clear();
            comboBoxStrCodes.Items.AddRange(ScriptControl.MessageClassListMapping.Keys.ToArray());
            comboBoxStrCodes.Text = selectedScript.CodeEvent.StrCode32.Text;
            comboBoxStrMsgs.Text = selectedScript.CodeEvent.msg.Text;
            comboBoxStrSenders.Text = selectedScript.CodeEvent.sender.Text;

            textBoxDescription.Text = selectedScript.Description;

            listBoxPreconditions.Items.Clear();
            listBoxPreconditions.Items.AddRange(selectedScriptNode.PreconditionsParent.GetScriptals().ToArray());

            listBoxActions.Items.Clear();
            listBoxActions.Items.AddRange(selectedScriptNode.OperationsParent.GetScriptals().ToArray());

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
            var selectedNode = TreeViewScripts.SelectedNode;
            string selectedMsg = comboBoxStrMsgs.SelectedItem.ToString();
            string selectedCode = comboBoxStrCodes.SelectedItem.ToString();
            string selectedSender = "";//comboBoxStrSenders.SelectedItem.ToString(); not implemented yet. needs to grab (useful) senders from sideop details

            if (selectedNode is UnEventedScriptNode scriptNode)
            {
                var script = scriptNode.getEvent();
                if (script.msg.Text != selectedMsg || script.sender.Text != selectedSender || script.StrCode32.Text != selectedCode)
                {
                    TreeViewScripts.SelectedNode = scriptNode.GetStrCode32TableNode().MoveScript(scriptNode, selectedCode, selectedMsg, selectedSender);
                    TreeViewScripts.Focus();
                }
            }
        }

        private void textBoxDescription_TextChanged(object sender, EventArgs e)
        {
            _isUpdatingControls = true;

            var selectedNode = TreeViewScripts.SelectedNode;
            if (selectedNode is UnEventedScriptNode scriptNode)
            {
                scriptNode.UpdateDescription(textBoxDescription.Text);
            }

            _isUpdatingControls = false;
        }

        private void buttonSaveScript_Click(object sender, EventArgs e)
        {
            var selectedNode = TreeViewScripts.SelectedNode;
            if (selectedNode is UnEventedScriptNode scriptNode)
            {
                Script script = scriptNode.ConvertToScript();
                SaveScript(script);
            }
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
