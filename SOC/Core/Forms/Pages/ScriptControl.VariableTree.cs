﻿using SOC.Classes.Lua;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using static SOC.Classes.Lua.LuaValue;

namespace SOC.UI
{
    public partial class ScriptControl : UserControl
    {
        private bool userPressedBackspace = false; 


        private void buttonNewVariable_Click(object sender, EventArgs e)
        {
            int i = 1;
            while (treeViewVariables.Nodes.ContainsKey($@"""UserVariable_{i}"""))
            {
                i++;
            }
            VariableNode node = new VariableNode(Create.TableEntry($"UserVariable_{i}", Create.String("Add Text Here")));
            treeViewVariables.Nodes.Add(node);
            treeViewVariables.SelectedNode = node;

            UpdateEmbeddedScriptSetDisplay();
            treeViewVariables.Focus();
        }

        private void buttonRemoveVariableIdentifier_Click(object sender, EventArgs e)
        {
            DeleteSelectedVariable();
        }

        private void treeViewVariables_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DeleteSelectedVariable();
            }
        }

        private void DeleteSelectedVariable()
        {

            if (treeViewVariables.SelectedNode == null) { return; }

            var selectedNode = (VariableNode)treeViewVariables.SelectedNode;
            TreeNodeCollection siblings = selectedNode.Parent == null ? treeViewVariables.Nodes : selectedNode.Parent.Nodes;

            selectedNode.Remove();

            if (selectedNode.Entry.Key is LuaNumber n)
            {
                var i = n.Value;
                for (int j = 0; j < siblings.Count; j++)
                {
                    VariableNode next = siblings.OfType<VariableNode>().FirstOrDefault(node => node.Entry.Key is LuaNumber m && m.Value == i + 1);
                    if (next == null)
                    {
                        break;
                    }
                    next.Entry.Key = Create.Number(i);
                    next.Name = next.Entry.Key.TokenValue;
                    next.UpdateText();
                    i++;
                }
            }
            UpdateVariableControlsToSelectedNode();

            if (treeViewVariables.SelectedNode == null)
            {
                textBoxVarName.Text = "";
                textBoxVarName.Enabled = false;
                comboBoxVarType.Enabled = false;
                buttonRemoveVariableIdentifier.Enabled = false;
                ShowVarTypeValueControl(panelPlaceholder);

                UpdateEmbeddedScriptSetDisplay();
            }

            selectedNode.NotifyDependentsOfVariableNodeChange(true);
        }

        private void buttonNewIdentifier_Click(object sender, EventArgs e)
        {
            VariableNode parentNode = (VariableNode)treeViewVariables.SelectedNode;
            foreach (LuaValue value in ((ChoiceKeyValues)comboBoxTableAddOptions.SelectedItem).Values)
            {
                int i = 1;
                while (parentNode.Nodes.ContainsKey($"{i}"))
                {
                    i++;
                }

                VariableNode node = new VariableNode(Create.TableEntry(i, value));
                parentNode.Nodes.Add(node);
            }
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

            _isUpdatingControls = true;
            switch (node.Entry.Value.Type)
            {
                case TemplateRestrictionType.STRING:
                    textBoxVarStringValue.Text = node.Entry.Value.TokenValue.Trim('"');
                    comboBoxVarType.Text = "STRING";
                    break;
                case TemplateRestrictionType.NUMBER:
                    textBoxVarNumberValue.Text = node.Entry.Value.TokenValue;
                    comboBoxVarType.Text = "NUMBER";
                    break;
                case TemplateRestrictionType.BOOLEAN:
                    radioButtonTrue.Checked = node.Entry.Value.TokenValue == "true";
                    comboBoxVarType.Text = "BOOLEAN";
                    break;
                case TemplateRestrictionType.TABLE:
                    comboBoxVarType.Text = "TABLE";
                    break;
            }
            _isUpdatingControls = false;

            textBoxVarName.Text = node.Name.Trim('"');
            UpdateNode();
        }

        private void treeViewVariables_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (_isUpdatingControls) return;
            UpdateVariableControlsToSelectedNode();
            RedrawScriptDependents();
        }

        public void RedrawScriptDependents()
        {
            ResetNodeUnderlinesRecursive(ScriptTablesRootNode);
            if (treeViewVariables.SelectedNode != null)
                ((VariableNode)treeViewVariables.SelectedNode).MarkDependents();
        }

        private void ResetNodeUnderlinesRecursive(TreeNode node)
        {
            if (node.NodeFont != REGULAR)
            {
                node.NodeFont = node is ScriptNode ? BOLD : REGULAR;
            }
            foreach (TreeNode child in node.Nodes)
                ResetNodeUnderlinesRecursive(child);
        }

        private void ShowVarTypeValueControl(Control control)
        {
            Control[] valueControls = { textBoxVarStringValue, textBoxVarNumberValue, panelBoolean, panelNewIdentifier, panelPlaceholder };

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

        private void comboBoxVarType_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateNode();
        }

        private void textBoxVarName_TextChanged(object sender, EventArgs e)
        {
            string userInputText = textBoxVarName.Text.Trim();

            VariableNode selectedNode = (VariableNode)treeViewVariables.SelectedNode;
            if (selectedNode == null) return;

            TreeNodeCollection siblings = selectedNode.Parent == null ? treeViewVariables.Nodes : selectedNode.Parent.Nodes;

            bool found = VariableNameExists(userInputText, siblings, selectedNode);

            selectedNode.Entry.Key = Create.GetEntryValueType(userInputText);
            selectedNode.Name = selectedNode.Entry.Key.TokenValue;
            selectedNode.UpdateText();

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

            UpdateEmbeddedScriptSetDisplay();
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
                if (node.Entry.Key.TokenValue.Trim('"') == baseName)
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

        private void radioButtonFalse_CheckedChanged(object sender, EventArgs e)
        {
            UpdateNode();
        }

        private void textBoxVarNumberValue_TextChanged(object sender, EventArgs e)
        {
            UpdateNode();
        }

        private void textBoxVarNumberValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            char keyChar = e.KeyChar;

            if (char.IsControl(keyChar))
            {
                return;
            }

            if (textBox.SelectionStart == 0 && textBox.SelectionLength == 0 && textBox.Text.Contains("-"))
            {
                textBox.SelectionStart = 1;
            }

            if (keyChar == '-')
            {
                if (!textBox.Text.Contains('-'))
                {
                    textBox.Text = $"-{textBox.Text}";
                }
                e.Handled = true;
                return;
            }

            if (char.IsDigit(keyChar))
            {
                return;
            }

            if (keyChar == '.')
            {
                if (textBox.Text.Contains('.'))
                {
                    e.Handled = true;
                    return;
                }
                return;
            }

            e.Handled = true;
        }

        private void UpdateNode()
        {
            if (_isUpdatingControls) return;

            VariableNode currentNode = (VariableNode)treeViewVariables.SelectedNode;
            switch (comboBoxVarType.Text)
            {
                case "STRING":
                    ShowVarTypeValueControl(textBoxVarStringValue);
                    currentNode.Nodes.Clear();
                    currentNode.Entry.Value = Create.String(textBoxVarStringValue.Text);
                    break;
                case "NUMBER":
                    ShowVarTypeValueControl(textBoxVarNumberValue);
                    currentNode.Nodes.Clear();
                    if (string.IsNullOrEmpty(textBoxVarNumberValue.Text))
                    {
                        textBoxVarNumberValue.Text = "0";
                        textBoxVarNumberValue.SelectAll();
                    }
                    currentNode.Entry.Value = Create.Number(textBoxVarNumberValue.Text);
                    break;
                case "BOOLEAN":
                    ShowVarTypeValueControl(panelBoolean);
                    currentNode.Nodes.Clear();
                    currentNode.Entry.Value = Create.Boolean(radioButtonTrue.Checked);
                    break;
                case "TABLE":
                    ShowVarTypeValueControl(panelNewIdentifier);
                    currentNode.Entry.Value = new LuaTable();
                    break;
            }

            currentNode.UpdateText();
            UpdateEmbeddedScriptSetDisplay();
        }

        private void textBoxName_KeyDown(object sender, KeyEventArgs e)
        {
            userPressedBackspace = e.KeyCode == Keys.Back;
        }

        private void panelNewIdentifier_VisibleChanged(object sender, EventArgs e)
        {
            if (panelNewIdentifier.Visible) {
                comboBoxTableAddOptions.Items.Clear();
                comboBoxTableAddOptions.Items.Add(new ChoiceKeyValues("New Table Value") { Values = new List<LuaValue>() { new LuaString("Add Text Here") } });
                comboBoxTableAddOptions.Items.AddRange(Quest.GetAllObjectsScriptValueSets().ChoiceKeyValues.Where(set => set.Key != "Event Default Arguments").ToArray());
                comboBoxTableAddOptions.SelectedIndex = 0;
            }
        }
    }
    public class VariableNode : TreeNode
    {
        public LuaTableEntry Entry;

        public List<Choice> Dependents = new List<Choice>();

        public VariableNode(LuaTableEntry entry)
        {
            Entry = entry;
            Name = Entry.Key.TokenValue;
            if (entry.Value is LuaTable table)
            {
                foreach (var nestedEntry in table.KeyValuePairs)
                {
                    Nodes.Add(new VariableNode(nestedEntry));
                }
            }
            UpdateText();
        }

        public static VariableNode ExtrapolateDefault(LuaTableIdentifier identifier)
        {
            LuaValue key = identifier.IdentifierKeys.Last();
            switch(identifier.EvaluatesTo)
            {
                case TemplateRestrictionType.STRING:
                    return new VariableNode(Create.TableEntry(key, ""));
                case TemplateRestrictionType.BOOLEAN:
                    return new VariableNode(Create.TableEntry(key, false));
                case TemplateRestrictionType.NUMBER:
                    return new VariableNode(Create.TableEntry(key, 0));
                case TemplateRestrictionType.TABLE:
                    return new VariableNode(Create.TableEntry(key, new LuaTable()));
            }

            return null;
        }

        public LuaTableEntry GetEntry()
        {
            if (Entry.Value is LuaTable)
            {
                LuaTableEntry flattenedTable = Create.TableEntry(Entry.Key, BuildTableFromChildNodes(Nodes));
                return flattenedTable;
            }

            return Entry;
        }

        private static LuaTable BuildTableFromChildNodes(TreeNodeCollection nodes)
        {
            LuaTable table = new LuaTable();
            foreach (VariableNode childNode in nodes)
            {
                if (childNode.Entry.Value is LuaTable)
                    table.TryAdd(Create.TableEntry(childNode.Entry.Key, BuildTableFromChildNodes(childNode.Nodes)));
                else
                    table.TryAdd(childNode.Entry);
            }

            return table;
        }

        public LuaTableIdentifier ToLuaTableIdentifier() => ConvertToLuaTableIdentifier(Entry);

        public static LuaTableIdentifier ConvertToLuaTableIdentifier(LuaTableEntry entry)
        {
            return new LuaTableIdentifier("qvars", entry.Value.Type, entry.Key);
        }

        public override string ToString() => Entry.Key.TokenValue;


        public void UpdateText()
        {
            Text = Entry.ToString();

            NotifyDependentsOfVariableNodeChange(false);
        }

        public void NotifyDependentsOfVariableNodeChange(bool isDoomed)
        {
            for (int i = Dependents.Count - 1; i >= 0; i--)
            {
                var dependent = Dependents[i];
                if (isDoomed || !dependent.CorrespondingRuntimeToken.Allows(Entry.Value, out _))
                {
                    dependent.ClearVarNodeDependency();
                } 
                else
                {
                    dependent.RefreshValue();
                }
            }
        }

        internal void MarkDependents()
        {
            foreach (ScriptalNode scriptalNode in Dependents.Select(dependent => dependent.ParentScriptalNode))
            {
                if (scriptalNode.Parent == null) continue;

                scriptalNode.NodeFont = ScriptControl.UNDERLINE;
                scriptalNode.Parent.NodeFont = ScriptControl.UNDERLINE;
                scriptalNode.Parent.Parent.NodeFont = ScriptControl.UNDERLINE_BOLD;
            }
            
        }
    }
}
