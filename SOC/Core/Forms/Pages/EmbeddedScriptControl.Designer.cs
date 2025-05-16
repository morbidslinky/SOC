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
            this.labelsender = new System.Windows.Forms.Label();
            this.splitContainerInner = new System.Windows.Forms.SplitContainer();
            this.groupBoxPreconditions = new System.Windows.Forms.GroupBox();
            this.listBoxPreconditions = new System.Windows.Forms.ListBox();
            this.groupBoxActions = new System.Windows.Forms.GroupBox();
            this.listBoxOperations = new System.Windows.Forms.ListBox();
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
            this.groupBoxDescription.Location = new System.Drawing.Point(0, 165);
            this.groupBoxDescription.Name = "groupBoxDescription";
            this.groupBoxDescription.Size = new System.Drawing.Size(285, 263);
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
            this.textBoxDescription.Size = new System.Drawing.Size(279, 241);
            this.textBoxDescription.TabIndex = 6;
            this.textBoxDescription.WordWrap = false;
            this.textBoxDescription.TextChanged += new System.EventHandler(this.textBoxDescription_TextChanged);
            // 
            // groupBoxTrigger
            // 
            this.groupBoxTrigger.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxTrigger.BackColor = System.Drawing.Color.Silver;
            this.groupBoxTrigger.Controls.Add(this.labelStrCode32);
            this.groupBoxTrigger.Controls.Add(this.comboBoxStrSenders);
            this.groupBoxTrigger.Controls.Add(this.comboBoxStrCodes);
            this.groupBoxTrigger.Controls.Add(this.labelmsg);
            this.groupBoxTrigger.Controls.Add(this.comboBoxStrMsgs);
            this.groupBoxTrigger.Controls.Add(this.labelsender);
            this.groupBoxTrigger.Location = new System.Drawing.Point(0, 5);
            this.groupBoxTrigger.Name = "groupBoxTrigger";
            this.groupBoxTrigger.Size = new System.Drawing.Size(285, 155);
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
            this.labelmsg.Size = new System.Drawing.Size(29, 13);
            this.labelmsg.TabIndex = 2;
            this.labelmsg.Text = "msg:";
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
            // labelsender
            // 
            this.labelsender.AutoSize = true;
            this.labelsender.Location = new System.Drawing.Point(6, 104);
            this.labelsender.Name = "labelsender";
            this.labelsender.Size = new System.Drawing.Size(42, 13);
            this.labelsender.TabIndex = 4;
            this.labelsender.Text = "sender:";
            // 
            // splitContainerInner
            // 
            this.splitContainerInner.Dock = System.Windows.Forms.DockStyle.Fill;
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
            this.splitContainerInner.SplitterDistance = 161;
            this.splitContainerInner.TabIndex = 1;
            // 
            // groupBoxPreconditions
            // 
            this.groupBoxPreconditions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxPreconditions.BackColor = System.Drawing.Color.Silver;
            this.groupBoxPreconditions.Controls.Add(this.listBoxPreconditions);
            this.groupBoxPreconditions.Location = new System.Drawing.Point(0, 5);
            this.groupBoxPreconditions.Name = "groupBoxPreconditions";
            this.groupBoxPreconditions.Size = new System.Drawing.Size(285, 155);
            this.groupBoxPreconditions.TabIndex = 2;
            this.groupBoxPreconditions.TabStop = false;
            this.groupBoxPreconditions.Text = "Preconditions :: \"If\"";
            // 
            // listBoxPreconditions
            // 
            this.listBoxPreconditions.BackColor = System.Drawing.Color.LightGray;
            this.listBoxPreconditions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listBoxPreconditions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxPreconditions.Font = new System.Drawing.Font("Consolas", 8.25F);
            this.listBoxPreconditions.FormattingEnabled = true;
            this.listBoxPreconditions.Location = new System.Drawing.Point(3, 16);
            this.listBoxPreconditions.Name = "listBoxPreconditions";
            this.listBoxPreconditions.Size = new System.Drawing.Size(279, 136);
            this.listBoxPreconditions.TabIndex = 7;
            // 
            // groupBoxActions
            // 
            this.groupBoxActions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxActions.BackColor = System.Drawing.Color.Silver;
            this.groupBoxActions.Controls.Add(this.listBoxOperations);
            this.groupBoxActions.Location = new System.Drawing.Point(0, 0);
            this.groupBoxActions.Name = "groupBoxActions";
            this.groupBoxActions.Size = new System.Drawing.Size(285, 263);
            this.groupBoxActions.TabIndex = 1;
            this.groupBoxActions.TabStop = false;
            this.groupBoxActions.Text = "Operations :: \"Do\"";
            // 
            // listBoxOperations
            // 
            this.listBoxOperations.BackColor = System.Drawing.Color.LightGray;
            this.listBoxOperations.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listBoxOperations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxOperations.Font = new System.Drawing.Font("Consolas", 8.25F);
            this.listBoxOperations.FormattingEnabled = true;
            this.listBoxOperations.Location = new System.Drawing.Point(3, 16);
            this.listBoxOperations.Name = "listBoxOperations";
            this.listBoxOperations.Size = new System.Drawing.Size(279, 244);
            this.listBoxOperations.TabIndex = 8;
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
        internal System.Windows.Forms.Label labelsender;
    }
}
