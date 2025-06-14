using SOC.Classes.Common;
using SOC.Classes.QuestBuild;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SOC.UI
{
    public partial class FormMain : Form
    {
        readonly string SideopExportDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SOCassets", "SideOp Library");

        enum Step
        {
            NONE,
            Setup,
            Detail,
            Script,
            Build
        }

        private Quest Quest;
        private SetupControl SetupControl;
        private DetailsControl DetailControl;
        private ScriptControl ScriptControl;
        private WaitingControl WaitingControl;
        private Step CurrentStep;

        public FormMain()
        {
            Quest = Quest.Create();
            SetupControl = new SetupControl(Quest);
            DetailControl = new DetailsControl(Quest);
            ScriptControl = new ScriptControl(Quest);
            WaitingControl = new WaitingControl();
            CurrentStep = Step.NONE;

            InitializeComponent();

            GoToPage(Step.Setup);
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            GoToPage(CurrentStep + 1);
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            GoToPage(CurrentStep - 1);
        }

        private void GoToPage(Step step)
        {
            if (step == CurrentStep)
            {
                return;
            }

            CurrentStep = step;
            switch (step)
            {
                case Step.Setup:
                    DetailControl.SyncQuestDataToUserInput();

                    ShowSetup();
                    break;

                case Step.Detail:
                    if (SetupControl.IsFilled())
                    {
                        ShowWait();
                        ScriptControl.SyncQuestDataToUserInput();
                        SetupControl.SyncQuestDataToUserInput();

                        ShowDetails();
                    }
                    else
                    {
                        MessageBox.Show("Please fill in the remaining Setup and Flavor Text fields.", "Missing Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CurrentStep--;
                        return;
                    }
                    break;

                case Step.Script:
                    ShowWait();
                    DetailControl.SyncQuestDataToUserInput();

                    ShowScript();
                    break;

                case Step.Build:
                    ScriptControl.SyncQuestDataToUserInput();

                    BuildQuest();
                    CurrentStep--;
                    break;
            }
        }

        private void ShowSetup()
        {
            panelMain.Controls.Clear();
            panelMain.Controls.Add(SetupControl);

            buttonNext.Text = "Next >>";
            buttonBack.Visible = false;
        }

        private void ShowWait()
        {
            panelMain.Controls.Clear(); 

            buttonNext.Enabled = false;
            this.Cursor = Cursors.WaitCursor;
            panelMain.Controls.Add(WaitingControl);
            WaitingControl.Refresh();
        }

        private async void ShowDetails()
        {
            panelMain.Controls.Add(DetailControl);
            panelMain.Controls.Remove(WaitingControl);

            buttonBack.Visible = true;
            buttonNext.Text = "Next >>";
            this.Cursor = Cursors.Default;

            await Task.Delay(100);

            buttonNext.Enabled = true;
        }

        private async void ShowScript()
        {
            panelMain.Controls.Add(ScriptControl);
            panelMain.Controls.Remove(WaitingControl);

            buttonBack.Visible = true;
            buttonNext.Text = "Build";
            this.Cursor = Cursors.Default;

            await Task.Delay(100);

            buttonNext.Enabled = true;
        }

        private void BuildQuest()
        {
            if (BuildManager.Build(Quest))
            {
                MessageBox.Show("Build Complete", "Sideop Companion", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Build Failed", "Sideop Companion", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void FormMain_Activated(object sender, EventArgs e)
        {
            SetupControl.refreshNotifsList();
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(SideopExportDir))
            {
                Directory.CreateDirectory(SideopExportDir);
            }
            OpenFileDialog loadFile = new OpenFileDialog();
            loadFile.Filter = "Xml Files|*.xml|All Files|*.*";
            loadFile.InitialDirectory = SideopExportDir;

            DialogResult result = loadFile.ShowDialog();
            if (result != DialogResult.OK) return;

            GoToPage(Step.Setup);

            if (Quest.Load(loadFile.FileName))
            {
                SetupControl.SyncUserInputToQuestData();
                ScriptControl = new ScriptControl(Quest);
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            DoSave();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (SetupControl.IsFilled())
            {
                DialogResult result = MessageBox.Show("Do you want to save this Sideop to an Xml file?", "SOC", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                {
                    if (!DoSave())
                        e.Cancel = true;
                }
                else if (result == DialogResult.Cancel)
                    e.Cancel = true;
            }
        }

        private bool DoSave()
        {
            switch (CurrentStep)
            {
                case Step.Setup:
                    SetupControl.SyncQuestDataToUserInput(true);
                    break;
                case Step.Detail:
                    GoToPage(Step.Setup);
                    break;
                case Step.Script:
                    ScriptControl.SyncQuestDataToUserInput();
                    break;
            }

            if (!Directory.Exists(SideopExportDir))
            {
                Directory.CreateDirectory(SideopExportDir);
            }

            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "Xml File|*.xml";
            saveFile.InitialDirectory = SideopExportDir;
            saveFile.FileName = Quest.SetupDetails.FpkName;
            DialogResult saveResult = saveFile.ShowDialog();
            if (saveResult != DialogResult.OK) return true;

            return Quest.Save(saveFile.FileName);
        }

        private void buttonOpenFolder_Click(object sender, EventArgs e)
        {
            string folderPath = AppDomain.CurrentDomain.BaseDirectory;

            try
            {
                if (Directory.Exists(folderPath))
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = folderPath,
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                else
                {
                    MessageBox.Show("Folder not found: " + folderPath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to open folder: " + ex.Message);
            }
        }

        private void buttonOpenScriptTemplates_Click(object sender, EventArgs e)
        {
            string folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SOCassets", "ScriptAssets");

            try
            {
                if (Directory.Exists(folderPath))
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = folderPath,
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                else
                {
                    MessageBox.Show("Folder not found: " + folderPath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to open folder: " + ex.Message);
            }
        }

        private void buttonBatchBuild_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(SideopExportDir))
            {
                Directory.CreateDirectory(SideopExportDir);
            }

            OpenFileDialog loadFile = new OpenFileDialog();
            loadFile.Filter = "Xml Files|*.xml|All Files|*.*";
            loadFile.InitialDirectory = SideopExportDir;
            loadFile.Multiselect = true;

            DialogResult result = loadFile.ShowDialog();
            if (result != DialogResult.OK) return;
            
            List<Quest> quests = new List<Quest>();

            int failedCount = 0;
            foreach (string filePath in loadFile.FileNames)
            {
                Quest quest = new Quest();
                if (quest.Load(filePath))
                {
                    if (!quests.Exists(questInList => questInList.SetupDetails.FpkName == quest.SetupDetails.FpkName)
                        && !quests.Exists(questInList => questInList.SetupDetails.QuestNum == quest.SetupDetails.QuestNum))
                        quests.Add(quest);
                    else failedCount++;
                }
                else failedCount++;
            }
            if (failedCount > 0)
                MessageBox.Show($"{failedCount} Sideops could not be built \n(Either caused by failing to load Xml file(s) or more than one sideop using the same .FPK Filename/Quest Number)", "Sideop Companion", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (quests.Count > 0)
            {
                if (BuildManager.Build(quests.ToArray()))
                {
                    MessageBox.Show("Batch Build Complete", "Sideop Companion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Batch Build Failed", "Sideop Companion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
