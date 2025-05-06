using SOC.Classes.Lua;
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

        private TreeView TreeViewScripts;

        public EmbeddedScriptalControl(TreeView treeViewScripts)
        {
            InitializeComponent();
            Dock = DockStyle.Fill;
            TreeViewScripts = treeViewScripts;
        }

        internal static string str(ScriptalType type)
        {
            return type == ScriptalType.Preconditional ? "Preconditional" : "Operational";
        }

        internal void UpdateFromScript(ScriptalNode scriptalNode)
        {
            _isUpdatingControls = true;

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

            SetDescription((Scriptal)comboBoxScriptal.SelectedItem);
            ApplyTemplate((Scriptal)comboBoxScriptal.SelectedItem);

            _isUpdatingControls = false;
        }

        private void ApplyTemplate(Scriptal selectedTemplate)
        {
            groupBoxChoices.Text = $"Populate :: {selectedTemplate.Name}";

            if (!_isUpdatingControls && TreeViewScripts.SelectedNode is ScriptalNode selectedScriptalNode)
            {
                selectedScriptalNode.Scriptal = selectedTemplate;
                selectedScriptalNode.UpdateText();
            }
        }

        private void SetDescription(Scriptal selectedTemplate)
        {
            textBoxDescription.Text = selectedTemplate.Description;
            groupBoxDescription.Text = $"Description :: {selectedTemplate.Name}";
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
            ApplyTemplate((Scriptal)comboBoxScriptal.SelectedItem);
        }
    }
}
