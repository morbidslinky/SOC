namespace SOC.UI
{
    partial class EmbeddedScriptalControl
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
            this.splitContainerScriptal = new System.Windows.Forms.SplitContainer();
            this.groupBoxDescription = new System.Windows.Forms.GroupBox();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.groupBoxScriptalSelect = new System.Windows.Forms.GroupBox();
            this.buttonApplyTemplate = new System.Windows.Forms.Button();
            this.labelTemplate = new System.Windows.Forms.Label();
            this.comboBoxScriptal = new System.Windows.Forms.ComboBox();
            this.groupBoxChoices = new System.Windows.Forms.GroupBox();
            this.labelChoiceDescription = new System.Windows.Forms.Label();
            this.textBoxChoiceDescription = new System.Windows.Forms.TextBox();
            this.listBoxChoices = new System.Windows.Forms.ListBox();
            this.labelChoice = new System.Windows.Forms.Label();
            this.labelChoiceSet = new System.Windows.Forms.Label();
            this.comboBoxChoiceSet = new System.Windows.Forms.ComboBox();
            this.comboBoxChoiceValue = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerScriptal)).BeginInit();
            this.splitContainerScriptal.Panel1.SuspendLayout();
            this.splitContainerScriptal.Panel2.SuspendLayout();
            this.splitContainerScriptal.SuspendLayout();
            this.groupBoxDescription.SuspendLayout();
            this.groupBoxScriptalSelect.SuspendLayout();
            this.groupBoxChoices.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerScriptal
            // 
            this.splitContainerScriptal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerScriptal.Location = new System.Drawing.Point(0, 0);
            this.splitContainerScriptal.Name = "splitContainerScriptal";
            // 
            // splitContainerScriptal.Panel1
            // 
            this.splitContainerScriptal.Panel1.Controls.Add(this.groupBoxDescription);
            this.splitContainerScriptal.Panel1.Controls.Add(this.groupBoxScriptalSelect);
            this.splitContainerScriptal.Panel1MinSize = 140;
            // 
            // splitContainerScriptal.Panel2
            // 
            this.splitContainerScriptal.Panel2.Controls.Add(this.groupBoxChoices);
            this.splitContainerScriptal.Panel2MinSize = 140;
            this.splitContainerScriptal.Size = new System.Drawing.Size(575, 431);
            this.splitContainerScriptal.SplitterDistance = 289;
            this.splitContainerScriptal.SplitterWidth = 3;
            this.splitContainerScriptal.TabIndex = 0;
            // 
            // groupBoxDescription
            // 
            this.groupBoxDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxDescription.BackColor = System.Drawing.Color.Silver;
            this.groupBoxDescription.Controls.Add(this.textBoxDescription);
            this.groupBoxDescription.Location = new System.Drawing.Point(0, 85);
            this.groupBoxDescription.Name = "groupBoxDescription";
            this.groupBoxDescription.Size = new System.Drawing.Size(287, 343);
            this.groupBoxDescription.TabIndex = 33;
            this.groupBoxDescription.TabStop = false;
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.AcceptsReturn = true;
            this.textBoxDescription.AcceptsTab = true;
            this.textBoxDescription.BackColor = System.Drawing.Color.Silver;
            this.textBoxDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxDescription.Location = new System.Drawing.Point(3, 16);
            this.textBoxDescription.Multiline = true;
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.ReadOnly = true;
            this.textBoxDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxDescription.Size = new System.Drawing.Size(281, 324);
            this.textBoxDescription.TabIndex = 6;
            // 
            // groupBoxScriptalSelect
            // 
            this.groupBoxScriptalSelect.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxScriptalSelect.BackColor = System.Drawing.Color.Silver;
            this.groupBoxScriptalSelect.Controls.Add(this.buttonApplyTemplate);
            this.groupBoxScriptalSelect.Controls.Add(this.labelTemplate);
            this.groupBoxScriptalSelect.Controls.Add(this.comboBoxScriptal);
            this.groupBoxScriptalSelect.Location = new System.Drawing.Point(0, 5);
            this.groupBoxScriptalSelect.Name = "groupBoxScriptalSelect";
            this.groupBoxScriptalSelect.Size = new System.Drawing.Size(287, 75);
            this.groupBoxScriptalSelect.TabIndex = 0;
            this.groupBoxScriptalSelect.TabStop = false;
            // 
            // buttonApplyTemplate
            // 
            this.buttonApplyTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonApplyTemplate.Location = new System.Drawing.Point(213, 39);
            this.buttonApplyTemplate.Name = "buttonApplyTemplate";
            this.buttonApplyTemplate.Size = new System.Drawing.Size(68, 23);
            this.buttonApplyTemplate.TabIndex = 2;
            this.buttonApplyTemplate.Text = "Apply >>";
            this.buttonApplyTemplate.UseVisualStyleBackColor = true;
            this.buttonApplyTemplate.Click += new System.EventHandler(this.buttonApplyTemplate_Click);
            // 
            // labelTemplate
            // 
            this.labelTemplate.AutoSize = true;
            this.labelTemplate.Location = new System.Drawing.Point(6, 24);
            this.labelTemplate.Name = "labelTemplate";
            this.labelTemplate.Size = new System.Drawing.Size(54, 13);
            this.labelTemplate.TabIndex = 1;
            this.labelTemplate.Text = "Template:";
            // 
            // comboBoxScriptal
            // 
            this.comboBoxScriptal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxScriptal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxScriptal.FormattingEnabled = true;
            this.comboBoxScriptal.Location = new System.Drawing.Point(6, 40);
            this.comboBoxScriptal.Name = "comboBoxScriptal";
            this.comboBoxScriptal.Size = new System.Drawing.Size(201, 21);
            this.comboBoxScriptal.TabIndex = 0;
            this.comboBoxScriptal.SelectedIndexChanged += new System.EventHandler(this.comboBoxScriptal_SelectedIndexChanged);
            // 
            // groupBoxChoices
            // 
            this.groupBoxChoices.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxChoices.BackColor = System.Drawing.Color.Silver;
            this.groupBoxChoices.Controls.Add(this.labelChoiceDescription);
            this.groupBoxChoices.Controls.Add(this.textBoxChoiceDescription);
            this.groupBoxChoices.Controls.Add(this.listBoxChoices);
            this.groupBoxChoices.Controls.Add(this.labelChoice);
            this.groupBoxChoices.Controls.Add(this.labelChoiceSet);
            this.groupBoxChoices.Controls.Add(this.comboBoxChoiceSet);
            this.groupBoxChoices.Controls.Add(this.comboBoxChoiceValue);
            this.groupBoxChoices.Location = new System.Drawing.Point(0, 5);
            this.groupBoxChoices.Name = "groupBoxChoices";
            this.groupBoxChoices.Size = new System.Drawing.Size(283, 423);
            this.groupBoxChoices.TabIndex = 0;
            this.groupBoxChoices.TabStop = false;
            // 
            // labelChoiceDescription
            // 
            this.labelChoiceDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelChoiceDescription.AutoSize = true;
            this.labelChoiceDescription.Location = new System.Drawing.Point(6, 256);
            this.labelChoiceDescription.Name = "labelChoiceDescription";
            this.labelChoiceDescription.Size = new System.Drawing.Size(93, 13);
            this.labelChoiceDescription.TabIndex = 7;
            this.labelChoiceDescription.Text = "Value Description:";
            // 
            // textBoxChoiceDescription
            // 
            this.textBoxChoiceDescription.AcceptsReturn = true;
            this.textBoxChoiceDescription.AcceptsTab = true;
            this.textBoxChoiceDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxChoiceDescription.BackColor = System.Drawing.Color.LightGray;
            this.textBoxChoiceDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxChoiceDescription.Location = new System.Drawing.Point(6, 272);
            this.textBoxChoiceDescription.Multiline = true;
            this.textBoxChoiceDescription.Name = "textBoxChoiceDescription";
            this.textBoxChoiceDescription.ReadOnly = true;
            this.textBoxChoiceDescription.Size = new System.Drawing.Size(270, 86);
            this.textBoxChoiceDescription.TabIndex = 6;
            // 
            // listBoxChoices
            // 
            this.listBoxChoices.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxChoices.BackColor = System.Drawing.Color.LightGray;
            this.listBoxChoices.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listBoxChoices.FormattingEnabled = true;
            this.listBoxChoices.Location = new System.Drawing.Point(6, 19);
            this.listBoxChoices.Name = "listBoxChoices";
            this.listBoxChoices.Size = new System.Drawing.Size(271, 223);
            this.listBoxChoices.TabIndex = 5;
            this.listBoxChoices.SelectedIndexChanged += new System.EventHandler(this.listBoxChoices_SelectedIndexChanged);
            // 
            // labelChoice
            // 
            this.labelChoice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelChoice.AutoSize = true;
            this.labelChoice.Location = new System.Drawing.Point(25, 394);
            this.labelChoice.Name = "labelChoice";
            this.labelChoice.Size = new System.Drawing.Size(37, 13);
            this.labelChoice.TabIndex = 4;
            this.labelChoice.Text = "Value:";
            // 
            // labelChoiceSet
            // 
            this.labelChoiceSet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelChoiceSet.AutoSize = true;
            this.labelChoiceSet.Location = new System.Drawing.Point(6, 367);
            this.labelChoiceSet.Name = "labelChoiceSet";
            this.labelChoiceSet.Size = new System.Drawing.Size(56, 13);
            this.labelChoiceSet.TabIndex = 3;
            this.labelChoiceSet.Text = "Value Set:";
            // 
            // comboBoxChoiceSet
            // 
            this.comboBoxChoiceSet.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxChoiceSet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxChoiceSet.FormattingEnabled = true;
            this.comboBoxChoiceSet.Location = new System.Drawing.Point(68, 364);
            this.comboBoxChoiceSet.Name = "comboBoxChoiceSet";
            this.comboBoxChoiceSet.Size = new System.Drawing.Size(209, 21);
            this.comboBoxChoiceSet.TabIndex = 2;
            this.comboBoxChoiceSet.SelectedIndexChanged += new System.EventHandler(this.comboBoxChoiceSet_SelectedIndexChanged);
            // 
            // comboBoxChoiceValue
            // 
            this.comboBoxChoiceValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxChoiceValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxChoiceValue.FormattingEnabled = true;
            this.comboBoxChoiceValue.Location = new System.Drawing.Point(68, 391);
            this.comboBoxChoiceValue.Name = "comboBoxChoiceValue";
            this.comboBoxChoiceValue.Size = new System.Drawing.Size(209, 21);
            this.comboBoxChoiceValue.TabIndex = 1;
            this.comboBoxChoiceValue.SelectedIndexChanged += new System.EventHandler(this.comboBoxChoiceValue_SelectedIndexChanged);
            // 
            // EmbeddedScriptalControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerScriptal);
            this.Name = "EmbeddedScriptalControl";
            this.Size = new System.Drawing.Size(575, 431);
            this.splitContainerScriptal.Panel1.ResumeLayout(false);
            this.splitContainerScriptal.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerScriptal)).EndInit();
            this.splitContainerScriptal.ResumeLayout(false);
            this.groupBoxDescription.ResumeLayout(false);
            this.groupBoxDescription.PerformLayout();
            this.groupBoxScriptalSelect.ResumeLayout(false);
            this.groupBoxScriptalSelect.PerformLayout();
            this.groupBoxChoices.ResumeLayout(false);
            this.groupBoxChoices.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainerScriptal;
        private System.Windows.Forms.GroupBox groupBoxScriptalSelect;
        private System.Windows.Forms.GroupBox groupBoxDescription;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.GroupBox groupBoxChoices;
        private System.Windows.Forms.ComboBox comboBoxChoiceSet;
        private System.Windows.Forms.ComboBox comboBoxChoiceValue;
        private System.Windows.Forms.ComboBox comboBoxScriptal;
        private System.Windows.Forms.Label labelTemplate;
        private System.Windows.Forms.Label labelChoice;
        private System.Windows.Forms.Label labelChoiceSet;
        private System.Windows.Forms.Button buttonApplyTemplate;
        private System.Windows.Forms.ListBox listBoxChoices;
        private System.Windows.Forms.TextBox textBoxChoiceDescription;
        private System.Windows.Forms.Label labelChoiceDescription;
    }
}
