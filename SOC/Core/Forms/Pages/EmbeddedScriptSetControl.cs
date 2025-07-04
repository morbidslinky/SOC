﻿using SOC.Classes.Common;
using SOC.Classes.Lua;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace SOC.UI
{
    public partial class EmbeddedScriptSetControl : UserControl
    {
        ScriptControl ParentControl;
        public readonly string ScriptExportDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SOCassets", "ScriptAssets", "Script Library");

        public EmbeddedScriptSetControl(ScriptControl parentControl)
        {
            InitializeComponent();
            Dock = DockStyle.Fill;
            ParentControl = parentControl;
        }

        internal UserControl Menu()
        {
            UpdateMenu();
            return this;
        }

        internal void UpdateMenu()
        {
            RefreshScriptNodes();
            RefreshVariableNodes();

            int totalItems = checkedListBoxScripts.Items.Count + checkedListBoxVariables.Items.Count;
            int totalChecks = checkedListBoxScripts.CheckedItems.Count + checkedListBoxVariables.CheckedItems.Count;

            buttonExportVariablesScripts.Enabled = totalChecks != 0;
            splitContainerOuter.Visible = totalItems != 0;
            panelCheckDependencies.Enabled = totalItems != 0;
            textEmptyHint.Visible = totalItems == 0;
        }

        private void RefreshVariableNodes()
        {
            var varNodes = ParentControl.treeViewVariables.Nodes.OfType<VariableNode>().ToList();
            var checkedNodes = checkedListBoxVariables.CheckedItems.OfType<VariableNode>().ToList();

            checkedListBoxVariables.Items.Clear();
            for (int i = 0; i < varNodes.Count; i++)
            {
                checkedListBoxVariables.Items.Add(varNodes[i]);
                if (checkedNodes.Contains(varNodes[i]))
                    checkedListBoxVariables.SetItemChecked(i, true);
            }
        }

        private void RefreshScriptNodes()
        {
            var scriptNodes = ParentControl.ScriptTablesRootNode.QStep_Main.GetScriptNodes();
            var checkedNodes = checkedListBoxScripts.CheckedItems.OfType<ScriptNode>().ToList();

            checkedListBoxScripts.Items.Clear();
            for (int i = 0; i < scriptNodes.Count; i++)
            {
                checkedListBoxScripts.Items.Add(scriptNodes[i]);
                if (checkedNodes.Contains(scriptNodes[i]))
                    checkedListBoxScripts.SetItemChecked(i, true);
            }
        }

        private void RefreshCheckBoxes()
        {
            foreach (ScriptNode scriptNode in checkedListBoxScripts.CheckedItems)
                checkmarkDependencies(scriptNode, true);
        }


        private void buttonLoadScript_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(ScriptExportDir))
            {
                Directory.CreateDirectory(ScriptExportDir);
            }
            OpenFileDialog loadFile = new OpenFileDialog();
            loadFile.Filter = "Xml Files|*.xml|All Files|*.*";
            loadFile.InitialDirectory = ScriptExportDir;

            DialogResult result = loadFile.ShowDialog();
            if (result != DialogResult.OK) return;

            try
            {
                ParentControl.Add(ScriptDetails.LoadFromXml(loadFile.FileName));
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
            if (!Directory.Exists(ScriptExportDir))
            {
                Directory.CreateDirectory(ScriptExportDir);
            }
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "Xml File|*.xml";
            saveFile.InitialDirectory = ScriptExportDir;
            saveFile.FileName = $"{ParentControl.Quest.SetupDetails.FpkName}.Script";
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
