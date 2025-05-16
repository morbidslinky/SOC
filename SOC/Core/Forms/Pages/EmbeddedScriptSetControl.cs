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
            ParentControl.SyncQuestDataToUserInput();
            ScriptDetails scriptDetails = ParentControl.Quest.ScriptDetails;

            checkedListBoxScripts.Items.Clear();
            checkedListBoxScripts.Items.AddRange(scriptDetails.QStep_Main.ToArray());

            checkedListBoxVariables.Items.Clear();
            checkedListBoxVariables.Items.AddRange(scriptDetails.VariableDeclarations.ToArray());

            int totalItems = checkedListBoxScripts.Items.Count + checkedListBoxVariables.Items.Count;
            int totalChecks = checkedListBoxScripts.CheckedItems.Count + checkedListBoxVariables.CheckedItems.Count;

            buttonExportVariablesScripts.Enabled = totalChecks != 0;
            splitContainerOuter.Visible = totalItems != 0;
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
                    checkedListBoxScripts.CheckedItems.OfType<Script>().ToList(), 
                    checkedListBoxVariables.CheckedItems.OfType<LuaTableEntry>().ToList()
                ).WriteToXml(saveFile.FileName);

                MessageBox.Show("Done!", "Script(s) Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void checkedListBoxScripts_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            int checkedCount = checkedListBoxScripts.CheckedItems.Count;

            if (e.NewValue == CheckState.Checked)
                checkedCount++;
            else if (e.NewValue == CheckState.Unchecked)
                checkedCount--;

            buttonExportVariablesScripts.Enabled = checkedCount + checkedListBoxVariables.CheckedItems.Count > 0;
        }

        private void checkedListBoxVariables_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            int checkedCount = checkedListBoxVariables.CheckedItems.Count;

            if (e.NewValue == CheckState.Checked)
                checkedCount++;
            else if (e.NewValue == CheckState.Unchecked)
                checkedCount--;

            buttonExportVariablesScripts.Enabled = checkedCount + checkedListBoxScripts.CheckedItems.Count > 0;
        }

        private void Insert(ScriptDetails scriptDetails)
        {
            foreach (var variable in scriptDetails.VariableDeclarations)
            {
                string name = variable.Key.Value.Trim('"');
                if (ParentControl.VariableNameExists(name, ParentControl.treeViewVariables.Nodes)) {
                    name = ParentControl.GetUniqueVariableName(name);
                }

                var varNode = new VariableNode(Lua.TableEntry(name, variable.Value));
                ParentControl.treeViewVariables.Nodes.Add(varNode);

                foreach(var script in scriptDetails.QStep_Main)
                {
                    foreach (var scriptal in script.Preconditionals.Union(script.Operationals))
                    {
                        foreach (var choice in scriptal.Choices)
                        {
                            if (choice.DependencyNameMatches(variable))
                            {
                                choice.SetVarNodeDependency(varNode);
                            }
                        }
                    }
                }
            }

            foreach(var script in scriptDetails.QStep_Main)
            {
                foreach (var scriptal in script.Preconditionals.Union(script.Operationals))
                {
                    scriptal.TryMapChoicesToTokens(out _);
                }
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
