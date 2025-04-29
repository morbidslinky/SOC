using System;
using System.Windows.Forms;
using SOC.Classes.Lua;
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

        private void AddVarNodesToVarTable(VariableNode node)
        {
            foreach (VariableNode child in node.Nodes) {
                AddVarNodesToVarTable(child);
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
        }

        private void buttonRemoveVariableIdentifier_Click(object sender, EventArgs e)
        {
            textBoxVarName.Text = "";
            TreeNode prevNode = treeViewVariables.SelectedNode.PrevNode;
            TreeNode parentNode = treeViewVariables.SelectedNode.Parent;

            treeViewVariables.SelectedNode.Remove();

            if (parentNode != null)
            {
                treeViewVariables.SelectedNode = parentNode;
            }
            else if (prevNode != null)
            {
                treeViewVariables.SelectedNode = prevNode;
            }
            else if (treeViewVariables.Nodes.Count > 0)
            {
                treeViewVariables.SelectedNode = treeViewVariables.Nodes[treeViewVariables.Nodes.Count - 1];
            } 
            else
            {
                textBoxVarName.Enabled = false;
                comboBoxVarType.Enabled = false;
                HideAllVarValueControls();
            }
        }

        private void buttonNewIdentifier_Click(object sender, EventArgs e)
        {
            VariableNode parentNode = (VariableNode)treeViewVariables.SelectedNode;
            parentNode.Entry.Value = new LuaTable();
            parentNode.UpdateText();

            int i = 1;
            while (parentNode.Nodes.ContainsKey($"{i}"))
            {
                i++;
            }

            VariableNode node = new VariableNode(Lua.TableEntry(i, Lua.Number(0)));
            parentNode.Nodes.Add(node);
            parentNode.Expand();
        }

        private void treeViewVariables_AfterSelect(object sender, TreeViewEventArgs e)
        {
            VariableNode node = (VariableNode)treeViewVariables.SelectedNode;
            comboBoxVarType.Enabled = true;
            textBoxVarName.Enabled = true;

            switch (node.Entry.Value.Type)
            {
                case TemplateRestrictionType.TEXT:
                    ShowVarTypeValueControl(textBoxVarTextValue);
                    textBoxVarTextValue.Text = node.Entry.Value.Value.Trim('"');
                    comboBoxVarType.Text = "TEXT";
                    break;
                case TemplateRestrictionType.NUMBER:
                    ShowVarTypeValueControl(numericUpDownVarNumberValue);
                    numericUpDownVarNumberValue.Value = decimal.Parse(node.Entry.Value.Value);
                    comboBoxVarType.Text = "NUMBER";
                    break;
                case TemplateRestrictionType.BOOLEAN:
                    ShowVarTypeValueControl(comboBoxVarBooleanValue);
                    comboBoxVarBooleanValue.Text = node.Entry.Value.Value;
                    comboBoxVarType.Text = "BOOLEAN";
                    break;
                case TemplateRestrictionType.TABLE:
                    ShowVarTypeValueControl(buttonNewIdentifier);
                    comboBoxVarType.Text = "TABLE";
                    break;
            }

            textBoxVarName.Text = node.Name.Trim('"');
        }

        private void ShowVarTypeValueControl(Control control)
        {
            Control[] valueControls = { textBoxVarTextValue, numericUpDownVarNumberValue, comboBoxVarBooleanValue, buttonNewIdentifier };

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
            Control[] valueControls = { textBoxVarTextValue, numericUpDownVarNumberValue, comboBoxVarBooleanValue, buttonNewIdentifier };
            foreach (Control valueControl in valueControls)
            {
                    valueControl.Visible = false;
            }
        }

        private void comboBoxVarType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBoxVarType.Text)
            {
                case "TEXT":
                    ShowVarTypeValueControl(textBoxVarTextValue);
                    break;
                case "NUMBER":
                    ShowVarTypeValueControl(numericUpDownVarNumberValue);
                    break;
                case "BOOLEAN":
                    ShowVarTypeValueControl(comboBoxVarBooleanValue);
                    break;
                case "TABLE":
                    ShowVarTypeValueControl(buttonNewIdentifier);
                    break;
            }
        }

        private void textBoxVarName_TextChanged(object sender, EventArgs e)
        {
            string userInputText = textBoxVarName.Text.Trim();

            VariableNode currentNode = (VariableNode)treeViewVariables.SelectedNode; 
            TreeNodeCollection siblings = currentNode.Parent != null ? currentNode.Parent.Nodes : treeViewVariables.Nodes;

            bool found = false;
            foreach (VariableNode node in siblings)
            {
                if (node.Entry.Key.Value.Trim('"') == userInputText && node != currentNode)
                {
                    found = true;
                    break;
                }
            }

            currentNode.Entry.Key = Lua.GetEntryValueType(userInputText);
            currentNode.Name = currentNode.Entry.Key.Value;
            currentNode.UpdateText();

            if (found)
            {
                string newName = GenerateNewNameForDuplicate(userInputText, siblings, currentNode);
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
        private string GenerateNewNameForDuplicate(string baseName, TreeNodeCollection siblingNodes, VariableNode currentNode)
        {
            int i = 1;
            while (treeViewVariables.Nodes.ContainsKey($@"""{baseName}_{i}"""))
            {
                i++;
            }

            return $"{baseName}_{i}";
        }

        private void textBoxTextVarValue_TextChanged(object sender, EventArgs e)
        {
            VariableNode currentNode = (VariableNode)treeViewVariables.SelectedNode;
            currentNode.Entry.Value = Lua.Text(textBoxVarTextValue.Text);
            currentNode.Nodes.Clear();
            currentNode.UpdateText();
        }

        private void comboBoxVarBooleanValue_SelectedIndexChanged(object sender, EventArgs e)
        {
            VariableNode currentNode = (VariableNode)treeViewVariables.SelectedNode;
            currentNode.Entry.Value = Lua.Boolean(comboBoxVarBooleanValue.Text);
            currentNode.Nodes.Clear();
            currentNode.UpdateText();
        }

        private void numericUpDownVarNumberValue_ValueChanged(object sender, EventArgs e)
        {
            VariableNode currentNode = (VariableNode)treeViewVariables.SelectedNode;
            currentNode.Entry.Value = Lua.Number((double)numericUpDownVarNumberValue.Value);
            currentNode.Nodes.Clear();
            currentNode.UpdateText();
        }

        private void textBoxName_KeyDown(object sender, KeyEventArgs e)
        {
            userPressedBackspace = e.KeyCode == Keys.Back;
        }
    }

    public class VariableNode : TreeNode
    {
        public LuaTableEntry Entry;
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

        public void UpdateText()
        {
            if (Entry.Value is LuaTable table)
            {
                Text = $"{Entry.Key.Value} :: {LuaTemplate.GetTemplateRestrictionTypeString(Entry.Value)}";
            }
            else
            {
                Text = $"{Entry.Key.Value} :: {LuaTemplate.GetTemplateRestrictionTypeString(Entry.Value)} :: {Entry.Value.Value}";
            }
        }
    }
}
