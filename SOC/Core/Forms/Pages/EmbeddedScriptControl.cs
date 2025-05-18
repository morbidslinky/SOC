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
using static SOC.Classes.Lua.Choice;

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
            ClearScriptNodePassthroughs();
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
            listBoxPreconditions.Items.AddRange(ScriptNode.PreconditionsParent.Nodes.OfType<ScriptalNode>().ToArray());
            if (listBoxPreconditions.Items.Count > 0)
                listBoxPreconditions.SelectedIndex = 0;

            listBoxOperations.Items.Clear();
            listBoxOperations.Items.AddRange(ScriptNode.OperationsParent.Nodes.OfType<ScriptalNode>().ToArray());
            if (listBoxOperations.Items.Count > 0)
                listBoxOperations.SelectedIndex = 0;

            List<VariableNode> uniqueDependencies = new List<VariableNode>();
            foreach (Choice choice in ScriptNode.GetAllChoicesContainingDependencies())
            {
                if (uniqueDependencies.Contains(choice.Dependency)) continue;
                uniqueDependencies.Add(choice.Dependency);

                if (!choice.HasPassthrough(this))
                    choice.VariableNodeEventPassthrough += VariableNodeEventPassthroughFunction;
            }

            _isUpdatingControls = false;
        }

        public void VariableNodeEventPassthroughFunction(object sender, VariableNodeEventArgs e)
        {
            if (sender is Choice choice)
            {
                if (e.Doomed)
                {
                    choice.VariableNodeEventPassthrough -= VariableNodeEventPassthroughFunction;
                }

                RefreshListBoxDisplay(listBoxPreconditions);
                RefreshListBoxDisplay(listBoxOperations);
            }
        }

        private void RefreshListBoxDisplay(ListBox listBox)
        {
            _isUpdatingControls = true;
            for (int i = 0; i < listBox.Items.Count; i++)
            {
                listBox.Items[i] = listBox.Items[i];
            }
            _isUpdatingControls = false;
        }

        private void ClearScriptNodePassthroughs()
        {
            if (ScriptNode != null)
            {
                foreach (Choice choice in ScriptNode.GetAllChoicesContainingDependencies())
                {
                    if (choice.HasPassthrough(this))
                        choice.VariableNodeEventPassthrough -= VariableNodeEventPassthroughFunction;
                }
            }
        }

        private void UpdateUpDownButtons(ListBox listBoxScriptals, Button buttonUpOrder, Button buttonDownOrder)
        {
            if (listBoxScriptals.Items.Count > 0 && listBoxScriptals.SelectedIndex != -1)
            {
                buttonUpOrder.Enabled = listBoxScriptals.SelectedIndex > 0;
                buttonDownOrder.Enabled = listBoxScriptals.SelectedIndex < listBoxScriptals.Items.Count - 1;
            } 
            else
            {
                buttonUpOrder.Enabled = false; buttonDownOrder.Enabled = false;
            }
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

        public void MoveScriptal(ScriptalNode node, bool up)
        {
            ScriptalParentNode parent = (ScriptalParentNode)node.Parent;
            int currentIndex = parent.Nodes.IndexOf(node);
            int newIndex = currentIndex + (up ? -1 : 1);
            if (newIndex >= 0 && newIndex <= parent.Nodes.Count - 1)
            {
                parent.Nodes.Remove(node);
                parent.Nodes.Insert(newIndex, node);
            }

        }

        private void buttonUpPrecondition_Click(object sender, EventArgs e)
        {
            var scriptalNode = (ScriptalNode)listBoxPreconditions.SelectedItem;
            if (scriptalNode != null)
            {
                MoveScriptal(scriptalNode, true);
                listBoxPreconditions.Items.Clear();
                listBoxPreconditions.Items.AddRange(scriptalNode.Parent.Nodes.OfType<ScriptalNode>().ToArray());
                listBoxPreconditions.SelectedItem = scriptalNode;
            }
        }

        private void buttonDownPrecondition_Click(object sender, EventArgs e)
        {
            var scriptalNode = (ScriptalNode)listBoxPreconditions.SelectedItem;
            if (scriptalNode != null)
            {
                MoveScriptal(scriptalNode, false);
                listBoxPreconditions.Items.Clear();
                listBoxPreconditions.Items.AddRange(scriptalNode.Parent.Nodes.OfType<ScriptalNode>().ToArray());
                listBoxPreconditions.SelectedItem = scriptalNode;
            }
        }

        private void buttonDownOperation_Click(object sender, EventArgs e)
        {
            var scriptalNode = (ScriptalNode)listBoxOperations.SelectedItem;
            if (scriptalNode != null)
            {
                MoveScriptal(scriptalNode, false);
                listBoxOperations.Items.Clear();
                listBoxOperations.Items.AddRange(scriptalNode.Parent.Nodes.OfType<ScriptalNode>().ToArray());
                listBoxOperations.SelectedItem = scriptalNode;
            }
        }

        private void buttonUpOperation_Click(object sender, EventArgs e)
        {
            var scriptalNode = (ScriptalNode)listBoxOperations.SelectedItem;
            if (scriptalNode != null)
            {
                MoveScriptal(scriptalNode, true);
                listBoxOperations.Items.Clear();
                listBoxOperations.Items.AddRange(scriptalNode.Parent.Nodes.OfType<ScriptalNode>().ToArray());
                listBoxOperations.SelectedItem = scriptalNode;
            }
        }

        private void listBoxPreconditions_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateUpDownButtons(listBoxPreconditions, buttonUpPrecondition, buttonDownPrecondition);
        }

        private void listBoxOperations_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateUpDownButtons(listBoxOperations, buttonUpOperation, buttonDownOperation);
        }
    }
}
