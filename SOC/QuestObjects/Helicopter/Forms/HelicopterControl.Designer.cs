﻿namespace SOC.QuestObjects.Helicopter
{
    partial class HelicopterControl
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
            label_ObjType = new System.Windows.Forms.Label();
            comboBox_ObjType = new System.Windows.Forms.ComboBox();
            panelQuestBoxes = new System.Windows.Forms.FlowLayoutPanel();
            groupboxDetail.SuspendLayout();
            SuspendLayout();
            // 
            // groupboxDetail
            // 
            groupboxDetail.Controls.Add(label_ObjType);
            groupboxDetail.Controls.Add(comboBox_ObjType);
            groupboxDetail.Controls.Add(panelQuestBoxes);
            groupboxDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            groupboxDetail.Location = new System.Drawing.Point(0, 0);
            groupboxDetail.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupboxDetail.Name = "groupboxDetail";
            groupboxDetail.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupboxDetail.Size = new System.Drawing.Size(350, 518);
            groupboxDetail.TabIndex = 36;
            groupboxDetail.TabStop = false;
            groupboxDetail.Text = "Enemy Helicopter";
            // 
            // label_ObjType
            // 
            label_ObjType.AutoSize = true;
            label_ObjType.Location = new System.Drawing.Point(7, 18);
            label_ObjType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label_ObjType.Name = "label_ObjType";
            label_ObjType.Size = new System.Drawing.Size(119, 15);
            label_ObjType.TabIndex = 16;
            label_ObjType.Text = "Target Objective Type";
            // 
            // comboBox_ObjType
            // 
            comboBox_ObjType.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            comboBox_ObjType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBox_ObjType.Font = new System.Drawing.Font("Consolas", 9F);
            comboBox_ObjType.FormattingEnabled = true;
            comboBox_ObjType.Items.AddRange(new object[] { "KILLREQUIRED" });
            comboBox_ObjType.Location = new System.Drawing.Point(7, 37);
            comboBox_ObjType.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            comboBox_ObjType.Name = "comboBox_ObjType";
            comboBox_ObjType.Size = new System.Drawing.Size(335, 22);
            comboBox_ObjType.TabIndex = 1;
            // 
            // panelQuestBoxes
            // 
            panelQuestBoxes.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            panelQuestBoxes.AutoScroll = true;
            panelQuestBoxes.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            panelQuestBoxes.Location = new System.Drawing.Point(4, 65);
            panelQuestBoxes.Margin = new System.Windows.Forms.Padding(0);
            panelQuestBoxes.Name = "panelQuestBoxes";
            panelQuestBoxes.Size = new System.Drawing.Size(343, 450);
            panelQuestBoxes.TabIndex = 2;
            panelQuestBoxes.WrapContents = false;
            // 
            // HelicopterControl
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(groupboxDetail);
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "HelicopterControl";
            Size = new System.Drawing.Size(350, 518);
            groupboxDetail.ResumeLayout(false);
            groupboxDetail.PerformLayout();
            ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.GroupBox groupboxDetail;
        public System.Windows.Forms.Label label_ObjType;
        public System.Windows.Forms.ComboBox comboBox_ObjType;
        public System.Windows.Forms.FlowLayoutPanel panelQuestBoxes;
    }
}
