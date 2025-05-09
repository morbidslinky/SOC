using SOC.Classes.Lua;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace SOC.UI
{
    public partial class EmbeddedScriptSetControl : UserControl
    {
        private TreeView TreeViewScripts;

        public EmbeddedScriptSetControl(TreeView treeViewScripts)
        {
            InitializeComponent();
            Dock = DockStyle.Fill;
            TreeViewScripts = treeViewScripts;
        }

        internal void UpdateListFromNode(TreeNode node)
        {
            List<Script> scripts = new List<Script>();

            switch (node)
            {
                case RootTableNode rootTableNode:
                    scripts = rootTableNode.QStep_Main.ConvertToScripts();
                    break;
                case Str32TableNode tableNode:
                    scripts = tableNode.ConvertToScripts();
                    break;
                case CodeNode codeNode:
                    scripts = codeNode.GetTableNode().ConvertToScripts();
                    break;
                case MsgSenderNode msgSenderNode:
                    scripts = msgSenderNode.GetTableNode().ConvertToScripts();
                    break;
            }

            checkedListBoxScripts.Items.Clear();
            checkedListBoxScripts.Items.AddRange(scripts.ToArray());

            buttonSaveScript.Enabled = checkedListBoxScripts.Items.Count != 0;
            checkedListBoxScripts.Visible = checkedListBoxScripts.Items.Count != 0;
            textEmptyHint.Visible = checkedListBoxScripts.Items.Count == 0;
        }

        private void buttonLoadScript_Click(object sender, EventArgs e)
        {
            var node = TreeViewScripts.SelectedNode;
            Str32TableNode parentTableNode;

            switch (node)
            {
                case Str32TableNode tableNode:
                    parentTableNode = tableNode;
                    break;
                case CodeNode codeNode:
                    parentTableNode = codeNode.GetTableNode();
                    break;
                case MsgSenderNode msgSenderNode:
                    parentTableNode = msgSenderNode.GetTableNode();
                    break;
                default:
                    return;
            }

            OpenFileDialog loadFile = new OpenFileDialog();
            loadFile.Filter = "Xml Files|*.xml|All Files|*.*";

            DialogResult result = loadFile.ShowDialog();
            if (result != DialogResult.OK)
                return;

            try
            {
                var script = Script.LoadFromXml(loadFile.FileName);
                parentTableNode.Add(script);
            }
            catch
            {
                try
                {
                    var scriptSet = ScriptSet.LoadFromXml(loadFile.FileName);
                    foreach (var script in scriptSet.Scripts)
                        parentTableNode.Add(script);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to load script(s) file: {ex.Message}");
                    return;
                }
            }

            UpdateListFromNode(parentTableNode);
        }

        private void buttonSaveScript_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();

            if (checkedListBoxScripts.CheckedItems.Count == 1)
            {
                EmbeddedScriptControl.SaveScript((Script)checkedListBoxScripts.CheckedItems[0]);
                return;
            }

            saveFile.Filter = "Xml File|*.xml";
            saveFile.FileName = "Exported Scripts";
            DialogResult saveResult = saveFile.ShowDialog();

            if (saveResult == DialogResult.OK)
            {
                List<Script> scriptList = new List<Script>();
                foreach (Script script in checkedListBoxScripts.CheckedItems) { scriptList.Add(script); }
                ScriptSet set = new ScriptSet(scriptList);

                set.WriteToXml(saveFile.FileName);
                MessageBox.Show("Done!", "Script(s) Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }

    public class ScriptSet
    {
        [XmlArray("Scripts")]
        [XmlArrayItem("Script")]
        public List<Script> Scripts = new List<Script>();

        public ScriptSet() { }

        public ScriptSet(List<Script> scripts)
        {
            Scripts = scripts;
        }

        public void WriteToXml(string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ScriptSet));
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, this);
            }
        }

        public static ScriptSet LoadFromXml(string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ScriptSet));
            using (StreamReader reader = new StreamReader(filePath))
            {
                return (ScriptSet)serializer.Deserialize(reader);
            }
        }
    }
}
