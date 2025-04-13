using SOC.QuestObjects.Common;
using SOC.Core.Classes.Route;
using System;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Collections.Generic;
using SOC.Classes.Common;
using System.Globalization;
using SOC.Classes.QuestBuild;
using SOC.QuestObjects.Enemy;
using SOC.QuestObjects.Vehicle;

namespace SOC.UI
{
    public partial class SetupControl : UserControl
    {
        PanelScroll locationalTabsScrolling;
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32 SendMessage(IntPtr hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)]string lParam);

        public int locationID = -1;
        public Quest Quest;

        public SetupControl(Quest quest)
        {
            InitializeComponent();
            locationalTabsScrolling = new PanelScroll(this.flowPanelLocationalStubs, true);
            Dock = DockStyle.Fill;
            SendMessage(textBoxQuestNum.Handle, 0x1501, 1, "30103");
            SendMessage(textBoxFPKName.Handle, 0x1501, 1, "Example_Quest_Name");
            SendMessage(textBoxQuestTitle.Handle, 0x1501, 1, "Example Quest Title Text");

            Quest = quest;
            flowPanelLocationalStubs.Controls.AddRange(Quest.GetLocationalStubs());

            refreshNotifsList();
            refreshRoutesList();
        }

        public bool IsFilled()
        {
            //return true; // FOR DEBUG
            if (string.IsNullOrEmpty(textBoxFPKName.Text) || string.IsNullOrEmpty(textBoxQuestNum.Text) || string.IsNullOrEmpty(textBoxQuestTitle.Text) || string.IsNullOrEmpty(textBoxQuestDesc.Text))
                return false;
            if (comboBoxCategory.SelectedIndex == -1 || comboBoxReward.SelectedIndex == -1 || comboBoxProgressNotifs.SelectedIndex == -1 || comboBoxRegion.SelectedIndex == -1)
                return false;
            if (comboBoxCP.Enabled)
                if (comboBoxCP.SelectedIndex == -1 || comboBoxLoadArea.SelectedIndex == -1 || comboBoxRadius.SelectedIndex == -1 || string.IsNullOrEmpty(textBoxXCoord.Text) || string.IsNullOrEmpty(textBoxYCoord.Text) || string.IsNullOrEmpty(textBoxZCoord.Text))
                    return false;

            return true;
        }

        public void SyncUserInputToQuestData()
        {
            textBoxQuestTitle.Text = Quest.SetupDetails.QuestTitle;
            textBoxQuestDesc.Text = Quest.SetupDetails.QuestDesc;
            textBoxFPKName.Text = Quest.SetupDetails.FpkName;
            textBoxQuestNum.Text = Quest.SetupDetails.QuestNum;

            locationID = Quest.SetupDetails.locationID;

            switch(locationID)
            {
                case 10:
                    comboBoxRegion.Text = "Afghanistan";
                    break;
                case 20:
                    comboBoxRegion.Text = "Central Africa";
                    break;
                case 50:
                    comboBoxRegion.Text = "Mother Base";
                    break;
            }

            comboBoxLoadArea.Text = Quest.SetupDetails.loadArea;

            textBoxXCoord.Text = Quest.SetupDetails.coords.xCoord;
            textBoxYCoord.Text = Quest.SetupDetails.coords.yCoord;
            textBoxZCoord.Text = Quest.SetupDetails.coords.zCoord;

            comboBoxRadius.Text = Quest.SetupDetails.radius;
            comboBoxCategory.Text = Quest.SetupDetails.category;
            comboBoxReward.Text = Quest.SetupDetails.reward;
            
            comboBoxCP.Text = Quest.SetupDetails.CPName;

            refreshNotifsList();
            string displayNotification = UpdateNotifsManager.GetDisplayNotification(Quest.SetupDetails.progressLangID);
            if (displayNotification != null)
                comboBoxProgressNotifs.Text = displayNotification;
            else if (comboBoxProgressNotifs.Items.Count > 0)
                comboBoxProgressNotifs.SelectedIndex = 0;

            refreshRoutesList();
            if (!string.IsNullOrEmpty(Quest.SetupDetails.routeName) && comboBoxRoute.Items.Contains(Quest.SetupDetails.routeName))
                comboBoxRoute.SelectedItem = Quest.SetupDetails.routeName;
            else
                comboBoxRoute.SelectedItem = "NONE";

            Quest.RefreshAllStubTexts();
        }

        public void refreshNotifsList()
        {
            UpdateComboBox(comboBoxProgressNotifs, UpdateNotifsManager.GetAllDisplayNotifications());
        }

        public void refreshRoutesList()
        {
            List<string> routesList = RouteManager.GetRouteFileNameList();
            routesList.Insert(0, "NONE");

            UpdateComboBox(comboBoxRoute, routesList);
        }

        private void disableRegionInput()
        {
            comboBoxRadius.Enabled = false; comboBoxCP.Enabled = false;
            textBoxXCoord.Enabled = false; textBoxYCoord.Enabled = false; textBoxZCoord.Enabled = false;
        }

        private void enableRegionInput()
        {
            comboBoxLoadArea.Enabled = true; comboBoxRadius.Enabled = true; comboBoxCP.Enabled = true;
            textBoxXCoord.Enabled = true; textBoxYCoord.Enabled = true; textBoxZCoord.Enabled = true;
        }

        private void comboBoxRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] loadArea = new string[0];
            string[] cpNames = new string[0];
            enableRegionInput();
            Quest.EnableObjectTypeStub(typeof(VehiclesDetail));

            switch (comboBoxRegion.Text)
            {
                case "Afghanistan":
                    locationID = 10;
                    loadArea = LoadAreas.afgh;
                    cpNames = EnemyInfo.GetCPNames(EnemyInfo.AfghCPs);
                    break;

                case "Central Africa":
                    locationID = 20;
                    loadArea = LoadAreas.mafr;
                    cpNames = EnemyInfo.GetCPNames(EnemyInfo.MafrCPs);
                    break;

                case "Mother Base":
                    locationID = 50;
                    loadArea = LoadAreas.mtbs;
                    cpNames = EnemyInfo.GetCPNames(EnemyInfo.MtbsCP);
                    disableRegionInput();
                    Quest.DisableObjectTypeStub(typeof(VehiclesDetail), "Disabled on Mother Base");
                    comboBoxRadius.Text = "1";
                    break;
            }

            UpdateComboBox(comboBoxLoadArea, loadArea);
            UpdateComboBox(comboBoxCP, cpNames);
        }

        private void buttonAddNotif_Click(object sender, EventArgs e)
        {
            formCustomProgressLang customLang = new formCustomProgressLang();
            customLang.ShowDialog();
            refreshNotifsList();
        }

        private void textBoxQuestNum_Leave(object sender, EventArgs e)
        {
            int qNumInt = 0;
            bool isvalid = false;

            if (Int32.TryParse(textBoxQuestNum.Text, out qNumInt))
            {
                if (qNumInt >= 30103 && qNumInt <= 39009)
                {
                    textBoxQuestNum.Text = qNumInt.ToString("F0", CultureInfo.InvariantCulture);
                    isvalid = true;
                }
            }
            if (!isvalid && !string.IsNullOrEmpty(textBoxQuestNum.Text))
            {
                MessageBox.Show(string.Format("Invalid Quest Number: {0} \nThe Quest Number must be an integer between 30103 and 39009", qNumInt.ToString()), "Invalid Quest Number", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void textBoxFPKName_Leave(object sender, EventArgs e)
        {
            string invalidchars = @"[\/\?\\\|\*\:\""\<\> ]";
            string replacement = "_";
            Regex fileNameFixer = new Regex(invalidchars);
            textBoxFPKName.Text = fileNameFixer.Replace(textBoxFPKName.Text, replacement);
        }

        private void comboBoxRoute_DropDown(object sender, EventArgs e)
        {
            refreshRoutesList();
        }

        private void flowPanelLocationalStubs_Layout(object sender, LayoutEventArgs e) // necessary jank for flowLayoutPanels: https://docs.microsoft.com/en-us/dotnet/framework/winforms/controls/how-to-anchor-and-dock-child-controls-in-a-flowlayoutpanel-control
        {
            labelFlowWidth.Width = flowPanelLocationalStubs.Width - 18;
        }
        
        public void UpdateComboBox(ComboBox box, List<string> itemList)
        {
            int currentIndex = box.SelectedIndex;

            if (currentIndex >= itemList.Count)
                currentIndex = 0;

            box.Items.Clear();
            box.Items.AddRange(itemList.ToArray());
            box.SelectedIndex = currentIndex;
        }

        public static void UpdateComboBox(ComboBox box, string[] itemList)
        {
            int currentIndex = box.SelectedIndex;

            if (currentIndex >= itemList.Length)
                currentIndex = 0;

            box.Items.Clear();
            box.Items.AddRange(itemList);
            box.SelectedIndex = currentIndex;
        }

        public void EnableScrolling()
        {
            Application.AddMessageFilter(locationalTabsScrolling);
        }

        public void DisableScrolling()
        {
            Application.RemoveMessageFilter(locationalTabsScrolling);
        }

        internal void SyncQuestDataToUserInput()
        {
            Quest.SetupDetails = new SetupDetails(this);

            foreach (ObjectsDetail detail in Quest.ObjectsDetails.Details)
            {
                var controlPanel = detail.GetControlPanel();
                controlPanel.SetDetailsFromSetup(detail, Quest.SetupDetails);

                if (detail.GetQuestObjects().Count > 0)
                {
                    controlPanel.ShowDetail();
                }
                else
                {
                    controlPanel.HideDetail();
                }

                controlPanel.RedrawControl(detail);
            }
        }
    }
}
