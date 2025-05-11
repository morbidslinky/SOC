using System;
using System.Windows.Forms;
using System.Collections.Generic;
using SOC.Classes.Common;
using System.IO;
using System.Xml;
using System.Reflection;
using SOC.Classes.Lua;
using System.Linq.Expressions;
using SOC.QuestObjects.Common;
using System.Xml.Serialization;
using System.Linq;
using System.Xml.Linq;

namespace SOC.UI
{
    public partial class ScriptControl : UserControl
    {
        public Quest Quest;

        ScriptTablesRootNode ScriptTablesRootNode = new ScriptTablesRootNode();

        EmbeddedScriptSetControl ScriptSetEmbed = new EmbeddedScriptSetControl();
        EmbeddedScriptControl ScriptEmbed = new EmbeddedScriptControl();
        EmbeddedScriptalControl ScriptalEmbed = new EmbeddedScriptalControl();
        List<TreeNode> SelectionRelationNodes = new List<TreeNode>();

        public static Dictionary<string, List<string>> MessageClassListMapping = new Dictionary<string, List<string>>();

        private bool _isUpdatingControls = false;

        public ScriptControl(Quest quest)
        {
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
            foreach (var entry in Quest.ScriptDetails.QStep_Main)
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
            foreach (VariableNode node in treeViewVariables.Nodes)
            {
                ClearVarTables(node);
                AddVarNodesToVarTable(node);
                Quest.ScriptDetails.VariableDeclarations.Add(node.Entry);
            }

            Quest.ScriptDetails.QStep_Main.Clear();
            Quest.ScriptDetails.QStep_Main.AddRange(ScriptTablesRootNode.QStep_Main.ConvertToScripts());
        }
    }
}