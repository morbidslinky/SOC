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
            groupBoxChoices.Text = $"Populate :: {scriptal.Name}";

            listBoxChoices.Items.Clear();
            listBoxChoices.Items.AddRange(scriptal.Choices.ToArray());
            if (scriptal.Choices.Count > 0)
            {
                listBoxChoices.Enabled = true;
                comboBoxChoiceSet.Enabled = true;
                comboBoxChoiceValue.Enabled = true;
                textBoxChoiceDescription.Enabled = true;
                listBoxChoices.SelectedIndex = 0;
            } else
            {
                listBoxChoices.Items.Clear();
                listBoxChoices.Enabled = false;
                comboBoxChoiceSet.Items.Clear();
                comboBoxChoiceSet.Enabled = false;
                comboBoxChoiceValue.Items.Clear();
                comboBoxChoiceValue.Enabled = false;
                textBoxChoiceDescription.Text = "This Template does not contain populatable values.";
                textBoxChoiceDescription.Enabled = false;
                SyncScriptalNode();
            }
        }

        private void SetDescription(Scriptal scriptal)
        {
            textBoxDescription.Text = scriptal.Description;
            groupBoxDescription.Text = $"Description :: {scriptal.Name}";
        }

        internal Scriptal[] GetTemplates(ScriptalNode scriptalNode)
        {
            List<Scriptal> scriptals = new List<Scriptal>();

            StrCode32Event scriptalEvent = scriptalNode.GetEvent();

            string[] scriptalSubDirs = { "Scriptals", str(scriptalNode.ScriptalType), scriptalEvent.StrCode32.Text, scriptalEvent.msg.Text };
            string scriptalDir = "";

            List<FileInfo> scriptalFiles = new List<FileInfo>();

            for (int i = 0; i < scriptalSubDirs.Length; i++)
            {
                scriptalDir = Path.Combine(scriptalDir, scriptalSubDirs[i]);
                scriptalFiles.AddRange(GetScriptalFiles(scriptalDir));
            }

            foreach (FileInfo scriptalFile in scriptalFiles)
            {
                try
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(Scriptal));
                    using (StreamReader reader = new StreamReader(scriptalFile.FullName))
                    {
                        scriptals.Add((Scriptal)serializer.Deserialize(reader));
                    }
                }
                catch
                {

                }
            }

            return scriptals.ToArray();
        }

        internal FileInfo[] GetScriptalFiles(string subdir)
        {
            string dir = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "SOCassets//ScriptAssets", subdir);
            DirectoryInfo scriptalDir = new DirectoryInfo(dir);

            if (scriptalDir.Exists)
            {
                return scriptalDir.GetFiles("*.xml");
            }

            return new FileInfo[0];
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

            textBoxChoiceDescription.Text = selectedChoice.Description;
            comboBoxChoiceSet.Items.Clear();

            var choosableSets = selectedScriptal.EmbeddedChoosables.Union(QuestValueSets).Where(set => selectedChoice.ChoosableValuesKeyFilter.Contains(set.Key));
            comboBoxChoiceSet.Items.AddRange(choosableSets.ToArray());

            if (choosableSets.Any(set => set.Key == selectedChoice.Key))
                comboBoxChoiceSet.Text = selectedChoice.Key;
            else
                comboBoxChoiceSet.SelectedIndex = 0;
        }

        private void comboBoxChoiceSet_SelectedIndexChanged(object sender, EventArgs e)
        {
            Choice selectedChoice = (Choice)listBoxChoices.SelectedItem;
            ChoosableValues selectedChoiceSet = (ChoosableValues)comboBoxChoiceSet.SelectedItem;

            comboBoxChoiceValue.Items.Clear();
            comboBoxChoiceValue.Items.AddRange(selectedChoiceSet.Values.ToArray());

            if (selectedChoiceSet.Values.Any(value => value.Equals(selectedChoice.Value)))
            {
                comboBoxChoiceValue.Text = selectedChoice.Value.ToString();
            }
            else 
            {
                selectedChoice.Key = selectedChoiceSet.Key;
                comboBoxChoiceValue.SelectedIndex = 0;
            }
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
