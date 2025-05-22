using SOC.Classes.Common;
using SOC.Classes.Lua;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace SOC.UI
{
    public partial class ScriptControl : UserControl
    {
        public Quest Quest;

        public ScriptTablesRootNode ScriptTablesRootNode = new ScriptTablesRootNode();

        EmbeddedScriptSetControl ScriptSetEmbed;
        EmbeddedScriptControl ScriptEmbed;
        EmbeddedScriptalControl ScriptalEmbed;

        public static ChoiceKeyValuesList StrCodeClasses = new ChoiceKeyValuesList();

        private bool _isUpdatingControls = false;

        public static readonly Font UNDERLINE = new Font("Consolas", 8.25F, FontStyle.Underline);
        public static readonly Font REGULAR = new Font("Consolas", 8.25F, FontStyle.Regular);

        public const string CUSTOM_VARIABLE_SET = "Custom Variable";
        public const string NUMBER_LITERAL_SET = "Number Literal";
        public const string STRING_LITERAL_SET = "String Literal";
        public const string BOOLEAN_LITERAL_SET = "Boolean Literal";

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
            string MessageClassList = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "SOCassets//ScriptAssets//StrCodeClasses.xml");

            if (!File.Exists(MessageClassList))
            {
                MessageBox.Show("StrCodeClasses.xml file not found!");
                return;
            }

            StrCodeClasses = ChoiceKeyValuesList.LoadFromXml(MessageClassList);
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