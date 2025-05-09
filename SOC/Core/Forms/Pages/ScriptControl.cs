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

namespace SOC.UI
{
    public partial class ScriptControl : UserControl
    {
        public Quest Quest;

        RootTableNode ScriptRootTable = new RootTableNode();

        EmbeddedScriptSetControl EmbeddedScriptSetControl;
        EmbeddedScriptControl EmbeddedScriptControl;
        EmbeddedScriptalControl EmbeddedScriptalControl;

        public static Dictionary<string, List<string>> MessageClassListMapping = new Dictionary<string, List<string>>();

        private bool _isUpdatingControls = false;

        public ScriptControl(Quest quest)
        {
            InitializeComponent();
            Dock = DockStyle.Fill;
            Quest = quest;

            foreach (var entry in Quest.ScriptDetails.VariableDeclarations)
            {
                treeViewVariables.Nodes.Add(new VariableNode(entry));
            }

            ParseMessageClassesFile();

            EmbeddedScriptSetControl = new EmbeddedScriptSetControl(treeViewScripts);
            EmbeddedScriptControl = new EmbeddedScriptControl(treeViewScripts);
            EmbeddedScriptalControl = new EmbeddedScriptalControl(treeViewScripts, treeViewVariables);

            treeViewScripts.Nodes.Add(ScriptRootTable);

            TreeNode selected = null;
            foreach (var entry in Quest.ScriptDetails.QStep_Main)
            {
                selected = ScriptRootTable.QStep_Main.Add(entry);
            }

            if (selected != null)
            {
                treeViewScripts.SelectedNode = selected;
            }
            else
            {
                treeViewScripts.SelectedNode = ScriptRootTable.QStep_Main;
            }
        }

        public void RefreshTreeViews()
        {
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
            Quest.ScriptDetails.QStep_Main.AddRange(ScriptRootTable.QStep_Main.ConvertToScripts());
        }
    }

    public class RootTableNode : TreeNode
    {
        public Str32TableNode QStep_Main;

        public RootTableNode() : base("Quest Tables") {
            QStep_Main = new Str32TableNode("QStep_Main");
            Nodes.Add(QStep_Main);
        }
    }
}