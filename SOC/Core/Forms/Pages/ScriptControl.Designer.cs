namespace SOC.UI
{
    partial class ScriptControl
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
            this.groupBoxVariables = new System.Windows.Forms.GroupBox();
            this.comboBoxVarType = new System.Windows.Forms.ComboBox();
            this.textBoxVarName = new System.Windows.Forms.TextBox();
            this.buttonNewVariable = new System.Windows.Forms.Button();
            this.buttonRemoveVariableIdentifier = new System.Windows.Forms.Button();
            this.treeViewVariables = new System.Windows.Forms.TreeView();
            this.comboBoxVarBooleanValue = new System.Windows.Forms.ComboBox();
            this.numericUpDownVarNumberValue = new System.Windows.Forms.NumericUpDown();
            this.buttonNewIdentifier = new System.Windows.Forms.Button();
            this.textBoxVarTextValue = new System.Windows.Forms.TextBox();
            this.groupBoxScripts = new System.Windows.Forms.GroupBox();
            this.comboBoxStrSenders = new System.Windows.Forms.ComboBox();
            this.textBoxScriptName = new System.Windows.Forms.TextBox();
            this.buttonRemoveScript = new System.Windows.Forms.Button();
            this.comboBoxStrMsgs = new System.Windows.Forms.ComboBox();
            this.comboBoxStrCodes = new System.Windows.Forms.ComboBox();
            this.buttonNewScript = new System.Windows.Forms.Button();
            this.treeViewScripts = new System.Windows.Forms.TreeView();
            this.panelScripting = new System.Windows.Forms.Panel();
            this.splitContainerOuter = new System.Windows.Forms.SplitContainer();
            this.splitContainerInner = new System.Windows.Forms.SplitContainer();
            this.groupBoxScriptDetails = new System.Windows.Forms.GroupBox();
            this.groupBoxVariables.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownVarNumberValue)).BeginInit();
            this.groupBoxScripts.SuspendLayout();
            this.panelScripting.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerOuter)).BeginInit();
            this.splitContainerOuter.Panel1.SuspendLayout();
            this.splitContainerOuter.Panel2.SuspendLayout();
            this.splitContainerOuter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerInner)).BeginInit();
            this.splitContainerInner.Panel1.SuspendLayout();
            this.splitContainerInner.Panel2.SuspendLayout();
            this.splitContainerInner.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxVariables
            // 
            this.groupBoxVariables.BackColor = System.Drawing.Color.Transparent;
            this.groupBoxVariables.Controls.Add(this.comboBoxVarType);
            this.groupBoxVariables.Controls.Add(this.textBoxVarName);
            this.groupBoxVariables.Controls.Add(this.buttonNewVariable);
            this.groupBoxVariables.Controls.Add(this.buttonRemoveVariableIdentifier);
            this.groupBoxVariables.Controls.Add(this.treeViewVariables);
            this.groupBoxVariables.Controls.Add(this.comboBoxVarBooleanValue);
            this.groupBoxVariables.Controls.Add(this.numericUpDownVarNumberValue);
            this.groupBoxVariables.Controls.Add(this.buttonNewIdentifier);
            this.groupBoxVariables.Controls.Add(this.textBoxVarTextValue);
            this.groupBoxVariables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxVariables.Location = new System.Drawing.Point(0, 0);
            this.groupBoxVariables.Name = "groupBoxVariables";
            this.groupBoxVariables.Size = new System.Drawing.Size(260, 450);
            this.groupBoxVariables.TabIndex = 0;
            this.groupBoxVariables.TabStop = false;
            this.groupBoxVariables.Text = "Custom Variables :: Type :: Initial Value";
            // 
            // comboBoxVarType
            // 
            this.comboBoxVarType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxVarType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxVarType.Enabled = false;
            this.comboBoxVarType.FormattingEnabled = true;
            this.comboBoxVarType.Items.AddRange(new object[] {
            "TEXT",
            "NUMBER",
            "BOOLEAN",
            "TABLE"});
            this.comboBoxVarType.Location = new System.Drawing.Point(136, 369);
            this.comboBoxVarType.Name = "comboBoxVarType";
            this.comboBoxVarType.Size = new System.Drawing.Size(121, 21);
            this.comboBoxVarType.TabIndex = 2;
            this.comboBoxVarType.SelectedIndexChanged += new System.EventHandler(this.comboBoxVarType_SelectedIndexChanged);
            // 
            // textBoxVarName
            // 
            this.textBoxVarName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxVarName.BackColor = System.Drawing.Color.Silver;
            this.textBoxVarName.Enabled = false;
            this.textBoxVarName.Location = new System.Drawing.Point(3, 369);
            this.textBoxVarName.Name = "textBoxVarName";
            this.textBoxVarName.Size = new System.Drawing.Size(127, 20);
            this.textBoxVarName.TabIndex = 3;
            this.textBoxVarName.TextChanged += new System.EventHandler(this.textBoxVarName_TextChanged);
            this.textBoxVarName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxName_KeyDown);
            // 
            // buttonNewVariable
            // 
            this.buttonNewVariable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonNewVariable.Location = new System.Drawing.Point(3, 340);
            this.buttonNewVariable.Name = "buttonNewVariable";
            this.buttonNewVariable.Size = new System.Drawing.Size(254, 23);
            this.buttonNewVariable.TabIndex = 1;
            this.buttonNewVariable.Text = "Add New Variable";
            this.buttonNewVariable.UseVisualStyleBackColor = true;
            this.buttonNewVariable.Click += new System.EventHandler(this.buttonNewVariable_Click);
            // 
            // buttonRemoveVariableIdentifier
            // 
            this.buttonRemoveVariableIdentifier.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRemoveVariableIdentifier.Location = new System.Drawing.Point(3, 421);
            this.buttonRemoveVariableIdentifier.Name = "buttonRemoveVariableIdentifier";
            this.buttonRemoveVariableIdentifier.Size = new System.Drawing.Size(254, 23);
            this.buttonRemoveVariableIdentifier.TabIndex = 5;
            this.buttonRemoveVariableIdentifier.Text = "Delete Selected Node";
            this.buttonRemoveVariableIdentifier.UseVisualStyleBackColor = true;
            this.buttonRemoveVariableIdentifier.Click += new System.EventHandler(this.buttonRemoveVariableIdentifier_Click);
            // 
            // treeViewVariables
            // 
            this.treeViewVariables.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeViewVariables.BackColor = System.Drawing.Color.Silver;
            this.treeViewVariables.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeViewVariables.HideSelection = false;
            this.treeViewVariables.Location = new System.Drawing.Point(3, 19);
            this.treeViewVariables.Name = "treeViewVariables";
            this.treeViewVariables.Size = new System.Drawing.Size(254, 315);
            this.treeViewVariables.TabIndex = 0;
            this.treeViewVariables.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewVariables_AfterSelect);
            // 
            // comboBoxVarBooleanValue
            // 
            this.comboBoxVarBooleanValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxVarBooleanValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxVarBooleanValue.FormattingEnabled = true;
            this.comboBoxVarBooleanValue.Items.AddRange(new object[] {
            "true",
            "false"});
            this.comboBoxVarBooleanValue.Location = new System.Drawing.Point(3, 395);
            this.comboBoxVarBooleanValue.Name = "comboBoxVarBooleanValue";
            this.comboBoxVarBooleanValue.Size = new System.Drawing.Size(254, 21);
            this.comboBoxVarBooleanValue.TabIndex = 4;
            this.comboBoxVarBooleanValue.Visible = false;
            this.comboBoxVarBooleanValue.SelectedIndexChanged += new System.EventHandler(this.comboBoxVarBooleanValue_SelectedIndexChanged);
            // 
            // numericUpDownVarNumberValue
            // 
            this.numericUpDownVarNumberValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownVarNumberValue.Location = new System.Drawing.Point(3, 395);
            this.numericUpDownVarNumberValue.Name = "numericUpDownVarNumberValue";
            this.numericUpDownVarNumberValue.Size = new System.Drawing.Size(254, 20);
            this.numericUpDownVarNumberValue.TabIndex = 4;
            this.numericUpDownVarNumberValue.Visible = false;
            this.numericUpDownVarNumberValue.ValueChanged += new System.EventHandler(this.numericUpDownVarNumberValue_ValueChanged);
            // 
            // buttonNewIdentifier
            // 
            this.buttonNewIdentifier.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonNewIdentifier.Location = new System.Drawing.Point(3, 395);
            this.buttonNewIdentifier.Name = "buttonNewIdentifier";
            this.buttonNewIdentifier.Size = new System.Drawing.Size(254, 20);
            this.buttonNewIdentifier.TabIndex = 4;
            this.buttonNewIdentifier.Text = "Add New Identifier";
            this.buttonNewIdentifier.UseVisualStyleBackColor = true;
            this.buttonNewIdentifier.Visible = false;
            this.buttonNewIdentifier.Click += new System.EventHandler(this.buttonNewIdentifier_Click);
            // 
            // textBoxVarTextValue
            // 
            this.textBoxVarTextValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxVarTextValue.BackColor = System.Drawing.Color.Silver;
            this.textBoxVarTextValue.Location = new System.Drawing.Point(3, 395);
            this.textBoxVarTextValue.Name = "textBoxVarTextValue";
            this.textBoxVarTextValue.Size = new System.Drawing.Size(254, 20);
            this.textBoxVarTextValue.TabIndex = 4;
            this.textBoxVarTextValue.Visible = false;
            this.textBoxVarTextValue.TextChanged += new System.EventHandler(this.textBoxTextVarValue_TextChanged);
            // 
            // groupBoxScripts
            // 
            this.groupBoxScripts.BackColor = System.Drawing.Color.Transparent;
            this.groupBoxScripts.Controls.Add(this.comboBoxStrSenders);
            this.groupBoxScripts.Controls.Add(this.textBoxScriptName);
            this.groupBoxScripts.Controls.Add(this.buttonRemoveScript);
            this.groupBoxScripts.Controls.Add(this.comboBoxStrMsgs);
            this.groupBoxScripts.Controls.Add(this.comboBoxStrCodes);
            this.groupBoxScripts.Controls.Add(this.buttonNewScript);
            this.groupBoxScripts.Controls.Add(this.treeViewScripts);
            this.groupBoxScripts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxScripts.Location = new System.Drawing.Point(0, 0);
            this.groupBoxScripts.Name = "groupBoxScripts";
            this.groupBoxScripts.Size = new System.Drawing.Size(311, 450);
            this.groupBoxScripts.TabIndex = 6;
            this.groupBoxScripts.TabStop = false;
            this.groupBoxScripts.Text = "Custom Scripts";
            // 
            // comboBoxStrSenders
            // 
            this.comboBoxStrSenders.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxStrSenders.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStrSenders.Enabled = false;
            this.comboBoxStrSenders.FormattingEnabled = true;
            this.comboBoxStrSenders.Location = new System.Drawing.Point(130, 395);
            this.comboBoxStrSenders.Name = "comboBoxStrSenders";
            this.comboBoxStrSenders.Size = new System.Drawing.Size(178, 21);
            this.comboBoxStrSenders.TabIndex = 7;
            this.comboBoxStrSenders.SelectedIndexChanged += new System.EventHandler(this.comboBoxStrSenders_SelectedIndexChanged);
            // 
            // textBoxScriptName
            // 
            this.textBoxScriptName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxScriptName.BackColor = System.Drawing.Color.Silver;
            this.textBoxScriptName.Enabled = false;
            this.textBoxScriptName.Location = new System.Drawing.Point(3, 369);
            this.textBoxScriptName.Name = "textBoxScriptName";
            this.textBoxScriptName.Size = new System.Drawing.Size(178, 20);
            this.textBoxScriptName.TabIndex = 6;
            this.textBoxScriptName.TextChanged += new System.EventHandler(this.textBoxScriptName_TextChanged);
            this.textBoxScriptName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxScriptName_KeyDown);
            // 
            // buttonRemoveScript
            // 
            this.buttonRemoveScript.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRemoveScript.Location = new System.Drawing.Point(3, 421);
            this.buttonRemoveScript.Name = "buttonRemoveScript";
            this.buttonRemoveScript.Size = new System.Drawing.Size(305, 23);
            this.buttonRemoveScript.TabIndex = 6;
            this.buttonRemoveScript.Text = "Delete Selected Node";
            this.buttonRemoveScript.UseVisualStyleBackColor = true;
            this.buttonRemoveScript.Click += new System.EventHandler(this.buttonRemoveScript_Click);
            // 
            // comboBoxStrMsgs
            // 
            this.comboBoxStrMsgs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBoxStrMsgs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStrMsgs.Enabled = false;
            this.comboBoxStrMsgs.FormattingEnabled = true;
            this.comboBoxStrMsgs.Location = new System.Drawing.Point(3, 395);
            this.comboBoxStrMsgs.Name = "comboBoxStrMsgs";
            this.comboBoxStrMsgs.Size = new System.Drawing.Size(121, 21);
            this.comboBoxStrMsgs.TabIndex = 3;
            this.comboBoxStrMsgs.SelectedIndexChanged += new System.EventHandler(this.comboBoxStrMsgs_SelectedIndexChanged);
            // 
            // comboBoxStrCodes
            // 
            this.comboBoxStrCodes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxStrCodes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStrCodes.Enabled = false;
            this.comboBoxStrCodes.FormattingEnabled = true;
            this.comboBoxStrCodes.Location = new System.Drawing.Point(187, 369);
            this.comboBoxStrCodes.Name = "comboBoxStrCodes";
            this.comboBoxStrCodes.Size = new System.Drawing.Size(121, 21);
            this.comboBoxStrCodes.TabIndex = 2;
            this.comboBoxStrCodes.SelectedIndexChanged += new System.EventHandler(this.comboBoxStrCodes_SelectedIndexChanged);
            // 
            // buttonNewScript
            // 
            this.buttonNewScript.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonNewScript.Location = new System.Drawing.Point(3, 340);
            this.buttonNewScript.Name = "buttonNewScript";
            this.buttonNewScript.Size = new System.Drawing.Size(305, 23);
            this.buttonNewScript.TabIndex = 1;
            this.buttonNewScript.Text = "Add New Script Event";
            this.buttonNewScript.UseVisualStyleBackColor = true;
            this.buttonNewScript.Click += new System.EventHandler(this.buttonNewScript_Click);
            // 
            // treeViewScripts
            // 
            this.treeViewScripts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeViewScripts.BackColor = System.Drawing.Color.Silver;
            this.treeViewScripts.HideSelection = false;
            this.treeViewScripts.Location = new System.Drawing.Point(3, 19);
            this.treeViewScripts.Name = "treeViewScripts";
            this.treeViewScripts.ShowRootLines = false;
            this.treeViewScripts.Size = new System.Drawing.Size(305, 315);
            this.treeViewScripts.TabIndex = 0;
            this.treeViewScripts.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewScripts_AfterSelect);
            // 
            // panelScripting
            // 
            this.panelScripting.AutoScroll = true;
            this.panelScripting.AutoSize = true;
            this.panelScripting.Controls.Add(this.splitContainerOuter);
            this.panelScripting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelScripting.Location = new System.Drawing.Point(0, 0);
            this.panelScripting.Name = "panelScripting";
            this.panelScripting.Size = new System.Drawing.Size(1160, 450);
            this.panelScripting.TabIndex = 2;
            // 
            // splitContainerOuter
            // 
            this.splitContainerOuter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerOuter.Location = new System.Drawing.Point(0, 0);
            this.splitContainerOuter.Name = "splitContainerOuter";
            // 
            // splitContainerOuter.Panel1
            // 
            this.splitContainerOuter.Panel1.Controls.Add(this.splitContainerInner);
            this.splitContainerOuter.Panel1MinSize = 512;
            // 
            // splitContainerOuter.Panel2
            // 
            this.splitContainerOuter.Panel2.Controls.Add(this.groupBoxScriptDetails);
            this.splitContainerOuter.Panel2MinSize = 250;
            this.splitContainerOuter.Size = new System.Drawing.Size(1160, 450);
            this.splitContainerOuter.SplitterDistance = 575;
            this.splitContainerOuter.TabIndex = 8;
            // 
            // splitContainerInner
            // 
            this.splitContainerInner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerInner.Location = new System.Drawing.Point(0, 0);
            this.splitContainerInner.Name = "splitContainerInner";
            // 
            // splitContainerInner.Panel1
            // 
            this.splitContainerInner.Panel1.Controls.Add(this.groupBoxVariables);
            this.splitContainerInner.Panel1MinSize = 254;
            // 
            // splitContainerInner.Panel2
            // 
            this.splitContainerInner.Panel2.Controls.Add(this.groupBoxScripts);
            this.splitContainerInner.Panel2MinSize = 254;
            this.splitContainerInner.Size = new System.Drawing.Size(575, 450);
            this.splitContainerInner.SplitterDistance = 260;
            this.splitContainerInner.TabIndex = 0;
            // 
            // groupBoxScriptDetails
            // 
            this.groupBoxScriptDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxScriptDetails.Location = new System.Drawing.Point(0, 0);
            this.groupBoxScriptDetails.Name = "groupBoxScriptDetails";
            this.groupBoxScriptDetails.Size = new System.Drawing.Size(581, 450);
            this.groupBoxScriptDetails.TabIndex = 7;
            this.groupBoxScriptDetails.TabStop = false;
            this.groupBoxScriptDetails.Text = "Script Component Details";
            // 
            // ScriptControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.panelScripting);
            this.Name = "ScriptControl";
            this.Size = new System.Drawing.Size(1160, 450);
            this.Load += new System.EventHandler(this.ScriptControl_Load);
            this.groupBoxVariables.ResumeLayout(false);
            this.groupBoxVariables.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownVarNumberValue)).EndInit();
            this.groupBoxScripts.ResumeLayout(false);
            this.groupBoxScripts.PerformLayout();
            this.panelScripting.ResumeLayout(false);
            this.splitContainerOuter.Panel1.ResumeLayout(false);
            this.splitContainerOuter.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerOuter)).EndInit();
            this.splitContainerOuter.ResumeLayout(false);
            this.splitContainerInner.Panel1.ResumeLayout(false);
            this.splitContainerInner.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerInner)).EndInit();
            this.splitContainerInner.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxVariables;
        private System.Windows.Forms.TreeView treeViewVariables;
        private System.Windows.Forms.GroupBox groupBoxScripts;
        private System.Windows.Forms.ComboBox comboBoxVarType;
        private System.Windows.Forms.TextBox textBoxVarName;
        private System.Windows.Forms.TextBox textBoxVarTextValue;
        private System.Windows.Forms.Button buttonNewVariable;
        private System.Windows.Forms.Button buttonRemoveVariableIdentifier;
        private System.Windows.Forms.Button buttonNewIdentifier;
        private System.Windows.Forms.ComboBox comboBoxVarBooleanValue;
        private System.Windows.Forms.NumericUpDown numericUpDownVarNumberValue;
        private System.Windows.Forms.Panel panelScripting;
        private System.Windows.Forms.GroupBox groupBoxScriptDetails;
        private System.Windows.Forms.TreeView treeViewScripts;
        private System.Windows.Forms.ComboBox comboBoxStrCodes;
        private System.Windows.Forms.Button buttonNewScript;
        private System.Windows.Forms.ComboBox comboBoxStrMsgs;
        private System.Windows.Forms.Button buttonRemoveScript;
        private System.Windows.Forms.TextBox textBoxScriptName;
        private System.Windows.Forms.ComboBox comboBoxStrSenders;
        private System.Windows.Forms.SplitContainer splitContainerOuter;
        private System.Windows.Forms.SplitContainer splitContainerInner;
    }
}
