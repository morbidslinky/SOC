namespace SOC.QuestObjects.Hostage
{
    partial class HostageControl
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
            groupboxDetail = new System.Windows.Forms.GroupBox();
            panelQuestBoxes = new System.Windows.Forms.FlowLayoutPanel();
            label_ObjType = new System.Windows.Forms.Label();
            comboBox_ObjType = new System.Windows.Forms.ComboBox();
            checkBox_intrgt = new System.Windows.Forms.CheckBox();
            label_Body = new System.Windows.Forms.Label();
            comboBox_Body = new System.Windows.Forms.ComboBox();
            groupboxDetail.SuspendLayout();
            SuspendLayout();
            // 
            // groupboxDetail
            // 
            groupboxDetail.Controls.Add(panelQuestBoxes);
            groupboxDetail.Controls.Add(label_ObjType);
            groupboxDetail.Controls.Add(comboBox_ObjType);
            groupboxDetail.Controls.Add(checkBox_intrgt);
            groupboxDetail.Controls.Add(label_Body);
            groupboxDetail.Controls.Add(comboBox_Body);
            groupboxDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            groupboxDetail.Location = new System.Drawing.Point(0, 0);
            groupboxDetail.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupboxDetail.Name = "groupboxDetail";
            groupboxDetail.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupboxDetail.Size = new System.Drawing.Size(350, 518);
            groupboxDetail.TabIndex = 2;
            groupboxDetail.TabStop = false;
            groupboxDetail.Text = "Prisoners";
            // 
            // panelQuestBoxes
            // 
            panelQuestBoxes.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            panelQuestBoxes.AutoScroll = true;
            panelQuestBoxes.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            panelQuestBoxes.Location = new System.Drawing.Point(4, 137);
            panelQuestBoxes.Margin = new System.Windows.Forms.Padding(0);
            panelQuestBoxes.Name = "panelQuestBoxes";
            panelQuestBoxes.Size = new System.Drawing.Size(343, 377);
            panelQuestBoxes.TabIndex = 3;
            panelQuestBoxes.WrapContents = false;
            // 
            // label_ObjType
            // 
            label_ObjType.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            label_ObjType.AutoSize = true;
            label_ObjType.Location = new System.Drawing.Point(7, 18);
            label_ObjType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label_ObjType.Name = "label_ObjType";
            label_ObjType.Size = new System.Drawing.Size(119, 15);
            label_ObjType.TabIndex = 13;
            label_ObjType.Text = "Target Objective Type";
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
            // checkBox_intrgt
            // 
            checkBox_intrgt.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            checkBox_intrgt.AutoSize = true;
            checkBox_intrgt.Font = new System.Drawing.Font("Consolas", 9F);
            checkBox_intrgt.Location = new System.Drawing.Point(7, 114);
            checkBox_intrgt.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            checkBox_intrgt.Name = "checkBox_intrgt";
            checkBox_intrgt.Size = new System.Drawing.Size(215, 18);
            checkBox_intrgt.TabIndex = 0;
            checkBox_intrgt.TabStop = false;
            checkBox_intrgt.Text = "Interrogate For Whereabouts";
            checkBox_intrgt.UseVisualStyleBackColor = true;
            // 
            // label_Body
            // 
            label_Body.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            label_Body.AutoSize = true;
            label_Body.Location = new System.Drawing.Point(7, 65);
            label_Body.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label_Body.Name = "label_Body";
            label_Body.Size = new System.Drawing.Size(34, 15);
            label_Body.TabIndex = 2;
            label_Body.Text = "Body";
            // 
            // comboBox_Body
            // 
            comboBox_Body.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            comboBox_Body.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBox_Body.Font = new System.Drawing.Font("Consolas", 9F);
            comboBox_Body.FormattingEnabled = true;
            comboBox_Body.Location = new System.Drawing.Point(7, 83);
            comboBox_Body.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            comboBox_Body.Name = "comboBox_Body";
            comboBox_Body.Size = new System.Drawing.Size(335, 22);
            comboBox_Body.TabIndex = 2;
            // 
            // HostageControl
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(groupboxDetail);
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "HostageControl";
            Size = new System.Drawing.Size(350, 518);
            groupboxDetail.ResumeLayout(false);
            groupboxDetail.PerformLayout();
            ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.GroupBox groupboxDetail;
        private System.Windows.Forms.Label label_ObjType;
        public System.Windows.Forms.CheckBox checkBox_intrgt;
        public System.Windows.Forms.ComboBox comboBox_Body;
        private System.Windows.Forms.Label label_Body;
        public System.Windows.Forms.ComboBox comboBox_ObjType;
        public System.Windows.Forms.FlowLayoutPanel panelQuestBoxes;
    }
}
