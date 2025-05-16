using System;
using System.Collections;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SOC.Classes.Lua;
using static SOC.Classes.Lua.Choice;
using static SOC.Classes.Lua.LuaValue;

namespace SOC.UI
{
    public partial class ScriptControl : UserControl
    {
        private bool userPressedBackspace = false;

        private void ClearVarTables(VariableNode node)
        {
            foreach (VariableNode child in node.Nodes)
            {
                ClearVarTables(child);
            }

            if (node.Parent != null)
            {
                var parentNode = (VariableNode)node.Parent;
                parentNode.Entry.Value = new LuaTable();
            }
        }

        private void AddVariableNode(VariableNode node)
        {
            foreach (VariableNode child in node.Nodes) {
                AddVariableNode(child);
            }

            if (node.Parent != null)
            {
                var parentNode = (VariableNode)node.Parent;
                var parentNodeEntry = (LuaTable)parentNode.Entry.Value;
                parentNodeEntry.TryAdd(node.Entry);
            }
        }

        private void buttonNewVariable_Click(object sender, EventArgs e)
        {
            int i = 1;
            while (treeViewVariables.Nodes.ContainsKey($@"""UserVariable_{i}"""))
            {
                i++;
            }
            VariableNode node = new VariableNode(Lua.TableEntry($"UserVariable_{i}", Lua.Number(0)));
            treeViewVariables.Nodes.Add(node);
            treeViewVariables.SelectedNode = node;
            treeViewVariables.Focus();

            ScriptalEmbed.UpdateVarNodesUI();
        }

        private void buttonRemoveVariableIdentifier_Click(object sender, EventArgs e)
        {
            if (treeViewVariables.SelectedNode == null) { return; }

            var selectedNode = (VariableNode)treeViewVariables.SelectedNode;

            selectedNode.NotifyDependentsOfVariableNodeChange(true);
            selectedNode.Remove();

            if (treeViewVariables.SelectedNode == null)
            {
                textBoxVarName.Text = "";
                textBoxVarName.Enabled = false;
                comboBoxVarType.Enabled = false;
                buttonRemoveVariableIdentifier.Enabled = false;
                HideAllVarValueControls();

                ScriptalEmbed.UpdateVarNodesUI();
            }
        }

        private void buttonNewIdentifier_Click(object sender, EventArgs e)
        {
            VariableNode parentNode = (VariableNode)treeViewVariables.SelectedNode;

            int i = 1;
            while (parentNode.Nodes.ContainsKey($"{i}"))
            {
                i++;
            }

            VariableNode node = new VariableNode(Lua.TableEntry(i, Lua.Number(0)));
            parentNode.Nodes.Add(node);
            parentNode.Expand();
        }

        private void UpdateVariableControlsToSelectedNode()
        {
            VariableNode node = (VariableNode)treeViewVariables.SelectedNode;
            if (node == null)
            {
                comboBoxVarType.Enabled = false;
                textBoxVarName.Enabled = false;
                buttonRemoveVariableIdentifier.Enabled = false;
                return;
            }
                
            comboBoxVarType.Enabled = true;
            textBoxVarName.Enabled = true;
            buttonRemoveVariableIdentifier.Enabled = true;

            switch (node.Entry.Value.Type)
            {
                case TemplateRestrictionType.STRING:
                    textBoxVarStringValue.Text = node.Entry.Value.Value.Trim('"');
                    comboBoxVarType.Text = "STRING";
                    break;
                case TemplateRestrictionType.NUMBER:
                    numericUpDownVarNumberValue.Value = decimal.Parse(node.Entry.Value.Value);
                    comboBoxVarType.Text = "NUMBER";
                    break;
                case TemplateRestrictionType.BOOLEAN:
                    radioButtonTrue.Checked = node.Entry.Value.Value == "true";
                    comboBoxVarType.Text = "BOOLEAN";
                    break;
                case TemplateRestrictionType.TABLE:
                    comboBoxVarType.Text = "TABLE";
                    break;
            }

            textBoxVarName.Text = node.Name.Trim('"');
        }

        private void treeViewVariables_AfterSelect(object sender, TreeViewEventArgs e)
        {
            UpdateVariableControlsToSelectedNode();
        }

        private void ShowVarTypeValueControl(Control control)
        {
            Control[] valueControls = { textBoxVarStringValue, numericUpDownVarNumberValue, panelBoolean, buttonNewIdentifier };

            foreach (Control valueControl in valueControls)
            {
                if (valueControl == control)
                {
                    valueControl.Visible = true;
                }
                else
                {
                    valueControl.Visible = false;
                }
            }
        }

        private void HideAllVarValueControls()
        {
            Control[] valueControls = { textBoxVarStringValue, numericUpDownVarNumberValue, panelBoolean, buttonNewIdentifier };
            foreach (Control valueControl in valueControls)
            {
                    valueControl.Visible = false;
            }
        }

        private void comboBoxVarType_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateNode();
        }

        private void textBoxVarName_TextChanged(object sender, EventArgs e)
        {
            string userInputText = textBoxVarName.Text.Trim();

            VariableNode currentNode = (VariableNode)treeViewVariables.SelectedNode;
            if (currentNode == null) return;

            TreeNodeCollection siblings = currentNode.Parent != null ? currentNode.Parent.Nodes : treeViewVariables.Nodes;

            bool found = VariableNameExists(userInputText, siblings, currentNode);

            currentNode.Entry.Key = Lua.GetEntryValueType(userInputText);
            currentNode.Name = currentNode.Entry.Key.Value;
            currentNode.UpdateText();

            if (found)
            {
                string newName = GetUniqueVariableName(userInputText);
                int addedCharCount = newName.Length - textBoxVarName.Text.Length;

                textBoxVarName.Text = newName; 
                int selectionStart = newName.Length - addedCharCount;
                int selectionLength = addedCharCount;

                if (userPressedBackspace && selectionStart > 0)
                {
                    selectionStart--;
                    selectionLength++;
                }

                textBoxVarName.SelectionStart = selectionStart;
                textBoxVarName.SelectionLength = selectionLength;
            }
        }
        internal string GetUniqueVariableName(string baseName)
        {
            int i = 1;
            while (treeViewVariables.Nodes.ContainsKey($@"""{baseName}_{i}"""))
            {
                i++;
            }

            return $"{baseName}_{i}";
        }

        internal bool VariableNameExists(string baseName, TreeNodeCollection nodes, TreeNode except = null)
        {
            foreach (VariableNode node in nodes)
            {
                if (node == except)
                {
                    continue;
                }
                if (node.Entry.Key.Value.Trim('"') == baseName)
                {
                    return true;
                }
            }
            return false;
        }

        private void textBoxTextVarValue_TextChanged(object sender, EventArgs e)
        {
            UpdateNode();
        }

        private void numericUpDownVarNumberValue_ValueChanged(object sender, EventArgs e)
        {
            UpdateNode();
        }

        private void radioButtonFalse_CheckedChanged(object sender, EventArgs e)
        {
            UpdateNode();
        }

        private void UpdateNode()
        {
            VariableNode currentNode = (VariableNode)treeViewVariables.SelectedNode;
            switch (comboBoxVarType.Text)
            {
                case "STRING":
                    ShowVarTypeValueControl(textBoxVarStringValue);
                    currentNode.Nodes.Clear();
                    currentNode.Entry.Value = Lua.String(textBoxVarStringValue.Text);
                    break;
                case "NUMBER":
                    ShowVarTypeValueControl(numericUpDownVarNumberValue);
                    currentNode.Nodes.Clear();
                    currentNode.Entry.Value = Lua.Number((double)numericUpDownVarNumberValue.Value);
                    break;
                case "BOOLEAN":
                    ShowVarTypeValueControl(panelBoolean);
                    currentNode.Nodes.Clear();
                    currentNode.Entry.Value = Lua.Boolean(radioButtonTrue.Checked);
                    break;
                case "TABLE":
                    ShowVarTypeValueControl(buttonNewIdentifier);
                    currentNode.Entry.Value = new LuaTable();
                    break;
            }

            currentNode.UpdateText();

            ScriptalEmbed.UpdateVarNodesUI();
        }

        private void textBoxName_KeyDown(object sender, KeyEventArgs e)
        {
            userPressedBackspace = e.KeyCode == Keys.Back;
        }
    }
    public class VariableNode : TreeNode
    {
        public LuaTableEntry Entry;

        public event EventHandler<VariableNodeEventArgs> VariableNodeEvent;

        public VariableNode(LuaTableEntry entry)
        {
            Entry = entry;
            Name = Entry.Key.Value;
            if (entry.Value is LuaTable table)
            {
                foreach (var nestedEntry in table.KeyValuePairs)
                {
                    Nodes.Add(new VariableNode(nestedEntry));
                }
            }
            UpdateText();
        }

        public LuaTableIdentifier ToLuaTableIdentifier() => ConvertToLuaTableIdentifier(Entry);

        public static LuaTableIdentifier ConvertToLuaTableIdentifier(LuaTableEntry entry)
        {
            return new LuaTableIdentifier("qvars", entry.Value.Type, entry.Key);
        }

        public override string ToString() => Entry.Key.Value;


        public void UpdateText()
        {
            Text = Entry.ToString();

            NotifyDependentsOfVariableNodeChange(false);
        }

        public void NotifyDependentsOfVariableNodeChange(bool isDoomed)
        {
            VariableNodeEvent?.Invoke(this, new VariableNodeEventArgs { Doomed = isDoomed });
        }
    }
}
