using System;
using System.Windows.Forms;
using System.Collections.Generic;
using SOC.Classes.Common;
using System.IO;
using System.Xml;
using System.Reflection;

namespace SOC.UI
{
    public partial class ScriptControl : UserControl
    {
        public Quest Quest;

        public ScriptControl(Quest quest)
        {
            InitializeComponent();
            Dock = DockStyle.Fill;
            Quest = quest;

            treeViewVariables.Nodes.Clear();
            foreach (var entry in Quest.ScriptDetails.VariableDeclarations)
            {
                treeViewVariables.Nodes.Add(new VariableNode(entry));
            }

            treeViewScripts.Nodes.Add(new TreeNode("Quest Tables", new TreeNode[] { qstep_main }));

            TreeNode selected = null;
            foreach (var entry in Quest.ScriptDetails.QStep_Main)
            {
                selected = qstep_main.Add(entry);
            }

            if (selected != null)
            {
                treeViewScripts.SelectedNode = selected;
            }

            qstep_main.ExpandAll();
        }

        private void ScriptControl_Load(object sender, EventArgs e)
        {
            string MessageClassList = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "SOCassets//ScriptAssets//MessageClasses.xml");

            if (!File.Exists(MessageClassList))
            {
                MessageBox.Show("MessageClassese.xml file not found!");
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
                comboBoxStrCodes.Items.Add(categoryName);
            }

            UpdateScriptControlsToSelectedNode();
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
            Quest.ScriptDetails.QStep_Main.AddRange(qstep_main.ConvertToScripts());
        }
    }
}