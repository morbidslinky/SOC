using SOC.Classes.Common;
using SOC.Classes.Lua;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        public const string MISSION_CODE = "Mission";

        public EmbeddedScriptControl(ScriptControl parentControl)
        {
            InitializeComponent();
            Dock = DockStyle.Fill;
            ParentControl = parentControl;
        }

        public UserControl Menu(ScriptNode scriptNode)
        {
            ClearScriptNodePassthroughs();

            ScriptNode = scriptNode;
            ParentControl.SetMenuText(ScriptNode.ToString(), ScriptNode.Identifier.Value);

            UpdateMenu();

            return this;
        }

        private void ClearScriptNodePassthroughs()
        {
            if (ScriptNode != null && ScriptNode.Parent != null)
            {
                foreach (Choice choice in ScriptNode.GetAllChoicesContainingDependencies())
                {
                    if (choice.HasPassthrough(this))
                        choice.VariableNodeEventPassthrough -= VariableNodeEventPassthroughFunction;
                }
            }
        }

        private void UpdateMenu()
        {
            _isUpdatingControls = true;
            PopulateCodeMessageSenderControls();
            PopulateScriptalControls();
            _isUpdatingControls = false;
            textBoxDescription.Text = ScriptNode.Description;
        }

        private void PopulateCodeMessageSenderControls()
        {
            MessageSenderNode msgSenderNode = (MessageSenderNode)ScriptNode.Parent;
            CodeNode codeNode = (CodeNode)msgSenderNode.Parent;

            comboBoxCode.Items.Clear();
            comboBoxCode.Items.AddRange(ScriptControl.StrCode32Classes.Keys());
            var matchIndex = comboBoxCode.Items.IndexOf(codeNode.CodeKey);
            if (matchIndex == -1 && comboBoxCode.Items.Count > 0)
                matchIndex = 0;
            comboBoxCode.SelectedIndex = matchIndex;

            RefreshSenderOptions();
        }

        private ChoiceKeyValues[] GetChoosableValuesSets()
        {
            List<ChoiceKeyValues> chooseableSets = new List<ChoiceKeyValues>();

            chooseableSets.AddRange(new ChoiceKeyValues[] {
                new ChoiceKeyValues() { Key = StrCode32.NIL_LITERAL_KEY },
                new ChoiceKeyValues() { Key = ScriptControl.STRING_LITERAL_SET },
                new ChoiceKeyValues() { Key = ScriptControl.NUMBER_LITERAL_SET }
                }
            );

            chooseableSets.AddRange(ParentControl.Quest.GetAllObjectsScriptValueSets().ChoiceKeyValues.Where(set => set.Key != "Routes" && set.Key != "Event Default Arguments"));

            return chooseableSets.ToArray();
        }

        private void comboBoxSenderOptions_DropDown(object sender, EventArgs e)
        {
            RefreshSenderOptions();
        }

        private void RefreshSenderOptions()
        {
            MessageSenderNode msgSenderNode = (MessageSenderNode)ScriptNode.Parent;

            comboBoxSenderOptions.Items.Clear();
            comboBoxSenderOptions.Items.AddRange(GetChoosableValuesSets());
            var match = comboBoxSenderOptions.Items.OfType<ChoiceKeyValues>().FirstOrDefault(set => set.Key == msgSenderNode.SenderKey);
            if (match == null && comboBoxSenderOptions.Items.Count > 0)
                match = (ChoiceKeyValues)comboBoxSenderOptions.Items[0];
            comboBoxSenderOptions.SelectedItem = match;
        }


        private void comboBoxCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            MessageSenderNode msgSenderNode = (MessageSenderNode)ScriptNode.Parent;
            string selectedCode = (string)comboBoxCode.SelectedItem;
            var codeMessages = ScriptControl.StrCode32Classes.Get(selectedCode);

            comboBoxMessage.Items.Clear();
            comboBoxMessage.Items.AddRange(codeMessages.ToArray());

            var match = comboBoxMessage.Items.OfType<LuaValue>().FirstOrDefault(value => value.Matches(msgSenderNode.Message));
            if (selectedCode == MISSION_CODE)
            {
                comboBoxMessage.DropDownStyle = ComboBoxStyle.DropDown;
                comboBoxSenderOptions.Enabled = false;

                if (match != null)
                {
                    comboBoxMessage.SelectedItem = match;
                }
                else if (msgSenderNode.Message is LuaString text)
                {
                    comboBoxMessage.Text = text.TokenValue;
                    MoveSelectedScript(Create.Nil());
                }
            }
            else
            {
                comboBoxMessage.DropDownStyle = ComboBoxStyle.DropDownList;
                comboBoxSenderOptions.Enabled = true;

                if (match != null)
                {
                    comboBoxMessage.SelectedItem = match;
                }
                else if (comboBoxMessage.Items.Count > 0)
                {
                    comboBoxMessage.SelectedItem = (LuaValue)comboBoxMessage.Items[0];
                }
            }

            buttonApplyMessage.Enabled = false;
        }

        private void buttonApplyMessage_Click(object sender, EventArgs e)
        {
            MoveSelectedScript();
        }

        private void comboBoxMessage_TextUpdate(object sender, EventArgs e)
        {
            buttonApplyMessage.Enabled = true;
        }

        private void comboBoxMessage_SelectedIndexChanged(object sender, EventArgs e)
        {
            MoveSelectedScript();
        }

        private void comboBoxSenderOptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            MessageSenderNode msgSenderNode = (MessageSenderNode)ScriptNode.Parent;
            ChoiceKeyValues selectedSenderChoosableSet = (ChoiceKeyValues)comboBoxSenderOptions.SelectedItem;

            switch (selectedSenderChoosableSet.Key)
            {
                case ScriptControl.NUMBER_LITERAL_SET:
                    showCorrespondingSenderControl(numericUpDownSenders);
                    if (msgSenderNode.Sender is LuaNumber number) numericUpDownSenders.Value = (decimal)number.Value;
                    MoveSelectedScript(Create.Number((double)numericUpDownSenders.Value));
                    break;

                case ScriptControl.STRING_LITERAL_SET:
                    showCorrespondingSenderControl(textBoxSenders);
                    if (msgSenderNode.Sender is LuaString text) textBoxSenders.Text = text.Value;
                    MoveSelectedScript(Create.String(textBoxSenders.Text));
                    break;

                case StrCode32.NIL_LITERAL_KEY:
                    showCorrespondingSenderControl(null);
                    MoveSelectedScript(Create.Nil());
                    break;

                default:
                    showCorrespondingSenderControl(comboBoxSenders);
                    RefreshPresetSenders(selectedSenderChoosableSet.Values);
                    MoveSelectedScript((LuaValue)comboBoxSenders.SelectedItem);
                    break;
            }
        }

        private void showCorrespondingSenderControl(Control choiceControl, bool enable = true)
        {
            Control[] controlSet = { comboBoxSenders, textBoxSenders, numericUpDownSenders };

            foreach (var control in controlSet)
            {
                control.Visible = control == choiceControl;
            }

            if (choiceControl != null)
            {
                choiceControl.Enabled = enable;
            }

            labelSenderValue.Visible = choiceControl != null;
            buttonApplySender.Visible = choiceControl == textBoxSenders || choiceControl == numericUpDownSenders;
        }

        private void MoveSelectedScript(LuaValue Sender = null)
        {
            if (_isUpdatingControls) return;

            string selectedCode = (string)comboBoxCode.SelectedItem;

            LuaValue selectedMsg;
            if (comboBoxMessage.SelectedItem is LuaValue selectedLuaValue)
            {
                selectedMsg = selectedLuaValue;
            }
            else
            {
                selectedMsg = Create.String(comboBoxMessage.Text.Replace("\"", ""));
            }
            ChoiceKeyValues selectedSenderChoosableSet = (ChoiceKeyValues)comboBoxSenderOptions.SelectedItem;

            if (Sender == null)
            {
                switch (selectedSenderChoosableSet.Key)
                {
                    case ScriptControl.NUMBER_LITERAL_SET:
                        Sender = Create.Number((double)numericUpDownSenders.Value);
                        break;

                    case ScriptControl.STRING_LITERAL_SET:
                        Sender = Create.String(textBoxSenders.Text);
                        break;

                    case StrCode32.NIL_LITERAL_KEY:
                        Sender = Create.Nil();
                        break;

                    default:
                        Sender = (LuaValue)comboBoxSenders.SelectedItem;
                        break;
                }
            }

            var strCodeEvent = ScriptNode.getEvent();
            if (!strCodeEvent.Message.Matches(selectedMsg) || !strCodeEvent.SenderValue.Matches(Sender) || !strCodeEvent.CodeKey.Equals(selectedCode) || !strCodeEvent.SenderKey.Equals(selectedSenderChoosableSet.Key))
            {
                var movedNode = ScriptNode.GetStrCode32TableNode().MoveScript(ScriptNode, selectedCode, selectedMsg, selectedSenderChoosableSet.Key, Sender);
                ParentControl.treeViewScripts.SelectedNode = movedNode;
                ParentControl.RedrawScriptDependents();
                movedNode.ExpandAll();
            }
        }

        private void RefreshPresetSenders(List<LuaValue> values)
        {
            MessageSenderNode msgSenderNode = (MessageSenderNode)ScriptNode.Parent;

            comboBoxSenders.Items.Clear();
            comboBoxSenders.Items.AddRange(values.ToArray());

            var match = comboBoxSenders.Items.OfType<LuaValue>().FirstOrDefault(value => value.Matches(msgSenderNode.Sender));
            if (match == null && comboBoxSenders.Items.Count > 0)
                match = (LuaValue)comboBoxSenders.Items[0];

            comboBoxSenders.SelectedItem = match;
        }

        private void comboBoxSenders_DropDown(object sender, EventArgs e)
        {
            RefreshPresetSenders(ParentControl.Quest.GetAllObjectsScriptValueSets().Get(((ChoiceKeyValues)comboBoxSenderOptions.SelectedItem).Key));
        }

        private void comboBoxSenders_SelectedIndexChanged(object sender, EventArgs e)
        {
            MoveSelectedScript();
        }

        private void buttonApplySender_Click(object sender, EventArgs e)
        {
            MoveSelectedScript();
        }

        private void numericUpDownSenders_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                MoveSelectedScript();
        }
        private void textBoxSenders_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                MoveSelectedScript();
        }

        public void PopulateScriptalControls()
        {
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

        private void listBoxPreconditions_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateUpDownButtons(listBoxPreconditions, buttonUpPrecondition, buttonDownPrecondition);
        }

        private void listBoxOperations_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateUpDownButtons(listBoxOperations, buttonUpOperation, buttonDownOperation);
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

        private void textBoxDescription_TextChanged(object sender, EventArgs e)
        {
            ScriptNode.UpdateDescription(textBoxDescription.Text);
        }

        private void listBoxPreconditions_DoubleClick(object sender, EventArgs e)
        {
            ScriptalNode selectedNode = (ScriptalNode)listBoxPreconditions.SelectedItem;
            if (selectedNode != null)
            {
                ParentControl.treeViewScripts.SelectedNode = selectedNode;
                ParentControl.treeViewScripts.Focus();
            }
        }

        private void listBoxOperations_DoubleClick(object sender, EventArgs e)
        {
            ScriptalNode selectedNode = (ScriptalNode)listBoxOperations.SelectedItem;
            if (selectedNode != null)
            {
                ParentControl.treeViewScripts.SelectedNode = selectedNode;
                ParentControl.treeViewScripts.Focus();
            }
        }
    }
}
