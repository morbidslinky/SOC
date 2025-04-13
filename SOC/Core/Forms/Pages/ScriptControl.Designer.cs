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
            this.comboBoxType = new System.Windows.Forms.ComboBox();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.buttonNewVariable = new System.Windows.Forms.Button();
            this.buttonRemoveVariableIdentifier = new System.Windows.Forms.Button();
            this.treeViewVariables = new System.Windows.Forms.TreeView();
            this.comboBoxBooleanValue = new System.Windows.Forms.ComboBox();
            this.numericUpDownNumberValue = new System.Windows.Forms.NumericUpDown();
            this.buttonNewIdentifier = new System.Windows.Forms.Button();
            this.textBoxTextValue = new System.Windows.Forms.TextBox();
            this.groupBoxScripts = new System.Windows.Forms.GroupBox();
            this.panelScripting = new System.Windows.Forms.Panel();
            this.groupBoxVariables.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNumberValue)).BeginInit();
            this.panelScripting.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxVariables
            // 
            this.groupBoxVariables.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBoxVariables.BackColor = System.Drawing.Color.Transparent;
            this.groupBoxVariables.Controls.Add(this.comboBoxType);
            this.groupBoxVariables.Controls.Add(this.textBoxName);
            this.groupBoxVariables.Controls.Add(this.buttonNewVariable);
            this.groupBoxVariables.Controls.Add(this.buttonRemoveVariableIdentifier);
            this.groupBoxVariables.Controls.Add(this.treeViewVariables);
            this.groupBoxVariables.Controls.Add(this.comboBoxBooleanValue);
            this.groupBoxVariables.Controls.Add(this.numericUpDownNumberValue);
            this.groupBoxVariables.Controls.Add(this.buttonNewIdentifier);
            this.groupBoxVariables.Controls.Add(this.textBoxTextValue);
            this.groupBoxVariables.Location = new System.Drawing.Point(1, 3);
            this.groupBoxVariables.Name = "groupBoxVariables";
            this.groupBoxVariables.Size = new System.Drawing.Size(248, 444);
            this.groupBoxVariables.TabIndex = 0;
            this.groupBoxVariables.TabStop = false;
            this.groupBoxVariables.Text = "Custom Variables, Initial Values";
            // 
            // comboBoxType
            // 
            this.comboBoxType.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.comboBoxType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxType.Enabled = false;
            this.comboBoxType.FormattingEnabled = true;
            this.comboBoxType.Items.AddRange(new object[] {
            "TEXT",
            "NUMBER",
            "BOOLEAN",
            "TABLE"});
            this.comboBoxType.Location = new System.Drawing.Point(113, 362);
            this.comboBoxType.Name = "comboBoxType";
            this.comboBoxType.Size = new System.Drawing.Size(128, 21);
            this.comboBoxType.TabIndex = 2;
            this.comboBoxType.SelectedIndexChanged += new System.EventHandler(this.comboBoxType_SelectedIndexChanged);
            // 
            // textBoxName
            // 
            this.textBoxName.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.textBoxName.BackColor = System.Drawing.Color.Silver;
            this.textBoxName.Enabled = false;
            this.textBoxName.Location = new System.Drawing.Point(7, 363);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(100, 20);
            this.textBoxName.TabIndex = 3;
            this.textBoxName.TextChanged += new System.EventHandler(this.textBoxName_TextChanged);
            // 
            // buttonNewVariable
            // 
            this.buttonNewVariable.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonNewVariable.Location = new System.Drawing.Point(7, 334);
            this.buttonNewVariable.Name = "buttonNewVariable";
            this.buttonNewVariable.Size = new System.Drawing.Size(234, 23);
            this.buttonNewVariable.TabIndex = 1;
            this.buttonNewVariable.Text = "Add New Variable";
            this.buttonNewVariable.UseVisualStyleBackColor = true;
            this.buttonNewVariable.Click += new System.EventHandler(this.buttonNewVariable_Click);
            // 
            // buttonRemoveVariableIdentifier
            // 
            this.buttonRemoveVariableIdentifier.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonRemoveVariableIdentifier.Location = new System.Drawing.Point(7, 415);
            this.buttonRemoveVariableIdentifier.Name = "buttonRemoveVariableIdentifier";
            this.buttonRemoveVariableIdentifier.Size = new System.Drawing.Size(234, 23);
            this.buttonRemoveVariableIdentifier.TabIndex = 5;
            this.buttonRemoveVariableIdentifier.Text = "Delete Selected Node";
            this.buttonRemoveVariableIdentifier.UseVisualStyleBackColor = true;
            this.buttonRemoveVariableIdentifier.Click += new System.EventHandler(this.buttonRemoveVariableIdentifier_Click);
            // 
            // treeViewVariables
            // 
            this.treeViewVariables.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.treeViewVariables.BackColor = System.Drawing.Color.Silver;
            this.treeViewVariables.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeViewVariables.HideSelection = false;
            this.treeViewVariables.Location = new System.Drawing.Point(7, 19);
            this.treeViewVariables.Name = "treeViewVariables";
            this.treeViewVariables.Size = new System.Drawing.Size(234, 309);
            this.treeViewVariables.TabIndex = 0;
            this.treeViewVariables.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewVariables_AfterSelect);
            // 
            // comboBoxBooleanValue
            // 
            this.comboBoxBooleanValue.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.comboBoxBooleanValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxBooleanValue.FormattingEnabled = true;
            this.comboBoxBooleanValue.Items.AddRange(new object[] {
            "true",
            "false"});
            this.comboBoxBooleanValue.Location = new System.Drawing.Point(7, 389);
            this.comboBoxBooleanValue.Name = "comboBoxBooleanValue";
            this.comboBoxBooleanValue.Size = new System.Drawing.Size(234, 21);
            this.comboBoxBooleanValue.TabIndex = 4;
            this.comboBoxBooleanValue.Visible = false;
            this.comboBoxBooleanValue.SelectedIndexChanged += new System.EventHandler(this.comboBoxBooleanValue_SelectedIndexChanged);
            // 
            // numericUpDownNumberValue
            // 
            this.numericUpDownNumberValue.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.numericUpDownNumberValue.Location = new System.Drawing.Point(7, 389);
            this.numericUpDownNumberValue.Name = "numericUpDownNumberValue";
            this.numericUpDownNumberValue.Size = new System.Drawing.Size(234, 20);
            this.numericUpDownNumberValue.TabIndex = 4;
            this.numericUpDownNumberValue.Visible = false;
            this.numericUpDownNumberValue.ValueChanged += new System.EventHandler(this.numericUpDownNumberValue_ValueChanged);
            // 
            // buttonNewIdentifier
            // 
            this.buttonNewIdentifier.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonNewIdentifier.Location = new System.Drawing.Point(7, 389);
            this.buttonNewIdentifier.Name = "buttonNewIdentifier";
            this.buttonNewIdentifier.Size = new System.Drawing.Size(234, 20);
            this.buttonNewIdentifier.TabIndex = 4;
            this.buttonNewIdentifier.Text = "Add New Identifier";
            this.buttonNewIdentifier.UseVisualStyleBackColor = true;
            this.buttonNewIdentifier.Visible = false;
            this.buttonNewIdentifier.Click += new System.EventHandler(this.buttonNewIdentifier_Click);
            // 
            // textBoxTextValue
            // 
            this.textBoxTextValue.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.textBoxTextValue.BackColor = System.Drawing.Color.Silver;
            this.textBoxTextValue.Location = new System.Drawing.Point(7, 389);
            this.textBoxTextValue.Name = "textBoxTextValue";
            this.textBoxTextValue.Size = new System.Drawing.Size(234, 20);
            this.textBoxTextValue.TabIndex = 4;
            this.textBoxTextValue.Visible = false;
            this.textBoxTextValue.TextChanged += new System.EventHandler(this.textBoxTextValue_TextChanged);
            // 
            // groupBoxScripts
            // 
            this.groupBoxScripts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxScripts.BackColor = System.Drawing.Color.Transparent;
            this.groupBoxScripts.Location = new System.Drawing.Point(255, 3);
            this.groupBoxScripts.Name = "groupBoxScripts";
            this.groupBoxScripts.Size = new System.Drawing.Size(904, 444);
            this.groupBoxScripts.TabIndex = 6;
            this.groupBoxScripts.TabStop = false;
            this.groupBoxScripts.Text = "Custom Scripts";
            // 
            // panelScripting
            // 
            this.panelScripting.AutoScroll = true;
            this.panelScripting.AutoSize = true;
            this.panelScripting.Controls.Add(this.groupBoxVariables);
            this.panelScripting.Controls.Add(this.groupBoxScripts);
            this.panelScripting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelScripting.Location = new System.Drawing.Point(0, 0);
            this.panelScripting.Name = "panelScripting";
            this.panelScripting.Size = new System.Drawing.Size(1160, 450);
            this.panelScripting.TabIndex = 2;
            // 
            // ScriptControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.panelScripting);
            this.Name = "ScriptControl";
            this.Size = new System.Drawing.Size(1160, 450);
            this.groupBoxVariables.ResumeLayout(false);
            this.groupBoxVariables.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNumberValue)).EndInit();
            this.panelScripting.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxVariables;
        private System.Windows.Forms.TreeView treeViewVariables;
        private System.Windows.Forms.GroupBox groupBoxScripts;
        private System.Windows.Forms.ComboBox comboBoxType;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.TextBox textBoxTextValue;
        private System.Windows.Forms.Button buttonNewVariable;
        private System.Windows.Forms.Button buttonRemoveVariableIdentifier;
        private System.Windows.Forms.Button buttonNewIdentifier;
        private System.Windows.Forms.ComboBox comboBoxBooleanValue;
        private System.Windows.Forms.NumericUpDown numericUpDownNumberValue;
        private System.Windows.Forms.Panel panelScripting;
    }
}
