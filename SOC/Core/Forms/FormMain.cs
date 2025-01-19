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

        private ObjectsDetails objectsDetails;
        private SetupDisplay setupPage;
        private DetailsDisplay detailPage;
        private Waiting waitingPage;
        private Step currentStep;

        public FormMain()
        {
            objectsDetails = new ObjectsDetails();
            setupPage = new SetupDisplay(objectsDetails);
            detailPage = new DetailsDisplay(objectsDetails);
            waitingPage = new Waiting();
            currentStep = Step.NONE;

            InitializeComponent();

            GoToPage(Step.Setup);
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            GoToPage(currentStep + 1);
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            GoToPage(currentStep - 1);
        }

        private bool isFilled()
        {
            //return true; // FOR DEBUG
            if (string.IsNullOrEmpty(setupPage.textBoxFPKName.Text) || string.IsNullOrEmpty(setupPage.textBoxQuestNum.Text) || string.IsNullOrEmpty(setupPage.textBoxQuestTitle.Text) || string.IsNullOrEmpty(setupPage.textBoxQuestDesc.Text))
                return false;
            if (setupPage.comboBoxCategory.SelectedIndex == -1 || setupPage.comboBoxReward.SelectedIndex == -1 || setupPage.comboBoxProgressNotifs.SelectedIndex == -1 || setupPage.comboBoxRegion.SelectedIndex == -1)
                return false;
            if (setupPage.comboBoxCP.Enabled)
                if (setupPage.comboBoxCP.SelectedIndex == -1 || setupPage.comboBoxLoadArea.SelectedIndex == -1 || setupPage.comboBoxRadius.SelectedIndex == -1 || string.IsNullOrEmpty(setupPage.textBoxXCoord.Text) || string.IsNullOrEmpty(setupPage.textBoxYCoord.Text) || string.IsNullOrEmpty(setupPage.textBoxZCoord.Text))
                    return false;

            return true;
        }

        private void GoToPage(Step step)
        {
            if (step == currentStep)
            {
                return;
            }

            currentStep = step;
            switch (step)
            {
                case Step.Setup:
                    ShowSetup();
                    break;

                case Step.Detail:
                    if (isFilled())
                    {
                        ShowWait();
                        ShowDetails();
                    }
                    else
                    {
                        MessageBox.Show("Please fill in the remaining Setup and Flavor Text fields.", "Missing Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        currentStep--;
                        return;
                    }
                    break;

                case Step.Build:
                    BuildQuest();
                    currentStep--;
                    break;
            }
        }

        private void ShowSetup()
        {
            objectsDetails.UpdateAllDetailsFromVisualizers();
            objectsDetails.RefreshAllStubTexts();

            panelMain.Controls.Clear();
            setupPage.EnableScrolling(); 
            panelMain.Controls.Add(setupPage);

            buttonNext.Text = "Next >>";
            buttonBack.Visible = false;
        }

        private void ShowWait()
        {
            panelMain.Controls.Clear(); 
            setupPage.DisableScrolling();

            buttonNext.Enabled = false;
            this.Cursor = Cursors.WaitCursor;
            panelMain.Controls.Add(waitingPage);
            waitingPage.Refresh();
        }

        private async void ShowDetails()
        {
            SetupDetails setupDetails = setupPage.GetSetupDetails();
            detailPage.RefreshObjectPanels(setupDetails, objectsDetails);

            panelMain.Controls.Add(detailPage);
            panelMain.Controls.Remove(waitingPage);

            buttonBack.Visible = true;
            buttonNext.Text = "Build";
            this.Cursor = Cursors.Default;

            await Task.Delay(100);

            buttonNext.Enabled = true;
        }

        private void BuildQuest()
        {
            objectsDetails.UpdateAllDetailsFromVisualizers();
            Quest quest = new Quest(setupPage.GetSetupDetails(), objectsDetails.GetQuestObjectDetails());
            
            if (BuildManager.Build(quest))
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
            setupPage.refreshNotifsList();
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog loadFile = new OpenFileDialog();
            loadFile.Filter = "Xml Files|*.xml|All Files|*.*";

            DialogResult result = loadFile.ShowDialog();
            if (result != DialogResult.OK) return;

            GoToPage(Step.Setup);

            Quest quest = new Quest();

            if (quest.Load(loadFile.FileName))
            {
                objectsDetails = new ObjectsDetails(quest.questObjectDetails);
                setupPage.managers.ToString();
                setupPage.SetForm(quest.setupDetails);
                objectsDetails.RefreshAllStubTexts();
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isFilled())
            {
                DialogResult result = MessageBox.Show("Do you want to save this Sideop to an Xml file?", "SOC", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                    Save();
                else if (result == DialogResult.Cancel)
                    e.Cancel = true;
            }
        }

        private void Save()
        {
            SetupDetails setup = setupPage.GetSetupDetails();
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "Xml File|*.xml";
            saveFile.FileName = setup.FpkName;
            DialogResult saveResult = saveFile.ShowDialog();
            if (saveResult != DialogResult.OK) return;

            GoToPage(Step.Setup);

            Quest quest = new Quest(setup, objectsDetails.GetQuestObjectDetails());
            quest.Save(saveFile.FileName);
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
                    if (!quests.Exists(questInList => questInList.setupDetails.FpkName == quest.setupDetails.FpkName)
                        && !quests.Exists(questInList => questInList.setupDetails.QuestNum == quest.setupDetails.QuestNum))
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
