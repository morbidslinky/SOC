using SOC.Classes.Common;
using SOC.Classes.QuestBuild;
using SOC.QuestObjects.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SOC.UI
{
    public partial class FormMain : Form
    {
        enum Step
        {
            NONE,
            Setup,
            Detail,
            Build
        }

        private Quest Quest;
        private SetupControl SetupControl;
        private DetailsControl DetailControl;
        private WaitingControl WaitingControl;
        private Step CurrentStep;

        public FormMain()
        {
            Quest = new Quest();
            SetupControl = new SetupControl(Quest);
            DetailControl = new DetailsControl(Quest);
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
                    ShowSetup();
                    break;

                case Step.Detail:
                    if (SetupControl.IsFilled())
                    {
                        ShowWait();
                        ShowDetails();
                    }
                    else
                    {
                        MessageBox.Show("Please fill in the remaining Setup and Flavor Text fields.", "Missing Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CurrentStep--;
                        return;
                    }
                    break;

                case Step.Build:
                    BuildQuest();
                    CurrentStep--;
                    break;
            }
        }

        private void ShowSetup()
        {
            DetailControl.SyncQuestDataToUserInput();

            panelMain.Controls.Clear();
            SetupControl.EnableScrolling(); 
            panelMain.Controls.Add(SetupControl);

            buttonNext.Text = "Next >>";
            buttonBack.Visible = false;
        }

        private void ShowWait()
        {
            panelMain.Controls.Clear(); 
            SetupControl.DisableScrolling();

            buttonNext.Enabled = false;
            this.Cursor = Cursors.WaitCursor;
            panelMain.Controls.Add(WaitingControl);
            WaitingControl.Refresh();
        }

        private async void ShowDetails()
        {
            SetupControl.SyncQuestDataToUserInput();

            panelMain.Controls.Add(DetailControl);
            panelMain.Controls.Remove(WaitingControl);

            buttonBack.Visible = true;
            buttonNext.Text = "Build";
            this.Cursor = Cursors.Default;

            await Task.Delay(100);

            buttonNext.Enabled = true;
        }

        private void BuildQuest()
        {
            DetailControl.SyncQuestDataToUserInput();
            
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
            OpenFileDialog loadFile = new OpenFileDialog();
            loadFile.Filter = "Xml Files|*.xml|All Files|*.*";

            DialogResult result = loadFile.ShowDialog();
            if (result != DialogResult.OK) return;

            GoToPage(Step.Setup);

            if (Quest.Load(loadFile.FileName))
            {
                SetupControl.SyncUserInputToQuestData();
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
            SetupControl.SyncQuestDataToUserInput();
            GoToPage(Step.Setup); // Syncs details and avoids time spent reloading ObjectsDetails control panels.

            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "Xml File|*.xml";
            saveFile.FileName = Quest.SetupDetails.FpkName;
            DialogResult saveResult = saveFile.ShowDialog();
            if (saveResult != DialogResult.OK) return true;

            return Quest.Save(saveFile.FileName);
        }

        private void buttonOpenFolder_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(AppDomain.CurrentDomain.BaseDirectory);
            }
            catch { }
        }

        private void buttonBatchBuild_Click(object sender, EventArgs e)
        {
            OpenFileDialog loadFile = new OpenFileDialog();
            loadFile.Filter = "Xml Files|*.xml|All Files|*.*";
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
