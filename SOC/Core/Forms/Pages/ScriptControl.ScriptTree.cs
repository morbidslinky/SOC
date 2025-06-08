using SOC.Classes.Common;
using SOC.Classes.Lua;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SOC.UI
{
    public partial class ScriptControl : UserControl
    {
        private void buttonNewScript_Click(object sender, EventArgs e)
        {
            Script newEventScript = new Script(GetEventFromSelection(), "NewEventScript");
            newEventScript.Preconditions.Add(Scriptal.AlwaysTrue());
            newEventScript.Operations.Add(Scriptal.DoNothing());

            treeViewScripts.SelectedNode = ScriptTablesRootNode.QStep_Main.Add(newEventScript);
            treeViewScripts.SelectedNode.ExpandAll();
        }

        private StrCode32 GetEventFromSelection()
        {
            var scriptNode = GetScriptNodeFromSelection();
            if (scriptNode != null)
            {
                return scriptNode.getEvent();
            }

            if (treeViewScripts.SelectedNode is MessageSenderNode msgSenderNode)
            {
                return msgSenderNode.GetEvent();
            }

            if (treeViewScripts.SelectedNode is CodeNode codeNode)
            {
                string key = codeNode.CodeKey;

                if (StrCode32Classes.Get(key).Count > 0)
                {
                    return new StrCode32(key, StrCode32Classes.Get(key)[0]);
                }
            }

            foreach (string key in StrCode32Classes.Keys())
            {
                if (StrCode32Classes.Get(key).Count > 0)
                {
                    return new StrCode32(key, StrCode32Classes.Get(key)[0]);
                }
            }

            return new StrCode32("", Create.String(""));
        }

        private void textBoxScriptName_TextChanged(object sender, EventArgs e)
        {
            if (_isUpdatingControls) return;
            _isUpdatingControls = true;

            if (textBoxScriptName.Text.Trim() == "")
            {
                textBoxScriptName.Text = "NewEventScript";
            }

            if (treeViewScripts.SelectedNode is ScriptNode selectedScriptNode)
            {
                string baseName = textBoxScriptName.Text;
                string uniqueName = ScriptTablesRootNode.QStep_Main.GetUniqueScriptName(baseName, selectedScriptNode);
                selectedScriptNode.UpdateNodeText(Create.String(uniqueName));

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
                case ScriptNode scriptNode:
                    foreach (ScriptalNode node in scriptNode.OperationsParent.Nodes) node.ClearAllVarNodeDependencies();
                    foreach (ScriptalNode node in scriptNode.PreconditionsParent.Nodes) node.ClearAllVarNodeDependencies();
                    GetParent32TableNode(scriptNode).DeleteScriptNode(scriptNode);
                    break;
                case ScriptalNode scriptalNode:
                    ScriptNode parentScriptNode = scriptalNode.GetUnEventedScriptNode();
                    scriptalNode.ClearAllVarNodeDependencies();
                    scriptalNode.Remove();
                    parentScriptNode.UpdateNodeText();
                    break;
                default:
                    break;
            }
            RedrawScriptDependents();
        }

        private Str32TableNode GetParent32TableNode(ScriptNode node)
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

        internal void Embed(UserControl control)
        {
            if (control != null && !panelComponentDetails.Controls.Contains(control))
            {
                panelComponentDetails.Controls.Clear();
                panelComponentDetails.Controls.Add(control);
            }
        }

        private void UpdateScriptControlsToSelectedNode()
        {
            _isUpdatingControls = true;

            UnmarkVariableDependencies();
            UserControl embedControl = null;
            switch (treeViewScripts.SelectedNode)
            {
                case ScriptNode scriptNode:
                    EnabledScriptEditControls(textBoxScriptName, buttonNewOperation, buttonNewPrecondition, buttonRemoveScript);
                    embedControl = ScriptEmbed.Menu(scriptNode);
                    scriptNode.MarkDependencies();
                    break;

                case ScriptalParentNode parentNode:
                    EnabledScriptEditControls(buttonNewPrecondition, buttonNewOperation);
                    embedControl = ScriptEmbed.Menu(parentNode.GetUnEventedScriptNode());
                    parentNode.MarkDependencies();
                    break;

                case ScriptalNode scriptalNode:
                    EnabledScriptEditControls(buttonNewPrecondition, buttonNewOperation, buttonRemoveScript);
                    embedControl = ScriptalEmbed.Menu(scriptalNode); 
                    scriptalNode.MarkDependencies();
                    break;

                default:
                    EnabledScriptEditControls();
                    SyncQuestDataToUserInput();
                    embedControl = ScriptSetEmbed.Menu();
                    break;
            }
            Embed(embedControl);

            _isUpdatingControls = false;
        }

        public void UnmarkVariableDependencies()
        {
            foreach (VariableNode node in treeViewVariables.Nodes)
            {
                ResetNodeUnderlinesRecursive(node);
            }
        }

        public void SetMenuText(string _groupBoxScriptDetails, string _textBoxScriptName)
        {
            groupBoxScriptDetails.Text = _groupBoxScriptDetails;
            textBoxScriptName.Text = _textBoxScriptName;
        }

        private void buttonNewPrecondition_Click(object sender, EventArgs e)
        {
            _isUpdatingControls = true;

            ScriptNode scriptNode = GetScriptNodeFromSelection();

            if (scriptNode != null)
            {
                ScriptalNode scriptalNode = scriptNode.AddPrecondition(Scriptal.AlwaysTrue());
                scriptNode.UpdateNodeText();

                _isUpdatingControls = false;

                treeViewScripts.SelectedNode = scriptalNode;
            }
            else
            {
                _isUpdatingControls = false;
            }
        }

        private void buttonNewOperation_Click(object sender, EventArgs e)
        {
            _isUpdatingControls = true;

            ScriptNode scriptNode = GetScriptNodeFromSelection();
            
            if (scriptNode != null)
            {
                ScriptalNode scriptalNode = scriptNode.AddOperation(Scriptal.DoNothing());
                scriptNode.UpdateNodeText();

                _isUpdatingControls = false;

                treeViewScripts.SelectedNode = scriptalNode;
            } 
            else
            {
                _isUpdatingControls = false;
            }
        }

        private ScriptNode GetScriptNodeFromSelection()
        {
            ScriptNode scriptNode = null;

            switch (treeViewScripts.SelectedNode)
            {
                case ScriptNode selectedScriptNode:
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

    public class ScriptTablesRootNode : TreeNode
    {
        public Str32TableNode QStep_Main;

        public ScriptTablesRootNode() : base("Quest Script Tables")
        {
            QStep_Main = new Str32TableNode("QStep_Main");
            Nodes.Add(QStep_Main);
            Expand();
        }
    }

    public class Str32TableNode : TreeNode
    {
        public Str32TableNode(string tableName) : base(tableName) { }

        public override string ToString() => Text;

        public ScriptNode Add(Script script)
        {
            script.Identifier = Create.String(GetUniqueScriptName(script.Identifier.Value));

            CodeNode nodeFamily = ConvertToNodeFamily(script);
            Add(nodeFamily);

            return Expand(nodeFamily);
        }

        public static CodeNode ConvertToNodeFamily(Script script)
        {
            CodeNode codeNode = new CodeNode(script.CodeEvent.CodeKey);
            MessageSenderNode msgSenderNode = new MessageSenderNode(script.CodeEvent.Message, script.CodeEvent.SenderKey, script.CodeEvent.SenderValue);
            ScriptNode scriptNode = new ScriptNode(script.Identifier, script.Description, script.Preconditions, script.Operations);

            msgSenderNode.Nodes.Add(scriptNode);
            codeNode.Nodes.Add(msgSenderNode);

            return codeNode;
        }

        public void Add(CodeNode incomingCodeNode)
        {
            if (Nodes.ContainsKey(incomingCodeNode.Name))
            {
                CodeNode existingCodeNode = (CodeNode)Nodes[incomingCodeNode.Name];
                foreach (MessageSenderNode incomingMsgSenderNode in incomingCodeNode.Nodes)
                {
                    if (existingCodeNode.Nodes.ContainsKey(incomingMsgSenderNode.Name))
                    {
                        MessageSenderNode existingMsgSenderNode = (MessageSenderNode)existingCodeNode.Nodes[incomingMsgSenderNode.Name];
                        foreach (ScriptNode incomingScriptNode in incomingMsgSenderNode.Nodes)
                        {
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

        public ScriptNode Expand(CodeNode incomingCodeNode)
        {
            ScriptNode selectedScriptNode = null;

            Expand();
            incomingCodeNode.Expand();
            foreach (MessageSenderNode incomingMsgSenderNode in incomingCodeNode.Nodes)
            {
                incomingMsgSenderNode.Expand();
                foreach (ScriptNode incomingScriptNode in incomingMsgSenderNode.Nodes)
                {
                    selectedScriptNode = incomingScriptNode;
                }
            }

            return selectedScriptNode;
        }

        public string GetUniqueScriptName(string baseName, ScriptNode except = null)
        {
            var names = GetScriptNodes().Where(scriptNode => scriptNode != except).Select(scriptNode => scriptNode.Name);

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

        public List<ScriptNode> GetScriptNodes()
        {
            List<ScriptNode> scriptNodes = new List<ScriptNode>();

            foreach (Str32TableNode table in Parent.Nodes)
            {
                foreach (CodeNode code in table.Nodes)
                {
                    foreach (MessageSenderNode msgSender in code.Nodes)
                    {
                        foreach (ScriptNode scriptNode in msgSender.Nodes)
                        {
                            scriptNodes.Add(scriptNode);
                        }
                    }
                }
            }

            return scriptNodes;
        }

        public void DeleteScriptNode(ScriptNode selectedScriptNode)
        {
            MessageSenderNode msgSenderNode = (MessageSenderNode)selectedScriptNode.Parent;
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
                foreach (MessageSenderNode msgSenderNode in codeNode.Nodes)
                {
                    foreach (ScriptNode scriptNode in msgSenderNode.Nodes)
                    {
                        result.Add(scriptNode.ConvertToScript());
                    }
                }
            }

            return result;
        }

        public ScriptNode MoveScript(ScriptNode selectedScriptNode, string newCode, LuaValue newMessage, string newSenderKey, LuaValue newSender)
        {
            Script script = selectedScriptNode.ConvertToScript();
            DeleteScriptNode(selectedScriptNode);

            script.CodeEvent = new StrCode32(newCode, newMessage, newSenderKey, newSender);

            return Add(script);
        }
    }

    public class ScriptNode : TreeNode
    {
        public LuaString Identifier;

        public string Description;

        public ScriptalParentNode PreconditionsParent;

        public ScriptalParentNode OperationsParent;

        public ScriptNode(LuaString identifier, string commment, List<Scriptal> conditions, List<Scriptal> operations)
        {
            NodeFont = ScriptControl.BOLD;

            PreconditionsParent = new ScriptalParentNode(ScriptalType.Precondition, conditions);
            Nodes.Add(PreconditionsParent);

            OperationsParent = new ScriptalParentNode(ScriptalType.Operation, operations);
            Nodes.Add(OperationsParent);

            UpdateNodeText(identifier);
            Description = commment;
        }

        public override string ToString() => Identifier.ToString();

        public ScriptalNode AddPrecondition(Scriptal scriptal)
        {
            ScriptalNode scriptalNode = new ScriptalNode(scriptal, ScriptalType.Precondition);
            PreconditionsParent.Nodes.Add(scriptalNode);
            return scriptalNode;
        }

        public ScriptalNode AddOperation(Scriptal scriptal)
        {
            ScriptalNode scriptalNode = new ScriptalNode(scriptal, ScriptalType.Operation);
            OperationsParent.Nodes.Add(scriptalNode);
            return scriptalNode;
        }

        public void UpdateNodeText(LuaString identifier = null)
        {
            if (identifier != null)
            {
                Identifier = identifier;
                Name = identifier.Value;
            }
            Text = $"{Identifier.Value}   ({PreconditionsParent.Nodes.Count}|{OperationsParent.Nodes.Count})";
        }

        public void UpdateDescription(string description)
        {
            Description = description;
        }

        public StrCode32 getEvent()
        {
            MessageSenderNode msgSenderNode = (MessageSenderNode)Parent;

            if (msgSenderNode == null)
                throw new InvalidOperationException("Script node is not in a valid tree structure.");
            return msgSenderNode.GetEvent();
        }

        public Str32TableNode GetStrCode32TableNode()
        {
            MessageSenderNode msgSenderNode = (MessageSenderNode)Parent;
            CodeNode codeNode = (CodeNode)msgSenderNode?.Parent;
            Str32TableNode str32TableNode = (Str32TableNode)codeNode?.Parent;

            if (msgSenderNode == null || codeNode == null || str32TableNode == null)
                throw new InvalidOperationException("Script node is not in a valid tree structure.");
            return str32TableNode;
        }

        public List<VariableNode> GetAllDependencies()
        {
            return GetAllChoicesContainingDependencies().Select(choice => choice.Dependency).ToList();
        }

        public List<Choice> GetAllChoicesContainingDependencies()
        {
            List<Choice> choices = new List<Choice>();
            var script = ConvertToScript();
            foreach (var scriptal in script.Operations.Union(script.Preconditions))
            {
                foreach (var choice in scriptal.Choices)
                {
                    if (choice.Dependency != null)
                    {
                        choices.Add(choice);
                    }
                }
            }
            return choices;
        }

        public Script ConvertToScript()
        {
            MessageSenderNode msgSenderNode = (MessageSenderNode)Parent;
            CodeNode codeNode = (CodeNode)msgSenderNode?.Parent;

            if (msgSenderNode == null || codeNode == null)
                throw new InvalidOperationException("Script node is not in a valid tree structure.");

            return new Script
            {
                CodeEvent = new StrCode32
                {
                    CodeKey = codeNode.CodeKey,
                    Message = msgSenderNode.Message,
                    SenderKey = msgSenderNode.SenderKey,
                    SenderValue = msgSenderNode.Sender,
                },

                Identifier = Identifier,
                Description = Description,
                Preconditions = PreconditionsParent.GetScriptals(),
                Operations = OperationsParent.GetScriptals()
            };
        }

        public void MarkDependencies()
        {
            foreach (var dependency in GetAllDependencies())
            {
                dependency.NodeFont = ScriptControl.UNDERLINE;
            }
        }
    }

    public class ScriptalParentNode : TreeNode
    {
        public ScriptalType ScriptalType;

        public ScriptalParentNode(ScriptalType type, List<Scriptal> scriptals)
        {
            ScriptalType = type;
            Text = (ScriptalType == ScriptalType.Precondition) ? "Preconditions" : "Operations";
                
            foreach (Scriptal scriptal in scriptals)
            {
                Nodes.Add(new ScriptalNode(scriptal, ScriptalType));
            }
        }

        public List<Scriptal> GetScriptals()
        {
            List<Scriptal> childScriptals = new List<Scriptal>();
            int index = 1;
            foreach (ScriptalNode scriptalNode in Nodes)
            {
                scriptalNode.Scriptal.ScriptPrefixID = $"{(ScriptalType == ScriptalType.Precondition ? "Precondition" : "Operation")} {index++}: ";
                childScriptals.Add(scriptalNode.Scriptal);
            }

            return childScriptals;
        }

        public ScriptNode GetUnEventedScriptNode()
        {
            ScriptNode parentNode = (ScriptNode)Parent;

            if (parentNode == null)
                throw new InvalidOperationException("Script node is not in a valid tree structure.");
            return parentNode;
        }

        public void MarkDependencies()
        {
            foreach (var scriptal in Nodes.OfType<ScriptalNode>().Select(node => node.Scriptal))
            {
                foreach (var dependency in scriptal.Choices.Select(choice => choice.Dependency))
                {
                    if (dependency != null)
                    {
                        dependency.NodeFont = ScriptControl.UNDERLINE;
                    }
                }
            }
        }
    }

    public enum ScriptalType
    {
        Precondition,
        Operation
    }

    public class ScriptalNode : TreeNode
    {
        public ScriptalType ScriptalType;

        public Scriptal Scriptal;

        public ScriptalNode(Scriptal scriptal, ScriptalType scriptalType)
        {
            ScriptalType = scriptalType;
            Set(scriptal);
        }

        public override string ToString() => Scriptal.Name + $" {string.Join(" ", Scriptal.Choices.Select(choice => choice.ToShortString()))}";

        public void Set(Scriptal scriptal)
        {
            Scriptal = scriptal;
            Text = Scriptal.Name;
            Scriptal.SetRespectiveNode(this);
        }

        public void ClearAllVarNodeDependencies()
        {
            foreach (var choice in Scriptal.Choices)
            {
                choice.ClearVarNodeDependency(false);
            }
        }

        public ScriptNode GetUnEventedScriptNode()
        {
            return GetScriptalParentNode().GetUnEventedScriptNode();
        }

        public StrCode32 GetEvent()
        {
            return GetUnEventedScriptNode().getEvent();
        }

        public ScriptalParentNode GetScriptalParentNode()
        {
            if (Parent == null)
                throw new InvalidOperationException("Script node is not in a valid tree structure.");
            return (ScriptalParentNode)Parent;
        }

        public void MarkDependencies()
        {
            foreach (var dependency in Scriptal.Choices.Select(choice => choice.Dependency))
            {
                if (dependency != null)
                    dependency.NodeFont = ScriptControl.UNDERLINE;
            }
        }
    }

    public class MessageSenderNode : TreeNode
    {
        public LuaValue Message;

        public string SenderKey;

        public LuaValue Sender;

        public MessageSenderNode(LuaValue message, string senderKey, LuaValue sender)
        {
            Message = message;
            SenderKey = senderKey;
            Sender = sender;

            if (Sender is LuaNil)
            {
                Name = Message.ToString();
                Text = Name;
            } else 
            {
                Name = $"{Message} :: {Sender}";
                Text = Name;
            }
        }

        public Str32TableNode GetTableNode()
        {
            CodeNode codeNode = (CodeNode)Parent;

            if (codeNode == null)
                throw new InvalidOperationException("Script node is not in a valid tree structure.");
            return codeNode.GetTableNode();
        }

        public StrCode32 GetEvent()
        {
            CodeNode codeNode = (CodeNode)Parent;

            if (codeNode == null)
                throw new InvalidOperationException("Script node is not in a valid tree structure.");
            return new StrCode32
            {
                CodeKey = codeNode.CodeKey,
                Message = Message,
                SenderKey = SenderKey,
                SenderValue = Sender,
            };
        }

    }

    public class CodeNode : TreeNode
    {
        public string CodeKey;

        public Str32TableNode GetTableNode()
        {
            Str32TableNode tableNode = (Str32TableNode)Parent;

            if (tableNode == null)
                throw new InvalidOperationException("Script node is not in a valid tree structure.");
            return tableNode;
        }

        public CodeNode(string code)
        {
            CodeKey = code;
            Name = code;
            Text = code;
        }
    }
}
