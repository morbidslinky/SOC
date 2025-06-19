using SOC.Classes.Lua;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using static SOC.Classes.Lua.Choice;

namespace SOC.UI
{
    public partial class EmbeddedScriptalControl : UserControl
    {
        private bool _isUpdatingControls = false;

        ScriptControl ParentControl;
        private ScriptalNode ScriptalNode;
        private Scriptal[] ScriptalTemplates;

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
            UpdateCategoryFilter();
            SetChoiceMenu(ScriptalNode.Scriptal);
            return this;
        }

        internal void UpdateCategoryFilter()
        {
            string scriptalType = str(ScriptalNode.ScriptalType);

            groupBoxScriptalSelect.Text = $"{scriptalType}";

            ScriptalTemplates = GetTemplates(ScriptalNode);

            string[] categories = ScriptalTemplates.Select(x => x.Category).Distinct().ToArray();

            comboBoxTemplateCategory.Items.Clear();
            comboBoxTemplateCategory.Items.AddRange(categories);
            if (!comboBoxTemplateCategory.Items.Contains("ALL"))
            {
                comboBoxTemplateCategory.Items.Insert(0, "ALL");
            }

            int matchingIndex = comboBoxTemplateCategory.Items.IndexOf(ScriptalNode.Scriptal.Category);
            if (matchingIndex == -1)
            {
                matchingIndex = 0;
            }
            comboBoxTemplateCategory.SelectedIndex = matchingIndex;
        }

        private void comboBoxTemplateCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxScriptalTemplate.Items.Clear();
            string selectedCategory = (string)comboBoxTemplateCategory.SelectedItem;

            if (selectedCategory == "ALL")
            {
                comboBoxScriptalTemplate.Items.Add(ScriptalNode.ScriptalType == ScriptalType.Precondition ? Scriptal.AlwaysTrue() : Scriptal.DoNothing());
                comboBoxScriptalTemplate.Items.AddRange(ScriptalTemplates);
            }
            else
            {
                comboBoxScriptalTemplate.Items.AddRange([.. ScriptalTemplates.Where(template => template.Category == selectedCategory)]);
            }

            Scriptal matchingTemplate = comboBoxScriptalTemplate.Items.OfType<Scriptal>().FirstOrDefault(template => template.Name == ScriptalNode.Scriptal.Name && template.EventFunctionTemplate == ScriptalNode.Scriptal.EventFunctionTemplate);
            if (matchingTemplate != null)
            {
                int matchingIndex = comboBoxScriptalTemplate.Items.IndexOf(matchingTemplate);
                comboBoxScriptalTemplate.Items.RemoveAt(matchingIndex);
                comboBoxScriptalTemplate.Items.Insert(matchingIndex, ScriptalNode.Scriptal);
                comboBoxScriptalTemplate.SelectedItem = ScriptalNode.Scriptal;
            }
            else
            {
                comboBoxScriptalTemplate.SelectedIndex = 0;
            }

            SetDescription((Scriptal)comboBoxScriptalTemplate.SelectedItem);
        }

        internal static string str(ScriptalType type)
        {
            return type == ScriptalType.Precondition ? "Precondition" : "Operation";
        }

        private void comboBoxScriptal_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetDescription((Scriptal)comboBoxScriptalTemplate.SelectedItem);
        }

        private void buttonApplyTemplate_Click(object sender, EventArgs e)
        {
            if ((Scriptal)comboBoxScriptalTemplate.SelectedItem == ScriptalNode.Scriptal) return;

            foreach (Choice choice in listBoxChoices.Items.OfType<Choice>().ToArray())
            {
                choice.ClearVarNodeDependency(false);
            }
            ParentControl.RedrawScriptDependents(); RedrawVariableDependencies();
            ScriptalNode.Set((Scriptal)comboBoxScriptalTemplate.SelectedItem);
            SetChoiceMenu(ScriptalNode.Scriptal);
        }

        private void SetDescription(Scriptal scriptal)
        {
            groupBoxDescription.Text = $"Template Description :: \"{scriptal.Name}\"";

            StringBuilder descriptionBuilder = new StringBuilder(scriptal.Description);

            if (!string.IsNullOrEmpty(scriptal.EventFunctionTemplate))
            {
                descriptionBuilder.Append($"\n\n\nEvent Function:\n{scriptal.EventFunctionTemplate}");
            }

            if (scriptal.EmbeddedChoosables.ChoiceKeyValues.Count > 0)
            {
                descriptionBuilder.Append($"\n\n\nProvided Value Sets:\n\n- {string.Join("\n\n- ", scriptal.EmbeddedChoosables.ChoiceKeyValues.Select(choosable => $"{choosable.Key}:\n     {string.Join("\n     ", choosable.Values)}"))}");
            }

            if (scriptal.CommonDefinitions.Count > 0)
            {
                descriptionBuilder.Append($"\n\n\nProvided qvars Definitions:\n\n- {string.Join("\n\n- ", scriptal.CommonDefinitions.Select(definition => $"{definition.Key}:\n{definition.Value}"))}");
            }

            textBoxDescription.Text = descriptionBuilder.ToString().Replace("\n", "\r\n");
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

            List<FileInfo> scriptalFiles = GetScriptalTemplates(scriptalNode);

            Exception lastExeption = null; string failedFileName = ""; int exceptionCount = 0;
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
                    failedFileName = scriptalFile.Name;
                }
            }
            if (lastExeption != null)
            {
                MessageBox.Show($"Error: {exceptionCount} error(s) occurred while attempting to load the Scriptal Template files. \n\nThe broken templates will not be listed. \n\nLast Error: {failedFileName}\n{lastExeption.Message}", "An Error has occurred", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            return scriptals.ToArray();
        }

        internal List<FileInfo> GetScriptalTemplates(ScriptalNode scriptalNode)
        {
            List<FileInfo> scriptalFiles = new List<FileInfo>();

            StrCode32 scriptalEvent = scriptalNode.GetEvent();

            string scriptsRootPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SOCassets", "ScriptAssets");
            string[] scriptalSubDirs = { "Scriptal Library", str(scriptalNode.ScriptalType), scriptalEvent.CodeKey, scriptalEvent.Message.TokenValue.Trim('"') };

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
                MessageBox.Show($"Error: {exceptionCount} error(s) occurred while attempting to read the files in the Scriptal Library. \n\nLast Error: {lastExeption.Message}", "An Error has occurred", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            return scriptalFiles;
        }

        private void listBoxChoices_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isUpdatingControls) return;

            Scriptal selectedScriptal = (Scriptal)comboBoxScriptalTemplate.SelectedItem;
            Choice selectedChoice = (Choice)listBoxChoices.SelectedItem;

            groupBoxChoice.Text = $"Choice :: \"{selectedChoice.Name}\"";
            textBoxChoiceDescription.Text = selectedChoice.Description.Replace("\n", "\r\n") + RestrictionsToString(selectedChoice);

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
"Allow Value Sets:", string.Join(", ", selectedChoice.ChoosableValueSetsFilter)) : "");
        }

        private void RefreshChoiceSets(Scriptal selectedScriptal, Choice selectedChoice)
        {
            comboBoxChoiceSet.Items.Clear();
            comboBoxChoiceSet.Items.AddRange(GetChoosableValuesSets(selectedScriptal, selectedChoice));

            var match = comboBoxChoiceSet.Items
                .OfType<ChoiceKeyValues>()
                .FirstOrDefault(set => set.Key == selectedChoice.Key);

            if (match == null && comboBoxChoiceSet.Items.Count > 0)
                match = (ChoiceKeyValues)comboBoxChoiceSet.Items[0];

            comboBoxChoiceSet.SelectedItem = match;

            comboBoxChoiceSet.Enabled = selectedChoice.AllowUIEdit;
        }

        private ChoiceKeyValues[] GetChoosableValuesSets(Scriptal selectedScriptal, Choice selectedChoice)
        {
            List<ChoiceKeyValues> chooseableSets = new List<ChoiceKeyValues>();

            chooseableSets.AddRange(selectedScriptal.EmbeddedChoosables.ChoiceKeyValues
                .Union(ParentControl.Quest.GetAllObjectsScriptValueSets().ChoiceKeyValues)
                .Where(set => selectedChoice.ChoosableValueSetsFilter.Contains(set.Key)));

            if (selectedChoice.AllowLiteral)
            {
                if (selectedChoice.CorrespondingRuntimeToken.AllowedTypes.Contains(LuaValue.TemplateRestrictionType.STRING))
                    chooseableSets.Add(new ChoiceKeyValues() { Key = ScriptControl.STRING_LITERAL_SET });
                if (selectedChoice.CorrespondingRuntimeToken.AllowedTypes.Contains(LuaValue.TemplateRestrictionType.NUMBER))
                    chooseableSets.Add(new ChoiceKeyValues() { Key = ScriptControl.NUMBER_LITERAL_SET });
                if (selectedChoice.CorrespondingRuntimeToken.AllowedTypes.Contains(LuaValue.TemplateRestrictionType.BOOLEAN))
                    chooseableSets.Add(new ChoiceKeyValues() { Key = ScriptControl.BOOLEAN_LITERAL_SET });
            }

            if (selectedChoice.AllowUserVariable && selectedChoice.AllowUIEdit)
                chooseableSets.Add(new ChoiceKeyValues() { Key = ScriptControl.CUSTOM_VARIABLE_SET });

            return chooseableSets.ToArray();
        }

        private void comboBoxChoiceSet_SelectedIndexChanged(object sender, EventArgs e)
        {
            Choice selectedChoice = (Choice)listBoxChoices.SelectedItem;
            ChoiceKeyValues selectedChoiceSet = (ChoiceKeyValues)comboBoxChoiceSet.SelectedItem;
            selectedChoice.Key = selectedChoiceSet.Key;

            switch (selectedChoice.Key)
            {
                case ScriptControl.CUSTOM_VARIABLE_SET:
                    showCorrespondingChoiceControl(comboBoxUserVarNodes);
                    RefreshUserVarNodeChoiceValues(selectedChoice, selectedChoiceSet);
                    break;

                case ScriptControl.NUMBER_LITERAL_SET:
                    selectedChoice.ClearVarNodeDependency();
                    showCorrespondingChoiceControl(textBoxLiteralNumberValue, selectedChoice.AllowUIEdit);
                    if (selectedChoice.Value is LuaNumber number) textBoxLiteralNumberValue.Text = number.TokenValue;
                    UpdateChoiceLiteralValue();
                    break;

                case ScriptControl.STRING_LITERAL_SET:
                    selectedChoice.ClearVarNodeDependency();
                    showCorrespondingChoiceControl(textBoxLiteralStringValue, selectedChoice.AllowUIEdit);
                    if (selectedChoice.Value is LuaString text) textBoxLiteralStringValue.Text = text.Value;
                    UpdateChoiceLiteralValue();
                    break;

                case ScriptControl.BOOLEAN_LITERAL_SET:
                    selectedChoice.ClearVarNodeDependency();
                    showCorrespondingChoiceControl(panelBoolean, selectedChoice.AllowUIEdit);
                    if (selectedChoice.Value is LuaBoolean boolean) radioButtonTrue.Checked = boolean.Value;
                    UpdateChoiceLiteralValue();
                    break;

                default:
                    selectedChoice.ClearVarNodeDependency();
                    showCorrespondingChoiceControl(comboBoxPresetChoosables, selectedChoice.AllowUIEdit);
                    RefreshPresetChoiceValues(selectedChoice, selectedChoiceSet);
                    break;
            }

            ParentControl.RedrawScriptDependents(); RedrawVariableDependencies();
            /* disorienting UX
            if (selectedChoice != null && selectedChoice.Dependency != null)
            {
                ParentControl.treeViewVariables.SelectedNode = selectedChoice.Dependency;
            }*/
        }

        private void showCorrespondingChoiceControl(Control choiceControl, bool enable = true)
        {
            Control[] controlSet = { comboBoxPresetChoosables, comboBoxUserVarNodes, textBoxLiteralNumberValue, textBoxLiteralStringValue, panelBoolean };

            foreach (var control in controlSet)
            {
                control.Visible = control == choiceControl;
            }

            choiceControl.Enabled = enable;
        }

        private void RefreshUserVarNodeChoiceValues(Choice selectedChoice, ChoiceKeyValues selectedChoiceSet)
        {
            if (selectedChoiceSet == null || selectedChoice == null) return;
            if (selectedChoiceSet.Key != ScriptControl.CUSTOM_VARIABLE_SET || !selectedChoice.AllowUserVariable || !selectedChoice.AllowUIEdit) return;

            var userVarNodes = ParentControl.treeViewVariables.Nodes.OfType<VariableNode>().Where(node => selectedChoice.CorrespondingRuntimeToken.Allows(node.Entry.Value, out _));

            comboBoxUserVarNodes.Items.Clear();
            comboBoxUserVarNodes.Items.AddRange(userVarNodes.ToArray());

            var match = comboBoxUserVarNodes.Items.OfType<VariableNode>().FirstOrDefault(node => node == selectedChoice.Dependency);
            if (match == null && comboBoxUserVarNodes.Items.Count > 0)
                match = (VariableNode)comboBoxUserVarNodes.Items[0];

            comboBoxUserVarNodes.SelectedItem = match;
        }

        private void comboBoxUserVarNodes_DropDown(object sender, EventArgs e)
        {
            Choice selectedChoice = (Choice)listBoxChoices.SelectedItem;
            ChoiceKeyValues selectedChoiceSet = (ChoiceKeyValues)comboBoxChoiceSet.SelectedItem;
            RefreshUserVarNodeChoiceValues(selectedChoice, selectedChoiceSet);
        }

        private void comboBoxUserVarNodes_SelectedIndexChanged(object sender, EventArgs e)
        {
            Choice selectedChoice = (Choice)listBoxChoices.SelectedItem;
            VariableNode selectedVarNode = (VariableNode)comboBoxUserVarNodes.SelectedItem;
            if (selectedChoice.AllowUIEdit && selectedChoice.AllowUserVariable)
            {
                selectedChoice.SetVarNodeDependency(selectedVarNode);

                if (!selectedChoice.HasPassthrough(this))
                    selectedChoice.VariableNodeEventPassthrough += VariableNodeEventPassthroughFunction;

                RefreshListBoxDisplay();
            }

            ParentControl.RedrawScriptDependents(); RedrawVariableDependencies();
        }

        public void VariableNodeEventPassthroughFunction(object sender, VariableNodeEventArgs e)
        {
            if (sender is Choice choice)
            {
                if (e.Doomed)
                {
                    choice.VariableNodeEventPassthrough -= VariableNodeEventPassthroughFunction;
                }

                var updateIndex = listBoxChoices.Items.IndexOf(choice);
                RefreshListBoxDisplay(updateIndex);

                if ((Choice)listBoxChoices.SelectedItem == choice)
                {
                    Scriptal selectedScriptal = (Scriptal)comboBoxScriptalTemplate.SelectedItem;
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

        private void RefreshPresetChoiceValues(Choice selectedChoice, ChoiceKeyValues selectedChoiceSet)
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

        private void textBoxVarStringValue_TextChanged(object sender, EventArgs e)
        {
            UpdateChoiceLiteralValue();
        }

        private void radioButtonFalse_CheckedChanged(object sender, EventArgs e)
        {
            UpdateChoiceLiteralValue();
        }

        private void textBoxVarNumberValue_TextChanged(object sender, EventArgs e)
        {
            UpdateChoiceLiteralValue();
        }

        private void textBoxVarNumberValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!char.IsControl(ch) && !char.IsDigit(ch) && ch != '.')
            {
                e.Handled = true;
            }

            if (ch == '.' && (sender as TextBox).Text.Contains('.'))
            {
                e.Handled = true;
            }
        }

        private void UpdateChoiceLiteralValue()
        {
            Choice currentChoice = (Choice)listBoxChoices.SelectedItem;
            switch (currentChoice.Key)
            {
                case ScriptControl.STRING_LITERAL_SET:
                    currentChoice.Value = Create.String(textBoxLiteralStringValue.Text);
                    break;
                case ScriptControl.NUMBER_LITERAL_SET:
                    if (string.IsNullOrEmpty(textBoxLiteralNumberValue.Text))
                    {
                        textBoxLiteralNumberValue.Text = "0";
                        textBoxLiteralNumberValue.SelectAll();
                    }
                    currentChoice.Value = Create.Number(textBoxLiteralNumberValue.Text);
                    break;
                case ScriptControl.BOOLEAN_LITERAL_SET:
                    currentChoice.Value = Create.Boolean(radioButtonTrue.Checked);
                    break;
            }
            RefreshListBoxDisplay(listBoxChoices.SelectedIndex);
        }
    }
}
