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
            this.buttonApplySender = new System.Windows.Forms.Button();
            this.labelSenderValue = new System.Windows.Forms.Label();
            this.labelStrCode32 = new System.Windows.Forms.Label();
            this.comboBoxSenderOptions = new System.Windows.Forms.ComboBox();
            this.comboBoxCode = new System.Windows.Forms.ComboBox();
            this.labelmsg = new System.Windows.Forms.Label();
            this.comboBoxMessage = new System.Windows.Forms.ComboBox();
            this.labelSenderOptions = new System.Windows.Forms.Label();
            this.textBoxSenders = new System.Windows.Forms.TextBox();
            this.comboBoxSenders = new System.Windows.Forms.ComboBox();
            this.numericUpDownSenders = new System.Windows.Forms.NumericUpDown();
            this.splitContainerInner = new System.Windows.Forms.SplitContainer();
            this.groupBoxPreconditions = new System.Windows.Forms.GroupBox();
            this.buttonUpPrecondition = new System.Windows.Forms.Button();
            this.buttonDownPrecondition = new System.Windows.Forms.Button();
            this.listBoxPreconditions = new System.Windows.Forms.ListBox();
            this.groupBoxActions = new System.Windows.Forms.GroupBox();
            this.buttonUpOperation = new System.Windows.Forms.Button();
            this.buttonDownOperation = new System.Windows.Forms.Button();
            this.listBoxOperations = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerOuter)).BeginInit();
            this.splitContainerOuter.Panel1.SuspendLayout();
            this.splitContainerOuter.Panel2.SuspendLayout();
            this.splitContainerOuter.SuspendLayout();
            this.groupBoxDescription.SuspendLayout();
            this.groupBoxTrigger.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSenders)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerInner)).BeginInit();
            this.splitContainerInner.Panel1.SuspendLayout();
            this.splitContainerInner.Panel2.SuspendLayout();
            this.splitContainerInner.SuspendLayout();
            this.groupBoxPreconditions.SuspendLayout();
            this.groupBoxActions.SuspendLayout();
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
            this.splitContainerOuter.Panel1MinSize = 210;
            // 
            // splitContainerOuter.Panel2
            // 
            this.splitContainerOuter.Panel2.Controls.Add(this.splitContainerInner);
            this.splitContainerOuter.Panel2MinSize = 210;
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
            this.groupBoxTrigger.Controls.Add(this.buttonApplySender);
            this.groupBoxTrigger.Controls.Add(this.labelSenderValue);
            this.groupBoxTrigger.Controls.Add(this.labelStrCode32);
            this.groupBoxTrigger.Controls.Add(this.comboBoxSenderOptions);
            this.groupBoxTrigger.Controls.Add(this.comboBoxCode);
            this.groupBoxTrigger.Controls.Add(this.labelmsg);
            this.groupBoxTrigger.Controls.Add(this.comboBoxMessage);
            this.groupBoxTrigger.Controls.Add(this.labelSenderOptions);
            this.groupBoxTrigger.Controls.Add(this.textBoxSenders);
            this.groupBoxTrigger.Controls.Add(this.comboBoxSenders);
            this.groupBoxTrigger.Controls.Add(this.numericUpDownSenders);
            this.groupBoxTrigger.Location = new System.Drawing.Point(0, 0);
            this.groupBoxTrigger.Name = "groupBoxTrigger";
            this.groupBoxTrigger.Size = new System.Drawing.Size(285, 179);
            this.groupBoxTrigger.TabIndex = 31;
            this.groupBoxTrigger.TabStop = false;
            this.groupBoxTrigger.Text = "Trigger :: \"When\"";
            // 
            // buttonApplySender
            // 
            this.buttonApplySender.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonApplySender.Location = new System.Drawing.Point(211, 146);
            this.buttonApplySender.Name = "buttonApplySender";
            this.buttonApplySender.Size = new System.Drawing.Size(71, 23);
            this.buttonApplySender.TabIndex = 17;
            this.buttonApplySender.Text = "Apply >>";
            this.buttonApplySender.UseVisualStyleBackColor = true;
            this.buttonApplySender.Click += new System.EventHandler(this.buttonApplySender_Click);
            // 
            // labelSenderValue
            // 
            this.labelSenderValue.AutoSize = true;
            this.labelSenderValue.Location = new System.Drawing.Point(6, 151);
            this.labelSenderValue.Name = "labelSenderValue";
            this.labelSenderValue.Size = new System.Drawing.Size(44, 13);
            this.labelSenderValue.TabIndex = 16;
            this.labelSenderValue.Text = "Sender:";
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
            // comboBoxSenderOptions
            // 
            this.comboBoxSenderOptions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxSenderOptions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSenderOptions.FormattingEnabled = true;
            this.comboBoxSenderOptions.Location = new System.Drawing.Point(6, 120);
            this.comboBoxSenderOptions.Name = "comboBoxSenderOptions";
            this.comboBoxSenderOptions.Size = new System.Drawing.Size(275, 21);
            this.comboBoxSenderOptions.TabIndex = 5;
            this.comboBoxSenderOptions.DropDown += new System.EventHandler(this.comboBoxSenderOptions_DropDown);
            this.comboBoxSenderOptions.SelectedIndexChanged += new System.EventHandler(this.comboBoxSendersOptions_SelectedIndexChanged);
            // 
            // comboBoxCode
            // 
            this.comboBoxCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCode.FormattingEnabled = true;
            this.comboBoxCode.Location = new System.Drawing.Point(6, 40);
            this.comboBoxCode.Name = "comboBoxCode";
            this.comboBoxCode.Size = new System.Drawing.Size(275, 21);
            this.comboBoxCode.TabIndex = 1;
            this.comboBoxCode.SelectedIndexChanged += new System.EventHandler(this.comboBoxCode_SelectedIndexChanged);
            // 
            // labelmsg
            // 
            this.labelmsg.AutoSize = true;
            this.labelmsg.Location = new System.Drawing.Point(6, 64);
            this.labelmsg.Name = "labelmsg";
            this.labelmsg.Size = new System.Drawing.Size(53, 13);
            this.labelmsg.TabIndex = 2;
            this.labelmsg.Text = "Message:";
            // 
            // comboBoxMessage
            // 
            this.comboBoxMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxMessage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMessage.FormattingEnabled = true;
            this.comboBoxMessage.Location = new System.Drawing.Point(6, 80);
            this.comboBoxMessage.Name = "comboBoxMessage";
            this.comboBoxMessage.Size = new System.Drawing.Size(275, 21);
            this.comboBoxMessage.TabIndex = 3;
            this.comboBoxMessage.SelectedIndexChanged += new System.EventHandler(this.comboBoxMessage_SelectedIndexChanged);
            // 
            // labelSenderOptions
            // 
            this.labelSenderOptions.AutoSize = true;
            this.labelSenderOptions.Location = new System.Drawing.Point(6, 104);
            this.labelSenderOptions.Name = "labelSenderOptions";
            this.labelSenderOptions.Size = new System.Drawing.Size(83, 13);
            this.labelSenderOptions.TabIndex = 4;
            this.labelSenderOptions.Text = "Sender Options:";
            // 
            // textBoxSenders
            // 
            this.textBoxSenders.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSenders.BackColor = System.Drawing.Color.LightGray;
            this.textBoxSenders.Location = new System.Drawing.Point(56, 148);
            this.textBoxSenders.Name = "textBoxSenders";
            this.textBoxSenders.Size = new System.Drawing.Size(149, 20);
            this.textBoxSenders.TabIndex = 15;
            this.textBoxSenders.Visible = false;
            this.textBoxSenders.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxSenders_KeyDown);
            // 
            // comboBoxSenders
            // 
            this.comboBoxSenders.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxSenders.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSenders.FormattingEnabled = true;
            this.comboBoxSenders.Location = new System.Drawing.Point(56, 148);
            this.comboBoxSenders.Name = "comboBoxSenders";
            this.comboBoxSenders.Size = new System.Drawing.Size(223, 21);
            this.comboBoxSenders.TabIndex = 10;
            this.comboBoxSenders.DropDown += new System.EventHandler(this.comboBoxSenders_DropDown);
            this.comboBoxSenders.SelectedIndexChanged += new System.EventHandler(this.comboBoxSenders_SelectedIndexChanged);
            // 
            // numericUpDownSenders
            // 
            this.numericUpDownSenders.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownSenders.Location = new System.Drawing.Point(56, 148);
            this.numericUpDownSenders.Maximum = new decimal(new int[] {
            1215752192,
            23,
            0,
            0});
            this.numericUpDownSenders.Minimum = new decimal(new int[] {
            1215752192,
            23,
            0,
            -2147483648});
            this.numericUpDownSenders.Name = "numericUpDownSenders";
            this.numericUpDownSenders.Size = new System.Drawing.Size(149, 20);
            this.numericUpDownSenders.TabIndex = 12;
            this.numericUpDownSenders.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownSenders.Visible = false;
            this.numericUpDownSenders.KeyDown += new System.Windows.Forms.KeyEventHandler(this.numericUpDownSenders_KeyDown);
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
            this.splitContainerInner.Panel1MinSize = 130;
            // 
            // splitContainerInner.Panel2
            // 
            this.splitContainerInner.Panel2.Controls.Add(this.groupBoxActions);
            this.splitContainerInner.Panel2MinSize = 130;
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
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSenders)).EndInit();
            this.splitContainerInner.Panel1.ResumeLayout(false);
            this.splitContainerInner.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerInner)).EndInit();
            this.splitContainerInner.ResumeLayout(false);
            this.groupBoxPreconditions.ResumeLayout(false);
            this.groupBoxActions.ResumeLayout(false);
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
        internal System.Windows.Forms.ComboBox comboBoxSenderOptions;
        internal System.Windows.Forms.ComboBox comboBoxCode;
        internal System.Windows.Forms.Label labelmsg;
        internal System.Windows.Forms.ComboBox comboBoxMessage;
        internal System.Windows.Forms.Label labelSenderOptions;
        private System.Windows.Forms.Button buttonUpPrecondition;
        private System.Windows.Forms.Button buttonDownPrecondition;
        private System.Windows.Forms.Button buttonUpOperation;
        private System.Windows.Forms.Button buttonDownOperation;
        internal System.Windows.Forms.NumericUpDown numericUpDownSenders;
        internal System.Windows.Forms.TextBox textBoxSenders;
        internal System.Windows.Forms.ComboBox comboBoxSenders;
        internal System.Windows.Forms.Label labelSenderValue;
        private System.Windows.Forms.Button buttonApplySender;
    }
}
