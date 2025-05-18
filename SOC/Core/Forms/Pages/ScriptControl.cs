using SOC.Classes.Common;
using SOC.Classes.Lua;
using SOC.QuestObjects.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace SOC.UI
{
    public partial class ScriptControl : UserControl
    {
        public Quest Quest;

        public ScriptTablesRootNode ScriptTablesRootNode = new ScriptTablesRootNode();

        EmbeddedScriptSetControl ScriptSetEmbed;
        EmbeddedScriptControl ScriptEmbed;
        EmbeddedScriptalControl ScriptalEmbed;

        public static Dictionary<string, List<string>> MessageClassListMapping = new Dictionary<string, List<string>>();

        private bool _isUpdatingControls = false;

        public static readonly Font UNDERLINE = new Font("Consolas", 8.25F, FontStyle.Underline);
        public static readonly Font REGULAR = new Font("Consolas", 8.25F, FontStyle.Regular);

        public ScriptControl(Quest quest)
        {
            ScriptSetEmbed = new EmbeddedScriptSetControl(this);
            ScriptEmbed = new EmbeddedScriptControl(this);
            ScriptalEmbed = new EmbeddedScriptalControl(this);

            InitializeComponent();
            ParseMessageClassesFile();
            Dock = DockStyle.Fill;
            Quest = quest;

            foreach (var entry in Quest.ScriptDetails.VariableDeclarations)
            {
                treeViewVariables.Nodes.Add(new VariableNode(entry));
            }

            TreeNode selected = null;
            treeViewScripts.Nodes.Add(ScriptTablesRootNode);
            foreach (Script entry in Quest.ScriptDetails.QStep_Main)
            {
                selected = ScriptTablesRootNode.QStep_Main.Add(entry);
            }
            treeViewScripts.SelectedNode = selected != null ? selected : ScriptTablesRootNode.QStep_Main;
        }

        private void ScriptControl_Load(object sender, EventArgs e)
        {
            UpdateScriptControlsToSelectedNode();
            UpdateVariableControlsToSelectedNode();
        }

        private static void ParseMessageClassesFile()
        {
            string MessageClassList = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "SOCassets//ScriptAssets//MessageClasses.xml");

            if (!File.Exists(MessageClassList))
            {
                MessageBox.Show("MessageClasses.xml file not found!");
                return;
            }

            XmlDocument doc = new XmlDocument();
            doc.Load(MessageClassList);

            XmlNodeList categories = doc.SelectNodes("//Category");

            foreach (XmlNode categoryNode in categories)
            {
                string categoryName = categoryNode.Attributes["name"].Value;
                List<string> items = new List<string>();

                foreach (XmlNode itemNode in categoryNode.SelectNodes("Item"))
                {
                    items.Add(itemNode.InnerText);
                }

                MessageClassListMapping[categoryName] = items;
            }
        }

        internal void SyncQuestDataToUserInput()
        {
            Quest.ScriptDetails.VariableDeclarations.Clear();
            Quest.ScriptDetails.VariableDeclarations.AddRange(treeViewVariables.Nodes.OfType<VariableNode>().Select(node => node.GetEntry()).ToList());

            Quest.ScriptDetails.QStep_Main.Clear();
            Quest.ScriptDetails.QStep_Main.AddRange(ScriptTablesRootNode.QStep_Main.ConvertToScripts());
        }
    }
}