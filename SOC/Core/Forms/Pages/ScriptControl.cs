using SOC.Classes.Common;
using SOC.Classes.Lua;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace SOC.UI
{
    public class DoubleBufferedTreeView : TreeView
    {
        public DoubleBufferedTreeView()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            UpdateStyles();
        }
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                // Enable composited style for even better flicker suppression
                cp.ExStyle |= 0x02000000; // WS_EX_COMPOSITED
                return cp;
            }
        }
    }

    public partial class ScriptControl : UserControl
    {
        public Quest Quest;

        public ScriptTablesRootNode ScriptTablesRootNode = new ScriptTablesRootNode();

        EmbeddedScriptSetControl ScriptSetEmbed;
        EmbeddedScriptControl ScriptEmbed;
        EmbeddedScriptalControl ScriptalEmbed;

        public static ChoiceKeyValuesList StrCode32Classes = new ChoiceKeyValuesList();

        internal bool _isUpdatingControls = false;

        public static readonly Font REGULAR = new Font("Consolas", 8.5F, FontStyle.Regular);
        public static readonly Font BOLD = new Font("Consolas", 8.5F, FontStyle.Bold);
        public static readonly Font UNDERLINE = new Font("Consolas", 8.5F, FontStyle.Underline);
        public static readonly Font UNDERLINE_BOLD = new Font("Consolas", 8.5F, FontStyle.Underline | FontStyle.Bold);

        public const string CUSTOM_VARIABLE_SET = "Variable";
        public const string NUMBER_LITERAL_SET = "Number Literal";
        public const string STRING_LITERAL_SET = "String Literal";
        public const string BOOLEAN_LITERAL_SET = "Boolean Literal";

        public ScriptControl(Quest quest)
        {
            InitializeComponent();
            Dock = DockStyle.Fill;

            ParseMessageClassesFile();
            Quest = quest;

            ScriptSetEmbed = new EmbeddedScriptSetControl(this);
            ScriptEmbed = new EmbeddedScriptControl(this);
            ScriptalEmbed = new EmbeddedScriptalControl(this);

            SyncUserInputToQuestData();

            if (ScriptTablesRootNode.IsEmpty() && treeViewVariables.Nodes.Count == 0)
            {
                AddDefault();
            }
        }

        private static void ParseMessageClassesFile()
        {
            string MessageClassList = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SOCassets", "ScriptAssets", "StrCode32Classes.xml");

            if (!File.Exists(MessageClassList))
            {
                MessageBox.Show("StrCodeClasses.xml file not found!");
                return;
            }

            StrCode32Classes = ChoiceKeyValuesList.LoadFromXml(MessageClassList);
        }

        public void Add(ScriptDetails incomingScriptDetails)
        {
            var variables = incomingScriptDetails.VariableDeclarations;

            var scripts = incomingScriptDetails.QStep_Main;

            var scriptals = scripts
                .SelectMany(script => script.Preconditions.Concat(script.Operations))
                .ToList();

            MapChoicesToCorrespondingRuntimeTokens(scriptals);

            var choices = scriptals.SelectMany(scriptal => scriptal.Choices).ToList();

            VariableNode[] incomingNodes = ConvertToVariableNodesWithDependencies(variables, choices);

            MapUnresolvedDependenciesFromControl(choices);

            VariableNode[] ExtrapolatedNodes = CreateVariableNodesForUnresolvedDependencies(choices);

            MapUnresolvedDependenciesToNil(choices);

            AddToVariableNodes(incomingNodes);

            if (ExtrapolatedNodes.Length > 0)
            {
                MessageBox.Show($"Notice: The imported script(s) depended on {ExtrapolatedNodes.Length} variable(s) that did not correspond to any imported or pre-existing variable(s).\n\nThe new dependency variable(s) have been extrapolated.", "Extrapolated Variable(s) Created", MessageBoxButtons.OK, MessageBoxIcon.Information);
                AddToVariableNodes(ExtrapolatedNodes);
            }

            AddToQStep_MainNode(scripts);
            RedrawScriptDependents();
        }

        public void AddDefault()
        {
            var defaultScriptDetails = Path.Combine(ScriptSetEmbed.ScriptExportDir, "Default.xml");
            if (File.Exists(defaultScriptDetails))
            {
                try
                {
                    Add(ScriptDetails.LoadFromXml(defaultScriptDetails));
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to load Default.xml script file: {ex.Message}");
                    return;
                }
            }
        }

        private void MapChoicesToCorrespondingRuntimeTokens(List<Scriptal> scriptals)
        {
            foreach (var scriptal in scriptals)
                scriptal.TryMapChoicesToCorrespondingRuntimeTokens(out _);
        }

        private VariableNode[] ConvertToVariableNodesWithDependencies(List<LuaTableEntry> variables, List<Choice> choices)
        {
            VariableNode[] variableNodes = new VariableNode[variables.Count];

            foreach (var variable in variables)
            {
                string name = variable.Key.TokenValue.Trim('"');
                if (VariableNameExists(name, treeViewVariables.Nodes))
                {
                    name = GetUniqueVariableName(name);
                }

                var variableNode = new VariableNode(Create.TableEntry(name, variable.Value));

                foreach (var choice in choices)
                {
                    if (choice.DependencyNameMatches(variable) &&
                        choice.CorrespondingRuntimeToken.Allows(variable.Value, out _))
                    {
                        choice.SetVarNodeDependency(variableNode);
                    }
                }

                variableNodes[variables.IndexOf(variable)] = variableNode;
            }

            return variableNodes;
        }

        private void MapUnresolvedDependenciesFromControl(List<Choice> choices)
        {
            foreach (VariableNode variableNode in treeViewVariables.Nodes)
            {
                foreach (var choice in choices)
                {
                    if (choice.Dependency == null &&
                        choice.DependencyNameMatches(variableNode.GetEntry()) &&
                        choice.CorrespondingRuntimeToken.Allows(variableNode.Entry.Value, out _))
                    {
                        choice.SetVarNodeDependency(variableNode);
                    }
                }
            }
        }

        private VariableNode[] CreateVariableNodesForUnresolvedDependencies(List<Choice> choices)
        {
            List<VariableNode> extrapolatedNodes = new List<VariableNode>();

            foreach (var choice in choices)
            {
                if (choice.Key == CUSTOM_VARIABLE_SET && choice.Dependency == null && choice.Value is LuaTableIdentifier choiceIdentifier)
                {
                    bool matchesExistingExtrapolatedNode = false;
                    foreach (VariableNode extrapolatedNode in extrapolatedNodes)
                    {
                        if (choice.DependencyNameMatches(extrapolatedNode.GetEntry()))
                        {
                            if (choiceIdentifier.EvaluatesTo == extrapolatedNode.GetEntry().Value.Type)
                            {
                                choice.SetVarNodeDependency(extrapolatedNode);
                                matchesExistingExtrapolatedNode = true;
                                break;
                            }
                            else
                            {
                                choiceIdentifier.IdentifierKeys[0] = Create.String(choiceIdentifier.IdentifierKeys[0].TokenValue.Trim('"') + $"_{choiceIdentifier.EvaluatesTo}");
                                break;
                            }
                        }
                    }

                    if (matchesExistingExtrapolatedNode)
                        continue;

                    VariableNode ExtrapolatedNode = VariableNode.ExtrapolateDefault(choiceIdentifier);
                    if (ExtrapolatedNode != null)
                    {
                        choice.SetVarNodeDependency(ExtrapolatedNode);
                        extrapolatedNodes.Add(ExtrapolatedNode);
                    }
                }
            }

            return extrapolatedNodes.ToArray();
        }

        private void MapUnresolvedDependenciesToNil(List<Choice> choices)
        {
            foreach (var choice in choices)
            {
                if (choice.Key == CUSTOM_VARIABLE_SET && choice.Dependency == null)
                {
                    choice.Value = new LuaNil();
                }
            }
        }

        private void AddToVariableNodes(VariableNode[] incomingNodes)
        {
            treeViewVariables.Nodes.AddRange(incomingNodes);
        }

        private void AddToQStep_MainNode(List<Script> scripts)
        {
            foreach (var script in scripts)
            {
                var scriptNode = ScriptTablesRootNode.QStep_Main.Add(script);
                ScriptTablesRootNode.QStep_Main.Expand(scriptNode.GetCodeNode());
            }
        }

        internal void SyncQuestDataToUserInput()
        {
            Quest.ScriptDetails.VariableDeclarations.Clear();
            Quest.ScriptDetails.VariableDeclarations.AddRange(treeViewVariables.Nodes.OfType<VariableNode>().Select(node => node.GetEntry()).ToList());

            Quest.ScriptDetails.QStep_Main.Clear();
            Quest.ScriptDetails.QStep_Main.AddRange(ScriptTablesRootNode.QStep_Main.ConvertToScripts());
        }

        internal void SyncUserInputToQuestData()
        {
            ScriptTablesRootNode = new ScriptTablesRootNode();

            treeViewVariables.Nodes.Clear();
            treeViewScripts.Nodes.Clear();

            treeViewScripts.Nodes.Add(ScriptTablesRootNode);
            Add(Quest.ScriptDetails);

            UpdateEmbeddedScriptSetDisplay();

            treeViewScripts.SelectedNode = ScriptTablesRootNode;
        }

        private TreeNode draggedNode = null;
        private TreeNodeCollection siblingNodes = null;

        private void treeView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (sender is not TreeView treeView) return;

            draggedNode = (TreeNode)e.Item;
            if (treeView.SelectedNode != draggedNode)
            {
                treeView.SelectedNode = draggedNode;
            }
            siblingNodes = draggedNode.Parent?.Nodes ?? treeView.Nodes;
            DoDragDrop(draggedNode, DragDropEffects.Move);
        }

        private void TreeView_DragOver(object sender, DragEventArgs e)
        {
            if (sender is not TreeView treeView) return;

            Point pt = treeView.PointToClient(new Point(e.X, e.Y));
            TreeNode hoverNode = treeView.GetNodeAt(pt);

            if (hoverNode == null || hoverNode == draggedNode || draggedNode is ScriptalParentNode || hoverNode.Parent != draggedNode?.Parent)
            {
                return;
            }

            e.Effect = DragDropEffects.Move;
            _isUpdatingControls = true;

            int hoverIndex = siblingNodes.IndexOf(hoverNode);
            int dragIndex = siblingNodes.IndexOf(draggedNode);

            if (hoverIndex != dragIndex)
            {
                siblingNodes.Remove(draggedNode);
                siblingNodes.Insert(hoverIndex, draggedNode);
                treeView.SelectedNode = draggedNode;
            }
        }

        private void TreeView_DragDrop(object sender, DragEventArgs e)
        {
            if (sender is not TreeView treeView) return;

            _isUpdatingControls = false;
            UpdateEmbeddedScriptSetDisplay();

            draggedNode = null;
            siblingNodes = null;
        }

        internal void UpdateEmbeddedScriptSetDisplay()
        {
            if (IsEmbedded(ScriptSetEmbed))
                ScriptSetEmbed.Menu();
            else if (IsEmbedded(ScriptalEmbed))
                ScriptalEmbed.UpdateVarNodesUI();
        }
    }
}