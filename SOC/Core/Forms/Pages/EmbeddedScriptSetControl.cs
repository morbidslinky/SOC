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
            ParentControl.SetMenuText("Import/Export Script Details", "");
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
                    checkedListBoxVariables.CheckedItems.OfType<VariableNode>().Select(node => node.ConvertToLuaTableEntry()).ToList()
                ).WriteToXml(saveFile.FileName);

                MessageBox.Show("Done!", "Script(s) Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void checkedListBoxScripts_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            UpdateExportEnabled(e.NewValue == CheckState.Checked);
        }

        private void checkedListBoxVariables_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            UpdateExportEnabled(e.NewValue == CheckState.Checked);
        }

        private void UpdateExportEnabled(bool newCheckboxValue)
        {
            buttonExportVariablesScripts.Enabled = checkedListBoxVariables.CheckedItems.Count + checkedListBoxScripts.CheckedItems.Count + (newCheckboxValue ? 1 : -1) > 0;
        }

        private void Insert(ScriptDetails scriptDetails)
        {
            var incomingScripts = scriptDetails.QStep_Main;

            var incomingScriptals = incomingScripts
                .SelectMany(script => script.Preconditionals.Concat(script.Operationals))
                .ToList();

            var incomingChoices = incomingScriptals.SelectMany(scriptal => scriptal.Choices).ToList();

            foreach (var variable in scriptDetails.VariableDeclarations)
            {
                string name = variable.Key.Value.Trim('"');
                if (ParentControl.VariableNameExists(name, ParentControl.treeViewVariables.Nodes)) {
                    name = ParentControl.GetUniqueVariableName(name);
                }

                var varNode = new VariableNode(Lua.TableEntry(name, variable.Value));
                ParentControl.treeViewVariables.Nodes.Add(varNode);

                foreach (var choice in incomingChoices)
                {
                    if (choice.Key == EmbeddedScriptalControl.CUSTOM_VARIABLE_SET && choice.DependencyNameMatches(variable))
                        choice.SetVarNodeDependency(varNode);
                }
            }

            foreach (VariableNode variableNode in ParentControl.treeViewVariables.Nodes)
            {
                foreach (var choice in incomingChoices)
                {
                    if (choice.Key == EmbeddedScriptalControl.CUSTOM_VARIABLE_SET && choice.Dependency == null && choice.DependencyNameMatches(variableNode.ConvertToLuaTableEntry()))
                        choice.SetVarNodeDependency(variableNode);
                }
            }

            foreach (var choice in incomingChoices)
            {
                if (choice.Key == EmbeddedScriptalControl.CUSTOM_VARIABLE_SET && choice.Dependency == null)
                    choice.Value = new LuaNil();
            }

            foreach (var scriptal in incomingScriptals)
            {
                scriptal.TryMapChoicesToTokens(out _);
            }

            foreach (var script in incomingScripts)
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
    }
}
