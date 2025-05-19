namespace SOC.UI
{
    partial class EmbeddedScriptControl
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
            this.splitContainerOuter = new System.Windows.Forms.SplitContainer();
            this.groupBoxDescription = new System.Windows.Forms.GroupBox();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.groupBoxTrigger = new System.Windows.Forms.GroupBox();
            this.labelStrCode32 = new System.Windows.Forms.Label();
            this.comboBoxStrSenders = new System.Windows.Forms.ComboBox();
            this.comboBoxStrCodes = new System.Windows.Forms.ComboBox();
            this.labelmsg = new System.Windows.Forms.Label();
            this.comboBoxStrMsgs = new System.Windows.Forms.ComboBox();
            this.labelSenderOptions = new System.Windows.Forms.Label();
            this.splitContainerInner = new System.Windows.Forms.SplitContainer();
            this.groupBoxPreconditions = new System.Windows.Forms.GroupBox();
            this.buttonUpPrecondition = new System.Windows.Forms.Button();
            this.buttonDownPrecondition = new System.Windows.Forms.Button();
            this.listBoxPreconditions = new System.Windows.Forms.ListBox();
            this.groupBoxActions = new System.Windows.Forms.GroupBox();
            this.buttonUpOperation = new System.Windows.Forms.Button();
            this.buttonDownOperation = new System.Windows.Forms.Button();
            this.listBoxOperations = new System.Windows.Forms.ListBox();
            this.numericUpDownVarNumberValue = new System.Windows.Forms.NumericUpDown();
            this.textBoxVarStringValue = new System.Windows.Forms.TextBox();
            this.comboBoxPresetChoosables = new System.Windows.Forms.ComboBox();
            this.labelSenderValue = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerOuter)).BeginInit();
            this.splitContainerOuter.Panel1.SuspendLayout();
            this.splitContainerOuter.Panel2.SuspendLayout();
            this.splitContainerOuter.SuspendLayout();
            this.groupBoxDescription.SuspendLayout();
            this.groupBoxTrigger.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerInner)).BeginInit();
            this.splitContainerInner.Panel1.SuspendLayout();
            this.splitContainerInner.Panel2.SuspendLayout();
            this.splitContainerInner.SuspendLayout();
            this.groupBoxPreconditions.SuspendLayout();
            this.groupBoxActions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownVarNumberValue)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainerOuter
            // 
            this.splitContainerOuter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerOuter.Location = new System.Drawing.Point(0, 0);
            this.splitContainerOuter.Name = "splitContainerOuter";
            // 
            // splitContainerOuter.Panel1
            // 
            this.splitContainerOuter.Panel1.Controls.Add(this.groupBoxDescription);
            this.splitContainerOuter.Panel1.Controls.Add(this.groupBoxTrigger);
            this.splitContainerOuter.Panel1MinSize = 140;
            // 
            // splitContainerOuter.Panel2
            // 
            this.splitContainerOuter.Panel2.Controls.Add(this.splitContainerInner);
            this.splitContainerOuter.Panel2MinSize = 140;
            this.splitContainerOuter.Size = new System.Drawing.Size(575, 431);
            this.splitContainerOuter.SplitterDistance = 287;
            this.splitContainerOuter.SplitterWidth = 3;
            this.splitContainerOuter.TabIndex = 22;
            // 
            // groupBoxDescription
            // 
            this.groupBoxDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxDescription.BackColor = System.Drawing.Color.Silver;
            this.groupBoxDescription.Controls.Add(this.textBoxDescription);
            this.groupBoxDescription.Location = new System.Drawing.Point(0, 184);
            this.groupBoxDescription.Name = "groupBoxDescription";
            this.groupBoxDescription.Size = new System.Drawing.Size(285, 247);
            this.groupBoxDescription.TabIndex = 32;
            this.groupBoxDescription.TabStop = false;
            this.groupBoxDescription.Text = "Custom Description / Notes (Optional) ";
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.AcceptsReturn = true;
            this.textBoxDescription.AcceptsTab = true;
            this.textBoxDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDescription.BackColor = System.Drawing.Color.LightGray;
            this.textBoxDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxDescription.Font = new System.Drawing.Font("Consolas", 8.25F);
            this.textBoxDescription.Location = new System.Drawing.Point(3, 16);
            this.textBoxDescription.Multiline = true;
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.Size = new System.Drawing.Size(279, 225);
            this.textBoxDescription.TabIndex = 6;
            this.textBoxDescription.WordWrap = false;
            this.textBoxDescription.TextChanged += new System.EventHandler(this.textBoxDescription_TextChanged);
            // 
            // groupBoxTrigger
            // 
            this.groupBoxTrigger.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxTrigger.BackColor = System.Drawing.Color.Silver;
            this.groupBoxTrigger.Controls.Add(this.labelSenderValue);
            this.groupBoxTrigger.Controls.Add(this.labelStrCode32);
            this.groupBoxTrigger.Controls.Add(this.comboBoxStrSenders);
            this.groupBoxTrigger.Controls.Add(this.comboBoxStrCodes);
            this.groupBoxTrigger.Controls.Add(this.labelmsg);
            this.groupBoxTrigger.Controls.Add(this.comboBoxStrMsgs);
            this.groupBoxTrigger.Controls.Add(this.labelSenderOptions);
            this.groupBoxTrigger.Controls.Add(this.numericUpDownVarNumberValue);
            this.groupBoxTrigger.Controls.Add(this.textBoxVarStringValue);
            this.groupBoxTrigger.Controls.Add(this.comboBoxPresetChoosables);
            this.groupBoxTrigger.Location = new System.Drawing.Point(0, 0);
            this.groupBoxTrigger.Name = "groupBoxTrigger";
            this.groupBoxTrigger.Size = new System.Drawing.Size(285, 179);
            this.groupBoxTrigger.TabIndex = 31;
            this.groupBoxTrigger.TabStop = false;
            this.groupBoxTrigger.Text = "Trigger :: \"When\"";
            // 
            // labelStrCode32
            // 
            this.labelStrCode32.AutoSize = true;
            this.labelStrCode32.Location = new System.Drawing.Point(6, 24);
            this.labelStrCode32.Name = "labelStrCode32";
            this.labelStrCode32.Size = new System.Drawing.Size(60, 13);
            this.labelStrCode32.TabIndex = 0;
            this.labelStrCode32.Text = "StrCode32:";
            // 
            // comboBoxStrSenders
            // 
            this.comboBoxStrSenders.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxStrSenders.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStrSenders.FormattingEnabled = true;
            this.comboBoxStrSenders.Location = new System.Drawing.Point(6, 120);
            this.comboBoxStrSenders.Name = "comboBoxStrSenders";
            this.comboBoxStrSenders.Size = new System.Drawing.Size(273, 21);
            this.comboBoxStrSenders.TabIndex = 5;
            this.comboBoxStrSenders.SelectedIndexChanged += new System.EventHandler(this.comboBoxStrSenders_SelectedIndexChanged);
            // 
            // comboBoxStrCodes
            // 
            this.comboBoxStrCodes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxStrCodes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStrCodes.FormattingEnabled = true;
            this.comboBoxStrCodes.Location = new System.Drawing.Point(6, 40);
            this.comboBoxStrCodes.Name = "comboBoxStrCodes";
            this.comboBoxStrCodes.Size = new System.Drawing.Size(273, 21);
            this.comboBoxStrCodes.TabIndex = 1;
            this.comboBoxStrCodes.SelectedIndexChanged += new System.EventHandler(this.comboBoxStrCodes_SelectedIndexChanged);
            // 
            // labelmsg
            // 
            this.labelmsg.AutoSize = true;
            this.labelmsg.Location = new System.Drawing.Point(6, 64);
            this.labelmsg.Name = "labelmsg";
            this.labelmsg.Size = new System.Drawing.Size(39, 13);
            this.labelmsg.TabIndex = 2;
            this.labelmsg.Text = "\"msg\":";
            // 
            // comboBoxStrMsgs
            // 
            this.comboBoxStrMsgs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxStrMsgs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStrMsgs.FormattingEnabled = true;
            this.comboBoxStrMsgs.Location = new System.Drawing.Point(6, 80);
            this.comboBoxStrMsgs.Name = "comboBoxStrMsgs";
            this.comboBoxStrMsgs.Size = new System.Drawing.Size(273, 21);
            this.comboBoxStrMsgs.TabIndex = 3;
            this.comboBoxStrMsgs.SelectedIndexChanged += new System.EventHandler(this.comboBoxStrMsgs_SelectedIndexChanged);
            // 
            // labelSenderOptions
            // 
            this.labelSenderOptions.AutoSize = true;
            this.labelSenderOptions.Location = new System.Drawing.Point(6, 104);
            this.labelSenderOptions.Name = "labelSenderOptions";
            this.labelSenderOptions.Size = new System.Drawing.Size(86, 13);
            this.labelSenderOptions.TabIndex = 4;
            this.labelSenderOptions.Text = "\"sender\" Option:";
            // 
            // splitContainerInner
            // 
            this.splitContainerInner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerInner.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerInner.Location = new System.Drawing.Point(0, 0);
            this.splitContainerInner.Name = "splitContainerInner";
            this.splitContainerInner.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerInner.Panel1
            // 
            this.splitContainerInner.Panel1.Controls.Add(this.groupBoxPreconditions);
            // 
            // splitContainerInner.Panel2
            // 
            this.splitContainerInner.Panel2.Controls.Add(this.groupBoxActions);
            this.splitContainerInner.Size = new System.Drawing.Size(285, 431);
            this.splitContainerInner.SplitterDistance = 180;
            this.splitContainerInner.TabIndex = 1;
            // 
            // groupBoxPreconditions
            // 
            this.groupBoxPreconditions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxPreconditions.BackColor = System.Drawing.Color.Silver;
            this.groupBoxPreconditions.Controls.Add(this.buttonUpPrecondition);
            this.groupBoxPreconditions.Controls.Add(this.buttonDownPrecondition);
            this.groupBoxPreconditions.Controls.Add(this.listBoxPreconditions);
            this.groupBoxPreconditions.Location = new System.Drawing.Point(0, 0);
            this.groupBoxPreconditions.Name = "groupBoxPreconditions";
            this.groupBoxPreconditions.Size = new System.Drawing.Size(285, 179);
            this.groupBoxPreconditions.TabIndex = 2;
            this.groupBoxPreconditions.TabStop = false;
            this.groupBoxPreconditions.Text = "Preconditions :: \"If\"";
            // 
            // buttonUpPrecondition
            // 
            this.buttonUpPrecondition.Location = new System.Drawing.Point(3, 36);
            this.buttonUpPrecondition.Name = "buttonUpPrecondition";
            this.buttonUpPrecondition.Size = new System.Drawing.Size(30, 36);
            this.buttonUpPrecondition.TabIndex = 12;
            this.buttonUpPrecondition.Text = "▲";
            this.buttonUpPrecondition.UseVisualStyleBackColor = true;
            this.buttonUpPrecondition.Click += new System.EventHandler(this.buttonUpPrecondition_Click);
            // 
            // buttonDownPrecondition
            // 
            this.buttonDownPrecondition.Location = new System.Drawing.Point(3, 70);
            this.buttonDownPrecondition.Name = "buttonDownPrecondition";
            this.buttonDownPrecondition.Size = new System.Drawing.Size(30, 36);
            this.buttonDownPrecondition.TabIndex = 11;
            this.buttonDownPrecondition.Text = "▼";
            this.buttonDownPrecondition.UseVisualStyleBackColor = true;
            this.buttonDownPrecondition.Click += new System.EventHandler(this.buttonDownPrecondition_Click);
            // 
            // listBoxPreconditions
            // 
            this.listBoxPreconditions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxPreconditions.BackColor = System.Drawing.Color.LightGray;
            this.listBoxPreconditions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listBoxPreconditions.Font = new System.Drawing.Font("Consolas", 8.25F);
            this.listBoxPreconditions.FormattingEnabled = true;
            this.listBoxPreconditions.Location = new System.Drawing.Point(33, 16);
            this.listBoxPreconditions.Name = "listBoxPreconditions";
            this.listBoxPreconditions.Size = new System.Drawing.Size(249, 158);
            this.listBoxPreconditions.TabIndex = 7;
            this.listBoxPreconditions.SelectedIndexChanged += new System.EventHandler(this.listBoxPreconditions_SelectedIndexChanged);
            // 
            // groupBoxActions
            // 
            this.groupBoxActions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxActions.BackColor = System.Drawing.Color.Silver;
            this.groupBoxActions.Controls.Add(this.buttonUpOperation);
            this.groupBoxActions.Controls.Add(this.buttonDownOperation);
            this.groupBoxActions.Controls.Add(this.listBoxOperations);
            this.groupBoxActions.Location = new System.Drawing.Point(0, 0);
            this.groupBoxActions.Name = "groupBoxActions";
            this.groupBoxActions.Size = new System.Drawing.Size(285, 247);
            this.groupBoxActions.TabIndex = 1;
            this.groupBoxActions.TabStop = false;
            this.groupBoxActions.Text = "Operations :: \"Do\"";
            // 
            // buttonUpOperation
            // 
            this.buttonUpOperation.Location = new System.Drawing.Point(3, 36);
            this.buttonUpOperation.Name = "buttonUpOperation";
            this.buttonUpOperation.Size = new System.Drawing.Size(30, 36);
            this.buttonUpOperation.TabIndex = 14;
            this.buttonUpOperation.Text = "▲";
            this.buttonUpOperation.UseVisualStyleBackColor = true;
            this.buttonUpOperation.Click += new System.EventHandler(this.buttonUpOperation_Click);
            // 
            // buttonDownOperation
            // 
            this.buttonDownOperation.Location = new System.Drawing.Point(3, 70);
            this.buttonDownOperation.Name = "buttonDownOperation";
            this.buttonDownOperation.Size = new System.Drawing.Size(30, 36);
            this.buttonDownOperation.TabIndex = 13;
            this.buttonDownOperation.Text = "▼";
            this.buttonDownOperation.UseVisualStyleBackColor = true;
            this.buttonDownOperation.Click += new System.EventHandler(this.buttonDownOperation_Click);
            // 
            // listBoxOperations
            // 
            this.listBoxOperations.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxOperations.BackColor = System.Drawing.Color.LightGray;
            this.listBoxOperations.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listBoxOperations.Font = new System.Drawing.Font("Consolas", 8.25F);
            this.listBoxOperations.FormattingEnabled = true;
            this.listBoxOperations.Location = new System.Drawing.Point(33, 16);
            this.listBoxOperations.Name = "listBoxOperations";
            this.listBoxOperations.Size = new System.Drawing.Size(249, 223);
            this.listBoxOperations.TabIndex = 8;
            this.listBoxOperations.SelectedIndexChanged += new System.EventHandler(this.listBoxOperations_SelectedIndexChanged);
            // 
            // numericUpDownVarNumberValue
            // 
            this.numericUpDownVarNumberValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownVarNumberValue.Location = new System.Drawing.Point(64, 148);
            this.numericUpDownVarNumberValue.Maximum = new decimal(new int[] {
            1215752192,
            23,
            0,
            0});
            this.numericUpDownVarNumberValue.Minimum = new decimal(new int[] {
            1215752192,
            23,
            0,
            -2147483648});
            this.numericUpDownVarNumberValue.Name = "numericUpDownVarNumberValue";
            this.numericUpDownVarNumberValue.Size = new System.Drawing.Size(215, 20);
            this.numericUpDownVarNumberValue.TabIndex = 12;
            this.numericUpDownVarNumberValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownVarNumberValue.Visible = false;
            // 
            // textBoxVarStringValue
            // 
            this.textBoxVarStringValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxVarStringValue.BackColor = System.Drawing.Color.LightGray;
            this.textBoxVarStringValue.Location = new System.Drawing.Point(64, 148);
            this.textBoxVarStringValue.Name = "textBoxVarStringValue";
            this.textBoxVarStringValue.Size = new System.Drawing.Size(215, 20);
            this.textBoxVarStringValue.TabIndex = 15;
            this.textBoxVarStringValue.Visible = false;
            // 
            // comboBoxPresetChoosables
            // 
            this.comboBoxPresetChoosables.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxPresetChoosables.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxPresetChoosables.FormattingEnabled = true;
            this.comboBoxPresetChoosables.Location = new System.Drawing.Point(64, 148);
            this.comboBoxPresetChoosables.Name = "comboBoxPresetChoosables";
            this.comboBoxPresetChoosables.Size = new System.Drawing.Size(215, 21);
            this.comboBoxPresetChoosables.TabIndex = 10;
            // 
            // labelSenderValue
            // 
            this.labelSenderValue.AutoSize = true;
            this.labelSenderValue.Location = new System.Drawing.Point(6, 151);
            this.labelSenderValue.Name = "labelSenderValue";
            this.labelSenderValue.Size = new System.Drawing.Size(52, 13);
            this.labelSenderValue.TabIndex = 16;
            this.labelSenderValue.Text = "\"sender\":";
            // 
            // EmbeddedScriptControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerOuter);
            this.Name = "EmbeddedScriptControl";
            this.Size = new System.Drawing.Size(575, 431);
            this.splitContainerOuter.Panel1.ResumeLayout(false);
            this.splitContainerOuter.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerOuter)).EndInit();
            this.splitContainerOuter.ResumeLayout(false);
            this.groupBoxDescription.ResumeLayout(false);
            this.groupBoxDescription.PerformLayout();
            this.groupBoxTrigger.ResumeLayout(false);
            this.groupBoxTrigger.PerformLayout();
            this.splitContainerInner.Panel1.ResumeLayout(false);
            this.splitContainerInner.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerInner)).EndInit();
            this.splitContainerInner.ResumeLayout(false);
            this.groupBoxPreconditions.ResumeLayout(false);
            this.groupBoxActions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownVarNumberValue)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        internal System.Windows.Forms.SplitContainer splitContainerOuter;
        internal System.Windows.Forms.SplitContainer splitContainerInner;
        internal System.Windows.Forms.GroupBox groupBoxPreconditions;
        internal System.Windows.Forms.ListBox listBoxPreconditions;
        internal System.Windows.Forms.GroupBox groupBoxActions;
        internal System.Windows.Forms.ListBox listBoxOperations;
        internal System.Windows.Forms.GroupBox groupBoxDescription;
        internal System.Windows.Forms.TextBox textBoxDescription;
        internal System.Windows.Forms.GroupBox groupBoxTrigger;
        internal System.Windows.Forms.Label labelStrCode32;
        internal System.Windows.Forms.ComboBox comboBoxStrSenders;
        internal System.Windows.Forms.ComboBox comboBoxStrCodes;
        internal System.Windows.Forms.Label labelmsg;
        internal System.Windows.Forms.ComboBox comboBoxStrMsgs;
        internal System.Windows.Forms.Label labelSenderOptions;
        private System.Windows.Forms.Button buttonUpPrecondition;
        private System.Windows.Forms.Button buttonDownPrecondition;
        private System.Windows.Forms.Button buttonUpOperation;
        private System.Windows.Forms.Button buttonDownOperation;
        internal System.Windows.Forms.NumericUpDown numericUpDownVarNumberValue;
        internal System.Windows.Forms.TextBox textBoxVarStringValue;
        internal System.Windows.Forms.ComboBox comboBoxPresetChoosables;
        internal System.Windows.Forms.Label labelSenderValue;
    }
}
