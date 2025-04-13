using SOC.QuestObjects.Common;
using SOC.Core.Classes.Route;
using System;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Collections.Generic;
using SOC.Classes.Common;
using System.Linq;
using SOC.Classes.Lua;
using static SOC.Classes.Lua.LuaValue;
using SOC.QuestObjects.Enemy;

namespace SOC.UI
{
    public partial class ScriptControl : UserControl
    {
        public Quest Quest;

        public ScriptControl(Quest quest)
        {
            InitializeComponent();
            Dock = DockStyle.Fill;
            Quest = quest;

            treeViewVariables.Nodes.Clear();
            foreach (var entry in Quest.ScriptDetails.VariableDeclarations)
            {
                treeViewVariables.Nodes.Add(new TreeVariableNode(entry));
            }

            // todo populate script panel boxes with quest scripts
        }

        internal void SyncQuestDataToUserInput()
        {
            Quest.ScriptDetails.VariableDeclarations.Clear();
            foreach (TreeVariableNode node in treeViewVariables.Nodes)
            {
                ClearTables(node);
                AddNodesToTable(node);
                Quest.ScriptDetails.VariableDeclarations.Add(node.Entry);
            }

            //todo populate quest scripts with script panel boxes
        }

        private void ClearTables(TreeVariableNode node)
        {
            foreach (TreeVariableNode child in node.Nodes)
            {
                ClearTables(child);
            }

            if (node.Parent != null)
            {
                var parentNode = (TreeVariableNode)node.Parent;
                parentNode.Entry.Value = new LuaTable();
            }
        }

        private void AddNodesToTable(TreeVariableNode node)
        {
            foreach (TreeVariableNode child in node.Nodes) {
                AddNodesToTable(child);
            }

            if (node.Parent != null)
            {
                var parentNode = (TreeVariableNode)node.Parent;
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
            TreeVariableNode node = new TreeVariableNode(Lua.TableEntry($"UserVariable_{i}", Lua.Number(0)));
            treeViewVariables.Nodes.Add(node);
            treeViewVariables.SelectedNode = node;
            treeViewVariables.Focus();
        }

        private void buttonRemoveVariableIdentifier_Click(object sender, EventArgs e)
        {
            textBoxName.Text = "";
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
                textBoxName.Enabled = false;
                comboBoxType.Enabled = false;
                HideAllValueControls();
            }
        }

        private void buttonNewIdentifier_Click(object sender, EventArgs e)
        {
            TreeVariableNode parentNode = (TreeVariableNode)treeViewVariables.SelectedNode;
            parentNode.Entry.Value = new LuaTable();
            parentNode.UpdateText();

            int i = 1;
            while (parentNode.Nodes.ContainsKey($"{i}"))
            {
                i++;
            }

            TreeVariableNode node = new TreeVariableNode(Lua.TableEntry(i, Lua.Number(0)));
            parentNode.Nodes.Add(node);
            parentNode.Expand();
        }

        private void treeViewVariables_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeVariableNode node = (TreeVariableNode)treeViewVariables.SelectedNode;
            comboBoxType.Enabled = true;
            textBoxName.Enabled = true;

            switch (node.Entry.Value.Type)
            {
                case TemplateRestrictionType.TEXT:
                    ShowTypeValueControl(textBoxTextValue);
                    textBoxTextValue.Text = node.Entry.Value.Value.Trim('"');
                    comboBoxType.Text = "TEXT";
                    break;
                case TemplateRestrictionType.NUMBER:
                    ShowTypeValueControl(numericUpDownNumberValue);
                    numericUpDownNumberValue.Value = decimal.Parse(node.Entry.Value.Value);
                    comboBoxType.Text = "NUMBER";
                    break;
                case TemplateRestrictionType.BOOLEAN:
                    ShowTypeValueControl(comboBoxBooleanValue);
                    comboBoxBooleanValue.Text = node.Entry.Value.Value;
                    comboBoxType.Text = "BOOLEAN";
                    break;
                case TemplateRestrictionType.TABLE:
                    ShowTypeValueControl(buttonNewIdentifier);
                    comboBoxType.Text = "TABLE";
                    break;
            }

            textBoxName.Text = node.Name.Trim('"');
        }

        private void ShowTypeValueControl(Control control)
        {
            Control[] valueControls = { textBoxTextValue, numericUpDownNumberValue, comboBoxBooleanValue, buttonNewIdentifier };

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

        private void HideAllValueControls()
        {
            Control[] valueControls = { textBoxTextValue, numericUpDownNumberValue, comboBoxBooleanValue, buttonNewIdentifier };
            foreach (Control valueControl in valueControls)
            {
                    valueControl.Visible = false;
            }
        }

        private void comboBoxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBoxType.Text)
            {
                case "TEXT":
                    ShowTypeValueControl(textBoxTextValue);
                    break;
                case "NUMBER":
                    ShowTypeValueControl(numericUpDownNumberValue);
                    break;
                case "BOOLEAN":
                    ShowTypeValueControl(comboBoxBooleanValue);
                    break;
                case "TABLE":
                    ShowTypeValueControl(buttonNewIdentifier);
                    break;
            }
        }

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {
            string userInputText = textBoxName.Text.Trim();

            TreeVariableNode currentNode = (TreeVariableNode)treeViewVariables.SelectedNode; 
            TreeNodeCollection siblings = currentNode.Parent != null ? currentNode.Parent.Nodes : treeViewVariables.Nodes;

            bool found = false;
            foreach (TreeVariableNode node in siblings)
            {
                if (node.Entry.Key.Value.Trim('"') == userInputText && node != currentNode)
                {
                    found = true;
                    break;
                }
            }

            if (found)
            {
                string newName = GenerateNewNameForDuplicate(userInputText, siblings, currentNode);
                int addedCharCount = newName.Length - textBoxName.Text.Length;

                textBoxName.Text = newName;
                textBoxName.SelectionStart = textBoxName.Text.Length - addedCharCount;
                textBoxName.SelectionLength = addedCharCount;
            } 
            else
            {
                currentNode.Entry.Key = Lua.GetEntryValueType(userInputText);
                currentNode.Name = currentNode.Entry.Key.Value;
                currentNode.UpdateText();
            }
        }
        private string GenerateNewNameForDuplicate(string baseName, TreeNodeCollection siblingNodes, TreeVariableNode currentNode)
        {
            int i = 1;
            while (treeViewVariables.Nodes.ContainsKey($@"""{baseName} ({i})"""))
            {
                i++;
            }

            return $"{baseName} ({i})";
        }

        private void textBoxTextValue_TextChanged(object sender, EventArgs e)
        {
            TreeVariableNode currentNode = (TreeVariableNode)treeViewVariables.SelectedNode;
            currentNode.Entry.Value = Lua.Text(textBoxTextValue.Text);
            currentNode.Nodes.Clear();
            currentNode.UpdateText();
        }

        private void comboBoxBooleanValue_SelectedIndexChanged(object sender, EventArgs e)
        {
            TreeVariableNode currentNode = (TreeVariableNode)treeViewVariables.SelectedNode;
            currentNode.Entry.Value = Lua.Boolean(comboBoxBooleanValue.Text);
            currentNode.Nodes.Clear();
            currentNode.UpdateText();
        }

        private void numericUpDownNumberValue_ValueChanged(object sender, EventArgs e)
        {
            TreeVariableNode currentNode = (TreeVariableNode)treeViewVariables.SelectedNode;
            currentNode.Entry.Value = Lua.Number((double)numericUpDownNumberValue.Value);
            currentNode.Nodes.Clear();
            currentNode.UpdateText();
        }

        public class TreeVariableNode : TreeNode
        {
            public LuaTableEntry Entry;
            public string UserEnteredName { get; set; }
            public TreeVariableNode(LuaTableEntry entry)
            {
                Entry = entry;
                Name = Entry.Key.Value;
                if (entry.Value is LuaTable table)
                {
                    foreach (var nestedEntry in table.KeyValuePairs)
                    {
                        Nodes.Add(new TreeVariableNode(nestedEntry));
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
}
