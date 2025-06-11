namespace SOC.UI
{
    partial class EmbeddedScriptSetControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        internal System.ComponentModel.IContainer components = null;

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
        internal void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EmbeddedScriptSetControl));
            buttonImportVariablesScripts = new System.Windows.Forms.Button();
            groupBoxScriptSet = new System.Windows.Forms.GroupBox();
            textEmptyHint = new System.Windows.Forms.TextBox();
            checkedListBoxScripts = new System.Windows.Forms.CheckedListBox();
            buttonExportVariablesScripts = new System.Windows.Forms.Button();
            checkBoxDependencies = new System.Windows.Forms.CheckBox();
            groupBoxScripts = new System.Windows.Forms.GroupBox();
            splitContainerOuter = new System.Windows.Forms.SplitContainer();
            groupBoxVariables = new System.Windows.Forms.GroupBox();
            checkedListBoxVariables = new System.Windows.Forms.CheckedListBox();
            panelCheckDependencies = new System.Windows.Forms.Panel();
            groupBoxScriptSet.SuspendLayout();
            groupBoxScripts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainerOuter).BeginInit();
            splitContainerOuter.Panel1.SuspendLayout();
            splitContainerOuter.Panel2.SuspendLayout();
            splitContainerOuter.SuspendLayout();
            groupBoxVariables.SuspendLayout();
            panelCheckDependencies.SuspendLayout();
            SuspendLayout();
            // 
            // buttonImportVariablesScripts
            // 
            buttonImportVariablesScripts.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            buttonImportVariablesScripts.Location = new System.Drawing.Point(0, 440);
            buttonImportVariablesScripts.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            buttonImportVariablesScripts.Name = "buttonImportVariablesScripts";
            buttonImportVariablesScripts.Size = new System.Drawing.Size(671, 24);
            buttonImportVariablesScripts.TabIndex = 3;
            buttonImportVariablesScripts.Text = "Import Variable(s) / Script(s) From Xml...";
            buttonImportVariablesScripts.UseVisualStyleBackColor = true;
            buttonImportVariablesScripts.Click += buttonLoadScript_Click;
            // 
            // groupBoxScriptSet
            // 
            groupBoxScriptSet.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            groupBoxScriptSet.BackColor = System.Drawing.Color.Silver;
            groupBoxScriptSet.Controls.Add(textEmptyHint);
            groupBoxScriptSet.Location = new System.Drawing.Point(0, 0);
            groupBoxScriptSet.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBoxScriptSet.Name = "groupBoxScriptSet";
            groupBoxScriptSet.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBoxScriptSet.Size = new System.Drawing.Size(671, 403);
            groupBoxScriptSet.TabIndex = 11;
            groupBoxScriptSet.TabStop = false;
            groupBoxScriptSet.Text = "Script Details";
            // 
            // textEmptyHint
            // 
            textEmptyHint.AcceptsReturn = true;
            textEmptyHint.AcceptsTab = true;
            textEmptyHint.BackColor = System.Drawing.Color.LightGray;
            textEmptyHint.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            textEmptyHint.Dock = System.Windows.Forms.DockStyle.Fill;
            textEmptyHint.Font = new System.Drawing.Font("Consolas", 8.5F);
            textEmptyHint.Location = new System.Drawing.Point(4, 19);
            textEmptyHint.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            textEmptyHint.Multiline = true;
            textEmptyHint.Name = "textEmptyHint";
            textEmptyHint.ReadOnly = true;
            textEmptyHint.Size = new System.Drawing.Size(663, 381);
            textEmptyHint.TabIndex = 0;
            textEmptyHint.Text = resources.GetString("textEmptyHint.Text");
            textEmptyHint.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // checkedListBoxScripts
            // 
            checkedListBoxScripts.BackColor = System.Drawing.Color.LightGray;
            checkedListBoxScripts.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            checkedListBoxScripts.CheckOnClick = true;
            checkedListBoxScripts.Dock = System.Windows.Forms.DockStyle.Fill;
            checkedListBoxScripts.Font = new System.Drawing.Font("Consolas", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            checkedListBoxScripts.FormattingEnabled = true;
            checkedListBoxScripts.Location = new System.Drawing.Point(4, 19);
            checkedListBoxScripts.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            checkedListBoxScripts.Name = "checkedListBoxScripts";
            checkedListBoxScripts.Size = new System.Drawing.Size(325, 381);
            checkedListBoxScripts.TabIndex = 0;
            checkedListBoxScripts.ItemCheck += checkedListBoxScripts_ItemCheck;
            checkedListBoxScripts.SelectedIndexChanged += checkedListBoxScripts_SelectedIndexChanged;
            // 
            // buttonExportVariablesScripts
            // 
            buttonExportVariablesScripts.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            buttonExportVariablesScripts.Location = new System.Drawing.Point(0, 470);
            buttonExportVariablesScripts.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            buttonExportVariablesScripts.Name = "buttonExportVariablesScripts";
            buttonExportVariablesScripts.Size = new System.Drawing.Size(671, 27);
            buttonExportVariablesScripts.TabIndex = 4;
            buttonExportVariablesScripts.Text = "Export ☑ Variable(s) / Script(s) To Xml...";
            buttonExportVariablesScripts.UseVisualStyleBackColor = true;
            buttonExportVariablesScripts.Click += buttonSaveScript_Click;
            // 
            // checkBoxDependencies
            // 
            checkBoxDependencies.AutoSize = true;
            checkBoxDependencies.Checked = true;
            checkBoxDependencies.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxDependencies.Dock = System.Windows.Forms.DockStyle.Right;
            checkBoxDependencies.Font = new System.Drawing.Font("Consolas", 9F);
            checkBoxDependencies.Location = new System.Drawing.Point(441, 0);
            checkBoxDependencies.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            checkBoxDependencies.Name = "checkBoxDependencies";
            checkBoxDependencies.Size = new System.Drawing.Size(227, 21);
            checkBoxDependencies.TabIndex = 2;
            checkBoxDependencies.Text = "Auto-☑ Variable Dependencies";
            checkBoxDependencies.UseVisualStyleBackColor = false;
            checkBoxDependencies.CheckedChanged += checkBoxDependencies_CheckedChanged;
            // 
            // groupBoxScripts
            // 
            groupBoxScripts.BackColor = System.Drawing.Color.Silver;
            groupBoxScripts.Controls.Add(checkedListBoxScripts);
            groupBoxScripts.Dock = System.Windows.Forms.DockStyle.Fill;
            groupBoxScripts.Location = new System.Drawing.Point(0, 0);
            groupBoxScripts.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBoxScripts.Name = "groupBoxScripts";
            groupBoxScripts.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBoxScripts.Size = new System.Drawing.Size(333, 403);
            groupBoxScripts.TabIndex = 1;
            groupBoxScripts.TabStop = false;
            groupBoxScripts.Text = "Custom Scripts";
            // 
            // splitContainerOuter
            // 
            splitContainerOuter.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            splitContainerOuter.Location = new System.Drawing.Point(0, 0);
            splitContainerOuter.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            splitContainerOuter.Name = "splitContainerOuter";
            // 
            // splitContainerOuter.Panel1
            // 
            splitContainerOuter.Panel1.Controls.Add(groupBoxVariables);
            splitContainerOuter.Panel1MinSize = 210;
            // 
            // splitContainerOuter.Panel2
            // 
            splitContainerOuter.Panel2.Controls.Add(groupBoxScripts);
            splitContainerOuter.Panel2MinSize = 210;
            splitContainerOuter.Size = new System.Drawing.Size(671, 403);
            splitContainerOuter.SplitterDistance = 334;
            splitContainerOuter.TabIndex = 14;
            // 
            // groupBoxVariables
            // 
            groupBoxVariables.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            groupBoxVariables.BackColor = System.Drawing.Color.Silver;
            groupBoxVariables.Controls.Add(checkedListBoxVariables);
            groupBoxVariables.Location = new System.Drawing.Point(0, 0);
            groupBoxVariables.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBoxVariables.Name = "groupBoxVariables";
            groupBoxVariables.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBoxVariables.Size = new System.Drawing.Size(331, 403);
            groupBoxVariables.TabIndex = 0;
            groupBoxVariables.TabStop = false;
            groupBoxVariables.Text = "Custom Variables";
            // 
            // checkedListBoxVariables
            // 
            checkedListBoxVariables.BackColor = System.Drawing.Color.LightGray;
            checkedListBoxVariables.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            checkedListBoxVariables.CheckOnClick = true;
            checkedListBoxVariables.Dock = System.Windows.Forms.DockStyle.Fill;
            checkedListBoxVariables.Font = new System.Drawing.Font("Consolas", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            checkedListBoxVariables.FormattingEnabled = true;
            checkedListBoxVariables.Location = new System.Drawing.Point(4, 19);
            checkedListBoxVariables.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            checkedListBoxVariables.Name = "checkedListBoxVariables";
            checkedListBoxVariables.Size = new System.Drawing.Size(323, 381);
            checkedListBoxVariables.TabIndex = 0;
            checkedListBoxVariables.ItemCheck += checkedListBoxVariables_ItemCheck;
            checkedListBoxVariables.SelectedIndexChanged += checkedListBoxVariables_SelectedIndexChanged;
            // 
            // panelCheckDependencies
            // 
            panelCheckDependencies.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            panelCheckDependencies.BackColor = System.Drawing.Color.Silver;
            panelCheckDependencies.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            panelCheckDependencies.Controls.Add(checkBoxDependencies);
            panelCheckDependencies.Location = new System.Drawing.Point(0, 410);
            panelCheckDependencies.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panelCheckDependencies.Name = "panelCheckDependencies";
            panelCheckDependencies.Size = new System.Drawing.Size(670, 23);
            panelCheckDependencies.TabIndex = 15;
            // 
            // EmbeddedScriptSetControl
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(splitContainerOuter);
            Controls.Add(panelCheckDependencies);
            Controls.Add(buttonImportVariablesScripts);
            Controls.Add(groupBoxScriptSet);
            Controls.Add(buttonExportVariablesScripts);
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "EmbeddedScriptSetControl";
            Size = new System.Drawing.Size(671, 497);
            groupBoxScriptSet.ResumeLayout(false);
            groupBoxScriptSet.PerformLayout();
            groupBoxScripts.ResumeLayout(false);
            splitContainerOuter.Panel1.ResumeLayout(false);
            splitContainerOuter.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerOuter).EndInit();
            splitContainerOuter.ResumeLayout(false);
            groupBoxVariables.ResumeLayout(false);
            panelCheckDependencies.ResumeLayout(false);
            panelCheckDependencies.PerformLayout();
            ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Button buttonImportVariablesScripts;
        internal System.Windows.Forms.GroupBox groupBoxScriptSet;
        internal System.Windows.Forms.Button buttonExportVariablesScripts;
        internal System.Windows.Forms.CheckedListBox checkedListBoxScripts;
        internal System.Windows.Forms.TextBox textEmptyHint;
        internal System.Windows.Forms.CheckBox checkBoxDependencies;
        internal System.Windows.Forms.GroupBox groupBoxScripts;
        internal System.Windows.Forms.SplitContainer splitContainerOuter;
        internal System.Windows.Forms.GroupBox groupBoxVariables;
        internal System.Windows.Forms.CheckedListBox checkedListBoxVariables;
        private System.Windows.Forms.Panel panelCheckDependencies;
    }
}
