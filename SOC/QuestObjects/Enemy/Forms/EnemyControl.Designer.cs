namespace SOC.QuestObjects.Enemy
{
    partial class EnemyControl
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
            label_Subtype = new System.Windows.Forms.Label();
            comboBox_Subtype = new System.Windows.Forms.ComboBox();
            groupboxDetail.SuspendLayout();
            SuspendLayout();
            // 
            // groupboxDetail
            // 
            groupboxDetail.Controls.Add(panelQuestBoxes);
            groupboxDetail.Controls.Add(label_ObjType);
            groupboxDetail.Controls.Add(comboBox_ObjType);
            groupboxDetail.Controls.Add(label_Subtype);
            groupboxDetail.Controls.Add(comboBox_Subtype);
            groupboxDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            groupboxDetail.Location = new System.Drawing.Point(0, 0);
            groupboxDetail.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupboxDetail.Name = "groupboxDetail";
            groupboxDetail.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupboxDetail.Size = new System.Drawing.Size(350, 518);
            groupboxDetail.TabIndex = 34;
            groupboxDetail.TabStop = false;
            groupboxDetail.Text = "Enemies";
            // 
            // panelQuestBoxes
            // 
            panelQuestBoxes.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            panelQuestBoxes.AutoScroll = true;
            panelQuestBoxes.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            panelQuestBoxes.Location = new System.Drawing.Point(4, 111);
            panelQuestBoxes.Margin = new System.Windows.Forms.Padding(0);
            panelQuestBoxes.Name = "panelQuestBoxes";
            panelQuestBoxes.Size = new System.Drawing.Size(343, 404);
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
            label_ObjType.TabIndex = 0;
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
            // label_Subtype
            // 
            label_Subtype.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            label_Subtype.AutoSize = true;
            label_Subtype.Location = new System.Drawing.Point(7, 65);
            label_Subtype.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label_Subtype.Name = "label_Subtype";
            label_Subtype.Size = new System.Drawing.Size(54, 15);
            label_Subtype.TabIndex = 0;
            label_Subtype.Text = "Sub Type";
            // 
            // comboBox_Subtype
            // 
            comboBox_Subtype.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            comboBox_Subtype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBox_Subtype.Font = new System.Drawing.Font("Consolas", 9F);
            comboBox_Subtype.FormattingEnabled = true;
            comboBox_Subtype.Location = new System.Drawing.Point(7, 83);
            comboBox_Subtype.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            comboBox_Subtype.Name = "comboBox_Subtype";
            comboBox_Subtype.Size = new System.Drawing.Size(335, 22);
            comboBox_Subtype.TabIndex = 2;
            // 
            // EnemyControl
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(groupboxDetail);
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "EnemyControl";
            Size = new System.Drawing.Size(350, 518);
            groupboxDetail.ResumeLayout(false);
            groupboxDetail.PerformLayout();
            ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.GroupBox groupboxDetail;
        public System.Windows.Forms.FlowLayoutPanel panelQuestBoxes;
        private System.Windows.Forms.Label label_ObjType;
        public System.Windows.Forms.ComboBox comboBox_ObjType;
        private System.Windows.Forms.Label label_Subtype;
        public System.Windows.Forms.ComboBox comboBox_Subtype;
    }
}
