namespace SOC.QuestObjects.ActiveItem
{
    partial class ActiveItemControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            groupActiveItemDet = new System.Windows.Forms.GroupBox();
            label_ObjType = new System.Windows.Forms.Label();
            panelQuestBoxes = new System.Windows.Forms.FlowLayoutPanel();
            comboBox_ObjType = new System.Windows.Forms.ComboBox();
            groupActiveItemDet.SuspendLayout();
            SuspendLayout();
            // 
            // groupActiveItemDet
            // 
            groupActiveItemDet.Controls.Add(label_ObjType);
            groupActiveItemDet.Controls.Add(panelQuestBoxes);
            groupActiveItemDet.Controls.Add(comboBox_ObjType);
            groupActiveItemDet.Dock = System.Windows.Forms.DockStyle.Fill;
            groupActiveItemDet.Location = new System.Drawing.Point(0, 0);
            groupActiveItemDet.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupActiveItemDet.Name = "groupActiveItemDet";
            groupActiveItemDet.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupActiveItemDet.Size = new System.Drawing.Size(350, 518);
            groupActiveItemDet.TabIndex = 29;
            groupActiveItemDet.TabStop = false;
            groupActiveItemDet.Text = "Active Items";
            // 
            // label_ObjType
            // 
            label_ObjType.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            label_ObjType.AutoSize = true;
            label_ObjType.Location = new System.Drawing.Point(7, 18);
            label_ObjType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label_ObjType.Name = "label_ObjType";
            label_ObjType.Size = new System.Drawing.Size(122, 15);
            label_ObjType.TabIndex = 35;
            label_ObjType.Text = "Target Objective Type:";
            // 
            // panelQuestBoxes
            // 
            panelQuestBoxes.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            panelQuestBoxes.AutoScroll = true;
            panelQuestBoxes.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            panelQuestBoxes.Location = new System.Drawing.Point(4, 68);
            panelQuestBoxes.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panelQuestBoxes.Name = "panelQuestBoxes";
            panelQuestBoxes.Size = new System.Drawing.Size(343, 447);
            panelQuestBoxes.TabIndex = 2;
            panelQuestBoxes.WrapContents = false;
            // 
            // comboBox_ObjType
            // 
            comboBox_ObjType.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            comboBox_ObjType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBox_ObjType.Font = new System.Drawing.Font("Consolas", 9F);
            comboBox_ObjType.FormattingEnabled = true;
            comboBox_ObjType.Items.AddRange(new object[] { "ELIMINATE", "RECOVERED", "KILLREQUIRED" });
            comboBox_ObjType.Location = new System.Drawing.Point(7, 37);
            comboBox_ObjType.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            comboBox_ObjType.Name = "comboBox_ObjType";
            comboBox_ObjType.Size = new System.Drawing.Size(335, 22);
            comboBox_ObjType.TabIndex = 1;
            // 
            // ActiveItemControl
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(groupActiveItemDet);
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "ActiveItemControl";
            Size = new System.Drawing.Size(350, 518);
            groupActiveItemDet.ResumeLayout(false);
            groupActiveItemDet.PerformLayout();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupActiveItemDet;
        private System.Windows.Forms.Label label_ObjType;
        public System.Windows.Forms.FlowLayoutPanel panelQuestBoxes;
        public System.Windows.Forms.ComboBox comboBox_ObjType;
    }
}
