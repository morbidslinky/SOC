using System;
using System.Windows.Forms;
using System.Collections.Generic;
using SOC.Classes.Lua;

namespace SOC.UI
{
    public partial class ScriptControl : UserControl
    {
        Str32TableNode qstep_main = new Str32TableNode("qstep_main");
        private bool _isUpdatingControls = false;

        private void buttonNewScript_Click(object sender, EventArgs e)
        {
            treeViewScripts.SelectedNode = qstep_main.Add(new StrCode32Script(GetDefaultEvent(), "NewEventScript"));
        }

        private void textBoxScriptName_TextChanged(object sender, EventArgs e)
        {
            if (_isUpdatingControls) return;
            _isUpdatingControls = true;

            if (textBoxScriptName.Text.Trim() == "")
            {
                textBoxScriptName.Text = "NewEventScript";
            }

            if (treeViewScripts.SelectedNode is UnEventedScriptNode selectedScriptNode)
            {
                string baseName = textBoxScriptName.Text;
                string uniqueName = qstep_main.GetUniqueScriptName(baseName, selectedScriptNode);
                selectedScriptNode.UpdateIdentifier(Lua.Text(uniqueName));

                if (baseName != uniqueName)
                {
                    textBoxScriptName.Text = uniqueName;
                    textBoxScriptName.SelectionStart = baseName.Length - (userPressedBackspace ? 1 : 0);
                    textBoxScriptName.SelectionLength = uniqueName.Length - baseName.Length + (userPressedBackspace ? 1 : 0);
                }
            }

            groupBoxScriptDetails.Text = textBoxScriptName.Text;
            _isUpdatingControls = false;
        }

        private void textBoxScriptName_KeyDown(object sender, KeyEventArgs e)
        {
            userPressedBackspace = e.KeyCode == Keys.Back;
        }

        private void buttonRemoveScript_Click(object sender, EventArgs e)
        {
            var selectedNode = treeViewScripts.SelectedNode;
            if (selectedNode is UnEventedScriptNode scriptNode)
            {
                GetParent32TableNode(scriptNode).DeleteScriptNode(scriptNode);
            }
        }

        private Str32TableNode GetParent32TableNode(UnEventedScriptNode node)
        {
            var parent = node.Parent;
            while (!(parent is Str32TableNode) && parent != null)
            {
                parent = parent.Parent;
            }

            return (Str32TableNode)parent;
        }

        private void EnableAllScriptEditControls(bool enable)
        {
            Control[] valueControls = { textBoxScriptName, buttonNewOperation, buttonNewPrecondition, buttonRemoveScript };
            foreach (Control valueControl in valueControls)
            {
                valueControl.Enabled = enable;
            }
        }

        private void treeViewScripts_AfterSelect(object sender, TreeViewEventArgs e)
        {
            UpdateScriptControlsToSelectedNode();
        }

        internal void SetDetail(UserControl control)
        {
            if (control != null && !panelComponentDetails.Controls.Contains(control))
            {
                panelComponentDetails.Controls.Clear();
                panelComponentDetails.Controls.Add(control);
            }
        }

        private UserControl UpdateScriptControlsToSelectedNode()
        {
            _isUpdatingControls = true;
            UserControl detailComponentControl = null;

            textBoxScriptName.Text = "";

            switch (treeViewScripts.SelectedNode)
            {
                case Str32TableNode tableNode:
                    EnableAllScriptEditControls(false);

                    break;
                case CodeNode codeNode:
                    EnableAllScriptEditControls(false);
                    break;
                case MsgSenderNode msgSenderNode:
                    EnableAllScriptEditControls(false);
                    break;
                case UnEventedScriptNode scriptNode:
                    EnableAllScriptEditControls(true);
                    textBoxScriptName.Text = scriptNode.Identifier.Text;
                    groupBoxScriptDetails.Text = textBoxScriptName.Text;
                    SetDetail(EmbeddedScriptControl);
                    EmbeddedScriptControl.UpdateFromSelectedScript();
                    break;
                case null:
                    EnableAllScriptEditControls(false);
                    break;
            }

            _isUpdatingControls = false;
            return detailComponentControl;
        }

        private void buttonNewPrecondition_Click(object sender, EventArgs e)
        {

        }

        private void buttonNewOperation_Click(object sender, EventArgs e)
        {

        }
    }

    public class Str32TableNode : TreeNode
    {
        public Str32TableNode(string tableName) : base(tableName) { }

        public UnEventedScriptNode Add(StrCode32Script script)
        {
            return Add(ConvertToNodeFamily(script));
        }
        public static CodeNode ConvertToNodeFamily(StrCode32Script script)
        {
            CodeNode codeNode = new CodeNode(script.CodeEvent.StrCode32);
            MsgSenderNode msgSenderNode = new MsgSenderNode(script.CodeEvent.msg, script.CodeEvent.sender);
            UnEventedScriptNode scriptNode = new UnEventedScriptNode(script.Identifier, script.Description, script.Preconditions, script.Operations);

            msgSenderNode.Nodes.Add(scriptNode);
            codeNode.Nodes.Add(msgSenderNode);

            return codeNode;
        }

        public UnEventedScriptNode Add(CodeNode incomingCodeNode)
        {
            UnEventedScriptNode selectedScriptNode = null;

            foreach (MsgSenderNode incomingMsgSenderNode in incomingCodeNode.Nodes)
            {
                foreach (UnEventedScriptNode incomingScriptNode in incomingMsgSenderNode.Nodes)
                {
                    selectedScriptNode = incomingScriptNode;
                }
            }

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
                            incomingScriptNode.UpdateIdentifier(Lua.Text(GetUniqueScriptName(incomingScriptNode.Name)));
                            existingMsgSenderNode.Nodes.Add(incomingScriptNode);
                            selectedScriptNode = incomingScriptNode;
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

            return selectedScriptNode;
        }

        public string GetUniqueScriptName(string baseName, UnEventedScriptNode except = null)
        {
            List<string> names = new List<string>();

            foreach (Str32TableNode table in Parent.Nodes)
            {
                foreach (CodeNode code in table.Nodes)
                {
                    foreach (MsgSenderNode msgSender in code.Nodes)
                    {
                        foreach (UnEventedScriptNode scriptNode in msgSender.Nodes)
                        {
                            if (except == null || scriptNode != except)
                            {
                                names.Add(scriptNode.Name);
                            }
                        }
                    }
                }
            }

            if (!names.Contains(baseName))
            {
                return baseName;
            }

            int suffix = 1;
            string newName;
            do
            {
                newName = $"{baseName}_{suffix++}";
            } while (names.Contains($@"{newName}"));

            return newName;
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
                        result.Add(scriptNode.ConvertToScript());
                    }
                }
            }

            return result;
        }

        public UnEventedScriptNode MoveScript(UnEventedScriptNode selectedScriptNode, string newCode, string newMessage, string newSender)
        {
            StrCode32Script script = selectedScriptNode.ConvertToScript();
            DeleteScriptNode(selectedScriptNode);

            script.CodeEvent = new StrCode32Event(newCode, newMessage, newSender);

            return Add(script);
        }
    }

    public class PreconditionsParentNode : TreeNode
    {
        public PreconditionsParentNode(List<LuaTableEntry> conditions)
        {
        }

        internal List<LuaTableEntry> ConvertToLuaTableEntries()
        {
            return new List<LuaTableEntry>();
        }
    }

    public class PreconditionNode : TreeNode
    {

    }

    public class OperationsParentNode : TreeNode
    {
        public OperationsParentNode(List<LuaTableEntry> operations)
        {
        }

        internal List<LuaTableEntry> ConvertToLuaTableEntries()
        {
            return new List<LuaTableEntry>();
        }
    }

    public class OperationNode : TreeNode
    {

    }

    public class UnEventedScriptNode : TreeNode
    {
        public LuaText Identifier;

        public string Description;

        public PreconditionsParentNode PreconditionsParent;

        public OperationsParentNode OperationsParent;

        public UnEventedScriptNode(LuaText identifier, string commment, List<LuaTableEntry> conditions, List<LuaTableEntry> operations)
        {
            PreconditionsParent = new PreconditionsParentNode(conditions);
            OperationsParent = new OperationsParentNode(operations);
            UpdateIdentifier(identifier);
            Description = commment;
        }

        public void UpdateIdentifier(LuaText identifier)
        {
            Identifier = identifier;

            Name = identifier.Text;
            Text = $"{identifier.Text}";
        }

        public void UpdateDescription(string description)
        {
            Description = description;
        }

        public StrCode32Event getEvent()
        {
            MsgSenderNode msgSenderNode = (MsgSenderNode)Parent;
            CodeNode codeNode = (CodeNode)msgSenderNode?.Parent;

            if (msgSenderNode == null || codeNode == null)
                throw new InvalidOperationException("Script node is not in a valid tree structure.");
            return new StrCode32Event
            {
                StrCode32 = codeNode.Code,
                msg = msgSenderNode.Msg,
                sender = msgSenderNode.Sender,
            };
        }

        public Str32TableNode GetStrCode32TableNode()
        {
            MsgSenderNode msgSenderNode = (MsgSenderNode)Parent;
            CodeNode codeNode = (CodeNode)msgSenderNode?.Parent;
            Str32TableNode str32TableNode = (Str32TableNode)codeNode?.Parent;

            if (msgSenderNode == null || codeNode == null || str32TableNode == null)
                throw new InvalidOperationException("Script node is not in a valid tree structure.");
            return str32TableNode;
        }

        public StrCode32Script ConvertToScript()
        {
            MsgSenderNode msgSenderNode = (MsgSenderNode)Parent;
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

                Identifier = Identifier,
                Description = Description,
                Preconditions = PreconditionsParent.ConvertToLuaTableEntries(),
                Operations = OperationsParent.ConvertToLuaTableEntries()
            };
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
}
