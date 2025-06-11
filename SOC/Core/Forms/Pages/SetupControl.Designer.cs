namespace SOC.UI
{
    partial class SetupControl
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panelSetup = new System.Windows.Forms.Panel();
            groupBoxSetup = new System.Windows.Forms.GroupBox();
            comboBoxRoute = new System.Windows.Forms.ComboBox();
            label15 = new System.Windows.Forms.Label();
            comboBoxCP = new System.Windows.Forms.ComboBox();
            label20 = new System.Windows.Forms.Label();
            label14 = new System.Windows.Forms.Label();
            comboBoxCategory = new System.Windows.Forms.ComboBox();
            label11 = new System.Windows.Forms.Label();
            textBoxFPKName = new System.Windows.Forms.TextBox();
            label12 = new System.Windows.Forms.Label();
            textBoxZCoord = new System.Windows.Forms.TextBox();
            label10 = new System.Windows.Forms.Label();
            textBoxYCoord = new System.Windows.Forms.TextBox();
            label9 = new System.Windows.Forms.Label();
            textBoxXCoord = new System.Windows.Forms.TextBox();
            label6 = new System.Windows.Forms.Label();
            comboBoxRegion = new System.Windows.Forms.ComboBox();
            label5 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            textBoxQuestNum = new System.Windows.Forms.TextBox();
            comboBoxRadius = new System.Windows.Forms.ComboBox();
            comboBoxReward = new System.Windows.Forms.ComboBox();
            comboBoxLoadArea = new System.Windows.Forms.ComboBox();
            groupBoxLocations = new System.Windows.Forms.GroupBox();
            flowPanelLocationalStubs = new System.Windows.Forms.FlowLayoutPanel();
            labelFlowWidth = new System.Windows.Forms.Label();
            groupBoxFlavor = new System.Windows.Forms.GroupBox();
            buttonAddNotif = new System.Windows.Forms.Button();
            label13 = new System.Windows.Forms.Label();
            label8 = new System.Windows.Forms.Label();
            comboBoxProgressNotifs = new System.Windows.Forms.ComboBox();
            label7 = new System.Windows.Forms.Label();
            textBoxQuestDesc = new System.Windows.Forms.TextBox();
            textBoxQuestTitle = new System.Windows.Forms.TextBox();
            panelSetup.SuspendLayout();
            groupBoxSetup.SuspendLayout();
            groupBoxLocations.SuspendLayout();
            flowPanelLocationalStubs.SuspendLayout();
            groupBoxFlavor.SuspendLayout();
            SuspendLayout();
            // 
            // panelSetup
            // 
            panelSetup.Controls.Add(groupBoxSetup);
            panelSetup.Controls.Add(groupBoxLocations);
            panelSetup.Controls.Add(groupBoxFlavor);
            panelSetup.Dock = System.Windows.Forms.DockStyle.Fill;
            panelSetup.Location = new System.Drawing.Point(0, 0);
            panelSetup.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panelSetup.Name = "panelSetup";
            panelSetup.Size = new System.Drawing.Size(1260, 519);
            panelSetup.TabIndex = 11;
            // 
            // groupBoxSetup
            // 
            groupBoxSetup.BackColor = System.Drawing.Color.Transparent;
            groupBoxSetup.Controls.Add(comboBoxRoute);
            groupBoxSetup.Controls.Add(label15);
            groupBoxSetup.Controls.Add(comboBoxCP);
            groupBoxSetup.Controls.Add(label20);
            groupBoxSetup.Controls.Add(label14);
            groupBoxSetup.Controls.Add(comboBoxCategory);
            groupBoxSetup.Controls.Add(label11);
            groupBoxSetup.Controls.Add(textBoxFPKName);
            groupBoxSetup.Controls.Add(label12);
            groupBoxSetup.Controls.Add(textBoxZCoord);
            groupBoxSetup.Controls.Add(label10);
            groupBoxSetup.Controls.Add(textBoxYCoord);
            groupBoxSetup.Controls.Add(label9);
            groupBoxSetup.Controls.Add(textBoxXCoord);
            groupBoxSetup.Controls.Add(label6);
            groupBoxSetup.Controls.Add(comboBoxRegion);
            groupBoxSetup.Controls.Add(label5);
            groupBoxSetup.Controls.Add(label4);
            groupBoxSetup.Controls.Add(label3);
            groupBoxSetup.Controls.Add(label2);
            groupBoxSetup.Controls.Add(label1);
            groupBoxSetup.Controls.Add(textBoxQuestNum);
            groupBoxSetup.Controls.Add(comboBoxRadius);
            groupBoxSetup.Controls.Add(comboBoxReward);
            groupBoxSetup.Controls.Add(comboBoxLoadArea);
            groupBoxSetup.Location = new System.Drawing.Point(1, 3);
            groupBoxSetup.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBoxSetup.Name = "groupBoxSetup";
            groupBoxSetup.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBoxSetup.Size = new System.Drawing.Size(600, 227);
            groupBoxSetup.TabIndex = 0;
            groupBoxSetup.TabStop = false;
            groupBoxSetup.Text = "Sideop Setup";
            // 
            // comboBoxRoute
            // 
            comboBoxRoute.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxRoute.Font = new System.Drawing.Font("Consolas", 9F);
            comboBoxRoute.FormattingEnabled = true;
            comboBoxRoute.Location = new System.Drawing.Point(424, 134);
            comboBoxRoute.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            comboBoxRoute.Name = "comboBoxRoute";
            comboBoxRoute.Size = new System.Drawing.Size(156, 22);
            comboBoxRoute.TabIndex = 10;
            comboBoxRoute.DropDown += comboBoxRoute_DropDown;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new System.Drawing.Point(343, 136);
            label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label15.Name = "label15";
            label15.Size = new System.Drawing.Size(62, 15);
            label15.TabIndex = 0;
            label15.Text = "Route File:";
            // 
            // comboBoxCP
            // 
            comboBoxCP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxCP.Enabled = false;
            comboBoxCP.Font = new System.Drawing.Font("Consolas", 9F);
            comboBoxCP.FormattingEnabled = true;
            comboBoxCP.Location = new System.Drawing.Point(424, 103);
            comboBoxCP.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            comboBoxCP.Name = "comboBoxCP";
            comboBoxCP.Size = new System.Drawing.Size(156, 22);
            comboBoxCP.TabIndex = 8;
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Location = new System.Drawing.Point(346, 106);
            label20.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label20.Name = "label20";
            label20.Size = new System.Drawing.Size(59, 15);
            label20.TabIndex = 0;
            label20.Text = "Quest CP:";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new System.Drawing.Point(13, 186);
            label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label14.Name = "label14";
            label14.Size = new System.Drawing.Size(92, 15);
            label14.TabIndex = 0;
            label14.Text = "Quest Category:";
            // 
            // comboBoxCategory
            // 
            comboBoxCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxCategory.Font = new System.Drawing.Font("Consolas", 9F);
            comboBoxCategory.FormattingEnabled = true;
            comboBoxCategory.Items.AddRange(new object[] { "STORY", "EXTRACT_INTERPRETER", "BLUEPRINT", "EXTRACT_HIGHLY_SKILLED", "PRISONER", "CAPTURE_ANIMAL", "WANDERING_SOLDIER", "DDOG_PRISONER", "ELIMINATE_HEAVY_INFANTRY", "MINE_CLEARING", "ELIMINATE_ARMOR_VEHICLE", "EXTRACT_GUNSMITH", "EXTRACT_CONTAINERS", "INTEL_AGENT_EXTRACTION", "ELIMINATE_TANK_UNIT", "ELIMINATE_PUPPETS", "TARGET_PRACTICE" });
            comboBoxCategory.Location = new System.Drawing.Point(122, 182);
            comboBoxCategory.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            comboBoxCategory.Name = "comboBoxCategory";
            comboBoxCategory.Size = new System.Drawing.Size(178, 22);
            comboBoxCategory.TabIndex = 11;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new System.Drawing.Point(19, 25);
            label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label11.Name = "label11";
            label11.Size = new System.Drawing.Size(84, 15);
            label11.TabIndex = 0;
            label11.Text = ".FPK Filename:";
            // 
            // textBoxFPKName
            // 
            textBoxFPKName.BackColor = System.Drawing.Color.Silver;
            textBoxFPKName.Font = new System.Drawing.Font("Consolas", 9F);
            textBoxFPKName.Location = new System.Drawing.Point(122, 22);
            textBoxFPKName.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            textBoxFPKName.Name = "textBoxFPKName";
            textBoxFPKName.Size = new System.Drawing.Size(178, 22);
            textBoxFPKName.TabIndex = 1;
            textBoxFPKName.Leave += textBoxFPKName_Leave;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.BackColor = System.Drawing.Color.Transparent;
            label12.Location = new System.Drawing.Point(243, 107);
            label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label12.Name = "label12";
            label12.RightToLeft = System.Windows.Forms.RightToLeft.No;
            label12.Size = new System.Drawing.Size(17, 15);
            label12.TabIndex = 0;
            label12.Text = "Z:";
            // 
            // textBoxZCoord
            // 
            textBoxZCoord.BackColor = System.Drawing.Color.Silver;
            textBoxZCoord.Enabled = false;
            textBoxZCoord.Font = new System.Drawing.Font("Consolas", 9F);
            textBoxZCoord.Location = new System.Drawing.Point(262, 104);
            textBoxZCoord.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            textBoxZCoord.Name = "textBoxZCoord";
            textBoxZCoord.Size = new System.Drawing.Size(38, 22);
            textBoxZCoord.TabIndex = 7;
            textBoxZCoord.Text = "0";
            textBoxZCoord.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.BackColor = System.Drawing.Color.Transparent;
            label10.Location = new System.Drawing.Point(181, 107);
            label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label10.Name = "label10";
            label10.RightToLeft = System.Windows.Forms.RightToLeft.No;
            label10.Size = new System.Drawing.Size(17, 15);
            label10.TabIndex = 0;
            label10.Text = "Y:";
            // 
            // textBoxYCoord
            // 
            textBoxYCoord.BackColor = System.Drawing.Color.Silver;
            textBoxYCoord.Enabled = false;
            textBoxYCoord.Font = new System.Drawing.Font("Consolas", 9F);
            textBoxYCoord.Location = new System.Drawing.Point(201, 104);
            textBoxYCoord.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            textBoxYCoord.Name = "textBoxYCoord";
            textBoxYCoord.Size = new System.Drawing.Size(38, 22);
            textBoxYCoord.TabIndex = 6;
            textBoxYCoord.Text = "0";
            textBoxYCoord.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.BackColor = System.Drawing.Color.Transparent;
            label9.Location = new System.Drawing.Point(119, 107);
            label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label9.Name = "label9";
            label9.RightToLeft = System.Windows.Forms.RightToLeft.No;
            label9.Size = new System.Drawing.Size(17, 15);
            label9.TabIndex = 0;
            label9.Text = "X:";
            // 
            // textBoxXCoord
            // 
            textBoxXCoord.BackColor = System.Drawing.Color.Silver;
            textBoxXCoord.Enabled = false;
            textBoxXCoord.Font = new System.Drawing.Font("Consolas", 9F);
            textBoxXCoord.Location = new System.Drawing.Point(139, 104);
            textBoxXCoord.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            textBoxXCoord.Name = "textBoxXCoord";
            textBoxXCoord.Size = new System.Drawing.Size(40, 22);
            textBoxXCoord.TabIndex = 5;
            textBoxXCoord.Text = "0";
            textBoxXCoord.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(37, 75);
            label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(68, 15);
            label6.TabIndex = 0;
            label6.Text = "Quest Map:";
            // 
            // comboBoxRegion
            // 
            comboBoxRegion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxRegion.Font = new System.Drawing.Font("Consolas", 9F);
            comboBoxRegion.FormattingEnabled = true;
            comboBoxRegion.Items.AddRange(new object[] { "Afghanistan", "Central Africa", "Mother Base" });
            comboBoxRegion.Location = new System.Drawing.Point(122, 72);
            comboBoxRegion.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            comboBoxRegion.Name = "comboBoxRegion";
            comboBoxRegion.Size = new System.Drawing.Size(178, 22);
            comboBoxRegion.TabIndex = 3;
            comboBoxRegion.SelectedIndexChanged += comboBoxRegion_SelectedIndexChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(332, 186);
            label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(70, 15);
            label5.TabIndex = 0;
            label5.Text = "Quest Rank:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(337, 75);
            label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(68, 15);
            label4.TabIndex = 0;
            label4.Text = "Quest Area:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(59, 136);
            label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(45, 15);
            label3.TabIndex = 0;
            label3.Text = "Radius:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(5, 107);
            label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(101, 15);
            label2.TabIndex = 0;
            label2.Text = "Map Coordinates:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(320, 25);
            label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(88, 15);
            label1.TabIndex = 0;
            label1.Text = "Quest Number:";
            // 
            // textBoxQuestNum
            // 
            textBoxQuestNum.BackColor = System.Drawing.Color.Silver;
            textBoxQuestNum.Font = new System.Drawing.Font("Consolas", 9F);
            textBoxQuestNum.Location = new System.Drawing.Point(424, 22);
            textBoxQuestNum.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            textBoxQuestNum.Name = "textBoxQuestNum";
            textBoxQuestNum.Size = new System.Drawing.Size(156, 22);
            textBoxQuestNum.TabIndex = 2;
            textBoxQuestNum.Leave += textBoxQuestNum_Leave;
            // 
            // comboBoxRadius
            // 
            comboBoxRadius.BackColor = System.Drawing.Color.Silver;
            comboBoxRadius.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxRadius.Enabled = false;
            comboBoxRadius.FlatStyle = System.Windows.Forms.FlatStyle.System;
            comboBoxRadius.Font = new System.Drawing.Font("Consolas", 9F);
            comboBoxRadius.FormattingEnabled = true;
            comboBoxRadius.Items.AddRange(new object[] { "1    (30 Meters)", "2    (40 Meters)", "3    (62.5 Meters)", "4    (125 Meters)", "5    (200 Meters)", "6    (325 Meters)", "7    (450 Meters)", "8    (600 Meters)", "9    (1,200 Meters)" });
            comboBoxRadius.Location = new System.Drawing.Point(122, 134);
            comboBoxRadius.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            comboBoxRadius.Name = "comboBoxRadius";
            comboBoxRadius.Size = new System.Drawing.Size(178, 22);
            comboBoxRadius.TabIndex = 9;
            // 
            // comboBoxReward
            // 
            comboBoxReward.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxReward.Font = new System.Drawing.Font("Consolas", 9F);
            comboBoxReward.FormattingEnabled = true;
            comboBoxReward.Items.AddRange(new object[] { "S    (300,000 GMP)", "A    (200,000 GMP)", "B    (180,000 GMP)", "C    (140,000 GMP)", "D    (120,000 GMP)", "E    (100,000 GMP)", "F    (90,000 GMP)", "G    (80,000 GMP)", "H    (60,000 GMP)", "I    (30,000 GMP)" });
            comboBoxReward.Location = new System.Drawing.Point(424, 182);
            comboBoxReward.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            comboBoxReward.Name = "comboBoxReward";
            comboBoxReward.Size = new System.Drawing.Size(156, 22);
            comboBoxReward.TabIndex = 12;
            // 
            // comboBoxLoadArea
            // 
            comboBoxLoadArea.BackColor = System.Drawing.SystemColors.Window;
            comboBoxLoadArea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxLoadArea.Enabled = false;
            comboBoxLoadArea.Font = new System.Drawing.Font("Consolas", 9F);
            comboBoxLoadArea.FormattingEnabled = true;
            comboBoxLoadArea.Location = new System.Drawing.Point(424, 72);
            comboBoxLoadArea.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            comboBoxLoadArea.Name = "comboBoxLoadArea";
            comboBoxLoadArea.Size = new System.Drawing.Size(156, 22);
            comboBoxLoadArea.TabIndex = 4;
            // 
            // groupBoxLocations
            // 
            groupBoxLocations.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            groupBoxLocations.BackColor = System.Drawing.Color.Transparent;
            groupBoxLocations.Controls.Add(flowPanelLocationalStubs);
            groupBoxLocations.Location = new System.Drawing.Point(608, 3);
            groupBoxLocations.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBoxLocations.Name = "groupBoxLocations";
            groupBoxLocations.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBoxLocations.Size = new System.Drawing.Size(651, 512);
            groupBoxLocations.TabIndex = 17;
            groupBoxLocations.TabStop = false;
            groupBoxLocations.Text = "Locational Data";
            // 
            // flowPanelLocationalStubs
            // 
            flowPanelLocationalStubs.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            flowPanelLocationalStubs.AutoScroll = true;
            flowPanelLocationalStubs.Controls.Add(labelFlowWidth);
            flowPanelLocationalStubs.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            flowPanelLocationalStubs.Location = new System.Drawing.Point(7, 18);
            flowPanelLocationalStubs.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            flowPanelLocationalStubs.Name = "flowPanelLocationalStubs";
            flowPanelLocationalStubs.Size = new System.Drawing.Size(640, 490);
            flowPanelLocationalStubs.TabIndex = 16;
            flowPanelLocationalStubs.WrapContents = false;
            flowPanelLocationalStubs.Layout += flowPanelLocationalStubs_Layout;
            // 
            // labelFlowWidth
            // 
            labelFlowWidth.Location = new System.Drawing.Point(0, 0);
            labelFlowWidth.Margin = new System.Windows.Forms.Padding(0);
            labelFlowWidth.Name = "labelFlowWidth";
            labelFlowWidth.Size = new System.Drawing.Size(639, 0);
            labelFlowWidth.TabIndex = 0;
            // 
            // groupBoxFlavor
            // 
            groupBoxFlavor.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            groupBoxFlavor.BackColor = System.Drawing.Color.Transparent;
            groupBoxFlavor.Controls.Add(buttonAddNotif);
            groupBoxFlavor.Controls.Add(label13);
            groupBoxFlavor.Controls.Add(label8);
            groupBoxFlavor.Controls.Add(comboBoxProgressNotifs);
            groupBoxFlavor.Controls.Add(label7);
            groupBoxFlavor.Controls.Add(textBoxQuestDesc);
            groupBoxFlavor.Controls.Add(textBoxQuestTitle);
            groupBoxFlavor.Location = new System.Drawing.Point(1, 238);
            groupBoxFlavor.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBoxFlavor.Name = "groupBoxFlavor";
            groupBoxFlavor.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBoxFlavor.Size = new System.Drawing.Size(600, 278);
            groupBoxFlavor.TabIndex = 0;
            groupBoxFlavor.TabStop = false;
            groupBoxFlavor.Text = "Sideop Flavor Text";
            // 
            // buttonAddNotif
            // 
            buttonAddNotif.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            buttonAddNotif.Location = new System.Drawing.Point(489, 238);
            buttonAddNotif.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            buttonAddNotif.Name = "buttonAddNotif";
            buttonAddNotif.Size = new System.Drawing.Size(76, 24);
            buttonAddNotif.TabIndex = 16;
            buttonAddNotif.Text = "Custom...";
            buttonAddNotif.UseVisualStyleBackColor = true;
            buttonAddNotif.Click += buttonAddNotif_Click;
            // 
            // label13
            // 
            label13.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            label13.AutoSize = true;
            label13.Location = new System.Drawing.Point(7, 219);
            label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label13.Name = "label13";
            label13.Size = new System.Drawing.Size(121, 15);
            label13.TabIndex = 0;
            label13.Text = "Progress Notification:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new System.Drawing.Point(7, 78);
            label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(104, 15);
            label8.TabIndex = 0;
            label8.Text = "Quest Description:";
            // 
            // comboBoxProgressNotifs
            // 
            comboBoxProgressNotifs.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            comboBoxProgressNotifs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxProgressNotifs.Font = new System.Drawing.Font("Consolas", 9F);
            comboBoxProgressNotifs.FormattingEnabled = true;
            comboBoxProgressNotifs.Location = new System.Drawing.Point(7, 239);
            comboBoxProgressNotifs.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            comboBoxProgressNotifs.Name = "comboBoxProgressNotifs";
            comboBoxProgressNotifs.Size = new System.Drawing.Size(474, 22);
            comboBoxProgressNotifs.TabIndex = 15;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new System.Drawing.Point(7, 23);
            label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(66, 15);
            label7.TabIndex = 0;
            label7.Text = "Quest Title:";
            // 
            // textBoxQuestDesc
            // 
            textBoxQuestDesc.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            textBoxQuestDesc.BackColor = System.Drawing.Color.Silver;
            textBoxQuestDesc.Font = new System.Drawing.Font("Consolas", 9F);
            textBoxQuestDesc.Location = new System.Drawing.Point(7, 97);
            textBoxQuestDesc.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            textBoxQuestDesc.Multiline = true;
            textBoxQuestDesc.Name = "textBoxQuestDesc";
            textBoxQuestDesc.Size = new System.Drawing.Size(585, 108);
            textBoxQuestDesc.TabIndex = 14;
            // 
            // textBoxQuestTitle
            // 
            textBoxQuestTitle.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            textBoxQuestTitle.BackColor = System.Drawing.Color.Silver;
            textBoxQuestTitle.Font = new System.Drawing.Font("Consolas", 9F);
            textBoxQuestTitle.Location = new System.Drawing.Point(7, 42);
            textBoxQuestTitle.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            textBoxQuestTitle.Name = "textBoxQuestTitle";
            textBoxQuestTitle.Size = new System.Drawing.Size(585, 22);
            textBoxQuestTitle.TabIndex = 13;
            // 
            // SetupControl
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            AutoScroll = true;
            Controls.Add(panelSetup);
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "SetupControl";
            Size = new System.Drawing.Size(1260, 519);
            panelSetup.ResumeLayout(false);
            groupBoxSetup.ResumeLayout(false);
            groupBoxSetup.PerformLayout();
            groupBoxLocations.ResumeLayout(false);
            flowPanelLocationalStubs.ResumeLayout(false);
            groupBoxFlavor.ResumeLayout(false);
            groupBoxFlavor.PerformLayout();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelSetup;
        private System.Windows.Forms.GroupBox groupBoxSetup;
        private System.Windows.Forms.Label label14;
        public System.Windows.Forms.ComboBox comboBoxCategory;
        private System.Windows.Forms.Label label11;
        public System.Windows.Forms.TextBox textBoxFPKName;
        private System.Windows.Forms.Label label12;
        public System.Windows.Forms.TextBox textBoxZCoord;
        private System.Windows.Forms.Label label10;
        public System.Windows.Forms.TextBox textBoxYCoord;
        private System.Windows.Forms.Label label9;
        public System.Windows.Forms.TextBox textBoxXCoord;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.ComboBox comboBoxRegion;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox textBoxQuestNum;
        public System.Windows.Forms.ComboBox comboBoxRadius;
        public System.Windows.Forms.ComboBox comboBoxReward;
        public System.Windows.Forms.ComboBox comboBoxLoadArea;
        private System.Windows.Forms.GroupBox groupBoxLocations;
        private System.Windows.Forms.GroupBox groupBoxFlavor;
        private System.Windows.Forms.Button buttonAddNotif;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label8;
        public System.Windows.Forms.ComboBox comboBoxProgressNotifs;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.TextBox textBoxQuestDesc;
        public System.Windows.Forms.TextBox textBoxQuestTitle;
        public System.Windows.Forms.ComboBox comboBoxCP;
        private System.Windows.Forms.Label label20;
        public System.Windows.Forms.ComboBox comboBoxRoute;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.FlowLayoutPanel flowPanelLocationalStubs;
        private System.Windows.Forms.Label labelFlowWidth;
    }
}