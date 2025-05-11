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
        private Str32TableNode TableNode;

        public EmbeddedScriptSetControl()
        {
            InitializeComponent();
            Dock = DockStyle.Fill;
        }

        public override string ToString() => TableNode.ToString();

        internal UserControl Menu(Str32TableNode tableNode)
        {
            TableNode = tableNode;
            UpdateMenu();
            return this;
        }

        private void UpdateMenu()
        {
            List<Script> scripts = TableNode.ConvertToScripts();

            checkedListBoxScripts.Items.Clear();
            checkedListBoxScripts.Items.AddRange(scripts.ToArray());

            buttonSaveScript.Enabled = checkedListBoxScripts.CheckedItems.Count != 0;
            checkedListBoxScripts.Visible = checkedListBoxScripts.Items.Count != 0;
            textEmptyHint.Visible = checkedListBoxScripts.Items.Count == 0;
        }

        private void buttonLoadScript_Click(object sender, EventArgs e)
        {
            OpenFileDialog loadFile = new OpenFileDialog();
            loadFile.Filter = "Xml Files|*.xml|All Files|*.*";

            DialogResult result = loadFile.ShowDialog();
            if (result != DialogResult.OK)
                return;

            try
            {
                var script = Script.LoadFromXml(loadFile.FileName);
                TableNode.Add(script);
            }
            catch
            {
                try
                {
                    var scriptSet = ScriptSet.LoadFromXml(loadFile.FileName);
                    foreach (var script in scriptSet.Scripts)
                        TableNode.Add(script);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to load script(s) file: {ex.Message}");
                    return;
                }
            }

            UpdateMenu();
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

        private void checkedListBoxScripts_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            int checkedCount = checkedListBoxScripts.CheckedItems.Count;

            if (e.NewValue == CheckState.Checked)
                checkedCount++;
            else if (e.NewValue == CheckState.Unchecked)
                checkedCount--;

            buttonSaveScript.Enabled = checkedCount > 0;
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
