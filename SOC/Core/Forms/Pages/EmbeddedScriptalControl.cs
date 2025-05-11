using SOC.Classes.Common;
using SOC.Classes.Lua;
using SOC.QuestObjects.Common;
using SOC.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using static SOC.Classes.Lua.Choice;

namespace SOC.UI
{
    public partial class EmbeddedScriptalControl : UserControl
    {
        private bool _isUpdatingControls = false;

        private ScriptalNode ScriptalNode;
        private TreeView TreeViewVariables;
        private List<ChoosableValues> QuestValueSets = new List<ChoosableValues>();

        public EmbeddedScriptalControl()
        {
            InitializeComponent();
            Dock = DockStyle.Fill;
        }

        public override string ToString() => ScriptalNode.ToString();

        internal UserControl Menu(ScriptalNode scriptalNode, TreeView treeViewVariables, List<ChoosableValues> questValueSets)
        {
            ScriptalNode = scriptalNode;
            TreeViewVariables = treeViewVariables;
            QuestValueSets = questValueSets;
            UpdateMenu();
            return this;
        }

        internal void UpdateMenu()
        {
            string scriptalType = str(ScriptalNode.ScriptalType);

            groupBoxScriptalSelect.Text = $"{scriptalType}";

            Scriptal[] scriptalTemplates = GetTemplates(ScriptalNode);

            comboBoxScriptal.Items.Clear();
            comboBoxScriptal.Items.Add(ScriptalNode.ScriptalType == ScriptalType.Preconditional ? Scriptal.AlwaysTrue() : Scriptal.DoNothing());
            comboBoxScriptal.Items.AddRange(scriptalTemplates);

            Scriptal matchingTemplate = scriptalTemplates.FirstOrDefault(template => template.Name == ScriptalNode.Scriptal.Name && template.EventFunctionTemplate == ScriptalNode.Scriptal.EventFunctionTemplate);
            if (matchingTemplate != null)
            {
                int matchingIndex = comboBoxScriptal.Items.IndexOf(matchingTemplate);
                comboBoxScriptal.Items.RemoveAt(matchingIndex);
                comboBoxScriptal.Items.Insert(matchingIndex, ScriptalNode.Scriptal);
                comboBoxScriptal.SelectedItem = ScriptalNode.Scriptal;
            }
            else
            {
                comboBoxScriptal.SelectedIndex = 0;
            }

            SetDescription(ScriptalNode.Scriptal);
            SetChoiceMenu(ScriptalNode.Scriptal);
        }

        internal static string str(ScriptalType type)
        {
            return type == ScriptalType.Preconditional ? "Preconditional" : "Operational";
        }

        private void comboBoxScriptal_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetDescription((Scriptal)comboBoxScriptal.SelectedItem);
        }

        private void buttonApplyTemplate_Click(object sender, EventArgs e)
        {
                ScriptalNode.Set((Scriptal)comboBoxScriptal.SelectedItem);
                SetChoiceMenu(ScriptalNode.Scriptal);
        }

        private void SetDescription(Scriptal scriptal)
        {
            groupBoxDescription.Text = $"Template Description :: \"{scriptal.Name}\"";
            textBoxDescription.Text = scriptal.Description +
                $"\r\n\r\nEvent Function:\r\n{scriptal.EventFunctionTemplate}";
            if (scriptal.EmbeddedChoosables.Count > 0)
            {
                textBoxDescription.Text += $"\r\n\r\nProvided Value Sets:\r\n\r\n- {string.Join("\r\n\r\n- ", scriptal.EmbeddedChoosables.Select(choosable => $"{choosable.Key}:\r\n     {string.Join("\r\n     ", choosable.Values)}"))}";
            }
        }

        private void SetChoiceMenu(Scriptal scriptal)
        {
            groupBoxChoicesList.Text = $"Choices List :: \"{scriptal.Name}\"";

            listBoxChoices.Items.Clear();
            listBoxChoices.Items.AddRange(scriptal.Choices.ToArray());

            if (scriptal.Choices.Count > 0)
            {
                groupBoxChoicesList.Enabled = true;
                groupBoxChoice.Enabled = true;
                textBoxChoiceDescription.TextAlign = HorizontalAlignment.Left;
                listBoxChoices.SelectedIndex = 0;
            }
            else
            {
                listBoxChoices.Items.Clear();
                comboBoxChoiceSet.Items.Clear();
                comboBoxPresetChoosables.Items.Clear();
                textBoxChoiceDescription.TextAlign = HorizontalAlignment.Center;
                textBoxChoiceDescription.Text = "\r\n[Event Function does not contain choices]";
                groupBoxChoicesList.Enabled = false;
                groupBoxChoice.Enabled = false;
            }
        }

        internal Scriptal[] GetTemplates(ScriptalNode scriptalNode)
        {
            List<Scriptal> scriptals = new List<Scriptal>();

            List<FileInfo> scriptalFiles = GetScriptalFiles(scriptalNode);

            Exception lastExeption = null; int exceptionCount = 0;
            foreach (FileInfo scriptalFile in scriptalFiles)
            {
                try
                {
                    scriptals.Add(Scriptal.LoadFromXml(scriptalFile.FullName));
                }
                catch (Exception e)
                {
                    exceptionCount++;
                    lastExeption = e;
                }
            }
            if (lastExeption != null)
            {
                MessageBox.Show($"Error: {exceptionCount} error(s) occurred while attempting to load the Scriptal Template files. \n\nThe broken templates will not be listed. \n\nLast Error: {lastExeption.Message}", "An Error has occurred", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            return scriptals.ToArray();
        }

        internal List<FileInfo> GetScriptalFiles(ScriptalNode scriptalNode)
        {
            List<FileInfo> scriptalFiles = new List<FileInfo>();

            StrCode32Event scriptalEvent = scriptalNode.GetEvent();

            string scriptsRootPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "SOCassets//ScriptAssets");
            string[] scriptalSubDirs = { "Scriptals", str(scriptalNode.ScriptalType), scriptalEvent.StrCode32.Text, scriptalEvent.msg.Text };

            Exception lastExeption = null; int exceptionCount = 0;
            for (int i = 0; i < scriptalSubDirs.Length; i++)
            {
                scriptsRootPath = Path.Combine(scriptsRootPath, scriptalSubDirs[i]);
                try
                {
                    DirectoryInfo scriptalDir = new DirectoryInfo(scriptsRootPath);
                    if (scriptalDir.Exists)
                    {
                        scriptalFiles.AddRange(scriptalDir.GetFiles("*.xml"));
                    }
                }
                catch (Exception e)
                {
                    exceptionCount++;
                    lastExeption = e;
                }
            }
            if (lastExeption != null)
            {
                MessageBox.Show($"Error: {exceptionCount} error(s) occurred while attempting to read the files in the ScriptAssets directory. \n\nLast Error: {lastExeption.Message}", "An Error has occurred", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            return scriptalFiles;
        }

        private void checkBoxChoiceFilter_CheckedChanged(object sender, EventArgs e)
        {
            Choice selectedChoice = (Choice)listBoxChoices.SelectedItem;
            Scriptal selectedScriptal = (Scriptal)comboBoxScriptal.SelectedItem;
            selectedChoice.EnableGuardrails = checkBoxChoiceFilter.Checked;

            RefreshChoiceSets(selectedScriptal, selectedChoice);
        }

        private void listBoxChoices_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isUpdatingControls) return;

            Scriptal selectedScriptal = (Scriptal)comboBoxScriptal.SelectedItem;
            Choice selectedChoice = (Choice)listBoxChoices.SelectedItem;

            groupBoxChoice.Text = $"Choice :: \"{selectedChoice.Name}\"";
            textBoxChoiceDescription.Text = selectedChoice.Description + RestrictionsToString(selectedChoice);

            RefreshChoiceSets(selectedScriptal, selectedChoice);
            comboBoxChoiceSet.Enabled = selectedChoice.AllowUIEdit;
        }

        private string RestrictionsToString(Choice selectedChoice)
        {
            return string.Format(@"

Template Restrictions:
{0,-20}{1}

{2,-20}{3}",
"Index|Type Safety:", $"|[{selectedChoice.CorrespondingRuntimeToken.PlaceholderString}]|",
"Allow User Edits:", selectedChoice.AllowUIEdit) + (selectedChoice.AllowUIEdit ?
string.Format(@"
{0,-20}{1}
{2,-20}{3}
{4,-20}{5}",
"Allow User Vars:", selectedChoice.AllowUserVariable,
"Allow Literals:", selectedChoice.AllowLiteral,
"Allow Value Sets:", string.Join(", ", selectedChoice.ChoosableValuesKeyFilter)) : "");
        }

        private void RefreshChoiceSets(Scriptal selectedScriptal, Choice selectedChoice)
        {
            comboBoxChoiceSet.Items.Clear();
            comboBoxChoiceSet.Items.AddRange(GetChoosableValuesSets(selectedScriptal, selectedChoice));

            var match = comboBoxChoiceSet.Items
                .OfType<ChoosableValues>()
                .FirstOrDefault(set => set.Key == selectedChoice.Key);

            if (match == null && comboBoxChoiceSet.Items.Count > 0)
                match = (ChoosableValues)comboBoxChoiceSet.Items[0];

            comboBoxChoiceSet.SelectedItem = match;

            comboBoxChoiceSet.Enabled = selectedChoice.AllowUIEdit;
        }

        private ChoosableValues[] GetChoosableValuesSets(Scriptal selectedScriptal, Choice selectedChoice)
        {
            List<ChoosableValues> chooseableSets = new List<ChoosableValues>();

            if (selectedChoice.EnableGuardrails)
                chooseableSets.AddRange(selectedScriptal.EmbeddedChoosables
                    .Union(QuestValueSets)
                    .Where(set => selectedChoice.ChoosableValuesKeyFilter.Contains(set.Key)));
            else
                chooseableSets.AddRange(selectedScriptal.EmbeddedChoosables);
                
            if (selectedChoice.AllowUserVariable && selectedChoice.AllowUIEdit)
                chooseableSets.Add(new ChoosableValues() { Key = "CUSTOM VARIABLES" });

            if (selectedChoice.AllowLiteral)
            {
                chooseableSets.Add(new ChoosableValues() { Key = "NUMBER" });
                chooseableSets.Add(new ChoosableValues() { Key = "STRING" });
                chooseableSets.Add(new ChoosableValues() { Key = "BOOLEAN" });
            }

            return chooseableSets.ToArray();
        }

        private void comboBoxChoiceSet_SelectedIndexChanged(object sender, EventArgs e)
        {
            Choice selectedChoice = (Choice)listBoxChoices.SelectedItem;
            ChoosableValues selectedChoiceSet = (ChoosableValues)comboBoxChoiceSet.SelectedItem;
            selectedChoice.Key = selectedChoiceSet.Key;

            switch (selectedChoice.Key)
            {
                case "CUSTOM VARIABLES":
                    showCorrespondingChoiceControl(comboBoxUserVariables);
                    RefreshUserVariableChoiceValues(selectedChoice, selectedChoiceSet);
                    break;
                case "NUMBER":
                case "STRING":
                case "BOOLEAN":
                    // todo literals
                    break;
                default:
                    showCorrespondingChoiceControl(comboBoxPresetChoosables, selectedChoice.AllowUIEdit);
                    RefreshPresetChoiceValues(selectedChoice, selectedChoiceSet);
                    break;
            }
        }

        private void showCorrespondingChoiceControl(Control choiceControl, bool enable = true)
        {
            Control[] controlSet = { comboBoxPresetChoosables, comboBoxUserVariables };

            foreach (var control in controlSet)
            {
                control.Visible = control == choiceControl;
            }

            choiceControl.Enabled = enable;
        }

        private void RefreshUserVariableChoiceValues(Choice selectedChoice, ChoosableValues selectedChoiceSet)
        {
            if (selectedChoiceSet.Key != "CUSTOM VARIABLES" || !selectedChoice.AllowUserVariable || !selectedChoice.AllowUIEdit) return;
            IEnumerable<VariableNode> userVariables = TreeViewVariables.Nodes.OfType<VariableNode>();

            if (selectedChoice.EnableGuardrails)
                userVariables = userVariables.Where(node => selectedChoice.CorrespondingRuntimeToken.Allows(node.Entry.Value, out _));

            comboBoxUserVariables.Items.Clear();
            comboBoxUserVariables.Items.AddRange(userVariables.ToArray());
        }

        private void comboBoxUserVariables_SelectedIndexChanged(object sender, EventArgs e)
        {
            Choice selectedChoice = (Choice)listBoxChoices.SelectedItem;
            VariableNode selectedValue = (VariableNode)comboBoxUserVariables.SelectedItem;
            if (selectedChoice.AllowUIEdit && selectedChoice.AllowUserVariable)
            {
                selectedChoice.SetUserVariableNodeDependency(selectedValue);
                selectedChoice.Value = selectedValue.AsLuaTableIdentifier();
                RefreshListBoxDisplay();
            }
        }

        private void RefreshPresetChoiceValues(Choice selectedChoice, ChoosableValues selectedChoiceSet)
        {
            IEnumerable<LuaValue> choosables;

            if (selectedChoice.EnableGuardrails)
                choosables = selectedChoiceSet.Values.Where(value => selectedChoice.CorrespondingRuntimeToken.Allows(value, out _));
            else
                choosables = selectedChoiceSet.Values;

            comboBoxPresetChoosables.Items.Clear();
            comboBoxPresetChoosables.Items.AddRange(choosables.ToArray());

            var match = selectedChoiceSet.Values.FirstOrDefault(value => value.Matches(selectedChoice.Value));
            if (match == null && comboBoxPresetChoosables.Items.Count > 0)
                match = (LuaValue)comboBoxPresetChoosables.Items[0];

            comboBoxPresetChoosables.SelectedItem = match;
        }

        private void comboBoxPresetChoosables_SelectedIndexChanged(object sender, EventArgs e)
        {
            Choice selectedChoice = (Choice)listBoxChoices.SelectedItem;
            if (selectedChoice.AllowUIEdit)
            {
                LuaValue selectedValue = (LuaValue)comboBoxPresetChoosables.SelectedItem;
                selectedChoice.Value = selectedValue;
                RefreshListBoxDisplay();
            }
        }

        private void RefreshListBoxDisplay()
        {
            _isUpdatingControls = true;
            listBoxChoices.Items[listBoxChoices.SelectedIndex] = listBoxChoices.Items[listBoxChoices.SelectedIndex];
            _isUpdatingControls = false;
        }
    }
}
