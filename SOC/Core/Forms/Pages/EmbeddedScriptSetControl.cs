using SOC.Classes.Common;
using SOC.Classes.Lua;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace SOC.UI
{
    public partial class EmbeddedScriptSetControl : UserControl
    {
        ScriptControl ParentControl;

        public EmbeddedScriptSetControl(ScriptControl parentControl)
        {
            InitializeComponent();
            Dock = DockStyle.Fill;
            ParentControl = parentControl;
        }

        internal UserControl Menu()
        {
            UpdateMenu();
            ParentControl.SetMenuText("Import / Export Script Details", "");
            return this;
        }

        private void UpdateMenu()
        {
            var varNodes = ParentControl.treeViewVariables.Nodes.OfType<VariableNode>().ToList();
            foreach (VariableNode varNode in varNodes)
            {
                if (!checkedListBoxVariables.Items.Contains(varNode))
                    checkedListBoxVariables.Items.Add(varNode);
            }

            foreach (VariableNode varNode in checkedListBoxVariables.Items.OfType<VariableNode>().ToList())
            {
                if (!varNodes.Contains(varNode))
                    checkedListBoxVariables.Items.Remove(varNode);
            }

            var scriptNodes = ParentControl.ScriptTablesRootNode.QStep_Main.GetScriptNodes();
            foreach (ScriptNode scriptNode in scriptNodes)
            {
                if (!checkedListBoxScripts.Items.Contains(scriptNode))
                    checkedListBoxScripts.Items.Add(scriptNode);
            }

            foreach (ScriptNode scriptNode in checkedListBoxScripts.Items.OfType<ScriptNode>().ToList())
            {
                if (!scriptNodes.Contains(scriptNode))
                    checkedListBoxScripts.Items.Remove(scriptNode);
            }

            int totalItems = checkedListBoxScripts.Items.Count + checkedListBoxVariables.Items.Count;
            int totalChecks = checkedListBoxScripts.CheckedItems.Count + checkedListBoxVariables.CheckedItems.Count;

            buttonExportVariablesScripts.Enabled = totalChecks != 0;
            splitContainerOuter.Visible = totalItems != 0;
            panelCheckDependencies.Enabled = totalItems != 0;
            textEmptyHint.Visible = totalItems == 0;
        }

        private void buttonLoadScript_Click(object sender, EventArgs e)
        {
            OpenFileDialog loadFile = new OpenFileDialog();
            loadFile.Filter = "Xml Files|*.xml|All Files|*.*";

            DialogResult result = loadFile.ShowDialog();
            if (result != DialogResult.OK)
                return;

            try
            {
                Insert(ScriptDetails.LoadFromXml(loadFile.FileName));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load script(s) file: {ex.Message}");
                return;
            }

            UpdateMenu();
        }

        private void buttonSaveScript_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "Xml File|*.xml";
            saveFile.FileName = $"{ParentControl.Quest.SetupDetails.FpkName}.ScriptExport";
            DialogResult saveResult = saveFile.ShowDialog();

            if (saveResult == DialogResult.OK)
            {
                new ScriptDetails(
                    checkedListBoxScripts.CheckedItems.OfType<ScriptNode>().Select(node => node.ConvertToScript()).ToList(), 
                    checkedListBoxVariables.CheckedItems.OfType<VariableNode>().Select(node => node.GetEntry()).ToList()
                ).WriteToXml(saveFile.FileName);

                MessageBox.Show("Done!", "Script(s) Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void checkedListBoxScripts_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            UpdateExportEnabled(e.NewValue == CheckState.Checked);

            if (checkBoxDependencies.Checked)
            {
                checkmarkDependencies((ScriptNode)checkedListBoxScripts.Items[e.Index], e.NewValue == CheckState.Checked);
            }
        }

        private void checkmarkDependencies(ScriptNode scriptNode, bool isCheck)
        {
            var dependencies = scriptNode.GetAllDependencies();

            if (isCheck)
            {
                foreach (var dependency in dependencies)
                {
                    int i = checkedListBoxVariables.Items.IndexOf(dependency);
                    if (i != -1)
                    {
                        checkedListBoxVariables.SetItemChecked(i, true);
                    }
                }
                return;
            }

            var theOtherScriptNodes = checkedListBoxScripts.CheckedItems.OfType<ScriptNode>().Where(node => node != scriptNode);
            var theOtherDependencies = theOtherScriptNodes.SelectMany(node => node.GetAllDependencies());

            foreach(var dependency in dependencies)
            {
                if (theOtherDependencies.Contains(dependency))
                {
                    continue;
                }

                int i = checkedListBoxVariables.Items.IndexOf(dependency);
                if (i != -1)
                {
                    BeginInvoke(new Action(() =>
                    {
                        checkedListBoxVariables.SetItemChecked(i, false);
                    }));
                }
            }
        }

        private void checkedListBoxVariables_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            UpdateExportEnabled(e.NewValue == CheckState.Checked);

            if (e.NewValue != CheckState.Checked && checkBoxDependencies.Checked)
            {
                var currentDependencies = checkedListBoxScripts.CheckedItems.OfType<ScriptNode>().SelectMany(node => node.GetAllDependencies());
                if (currentDependencies.Contains(checkedListBoxVariables.Items[e.Index]))
                {
                    e.NewValue = CheckState.Checked;
                }
            }
        }

        private void UpdateExportEnabled(bool newCheckboxValue)
        {
            buttonExportVariablesScripts.Enabled = checkedListBoxVariables.CheckedItems.Count + checkedListBoxScripts.CheckedItems.Count + (newCheckboxValue ? 1 : -1) > 0;
        }

        private void Insert(ScriptDetails incomingScriptDetails)
        {
            var variables = incomingScriptDetails.VariableDeclarations;

            var scripts = incomingScriptDetails.QStep_Main;

            var scriptals = scripts
                .SelectMany(script => script.Preconditionals.Concat(script.Operationals))
                .ToList();
            MapChoicesToTokens(scriptals);

            var choices = scriptals.SelectMany(scriptal => scriptal.Choices).ToList();

            VariableNode[] incomingNodes = ConvertToVariableNodesWithDependencies(variables, choices);

            MapUnresolvedDependenciesFromControl(choices);
            
            VariableNode[] bestGuessNodes = CreateVariableNodesForUnresolvedDependencies(choices);

            MapUnresolvedDependenciesToNil(choices);

            AddToControl(incomingNodes);

            if (bestGuessNodes.Length > 0)
            {
                MessageBox.Show($"Notice: The imported script(s) depended on {bestGuessNodes.Length} variable(s) that did not correspond to any imported or pre-existing variable(s).\n\nThe new dependency variable(s) have been extrapolated.", "Extrapolated Variable(s) Created", MessageBoxButtons.OK, MessageBoxIcon.Information);
                AddToControl(bestGuessNodes);
            }

            AddToControl(scripts);
            ParentControl.RedrawScriptDependents();
        }

        private void MapChoicesToTokens(List<Scriptal> scriptals)
        {
            foreach (var scriptal in scriptals)
                scriptal.TryMapChoicesToTokens(out _);
        }

        private VariableNode[] ConvertToVariableNodesWithDependencies(List<LuaTableEntry> variables, List<Choice> choices)
        {
            VariableNode[] variableNodes = new VariableNode[variables.Count];

            foreach (var variable in variables)
            {
                string name = variable.Key.Value.Trim('"');
                if (ParentControl.VariableNameExists(name, ParentControl.treeViewVariables.Nodes))
                {
                    name = ParentControl.GetUniqueVariableName(name);
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
            foreach (VariableNode variableNode in ParentControl.treeViewVariables.Nodes)
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
                if (choice.Key == ScriptControl.CUSTOM_VARIABLE_SET && choice.Dependency == null && choice.Value is LuaTableIdentifier choiceIdentifier)
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
                if (choice.Key == ScriptControl.CUSTOM_VARIABLE_SET && choice.Dependency == null)
                {
                    choice.Value = new LuaNil();
                }
            }
        }

        private void AddToControl(VariableNode[] incomingNodes)
        {
            ParentControl.treeViewVariables.Nodes.AddRange(incomingNodes);
        }

        private void AddToControl(List<Script> scripts)
        {
            foreach (var script in scripts)
            {
                ParentControl.ScriptTablesRootNode.QStep_Main.Add(script);
            }
        }

        private void checkedListBoxVariables_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkedListBoxVariables.ClearSelected();
        }

        private void checkedListBoxScripts_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkedListBoxScripts.ClearSelected();
        }

        private void checkBoxDependencies_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxDependencies.Checked)
                foreach(ScriptNode node in checkedListBoxScripts.CheckedItems)
                    checkmarkDependencies(node, true);
        }
    }
}
