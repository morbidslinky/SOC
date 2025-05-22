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
            this.buttonImportVariablesScripts = new System.Windows.Forms.Button();
            this.groupBoxScriptSet = new System.Windows.Forms.GroupBox();
            this.textEmptyHint = new System.Windows.Forms.TextBox();
            this.checkedListBoxScripts = new System.Windows.Forms.CheckedListBox();
            this.buttonExportVariablesScripts = new System.Windows.Forms.Button();
            this.checkBoxDependencies = new System.Windows.Forms.CheckBox();
            this.groupBoxScripts = new System.Windows.Forms.GroupBox();
            this.splitContainerOuter = new System.Windows.Forms.SplitContainer();
            this.groupBoxVariables = new System.Windows.Forms.GroupBox();
            this.checkedListBoxVariables = new System.Windows.Forms.CheckedListBox();
            this.panelCheckDependencies = new System.Windows.Forms.Panel();
            this.groupBoxScriptSet.SuspendLayout();
            this.groupBoxScripts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerOuter)).BeginInit();
            this.splitContainerOuter.Panel1.SuspendLayout();
            this.splitContainerOuter.Panel2.SuspendLayout();
            this.splitContainerOuter.SuspendLayout();
            this.groupBoxVariables.SuspendLayout();
            this.panelCheckDependencies.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonImportVariablesScripts
            // 
            this.buttonImportVariablesScripts.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonImportVariablesScripts.Location = new System.Drawing.Point(0, 382);
            this.buttonImportVariablesScripts.Name = "buttonImportVariablesScripts";
            this.buttonImportVariablesScripts.Size = new System.Drawing.Size(575, 21);
            this.buttonImportVariablesScripts.TabIndex = 12;
            this.buttonImportVariablesScripts.Text = "Import Variable(s) / Script(s) From Xml...";
            this.buttonImportVariablesScripts.UseVisualStyleBackColor = true;
            this.buttonImportVariablesScripts.Click += new System.EventHandler(this.buttonLoadScript_Click);
            // 
            // groupBoxScriptSet
            // 
            this.groupBoxScriptSet.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxScriptSet.BackColor = System.Drawing.Color.Silver;
            this.groupBoxScriptSet.Controls.Add(this.textEmptyHint);
            this.groupBoxScriptSet.Location = new System.Drawing.Point(0, 0);
            this.groupBoxScriptSet.Name = "groupBoxScriptSet";
            this.groupBoxScriptSet.Size = new System.Drawing.Size(575, 349);
            this.groupBoxScriptSet.TabIndex = 11;
            this.groupBoxScriptSet.TabStop = false;
            this.groupBoxScriptSet.Text = "Script Details";
            // 
            // textEmptyHint
            // 
            this.textEmptyHint.AcceptsReturn = true;
            this.textEmptyHint.AcceptsTab = true;
            this.textEmptyHint.BackColor = System.Drawing.Color.LightGray;
            this.textEmptyHint.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textEmptyHint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textEmptyHint.Font = new System.Drawing.Font("Consolas", 8.25F);
            this.textEmptyHint.Location = new System.Drawing.Point(3, 16);
            this.textEmptyHint.Multiline = true;
            this.textEmptyHint.Name = "textEmptyHint";
            this.textEmptyHint.ReadOnly = true;
            this.textEmptyHint.Size = new System.Drawing.Size(569, 330);
            this.textEmptyHint.TabIndex = 0;
            this.textEmptyHint.Text = resources.GetString("textEmptyHint.Text");
            this.textEmptyHint.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // checkedListBoxScripts
            // 
            this.checkedListBoxScripts.BackColor = System.Drawing.Color.LightGray;
            this.checkedListBoxScripts.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.checkedListBoxScripts.CheckOnClick = true;
            this.checkedListBoxScripts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkedListBoxScripts.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkedListBoxScripts.FormattingEnabled = true;
            this.checkedListBoxScripts.Location = new System.Drawing.Point(3, 16);
            this.checkedListBoxScripts.Name = "checkedListBoxScripts";
            this.checkedListBoxScripts.Size = new System.Drawing.Size(279, 330);
            this.checkedListBoxScripts.TabIndex = 0;
            this.checkedListBoxScripts.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBoxScripts_ItemCheck);
            this.checkedListBoxScripts.SelectedIndexChanged += new System.EventHandler(this.checkedListBoxScripts_SelectedIndexChanged);
            // 
            // buttonExportVariablesScripts
            // 
            this.buttonExportVariablesScripts.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonExportVariablesScripts.Location = new System.Drawing.Point(0, 408);
            this.buttonExportVariablesScripts.Name = "buttonExportVariablesScripts";
            this.buttonExportVariablesScripts.Size = new System.Drawing.Size(575, 23);
            this.buttonExportVariablesScripts.TabIndex = 13;
            this.buttonExportVariablesScripts.Text = "Export ☑ Variable(s) / Script(s) To Xml...";
            this.buttonExportVariablesScripts.UseVisualStyleBackColor = true;
            this.buttonExportVariablesScripts.Click += new System.EventHandler(this.buttonSaveScript_Click);
            // 
            // checkBoxDependencies
            // 
            this.checkBoxDependencies.AutoSize = true;
            this.checkBoxDependencies.Checked = true;
            this.checkBoxDependencies.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxDependencies.Dock = System.Windows.Forms.DockStyle.Right;
            this.checkBoxDependencies.Location = new System.Drawing.Point(397, 0);
            this.checkBoxDependencies.Name = "checkBoxDependencies";
            this.checkBoxDependencies.Size = new System.Drawing.Size(176, 18);
            this.checkBoxDependencies.TabIndex = 2;
            this.checkBoxDependencies.Text = "Auto-☑ Variable Dependencies";
            this.checkBoxDependencies.UseVisualStyleBackColor = false;
            this.checkBoxDependencies.CheckedChanged += new System.EventHandler(this.checkBoxDependencies_CheckedChanged);
            // 
            // groupBoxScripts
            // 
            this.groupBoxScripts.BackColor = System.Drawing.Color.Silver;
            this.groupBoxScripts.Controls.Add(this.checkedListBoxScripts);
            this.groupBoxScripts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxScripts.Location = new System.Drawing.Point(0, 0);
            this.groupBoxScripts.Name = "groupBoxScripts";
            this.groupBoxScripts.Size = new System.Drawing.Size(285, 349);
            this.groupBoxScripts.TabIndex = 2;
            this.groupBoxScripts.TabStop = false;
            this.groupBoxScripts.Text = "Custom Scripts";
            // 
            // splitContainerOuter
            // 
            this.splitContainerOuter.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainerOuter.Location = new System.Drawing.Point(0, 0);
            this.splitContainerOuter.Name = "splitContainerOuter";
            // 
            // splitContainerOuter.Panel1
            // 
            this.splitContainerOuter.Panel1.Controls.Add(this.groupBoxVariables);
            this.splitContainerOuter.Panel1MinSize = 210;
            // 
            // splitContainerOuter.Panel2
            // 
            this.splitContainerOuter.Panel2.Controls.Add(this.groupBoxScripts);
            this.splitContainerOuter.Panel2MinSize = 210;
            this.splitContainerOuter.Size = new System.Drawing.Size(575, 349);
            this.splitContainerOuter.SplitterDistance = 287;
            this.splitContainerOuter.SplitterWidth = 3;
            this.splitContainerOuter.TabIndex = 14;
            // 
            // groupBoxVariables
            // 
            this.groupBoxVariables.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxVariables.BackColor = System.Drawing.Color.Silver;
            this.groupBoxVariables.Controls.Add(this.checkedListBoxVariables);
            this.groupBoxVariables.Location = new System.Drawing.Point(0, 0);
            this.groupBoxVariables.Name = "groupBoxVariables";
            this.groupBoxVariables.Size = new System.Drawing.Size(285, 349);
            this.groupBoxVariables.TabIndex = 3;
            this.groupBoxVariables.TabStop = false;
            this.groupBoxVariables.Text = "Custom Variables";
            // 
            // checkedListBoxVariables
            // 
            this.checkedListBoxVariables.BackColor = System.Drawing.Color.LightGray;
            this.checkedListBoxVariables.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.checkedListBoxVariables.CheckOnClick = true;
            this.checkedListBoxVariables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkedListBoxVariables.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkedListBoxVariables.FormattingEnabled = true;
            this.checkedListBoxVariables.Location = new System.Drawing.Point(3, 16);
            this.checkedListBoxVariables.Name = "checkedListBoxVariables";
            this.checkedListBoxVariables.Size = new System.Drawing.Size(279, 330);
            this.checkedListBoxVariables.TabIndex = 0;
            this.checkedListBoxVariables.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBoxVariables_ItemCheck);
            this.checkedListBoxVariables.SelectedIndexChanged += new System.EventHandler(this.checkedListBoxVariables_SelectedIndexChanged);
            // 
            // panelCheckDependencies
            // 
            this.panelCheckDependencies.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelCheckDependencies.BackColor = System.Drawing.Color.Silver;
            this.panelCheckDependencies.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelCheckDependencies.Controls.Add(this.checkBoxDependencies);
            this.panelCheckDependencies.Location = new System.Drawing.Point(0, 356);
            this.panelCheckDependencies.Name = "panelCheckDependencies";
            this.panelCheckDependencies.Size = new System.Drawing.Size(575, 20);
            this.panelCheckDependencies.TabIndex = 15;
            // 
            // EmbeddedScriptSetControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerOuter);
            this.Controls.Add(this.panelCheckDependencies);
            this.Controls.Add(this.buttonImportVariablesScripts);
            this.Controls.Add(this.groupBoxScriptSet);
            this.Controls.Add(this.buttonExportVariablesScripts);
            this.Name = "EmbeddedScriptSetControl";
            this.Size = new System.Drawing.Size(575, 431);
            this.groupBoxScriptSet.ResumeLayout(false);
            this.groupBoxScriptSet.PerformLayout();
            this.groupBoxScripts.ResumeLayout(false);
            this.splitContainerOuter.Panel1.ResumeLayout(false);
            this.splitContainerOuter.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerOuter)).EndInit();
            this.splitContainerOuter.ResumeLayout(false);
            this.groupBoxVariables.ResumeLayout(false);
            this.panelCheckDependencies.ResumeLayout(false);
            this.panelCheckDependencies.PerformLayout();
            this.ResumeLayout(false);

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
