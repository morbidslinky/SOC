using SOC.Classes.Lua;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using static SOC.Classes.Lua.Choice;

namespace SOC.UI
{
    public partial class EmbeddedScriptalControl : UserControl
    {
        private bool _isUpdatingControls = false;

        ScriptControl ParentControl;
        private ScriptalNode ScriptalNode;

        public const string CUSTOM_VARIABLE_SET = "Custom Variables";
        public const string NUMBER_LITERAL_SET = "Number Literal";
        public const string STRING_LITERAL_SET = "String Literal";
        public const string BOOLEAN_LITERAL_SET = "Boolean Literal";

        public EmbeddedScriptalControl(ScriptControl parentControl)
        {
            InitializeComponent();
            Dock = DockStyle.Fill;
            ParentControl = parentControl;
        }

        public override string ToString() => ScriptalNode.ToString();

        internal UserControl Menu(ScriptalNode scriptalNode)
        {
            ScriptalNode = scriptalNode;
            ParentControl.SetMenuText(ToString(), ScriptalNode.GetUnEventedScriptNode().Identifier.Text);
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

        internal void UpdateVarNodesUI()
        {
            Choice selectedChoice = (Choice)listBoxChoices.SelectedItem;
            ChoosableValues selectedChoiceSet = (ChoosableValues)comboBoxChoiceSet.SelectedItem;
            RefreshUserVarNodeChoiceValues(selectedChoice, selectedChoiceSet);
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

            chooseableSets.AddRange(selectedScriptal.EmbeddedChoosables
                .Union(ParentControl.Quest.GetAllObjectsScriptValueSets())
                .Where(set => selectedChoice.ChoosableValuesKeyFilter.Contains(set.Key)));
                
            if (selectedChoice.AllowUserVariable && selectedChoice.AllowUIEdit)
                chooseableSets.Add(new ChoosableValues() { Key = CUSTOM_VARIABLE_SET });

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
                case CUSTOM_VARIABLE_SET:
                    showCorrespondingChoiceControl(comboBoxUserVarNodes);
                    RefreshUserVarNodeChoiceValues(selectedChoice, selectedChoiceSet);
                    break;
                case NUMBER_LITERAL_SET:
                case STRING_LITERAL_SET:
                case BOOLEAN_LITERAL_SET:
                    selectedChoice.ClearVarNodeDependency();
                    // todo literals
                    break;
                default:
                    selectedChoice.ClearVarNodeDependency();
                    showCorrespondingChoiceControl(comboBoxPresetChoosables, selectedChoice.AllowUIEdit);
                    RefreshPresetChoiceValues(selectedChoice, selectedChoiceSet);
                    break;
            }

            ParentControl.RedrawScriptDependents(); RedrawVariableDependencies();
        }

        private void showCorrespondingChoiceControl(Control choiceControl, bool enable = true)
        {
            Control[] controlSet = { comboBoxPresetChoosables, comboBoxUserVarNodes };

            foreach (var control in controlSet)
            {
                control.Visible = control == choiceControl;
            }

            choiceControl.Enabled = enable;
        }

        private void RefreshUserVarNodeChoiceValues(Choice selectedChoice, ChoosableValues selectedChoiceSet)
        {
            if (selectedChoiceSet == null || selectedChoice == null) return;
            if (selectedChoiceSet.Key != CUSTOM_VARIABLE_SET || !selectedChoice.AllowUserVariable || !selectedChoice.AllowUIEdit) return;

            var userVarNodes = ParentControl.treeViewVariables.Nodes.OfType<VariableNode>().Where(node => selectedChoice.CorrespondingRuntimeToken.Allows(node.Entry.Value, out _));

            comboBoxUserVarNodes.Items.Clear();
            comboBoxUserVarNodes.Items.AddRange(userVarNodes.ToArray());

            var match = comboBoxUserVarNodes.Items.OfType<VariableNode>().FirstOrDefault(node => node == selectedChoice.Dependency);
            if (match == null && comboBoxUserVarNodes.Items.Count > 0)
                match = (VariableNode)comboBoxUserVarNodes.Items[0];

            comboBoxUserVarNodes.SelectedItem = match;
        }

        private void comboBoxUserVarNodes_SelectedIndexChanged(object sender, EventArgs e)
        {
            Choice selectedChoice = (Choice)listBoxChoices.SelectedItem;
            VariableNode selectedVarNode = (VariableNode)comboBoxUserVarNodes.SelectedItem;
            if (selectedChoice.AllowUIEdit && selectedChoice.AllowUserVariable)
            {
                selectedChoice.SetVarNodeDependency(selectedVarNode);
                if (!selectedChoice.HasPassthrough(this))
                    selectedChoice.VariableNodeEventPassthrough += SelectedChoice_VariableNodeEventPassthrough;

                RefreshListBoxDisplay();
            }

            ParentControl.RedrawScriptDependents(); RedrawVariableDependencies();
        }

        public void SelectedChoice_VariableNodeEventPassthrough(object sender, VariableNodeEventArgs e)
        {
            if (sender is Choice choice)
            {
                if (e.Doomed)
                {
                    choice.VariableNodeEventPassthrough -= SelectedChoice_VariableNodeEventPassthrough;
                }

                var updateIndex = listBoxChoices.Items.IndexOf(choice);
                RefreshListBoxDisplay(updateIndex);

                if ((Choice)listBoxChoices.SelectedItem == choice)
                {
                    Scriptal selectedScriptal = (Scriptal)comboBoxScriptal.SelectedItem;
                    RefreshChoiceSets(selectedScriptal, choice);
                }

                ParentControl.RedrawScriptDependents(); RedrawVariableDependencies();
            }
        }

        public void RedrawVariableDependencies()
        {
            ParentControl.UnmarkVariableDependencies();
            ScriptalNode.MarkDependencies();
        }

        private void RefreshPresetChoiceValues(Choice selectedChoice, ChoosableValues selectedChoiceSet)
        {
            IEnumerable<LuaValue> choosables = selectedChoiceSet.Values.Where(value => selectedChoice.CorrespondingRuntimeToken.Allows(value, out _));

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

        private void RefreshListBoxDisplay(int index = -1)
        {
            if (index < 0 || index >= listBoxChoices.Items.Count) 
                index = listBoxChoices.SelectedIndex;
            _isUpdatingControls = true;
            if (listBoxChoices.Items.Count > 0 && index >= 0)
                listBoxChoices.Items[index] = listBoxChoices.Items[index];
            _isUpdatingControls = false;
        }
    }
}
