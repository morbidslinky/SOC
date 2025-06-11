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
            splitContainerOuter = new System.Windows.Forms.SplitContainer();
            groupBoxDescription = new System.Windows.Forms.GroupBox();
            textBoxDescription = new System.Windows.Forms.TextBox();
            groupBoxTrigger = new System.Windows.Forms.GroupBox();
            buttonApplyMessage = new System.Windows.Forms.Button();
            textBoxMessage = new System.Windows.Forms.TextBox();
            buttonApplySender = new System.Windows.Forms.Button();
            labelSenderValue = new System.Windows.Forms.Label();
            labelStrCode32 = new System.Windows.Forms.Label();
            comboBoxSenderOptions = new System.Windows.Forms.ComboBox();
            comboBoxCode = new System.Windows.Forms.ComboBox();
            labelmsg = new System.Windows.Forms.Label();
            comboBoxMessage = new System.Windows.Forms.ComboBox();
            labelSenderOptions = new System.Windows.Forms.Label();
            numericUpDownSenders = new System.Windows.Forms.NumericUpDown();
            textBoxSenders = new System.Windows.Forms.TextBox();
            comboBoxSenders = new System.Windows.Forms.ComboBox();
            splitContainerInner = new System.Windows.Forms.SplitContainer();
            groupBoxPreconditions = new System.Windows.Forms.GroupBox();
            buttonUpPrecondition = new System.Windows.Forms.Button();
            buttonDownPrecondition = new System.Windows.Forms.Button();
            listBoxPreconditions = new System.Windows.Forms.ListBox();
            groupBoxActions = new System.Windows.Forms.GroupBox();
            buttonUpOperation = new System.Windows.Forms.Button();
            buttonDownOperation = new System.Windows.Forms.Button();
            listBoxOperations = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)splitContainerOuter).BeginInit();
            splitContainerOuter.Panel1.SuspendLayout();
            splitContainerOuter.Panel2.SuspendLayout();
            splitContainerOuter.SuspendLayout();
            groupBoxDescription.SuspendLayout();
            groupBoxTrigger.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownSenders).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainerInner).BeginInit();
            splitContainerInner.Panel1.SuspendLayout();
            splitContainerInner.Panel2.SuspendLayout();
            splitContainerInner.SuspendLayout();
            groupBoxPreconditions.SuspendLayout();
            groupBoxActions.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainerOuter
            // 
            splitContainerOuter.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainerOuter.Location = new System.Drawing.Point(0, 0);
            splitContainerOuter.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            splitContainerOuter.Name = "splitContainerOuter";
            // 
            // splitContainerOuter.Panel1
            // 
            splitContainerOuter.Panel1.Controls.Add(groupBoxDescription);
            splitContainerOuter.Panel1.Controls.Add(groupBoxTrigger);
            splitContainerOuter.Panel1MinSize = 210;
            // 
            // splitContainerOuter.Panel2
            // 
            splitContainerOuter.Panel2.Controls.Add(splitContainerInner);
            splitContainerOuter.Panel2MinSize = 210;
            splitContainerOuter.Size = new System.Drawing.Size(671, 497);
            splitContainerOuter.SplitterDistance = 334;
            splitContainerOuter.TabIndex = 22;
            // 
            // groupBoxDescription
            // 
            groupBoxDescription.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            groupBoxDescription.BackColor = System.Drawing.Color.Silver;
            groupBoxDescription.Controls.Add(textBoxDescription);
            groupBoxDescription.Location = new System.Drawing.Point(0, 212);
            groupBoxDescription.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBoxDescription.Name = "groupBoxDescription";
            groupBoxDescription.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBoxDescription.Size = new System.Drawing.Size(331, 283);
            groupBoxDescription.TabIndex = 5;
            groupBoxDescription.TabStop = false;
            groupBoxDescription.Text = "Custom Description / Notes (Optional) ";
            // 
            // textBoxDescription
            // 
            textBoxDescription.AcceptsReturn = true;
            textBoxDescription.AcceptsTab = true;
            textBoxDescription.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            textBoxDescription.BackColor = System.Drawing.Color.LightGray;
            textBoxDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            textBoxDescription.Font = new System.Drawing.Font("Consolas", 8.5F);
            textBoxDescription.Location = new System.Drawing.Point(4, 18);
            textBoxDescription.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            textBoxDescription.Multiline = true;
            textBoxDescription.Name = "textBoxDescription";
            textBoxDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            textBoxDescription.Size = new System.Drawing.Size(324, 257);
            textBoxDescription.TabIndex = 21;
            textBoxDescription.TextChanged += textBoxDescription_TextChanged;
            // 
            // groupBoxTrigger
            // 
            groupBoxTrigger.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            groupBoxTrigger.BackColor = System.Drawing.Color.Silver;
            groupBoxTrigger.Controls.Add(buttonApplyMessage);
            groupBoxTrigger.Controls.Add(textBoxMessage);
            groupBoxTrigger.Controls.Add(buttonApplySender);
            groupBoxTrigger.Controls.Add(labelSenderValue);
            groupBoxTrigger.Controls.Add(labelStrCode32);
            groupBoxTrigger.Controls.Add(comboBoxSenderOptions);
            groupBoxTrigger.Controls.Add(comboBoxCode);
            groupBoxTrigger.Controls.Add(labelmsg);
            groupBoxTrigger.Controls.Add(comboBoxMessage);
            groupBoxTrigger.Controls.Add(labelSenderOptions);
            groupBoxTrigger.Controls.Add(numericUpDownSenders);
            groupBoxTrigger.Controls.Add(textBoxSenders);
            groupBoxTrigger.Controls.Add(comboBoxSenders);
            groupBoxTrigger.Location = new System.Drawing.Point(0, 0);
            groupBoxTrigger.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBoxTrigger.Name = "groupBoxTrigger";
            groupBoxTrigger.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBoxTrigger.Size = new System.Drawing.Size(331, 207);
            groupBoxTrigger.TabIndex = 31;
            groupBoxTrigger.TabStop = false;
            groupBoxTrigger.Text = "Trigger :: \"When\"";
            // 
            // buttonApplyMessage
            // 
            buttonApplyMessage.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            buttonApplyMessage.Location = new System.Drawing.Point(245, 90);
            buttonApplyMessage.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            buttonApplyMessage.Name = "buttonApplyMessage";
            buttonApplyMessage.Size = new System.Drawing.Size(83, 27);
            buttonApplyMessage.TabIndex = 18;
            buttonApplyMessage.Text = "Apply >>";
            buttonApplyMessage.UseVisualStyleBackColor = true;
            buttonApplyMessage.Click += buttonApplyMessage_Click;
            // 
            // textBoxMessage
            // 
            textBoxMessage.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            textBoxMessage.BackColor = System.Drawing.Color.LightGray;
            textBoxMessage.Font = new System.Drawing.Font("Consolas", 9F);
            textBoxMessage.Location = new System.Drawing.Point(7, 92);
            textBoxMessage.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            textBoxMessage.Name = "textBoxMessage";
            textBoxMessage.Size = new System.Drawing.Size(230, 22);
            textBoxMessage.TabIndex = 17;
            textBoxMessage.Visible = false;
            // 
            // buttonApplySender
            // 
            buttonApplySender.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            buttonApplySender.Location = new System.Drawing.Point(245, 168);
            buttonApplySender.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            buttonApplySender.Name = "buttonApplySender";
            buttonApplySender.Size = new System.Drawing.Size(83, 27);
            buttonApplySender.TabIndex = 4;
            buttonApplySender.Text = "Apply >>";
            buttonApplySender.UseVisualStyleBackColor = true;
            buttonApplySender.Click += buttonApplySender_Click;
            // 
            // labelSenderValue
            // 
            labelSenderValue.AutoSize = true;
            labelSenderValue.Location = new System.Drawing.Point(7, 174);
            labelSenderValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            labelSenderValue.Name = "labelSenderValue";
            labelSenderValue.Size = new System.Drawing.Size(46, 15);
            labelSenderValue.TabIndex = 16;
            labelSenderValue.Text = "Sender:";
            // 
            // labelStrCode32
            // 
            labelStrCode32.AutoSize = true;
            labelStrCode32.Location = new System.Drawing.Point(7, 28);
            labelStrCode32.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            labelStrCode32.Name = "labelStrCode32";
            labelStrCode32.Size = new System.Drawing.Size(64, 15);
            labelStrCode32.TabIndex = 0;
            labelStrCode32.Text = "StrCode32:";
            // 
            // comboBoxSenderOptions
            // 
            comboBoxSenderOptions.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            comboBoxSenderOptions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxSenderOptions.Font = new System.Drawing.Font("Consolas", 9F);
            comboBoxSenderOptions.FormattingEnabled = true;
            comboBoxSenderOptions.Location = new System.Drawing.Point(7, 138);
            comboBoxSenderOptions.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            comboBoxSenderOptions.Name = "comboBoxSenderOptions";
            comboBoxSenderOptions.Size = new System.Drawing.Size(319, 22);
            comboBoxSenderOptions.TabIndex = 2;
            comboBoxSenderOptions.DropDown += comboBoxSenderOptions_DropDown;
            comboBoxSenderOptions.SelectedIndexChanged += comboBoxSendersOptions_SelectedIndexChanged;
            // 
            // comboBoxCode
            // 
            comboBoxCode.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            comboBoxCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxCode.Font = new System.Drawing.Font("Consolas", 9F);
            comboBoxCode.FormattingEnabled = true;
            comboBoxCode.Location = new System.Drawing.Point(7, 46);
            comboBoxCode.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            comboBoxCode.Name = "comboBoxCode";
            comboBoxCode.Size = new System.Drawing.Size(319, 22);
            comboBoxCode.TabIndex = 0;
            comboBoxCode.SelectedIndexChanged += comboBoxCode_SelectedIndexChanged;
            // 
            // labelmsg
            // 
            labelmsg.AutoSize = true;
            labelmsg.Location = new System.Drawing.Point(7, 74);
            labelmsg.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            labelmsg.Name = "labelmsg";
            labelmsg.Size = new System.Drawing.Size(56, 15);
            labelmsg.TabIndex = 2;
            labelmsg.Text = "Message:";
            // 
            // comboBoxMessage
            // 
            comboBoxMessage.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            comboBoxMessage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxMessage.Font = new System.Drawing.Font("Consolas", 9F);
            comboBoxMessage.FormattingEnabled = true;
            comboBoxMessage.Location = new System.Drawing.Point(7, 92);
            comboBoxMessage.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            comboBoxMessage.Name = "comboBoxMessage";
            comboBoxMessage.Size = new System.Drawing.Size(319, 22);
            comboBoxMessage.TabIndex = 1;
            comboBoxMessage.SelectedIndexChanged += comboBoxMessage_SelectedIndexChanged;
            // 
            // labelSenderOptions
            // 
            labelSenderOptions.AutoSize = true;
            labelSenderOptions.Location = new System.Drawing.Point(7, 120);
            labelSenderOptions.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            labelSenderOptions.Name = "labelSenderOptions";
            labelSenderOptions.Size = new System.Drawing.Size(91, 15);
            labelSenderOptions.TabIndex = 4;
            labelSenderOptions.Text = "Sender Options:";
            // 
            // numericUpDownSenders
            // 
            numericUpDownSenders.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            numericUpDownSenders.Font = new System.Drawing.Font("Consolas", 9F);
            numericUpDownSenders.Location = new System.Drawing.Point(65, 171);
            numericUpDownSenders.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            numericUpDownSenders.Maximum = new decimal(new int[] { 1215752192, 23, 0, 0 });
            numericUpDownSenders.Minimum = new decimal(new int[] { 1215752192, 23, 0, int.MinValue });
            numericUpDownSenders.Name = "numericUpDownSenders";
            numericUpDownSenders.Size = new System.Drawing.Size(173, 22);
            numericUpDownSenders.TabIndex = 3;
            numericUpDownSenders.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            numericUpDownSenders.Visible = false;
            numericUpDownSenders.KeyDown += numericUpDownSenders_KeyDown;
            // 
            // textBoxSenders
            // 
            textBoxSenders.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            textBoxSenders.BackColor = System.Drawing.Color.LightGray;
            textBoxSenders.Font = new System.Drawing.Font("Consolas", 9F);
            textBoxSenders.Location = new System.Drawing.Point(65, 171);
            textBoxSenders.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            textBoxSenders.Name = "textBoxSenders";
            textBoxSenders.Size = new System.Drawing.Size(172, 22);
            textBoxSenders.TabIndex = 3;
            textBoxSenders.Visible = false;
            textBoxSenders.KeyDown += textBoxSenders_KeyDown;
            // 
            // comboBoxSenders
            // 
            comboBoxSenders.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            comboBoxSenders.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxSenders.Font = new System.Drawing.Font("Consolas", 9F);
            comboBoxSenders.FormattingEnabled = true;
            comboBoxSenders.Location = new System.Drawing.Point(65, 171);
            comboBoxSenders.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            comboBoxSenders.Name = "comboBoxSenders";
            comboBoxSenders.Size = new System.Drawing.Size(258, 22);
            comboBoxSenders.TabIndex = 3;
            comboBoxSenders.DropDown += comboBoxSenders_DropDown;
            comboBoxSenders.SelectedIndexChanged += comboBoxSenders_SelectedIndexChanged;
            // 
            // splitContainerInner
            // 
            splitContainerInner.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainerInner.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            splitContainerInner.Location = new System.Drawing.Point(0, 0);
            splitContainerInner.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            splitContainerInner.Name = "splitContainerInner";
            splitContainerInner.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerInner.Panel1
            // 
            splitContainerInner.Panel1.Controls.Add(groupBoxPreconditions);
            splitContainerInner.Panel1MinSize = 130;
            // 
            // splitContainerInner.Panel2
            // 
            splitContainerInner.Panel2.Controls.Add(groupBoxActions);
            splitContainerInner.Panel2MinSize = 130;
            splitContainerInner.Size = new System.Drawing.Size(333, 497);
            splitContainerInner.SplitterDistance = 208;
            splitContainerInner.SplitterWidth = 5;
            splitContainerInner.TabIndex = 1;
            // 
            // groupBoxPreconditions
            // 
            groupBoxPreconditions.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            groupBoxPreconditions.BackColor = System.Drawing.Color.Silver;
            groupBoxPreconditions.Controls.Add(buttonUpPrecondition);
            groupBoxPreconditions.Controls.Add(buttonDownPrecondition);
            groupBoxPreconditions.Controls.Add(listBoxPreconditions);
            groupBoxPreconditions.Location = new System.Drawing.Point(0, 0);
            groupBoxPreconditions.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBoxPreconditions.Name = "groupBoxPreconditions";
            groupBoxPreconditions.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBoxPreconditions.Size = new System.Drawing.Size(333, 207);
            groupBoxPreconditions.TabIndex = 6;
            groupBoxPreconditions.TabStop = false;
            groupBoxPreconditions.Text = "Preconditions :: \"If\"";
            // 
            // buttonUpPrecondition
            // 
            buttonUpPrecondition.Location = new System.Drawing.Point(3, 42);
            buttonUpPrecondition.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            buttonUpPrecondition.Name = "buttonUpPrecondition";
            buttonUpPrecondition.Size = new System.Drawing.Size(35, 42);
            buttonUpPrecondition.TabIndex = 7;
            buttonUpPrecondition.Text = "▲";
            buttonUpPrecondition.UseVisualStyleBackColor = true;
            buttonUpPrecondition.Click += buttonUpPrecondition_Click;
            // 
            // buttonDownPrecondition
            // 
            buttonDownPrecondition.Location = new System.Drawing.Point(3, 81);
            buttonDownPrecondition.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            buttonDownPrecondition.Name = "buttonDownPrecondition";
            buttonDownPrecondition.Size = new System.Drawing.Size(35, 42);
            buttonDownPrecondition.TabIndex = 8;
            buttonDownPrecondition.Text = "▼";
            buttonDownPrecondition.UseVisualStyleBackColor = true;
            buttonDownPrecondition.Click += buttonDownPrecondition_Click;
            // 
            // listBoxPreconditions
            // 
            listBoxPreconditions.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            listBoxPreconditions.BackColor = System.Drawing.Color.LightGray;
            listBoxPreconditions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            listBoxPreconditions.Font = new System.Drawing.Font("Consolas", 8.5F);
            listBoxPreconditions.FormattingEnabled = true;
            listBoxPreconditions.ItemHeight = 13;
            listBoxPreconditions.Location = new System.Drawing.Point(38, 18);
            listBoxPreconditions.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            listBoxPreconditions.Name = "listBoxPreconditions";
            listBoxPreconditions.Size = new System.Drawing.Size(291, 184);
            listBoxPreconditions.TabIndex = 9;
            listBoxPreconditions.SelectedIndexChanged += listBoxPreconditions_SelectedIndexChanged;
            listBoxPreconditions.DoubleClick += listBoxPreconditions_DoubleClick;
            // 
            // groupBoxActions
            // 
            groupBoxActions.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            groupBoxActions.BackColor = System.Drawing.Color.Silver;
            groupBoxActions.Controls.Add(buttonUpOperation);
            groupBoxActions.Controls.Add(buttonDownOperation);
            groupBoxActions.Controls.Add(listBoxOperations);
            groupBoxActions.Location = new System.Drawing.Point(0, 0);
            groupBoxActions.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBoxActions.Name = "groupBoxActions";
            groupBoxActions.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBoxActions.Size = new System.Drawing.Size(333, 284);
            groupBoxActions.TabIndex = 10;
            groupBoxActions.TabStop = false;
            groupBoxActions.Text = "Operations :: \"Do\"";
            // 
            // buttonUpOperation
            // 
            buttonUpOperation.Location = new System.Drawing.Point(3, 42);
            buttonUpOperation.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            buttonUpOperation.Name = "buttonUpOperation";
            buttonUpOperation.Size = new System.Drawing.Size(35, 42);
            buttonUpOperation.TabIndex = 11;
            buttonUpOperation.Text = "▲";
            buttonUpOperation.UseVisualStyleBackColor = true;
            buttonUpOperation.Click += buttonUpOperation_Click;
            // 
            // buttonDownOperation
            // 
            buttonDownOperation.Location = new System.Drawing.Point(3, 81);
            buttonDownOperation.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            buttonDownOperation.Name = "buttonDownOperation";
            buttonDownOperation.Size = new System.Drawing.Size(35, 42);
            buttonDownOperation.TabIndex = 12;
            buttonDownOperation.Text = "▼";
            buttonDownOperation.UseVisualStyleBackColor = true;
            buttonDownOperation.Click += buttonDownOperation_Click;
            // 
            // listBoxOperations
            // 
            listBoxOperations.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            listBoxOperations.BackColor = System.Drawing.Color.LightGray;
            listBoxOperations.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            listBoxOperations.Font = new System.Drawing.Font("Consolas", 8.5F);
            listBoxOperations.FormattingEnabled = true;
            listBoxOperations.ItemHeight = 13;
            listBoxOperations.Location = new System.Drawing.Point(38, 18);
            listBoxOperations.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            listBoxOperations.Name = "listBoxOperations";
            listBoxOperations.Size = new System.Drawing.Size(291, 249);
            listBoxOperations.TabIndex = 13;
            listBoxOperations.SelectedIndexChanged += listBoxOperations_SelectedIndexChanged;
            listBoxOperations.DoubleClick += listBoxOperations_DoubleClick;
            // 
            // EmbeddedScriptControl
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(splitContainerOuter);
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "EmbeddedScriptControl";
            Size = new System.Drawing.Size(671, 497);
            splitContainerOuter.Panel1.ResumeLayout(false);
            splitContainerOuter.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerOuter).EndInit();
            splitContainerOuter.ResumeLayout(false);
            groupBoxDescription.ResumeLayout(false);
            groupBoxDescription.PerformLayout();
            groupBoxTrigger.ResumeLayout(false);
            groupBoxTrigger.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownSenders).EndInit();
            splitContainerInner.Panel1.ResumeLayout(false);
            splitContainerInner.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerInner).EndInit();
            splitContainerInner.ResumeLayout(false);
            groupBoxPreconditions.ResumeLayout(false);
            groupBoxActions.ResumeLayout(false);
            ResumeLayout(false);

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
        private System.Windows.Forms.Button buttonApplyMessage;
        internal System.Windows.Forms.TextBox textBoxMessage;
    }
}
