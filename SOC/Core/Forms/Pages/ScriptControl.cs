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
    public partial class ScriptControl : UserControl
    {
        public Quest Quest;

        public ScriptTablesRootNode ScriptTablesRootNode = new ScriptTablesRootNode();

        EmbeddedScriptSetControl ScriptSetEmbed;
        EmbeddedScriptControl ScriptEmbed;
        EmbeddedScriptalControl ScriptalEmbed;

        public static ChoiceKeyValuesList StrCodeClasses = new ChoiceKeyValuesList();

        private bool _isUpdatingControls = false;

        public static readonly Font UNDERLINE = new Font("Consolas", 8.25F, FontStyle.Underline);
        public static readonly Font REGULAR = new Font("Consolas", 8.25F, FontStyle.Regular);

        public const string CUSTOM_VARIABLE_SET = "Custom Variable";
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

            treeViewScripts.Nodes.Add(ScriptTablesRootNode);
            Add(Quest.ScriptDetails);

            treeViewScripts.SelectedNode = ScriptTablesRootNode;
        }

        private static void ParseMessageClassesFile()
        {
            string MessageClassList = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "SOCassets//ScriptAssets//StrCodeClasses.xml");

            if (!File.Exists(MessageClassList))
            {
                MessageBox.Show("StrCodeClasses.xml file not found!");
                return;
            }

            StrCodeClasses = ChoiceKeyValuesList.LoadFromXml(MessageClassList);
        }

        public void Add(ScriptDetails incomingScriptDetails)
        {
            var variables = incomingScriptDetails.VariableDeclarations;

            var scripts = incomingScriptDetails.QStep_Main;

            var scriptals = scripts
                .SelectMany(script => script.Preconditionals.Concat(script.Operationals))
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
                string name = variable.Key.Value.Trim('"');
                if (VariableNameExists(name, treeViewVariables.Nodes))
                {
                    name = GetUniqueVariableName(name);
                }

                var variableNode = new VariableNode(Lua.TableEntry(name, variable.Value));

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
                                choiceIdentifier.IdentifierKeys[0] = Lua.String(choiceIdentifier.IdentifierKeys[0].Value.Trim('"') + $"_{choiceIdentifier.EvaluatesTo}");
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
                ScriptTablesRootNode.QStep_Main.Add(script);
            }
        }

        internal void SyncQuestDataToUserInput()
        {
            Quest.ScriptDetails.VariableDeclarations.Clear();
            Quest.ScriptDetails.VariableDeclarations.AddRange(treeViewVariables.Nodes.OfType<VariableNode>().Select(node => node.GetEntry()).ToList());

            Quest.ScriptDetails.QStep_Main.Clear();
            Quest.ScriptDetails.QStep_Main.AddRange(ScriptTablesRootNode.QStep_Main.ConvertToScripts());
        }
    }
}