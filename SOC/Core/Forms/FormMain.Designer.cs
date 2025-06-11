namespace SOC.UI
{
    partial class FormMain
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
            buttonNext = new System.Windows.Forms.Button();
            buttonBack = new System.Windows.Forms.Button();
            panelMain = new System.Windows.Forms.Panel();
            buttonLoad = new System.Windows.Forms.Button();
            buttonSave = new System.Windows.Forms.Button();
            buttonBatchBuild = new System.Windows.Forms.Button();
            buttonOpenFolder = new System.Windows.Forms.Button();
            buttonOpenScriptTemplates = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // buttonNext
            // 
            buttonNext.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            buttonNext.Location = new System.Drawing.Point(1210, 540);
            buttonNext.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            buttonNext.Name = "buttonNext";
            buttonNext.Size = new System.Drawing.Size(127, 44);
            buttonNext.TabIndex = 8;
            buttonNext.Text = "Next >>";
            buttonNext.UseVisualStyleBackColor = true;
            buttonNext.Click += buttonNext_Click;
            // 
            // buttonBack
            // 
            buttonBack.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            buttonBack.Location = new System.Drawing.Point(1076, 540);
            buttonBack.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            buttonBack.Name = "buttonBack";
            buttonBack.Size = new System.Drawing.Size(127, 44);
            buttonBack.TabIndex = 7;
            buttonBack.Text = "<< Back";
            buttonBack.UseVisualStyleBackColor = true;
            buttonBack.Click += buttonBack_Click;
            // 
            // panelMain
            // 
            panelMain.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            panelMain.Location = new System.Drawing.Point(14, 14);
            panelMain.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panelMain.Name = "panelMain";
            panelMain.Size = new System.Drawing.Size(1323, 519);
            panelMain.TabIndex = 1;
            // 
            // buttonLoad
            // 
            buttonLoad.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            buttonLoad.Location = new System.Drawing.Point(14, 540);
            buttonLoad.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            buttonLoad.Name = "buttonLoad";
            buttonLoad.Size = new System.Drawing.Size(127, 44);
            buttonLoad.TabIndex = 2;
            buttonLoad.Text = "Load From Xml...";
            buttonLoad.UseVisualStyleBackColor = true;
            buttonLoad.Click += buttonLoad_Click;
            // 
            // buttonSave
            // 
            buttonSave.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            buttonSave.Location = new System.Drawing.Point(148, 540);
            buttonSave.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            buttonSave.Name = "buttonSave";
            buttonSave.Size = new System.Drawing.Size(127, 44);
            buttonSave.TabIndex = 3;
            buttonSave.Text = "Save To Xml...";
            buttonSave.UseVisualStyleBackColor = true;
            buttonSave.Click += buttonSave_Click;
            // 
            // buttonBatchBuild
            // 
            buttonBatchBuild.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            buttonBatchBuild.Location = new System.Drawing.Point(282, 540);
            buttonBatchBuild.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            buttonBatchBuild.Name = "buttonBatchBuild";
            buttonBatchBuild.Size = new System.Drawing.Size(127, 44);
            buttonBatchBuild.TabIndex = 4;
            buttonBatchBuild.Text = "Batch Build Xml...";
            buttonBatchBuild.UseVisualStyleBackColor = true;
            buttonBatchBuild.Click += buttonBatchBuild_Click;
            // 
            // buttonOpenFolder
            // 
            buttonOpenFolder.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            buttonOpenFolder.Location = new System.Drawing.Point(435, 540);
            buttonOpenFolder.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            buttonOpenFolder.Name = "buttonOpenFolder";
            buttonOpenFolder.Size = new System.Drawing.Size(150, 44);
            buttonOpenFolder.TabIndex = 5;
            buttonOpenFolder.Text = "Open SOC Folder";
            buttonOpenFolder.UseVisualStyleBackColor = true;
            buttonOpenFolder.Click += buttonOpenFolder_Click;
            // 
            // buttonOpenScriptTemplates
            // 
            buttonOpenScriptTemplates.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            buttonOpenScriptTemplates.Location = new System.Drawing.Point(593, 540);
            buttonOpenScriptTemplates.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            buttonOpenScriptTemplates.Name = "buttonOpenScriptTemplates";
            buttonOpenScriptTemplates.Size = new System.Drawing.Size(181, 44);
            buttonOpenScriptTemplates.TabIndex = 6;
            buttonOpenScriptTemplates.Text = "Open Script Assets Folder";
            buttonOpenScriptTemplates.UseVisualStyleBackColor = true;
            buttonOpenScriptTemplates.Click += buttonOpenScriptTemplates_Click;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.Gray;
            ClientSize = new System.Drawing.Size(1351, 591);
            Controls.Add(buttonOpenScriptTemplates);
            Controls.Add(buttonBatchBuild);
            Controls.Add(buttonOpenFolder);
            Controls.Add(buttonSave);
            Controls.Add(buttonLoad);
            Controls.Add(buttonBack);
            Controls.Add(buttonNext);
            Controls.Add(panelMain);
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            MinimumSize = new System.Drawing.Size(1351, 629);
            Name = "FormMain";
            ShowIcon = false;
            SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Sideop Companion";
            Activated += FormMain_Activated;
            FormClosing += FormMain_FormClosing;
            ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button buttonNext;
        private System.Windows.Forms.Button buttonBack;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Button buttonLoad;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonBatchBuild;
        private System.Windows.Forms.Button buttonOpenFolder;
        private System.Windows.Forms.Button buttonOpenScriptTemplates;
    }
}

