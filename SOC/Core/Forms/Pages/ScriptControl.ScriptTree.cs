using System;
using System.Windows.Forms;
using System.Collections.Generic;
using SOC.Classes.Lua;
using System.Linq;
using System.Xml.Linq;

namespace SOC.UI
{
    public partial class ScriptControl : UserControl
    {
        private void buttonNewScript_Click(object sender, EventArgs e)
        {
            treeViewScripts.SelectedNode = qstep_main.Add(new Script(GetEventFromSelection(), "NewEventScript"));
        }

        private StrCode32Event GetEventFromSelection()
        {
            var scriptNode = GetScriptNodeFromSelection();
            if (scriptNode != null)
            {
                return scriptNode.getEvent();
            }

            if (treeViewScripts.SelectedNode is MsgSenderNode msgSenderNode)
            {
                return msgSenderNode.GetEvent();
            }

            if (treeViewScripts.SelectedNode is CodeNode codeNode)
            {
                string key = codeNode.Code.Text;
                if (MessageClassListMapping[key].Count > 0)
                {
                    return new StrCode32Event(key, MessageClassListMapping[key][0], "");
                }
            }

            foreach (string key in MessageClassListMapping.Keys)
            {
                if (MessageClassListMapping[key].Count > 0)
                {
                    return new StrCode32Event(key, MessageClassListMapping[key][0], "");
                }
            }

            return new StrCode32Event("", "", "");
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
                selectedScriptNode.UpdateNodeText(Lua.Text(uniqueName));

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
            switch (treeViewScripts.SelectedNode)
            {
                case UnEventedScriptNode scriptNode:
                    GetParent32TableNode(scriptNode).DeleteScriptNode(scriptNode);
                    break;
                case ScriptalNode scriptalNode:
                    scriptalNode.GetUnEventedScriptNode().DeleteChild(scriptalNode);
                    break;
                default:
                    break;
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

        private void EnabledScriptEditControls(params Control[] valueControls)
        {
            Control[] allScriptEditControls = { textBoxScriptName, buttonNewOperation, buttonNewPrecondition, buttonRemoveScript };
            foreach (Control control in allScriptEditControls)
            {
                control.Enabled = (valueControls != null && valueControls.Contains(control));
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
            groupBoxScriptDetails.Text = "";

            UnEventedScriptNode selectedScriptNode = null;

            switch (treeViewScripts.SelectedNode)
            {
                case Str32TableNode tableNode:
                    EnabledScriptEditControls();
                    break;
                case CodeNode codeNode:
                    EnabledScriptEditControls();
                    break;
                case MsgSenderNode msgSenderNode:
                    EnabledScriptEditControls();
                    break;
                case UnEventedScriptNode scriptNode:
                    selectedScriptNode = scriptNode;
                    EnabledScriptEditControls(textBoxScriptName, buttonNewOperation, buttonNewPrecondition, buttonRemoveScript);

                    SetDetail(EmbeddedScriptControl);
                    EmbeddedScriptControl.UpdateFromScript(selectedScriptNode);
                    break;
                case ScriptalParentNode parentNode:
                    selectedScriptNode = parentNode.GetUnEventedScriptNode();
                    EnabledScriptEditControls((parentNode.ScriptalType == ScriptalType.Preconditional) ? buttonNewPrecondition : buttonNewOperation);

                    SetDetail(EmbeddedScriptControl);
                    EmbeddedScriptControl.UpdateFromScript(selectedScriptNode);
                    break;
                case ScriptalNode scriptalNode:
                    selectedScriptNode = scriptalNode.GetUnEventedScriptNode();
                    EnabledScriptEditControls(((scriptalNode.ScriptalType == ScriptalType.Preconditional) ? buttonNewPrecondition : buttonNewOperation), buttonRemoveScript);

                    SetDetail(EmbeddedScriptalControl);
                    EmbeddedScriptalControl.UpdateFromScript(scriptalNode);
                    break;
                case null:
                    EnabledScriptEditControls();
                    break;
            }

            if (selectedScriptNode != null)
            {
                textBoxScriptName.Text = selectedScriptNode.Identifier.Text ;
                groupBoxScriptDetails.Text = selectedScriptNode.Identifier.ToString();
            }

            _isUpdatingControls = false;
            return detailComponentControl;
        }

        private void buttonNewPrecondition_Click(object sender, EventArgs e)
        {
            _isUpdatingControls = true;

            Random random = new Random();
            UnEventedScriptNode scriptNode = GetScriptNodeFromSelection();
            if (scriptNode != null)
            {
                scriptNode.AddPreconditional(Scriptal.Default());
                scriptNode.UpdateNodeText();
            }

            _isUpdatingControls = false;
        }

        private void buttonNewOperation_Click(object sender, EventArgs e)
        {
            _isUpdatingControls = true;

            Random random = new Random();
            UnEventedScriptNode scriptNode = GetScriptNodeFromSelection();
            if (scriptNode != null)
            {
                scriptNode.AddOperational(Scriptal.Default());
                scriptNode.UpdateNodeText();
            }

            _isUpdatingControls = false;
        }

        private UnEventedScriptNode GetScriptNodeFromSelection()
        {
            UnEventedScriptNode scriptNode = null;

            switch (treeViewScripts.SelectedNode)
            {
                case UnEventedScriptNode selectedScriptNode:
                    scriptNode = selectedScriptNode;
                    break;
                case ScriptalParentNode selectedParentNode:
                    scriptNode = selectedParentNode.GetUnEventedScriptNode();
                    break;
                case ScriptalNode selectedScriptalNode:
                    scriptNode = selectedScriptalNode.GetUnEventedScriptNode();
                    break;
            }
            return scriptNode;
        }
    }

    public class Str32TableNode : TreeNode
    {
        public Str32TableNode(string tableName) : base(tableName) { }

        public UnEventedScriptNode Add(Script script)
        {
            return Add(ConvertToNodeFamily(script));
        }
        public static CodeNode ConvertToNodeFamily(Script script)
        {
            CodeNode codeNode = new CodeNode(script.CodeEvent.StrCode32);
            MsgSenderNode msgSenderNode = new MsgSenderNode(script.CodeEvent.msg, script.CodeEvent.sender);
            UnEventedScriptNode scriptNode = new UnEventedScriptNode(script.Identifier, script.Description, script.Preconditionals, script.Operationals);

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
                            incomingScriptNode.UpdateNodeText(Lua.Text(GetUniqueScriptName(incomingScriptNode.Name)));
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

        public List<Script> ConvertToScripts()
        {
            List<Script> result = new List<Script>();

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
            Script script = selectedScriptNode.ConvertToScript();
            DeleteScriptNode(selectedScriptNode);

            script.CodeEvent = new StrCode32Event(newCode, newMessage, newSender);

            return Add(script);
        }
    }

    public class UnEventedScriptNode : TreeNode
    {
        public LuaText Identifier;

        public string Description;

        public ScriptalParentNode PreconditionsParent;

        public ScriptalParentNode OperationsParent;

        public UnEventedScriptNode(LuaText identifier, string commment, List<Scriptal> conditions, List<Scriptal> operations)
        {
            PreconditionsParent = new ScriptalParentNode(ScriptalType.Preconditional, conditions);
            Nodes.Add(PreconditionsParent);

            OperationsParent = new ScriptalParentNode(ScriptalType.Operational, operations);
            Nodes.Add(OperationsParent);

            UpdateNodeText(identifier);
            Description = commment;
        }

        public void AddPreconditional(Scriptal scriptal)
        {
            PreconditionsParent.Nodes.Add(new ScriptalNode(scriptal, ScriptalType.Preconditional));
            PreconditionsParent.Expand();
            PreconditionsParent.GetUnEventedScriptNode().Expand();
        }

        public void AddOperational(Scriptal scriptal)
        {
            OperationsParent.Nodes.Add(new ScriptalNode(scriptal, ScriptalType.Operational));
            OperationsParent.Expand();
            OperationsParent.GetUnEventedScriptNode().Expand();
        }

        public void DeleteChild(TreeNode childNode)
        {
            if (PreconditionsParent.Nodes.Contains(childNode))
            {
                PreconditionsParent.Nodes.Remove(childNode);
            } 
            else if (OperationsParent.Nodes.Contains(childNode))
            {
                OperationsParent.Nodes.Remove(childNode);
            }
        }

        public void UpdateNodeText(LuaText identifier = null)
        {
            if (identifier != null)
            {
                Identifier = identifier;
                Name = identifier.Text;
            }
            Text = $"{Identifier.Text}   ({PreconditionsParent.Nodes.Count}|{OperationsParent.Nodes.Count})";
        }

        public void UpdateDescription(string description)
        {
            Description = description;
        }

        public StrCode32Event getEvent()
        {
            MsgSenderNode msgSenderNode = (MsgSenderNode)Parent;

            if (msgSenderNode == null)
                throw new InvalidOperationException("Script node is not in a valid tree structure.");
            return msgSenderNode.GetEvent();
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

        public Script ConvertToScript()
        {
            MsgSenderNode msgSenderNode = (MsgSenderNode)Parent;
            CodeNode codeNode = (CodeNode)msgSenderNode?.Parent;

            if (msgSenderNode == null || codeNode == null)
                throw new InvalidOperationException("Script node is not in a valid tree structure.");

            return new Script
            {
                CodeEvent = new StrCode32Event
                {
                    StrCode32 = codeNode.Code,
                    msg = msgSenderNode.Msg,
                    sender = msgSenderNode.Sender,
                },

                Identifier = Identifier,
                Description = Description,
                Preconditionals = PreconditionsParent.GetScriptals(),
                Operationals = OperationsParent.GetScriptals()
            };
        }
    }

    public class ScriptalParentNode : TreeNode
    {
        public ScriptalType ScriptalType;

        public ScriptalParentNode(ScriptalType type, List<Scriptal> scriptals)
        {
            ScriptalType = type;
            Text = (ScriptalType == ScriptalType.Preconditional) ? "Preconditionals" : "Operationals";
                
            foreach (Scriptal scriptal in scriptals)
            {
                Nodes.Add(new ScriptalNode(scriptal, ScriptalType));
            }
        }

        public List<Scriptal> GetScriptals()
        {
            List<Scriptal> childScriptals = new List<Scriptal>();

            foreach (ScriptalNode scriptalNode in Nodes)
            {
                childScriptals.Add(scriptalNode.Scriptal);
            }

            return childScriptals;
        }

        public UnEventedScriptNode GetUnEventedScriptNode()
        {
            UnEventedScriptNode parentNode = (UnEventedScriptNode)Parent;

            if (parentNode == null)
                throw new InvalidOperationException("Script node is not in a valid tree structure.");
            return parentNode;
        }
    }

    public enum ScriptalType
    {
        Preconditional,
        Operational
    }

    public class ScriptalNode : TreeNode
    {
        public ScriptalType ScriptalType;

        public Scriptal Scriptal;

        public ScriptalNode(Scriptal scriptal, ScriptalType scriptalType)
        {
            ScriptalType = scriptalType;
            Scriptal = scriptal;
            UpdateText();
        }

        public UnEventedScriptNode GetUnEventedScriptNode()
        {
            return GetScriptalParentNode().GetUnEventedScriptNode();
        }

        public StrCode32Event GetEvent()
        {
            return GetUnEventedScriptNode().getEvent();
        }

        public ScriptalParentNode GetScriptalParentNode()
        {
            if (Parent == null)
                throw new InvalidOperationException("Script node is not in a valid tree structure.");
            return (ScriptalParentNode)Parent;
        }

        internal void UpdateText()
        {
            Text = Scriptal.Name;
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

        public StrCode32Event GetEvent()
        {
            CodeNode codeNode = (CodeNode)Parent;

            if (codeNode == null)
                throw new InvalidOperationException("Script node is not in a valid tree structure.");
            return new StrCode32Event
            {
                StrCode32 = codeNode.Code,
                msg = Msg,
                sender = Sender,
            };
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
