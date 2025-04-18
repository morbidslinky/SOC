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
        private bool userPressedBackspace = false;
        public Quest Quest;

        Str32TableNode qstep_main = new Str32TableNode("qstep_main");

        public ScriptControl(Quest quest)
        {
            InitializeComponent();
            Dock = DockStyle.Fill;
            Quest = quest;

            treeViewVariables.Nodes.Clear();
            foreach (var entry in Quest.ScriptDetails.VariableDeclarations)
            {
                treeViewVariables.Nodes.Add(new VariableNode(entry));
            }

            treeViewScripts.Nodes.Clear();
            foreach (var entry in Quest.ScriptDetails.QStep_Main)
            {
                qstep_main.Add(entry);
            }
            treeViewScripts.Nodes.Add(qstep_main);
        }

        internal void SyncQuestDataToUserInput()
        {
            Quest.ScriptDetails.VariableDeclarations.Clear();
            foreach (VariableNode node in treeViewVariables.Nodes)
            {
                ClearVarTables(node);
                AddVarNodesToVarTable(node);
                Quest.ScriptDetails.VariableDeclarations.Add(node.Entry);
            }

            Quest.ScriptDetails.QStep_Main.Clear();
            Quest.ScriptDetails.QStep_Main.AddRange(qstep_main.ConvertToScripts());
        }

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
            while (treeViewVariables.Nodes.ContainsKey($@"""{baseName} ({i})"""))
            {
                i++;
            }

            return $"{baseName} ({i})";
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

        private void textBoxName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back)
            {
                userPressedBackspace = true;
            }
            else
            {
                userPressedBackspace = false;
            }
        }

        public class Str32TableNode : TreeNode
        {
            public Str32TableNode(string tableName) : base(tableName) { }

            public void Add(StrCode32Script script)
            {
                Add(ConvertToNodeFamily(script));
            }
            public static CodeNode ConvertToNodeFamily(StrCode32Script script)
            {
                CodeNode codeNode = new CodeNode(script.CodeEvent.StrCode32);
                MsgSenderNode msgSenderNode = new MsgSenderNode(script.CodeEvent.msg, script.CodeEvent.sender);
                UnEventedScriptNode scriptNode = new UnEventedScriptNode(script.Identifier, script.Conditions, script.Operations);

                msgSenderNode.Nodes.Add(scriptNode);
                codeNode.Nodes.Add(msgSenderNode);

                return codeNode;
            }

            public void Add(CodeNode incomingCodeNode)
            {
                if (Nodes.ContainsKey(incomingCodeNode.Name)) 
                {
                    CodeNode existingCodeNode = (CodeNode)Nodes[incomingCodeNode.Name];
                    foreach (MsgSenderNode incomingMsgSenderNode in incomingCodeNode.Nodes)
                    {
                        if (existingCodeNode.Nodes.ContainsKey(incomingMsgSenderNode.Name))
                        {
                            MsgSenderNode existingMsgSenderNode = (MsgSenderNode)existingCodeNode.Nodes[incomingMsgSenderNode.Name];
                            foreach (UnEventedScriptNode incomingScriptNode in incomingMsgSenderNode.Nodes)
                            {
                                if (existingMsgSenderNode.Nodes.ContainsKey(incomingScriptNode.Name))
                                {
                                    string baseName = incomingScriptNode.Identifier.Text;
                                    int suffix = 1;
                                    string newName;
                                    do
                                    {
                                        newName = $"{baseName} ({suffix++})";
                                    } while (existingMsgSenderNode.Nodes.ContainsKey($@"{newName}"));

                                    incomingScriptNode.UpdateIdentifier(Lua.Text(newName));
                                }
                                existingMsgSenderNode.Nodes.Add(incomingScriptNode);
                            }
                        }
                        else
                        {
                            existingCodeNode.Nodes.Add(incomingMsgSenderNode);
                        }
                    }
                } 
                else
                {
                    Nodes.Add(incomingCodeNode);
                }  
            }

            public void DeleteScriptNode(UnEventedScriptNode selectedScriptNode)
            {
                MsgSenderNode msgSenderNode = (MsgSenderNode)selectedScriptNode.Parent;
                CodeNode codeNode = (CodeNode)msgSenderNode?.Parent;

                if (msgSenderNode == null || codeNode == null)
                    return;

                msgSenderNode.Nodes.Remove(selectedScriptNode);

                if (msgSenderNode.Nodes.Count == 0)
                {
                    codeNode.Nodes.Remove(msgSenderNode);

                    if (codeNode.Nodes.Count == 0)
                    {
                        Nodes.Remove(codeNode);
                    }
                }
            }

            public List<StrCode32Script> ConvertToScripts()
            {
                List<StrCode32Script> result = new List<StrCode32Script>();

                foreach (CodeNode codeNode in Nodes)
                {
                    foreach (MsgSenderNode msgSenderNode in codeNode.Nodes)
                    {
                        foreach (UnEventedScriptNode scriptNode in msgSenderNode.Nodes)
                        {
                            result.Add(ConvertToScript(scriptNode));
                        }
                    }
                }

                return result;
            }

            public StrCode32Script ConvertToScript(UnEventedScriptNode selectedScriptNode)
            {
                MsgSenderNode msgSenderNode = (MsgSenderNode)selectedScriptNode.Parent;
                CodeNode codeNode = (CodeNode)msgSenderNode?.Parent;

                if (msgSenderNode == null || codeNode == null)
                    throw new InvalidOperationException("Script node is not in a valid tree structure.");

                return new StrCode32Script
                {
                    CodeEvent = new StrCode32Event
                    {
                        StrCode32 = codeNode.Code,
                        msg = msgSenderNode.Msg,
                        sender = msgSenderNode.Sender,
                    },

                    Identifier = selectedScriptNode.Identifier,
                    Conditions = selectedScriptNode.Conditions,
                    Operations = selectedScriptNode.Operations
                };
            }

            public void MoveScript(UnEventedScriptNode selectedScriptNode, string newCode, string newMessage, string newSender)
            {
                StrCode32Script script = ConvertToScript(selectedScriptNode);
                DeleteScriptNode(selectedScriptNode);

                script.CodeEvent = new StrCode32Event(newCode, newMessage, newSender);

                Add(script);
            }
        }

        public class UnEventedScriptNode : TreeNode
        {
            public LuaText Identifier;

            public List<LuaTableEntry> Conditions;

            public List<LuaTableEntry> Operations;

            public UnEventedScriptNode(LuaText identifier, List<LuaTableEntry> conditions, List<LuaTableEntry> operations)
            {
                Conditions = conditions;
                Operations = operations;
                UpdateIdentifier(identifier);
            }

            public void UpdateIdentifier(LuaText identifier)
            {
                Identifier = identifier;

                Name = identifier.Text;
                Text = $"{identifier.Text}";
            }
        }

        public class MsgSenderNode : TreeNode
        {
            public LuaText Msg;

            public LuaText Sender;

            public MsgSenderNode(LuaText msg, LuaText sender)
            {
                Msg = msg;
                Sender = sender;

                if (Sender.Text != "")
                {
                    Name = $"{Msg.ToString()} :: {Sender.ToString()}";
                    Text = Name;
                } 
                else
                {
                    Name = Msg.ToString();
                    Text = Name;
                }
            }
        }

        public class CodeNode : TreeNode
        {
            public LuaText Code;

            public CodeNode(LuaText code)
            {
                Code = code;

                Name = code.Text;
                Text = code.Text;
            }
        }

        private void buttonNewScript_Click(object sender, EventArgs e)
        {
            qstep_main.Add(new StrCode32Script(
        new StrCode32Event("GameObject", "TestMsg", "TestSender", "gameObjectId", "gameObjectId01", "animalId"),
        LuaFunction.ToTableEntry(
            "SomeUserScript",
            new string[] { "gameObjectId", "gameObjectId01", "animalId" },
            "")));
            qstep_main.Add(new StrCode32Script(
        new StrCode32Event("Marker", "TestMsg", "", "gameObjectId", "gameObjectId01", "animalId"),
        LuaFunction.ToTableEntry(
            "SomeOtherUserScript",
            new string[] { "gameObjectId", "gameObjectId01", "animalId" },
            "")));
        }

        private void textBoxScriptName_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxScriptName_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void comboBoxStrCodes_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxStrMsgs_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxStrSenders_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void buttonRemoveScript_Click(object sender, EventArgs e)
        {

        }

        private void treeViewScripts_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }
    }
}
