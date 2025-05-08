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
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace SOC.UI
{
    public partial class EmbeddedScriptalControl : UserControl
    {
        private bool _isUpdatingControls = false;

        private List<ChoosableValues> QuestValueSets = new List<ChoosableValues>();
        private List<LuaTemplatePlaceholder> TemplatePlaceholderTokens = new List<LuaTemplatePlaceholder>();

        private TreeView TreeViewScripts;
        private TreeView TreeViewVariables;

        public EmbeddedScriptalControl(TreeView treeViewScripts, TreeView treeViewVariables)
        {
            InitializeComponent();
            Dock = DockStyle.Fill;
            TreeViewScripts = treeViewScripts;
            TreeViewVariables = treeViewVariables;
        }

        internal static string str(ScriptalType type)
        {
            return type == ScriptalType.Preconditional ? "Preconditional" : "Operational";
        }

        internal void UpdateFromScript(ScriptalNode scriptalNode, List<ChoosableValues> questValueSets)
        {
            QuestValueSets = questValueSets;

            string scriptalType = str(scriptalNode.ScriptalType);

            groupBoxScriptalSelect.Text = $"{scriptalType}";

            Scriptal[] scriptalTemplates = GetTemplates(scriptalNode);

            comboBoxScriptal.Items.Clear();
            comboBoxScriptal.Items.Add(Scriptal.Default());
            comboBoxScriptal.Items.AddRange(scriptalTemplates);

            Scriptal matchingTemplate = scriptalTemplates.FirstOrDefault(template => template.Name == scriptalNode.Scriptal.Name && template.EventFunctionTemplate == scriptalNode.Scriptal.EventFunctionTemplate);
            if (matchingTemplate != null)
            {
                int matchingIndex = comboBoxScriptal.Items.IndexOf(matchingTemplate);
                comboBoxScriptal.Items.RemoveAt(matchingIndex);
                comboBoxScriptal.Items.Insert(matchingIndex, scriptalNode.Scriptal);
                comboBoxScriptal.SelectedItem = scriptalNode.Scriptal;
            }
            else
            {
                comboBoxScriptal.SelectedIndex = 0;
            }

            SetDescription(scriptalNode.Scriptal);
            SetChoiceMenu(scriptalNode.Scriptal);
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
                comboBoxChoiceValue.Items.Clear();
                textBoxChoiceDescription.TextAlign = HorizontalAlignment.Center;
                textBoxChoiceDescription.Text = "\r\n[Event Function does not contain choosable values]";
                groupBoxChoicesList.Enabled = false;
                groupBoxChoice.Enabled = false;
                SyncScriptalNode();
            }
        }

        private void SetDescription(Scriptal scriptal)
        {
            groupBoxDescription.Text = $"Template Description :: \"{scriptal.Name}\"";
            textBoxDescription.Text = scriptal.Description +
                $"\r\n\r\nEvent Function:\r\n{scriptal.EventFunctionTemplate}";
            if (scriptal.EmbeddedChoosables.Count > 0)
            {
                textBoxDescription.Text += $"\r\n\r\nProvided Value Sets:\r\n{string.Join("\r\n\r\n", scriptal.EmbeddedChoosables.Select(choosable => $"{choosable.Key}:\r\n     {string.Join("\r\n     ", choosable.Values)}"))}";
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

        private void comboBoxScriptal_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetDescription((Scriptal)comboBoxScriptal.SelectedItem);
        }

        private void buttonApplyTemplate_Click(object sender, EventArgs e)
        {
            SetChoiceMenu((Scriptal)comboBoxScriptal.SelectedItem);
        }

        private void listBoxChoices_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isUpdatingControls) return;

            Scriptal selectedScriptal = (Scriptal)comboBoxScriptal.SelectedItem;
            Choice selectedChoice = (Choice)listBoxChoices.SelectedItem;

            groupBoxChoice.Text = $"Choice :: \"{selectedChoice.Name}\"";
            textBoxChoiceDescription.Text = selectedChoice.Description;
            string choiceRestrictions = $"\r\n\r\nChoice Restrictions:\r\n" +
$"\r\nAllow User Edits:\t{selectedChoice.AllowUIEdit}," +
$"\r\nAllow User Vars:\t{selectedChoice.AllowUserVariable}," +
$"\r\nAllow Literals:\t{selectedChoice.AllowLiteral}," +
$"\r\nType Restriction:\t{selectedChoice.CorrespondingRuntimeToken.AllowedType}," +
$"\r\nAllow Value Sets:\t{string.Join(", ", selectedChoice.ChoosableValuesKeyFilter)}";

            textBoxChoiceDescription.Text += choiceRestrictions;
            comboBoxChoiceSet.Items.Clear();
                
            var choosableSets = selectedScriptal.EmbeddedChoosables
                .Union(QuestValueSets)
                .Where(
                    set => selectedChoice.ChoosableValuesKeyFilter.Contains(set.Key) && 
                    set.Values.Any(setValue => LuaTemplate.MatchesRestriction(setValue, selectedChoice.CorrespondingRuntimeToken))
                );

            comboBoxChoiceSet.Items.AddRange(choosableSets.ToArray());

            if (choosableSets.Any(set => set.Key == selectedChoice.Key))
                comboBoxChoiceSet.Text = selectedChoice.Key;
            else if (comboBoxChoiceSet.Items.Count > 0)
                comboBoxChoiceSet.SelectedIndex = 0;
        }

        private void comboBoxChoiceSet_SelectedIndexChanged(object sender, EventArgs e)
        {
            Choice selectedChoice = (Choice)listBoxChoices.SelectedItem;
            ChoosableValues selectedChoiceSet = (ChoosableValues)comboBoxChoiceSet.SelectedItem;

            selectedChoice.Key = selectedChoiceSet.Key;

            comboBoxChoiceValue.Items.Clear();
            comboBoxChoiceValue.Items.AddRange(selectedChoiceSet.Values.Where(value => LuaTemplate.MatchesRestriction(value, selectedChoice.CorrespondingRuntimeToken)).ToArray());

            if (selectedChoiceSet.Values.Any(value => value.Equals(selectedChoice.Value)))
                comboBoxChoiceValue.Text = selectedChoice.Value.ToString();
            else if (comboBoxChoiceValue.Items.Count > 0)
                comboBoxChoiceValue.SelectedIndex = 0;
        }

        private void comboBoxChoiceValue_SelectedIndexChanged(object sender, EventArgs e)
        {
            Choice selectedChoice = (Choice)listBoxChoices.SelectedItem;
            LuaValue value = (LuaValue)comboBoxChoiceValue.SelectedItem;
            selectedChoice.Value = value;

            _isUpdatingControls = true;

            int index = listBoxChoices.Items.IndexOf(selectedChoice);
            listBoxChoices.Items[index] = listBoxChoices.Items[index];
            SyncScriptalNode();

            _isUpdatingControls = false;
        }

        private void SyncScriptalNode()
        {
            if (TreeViewScripts.SelectedNode is ScriptalNode selectedScriptalNode)
            {
                selectedScriptalNode.Scriptal = (Scriptal)comboBoxScriptal.SelectedItem;
                selectedScriptalNode.UpdateText();
            }
        }
    }
}
