namespace SOC.UI
{
    partial class ScriptControl
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
            groupBoxVariables = new System.Windows.Forms.GroupBox();
            comboBoxVarType = new System.Windows.Forms.ComboBox();
            textBoxVarName = new System.Windows.Forms.TextBox();
            buttonNewVariable = new System.Windows.Forms.Button();
            buttonRemoveVariableIdentifier = new System.Windows.Forms.Button();
            treeViewVariables = new DoubleBufferedTreeView();
            panelBoolean = new System.Windows.Forms.Panel();
            radioButtonFalse = new System.Windows.Forms.RadioButton();
            radioButtonTrue = new System.Windows.Forms.RadioButton();
            textBoxVarNumberValue = new System.Windows.Forms.TextBox();
            textBoxVarStringValue = new System.Windows.Forms.TextBox();
            panelPlaceholder = new System.Windows.Forms.Panel();
            panelNewIdentifier = new System.Windows.Forms.Panel();
            comboBoxTableAddOptions = new System.Windows.Forms.ComboBox();
            buttonNewIdentifier = new System.Windows.Forms.Button();
            groupBoxScripts = new System.Windows.Forms.GroupBox();
            splitContainerPreconditionOperation = new System.Windows.Forms.SplitContainer();
            buttonNewPrecondition = new System.Windows.Forms.Button();
            buttonNewOperation = new System.Windows.Forms.Button();
            textBoxScriptName = new System.Windows.Forms.TextBox();
            buttonRemoveScript = new System.Windows.Forms.Button();
            buttonNewScript = new System.Windows.Forms.Button();
            treeViewScripts = new DoubleBufferedTreeView();
            panelScripting = new System.Windows.Forms.Panel();
            splitContainerOuter = new System.Windows.Forms.SplitContainer();
            splitContainerInner = new System.Windows.Forms.SplitContainer();
            groupBoxScriptDetails = new System.Windows.Forms.GroupBox();
            panelComponentDetails = new System.Windows.Forms.Panel();
            groupBoxVariables.SuspendLayout();
            panelBoolean.SuspendLayout();
            panelNewIdentifier.SuspendLayout();
            groupBoxScripts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainerPreconditionOperation).BeginInit();
            splitContainerPreconditionOperation.Panel1.SuspendLayout();
            splitContainerPreconditionOperation.Panel2.SuspendLayout();
            splitContainerPreconditionOperation.SuspendLayout();
            panelScripting.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainerOuter).BeginInit();
            splitContainerOuter.Panel1.SuspendLayout();
            splitContainerOuter.Panel2.SuspendLayout();
            splitContainerOuter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainerInner).BeginInit();
            splitContainerInner.Panel1.SuspendLayout();
            splitContainerInner.Panel2.SuspendLayout();
            splitContainerInner.SuspendLayout();
            groupBoxScriptDetails.SuspendLayout();
            SuspendLayout();
            // 
            // groupBoxVariables
            // 
            groupBoxVariables.BackColor = System.Drawing.Color.Transparent;
            groupBoxVariables.Controls.Add(comboBoxVarType);
            groupBoxVariables.Controls.Add(textBoxVarName);
            groupBoxVariables.Controls.Add(buttonNewVariable);
            groupBoxVariables.Controls.Add(buttonRemoveVariableIdentifier);
            groupBoxVariables.Controls.Add(treeViewVariables);
            groupBoxVariables.Controls.Add(panelBoolean);
            groupBoxVariables.Controls.Add(textBoxVarNumberValue);
            groupBoxVariables.Controls.Add(textBoxVarStringValue);
            groupBoxVariables.Controls.Add(panelPlaceholder);
            groupBoxVariables.Controls.Add(panelNewIdentifier);
            groupBoxVariables.Dock = System.Windows.Forms.DockStyle.Fill;
            groupBoxVariables.Location = new System.Drawing.Point(0, 0);
            groupBoxVariables.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBoxVariables.Name = "groupBoxVariables";
            groupBoxVariables.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBoxVariables.Size = new System.Drawing.Size(302, 519);
            groupBoxVariables.TabIndex = 0;
            groupBoxVariables.TabStop = false;
            groupBoxVariables.Text = "Custom Variables";
            // 
            // comboBoxVarType
            // 
            comboBoxVarType.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            comboBoxVarType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxVarType.Enabled = false;
            comboBoxVarType.Font = new System.Drawing.Font("Consolas", 9F);
            comboBoxVarType.FormattingEnabled = true;
            comboBoxVarType.Items.AddRange(new object[] { "STRING", "NUMBER", "BOOLEAN", "TABLE" });
            comboBoxVarType.Location = new System.Drawing.Point(158, 426);
            comboBoxVarType.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            comboBoxVarType.Name = "comboBoxVarType";
            comboBoxVarType.Size = new System.Drawing.Size(140, 22);
            comboBoxVarType.TabIndex = 3;
            comboBoxVarType.SelectedIndexChanged += comboBoxVarType_SelectedIndexChanged;
            // 
            // textBoxVarName
            // 
            textBoxVarName.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            textBoxVarName.BackColor = System.Drawing.Color.Silver;
            textBoxVarName.Enabled = false;
            textBoxVarName.Font = new System.Drawing.Font("Consolas", 9F);
            textBoxVarName.Location = new System.Drawing.Point(4, 426);
            textBoxVarName.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            textBoxVarName.Name = "textBoxVarName";
            textBoxVarName.Size = new System.Drawing.Size(147, 22);
            textBoxVarName.TabIndex = 2;
            textBoxVarName.TextChanged += textBoxVarName_TextChanged;
            textBoxVarName.KeyDown += textBoxName_KeyDown;
            // 
            // buttonNewVariable
            // 
            buttonNewVariable.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            buttonNewVariable.Location = new System.Drawing.Point(4, 392);
            buttonNewVariable.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            buttonNewVariable.Name = "buttonNewVariable";
            buttonNewVariable.Size = new System.Drawing.Size(295, 27);
            buttonNewVariable.TabIndex = 1;
            buttonNewVariable.Text = "Add New Variable";
            buttonNewVariable.UseVisualStyleBackColor = true;
            buttonNewVariable.Click += buttonNewVariable_Click;
            // 
            // buttonRemoveVariableIdentifier
            // 
            buttonRemoveVariableIdentifier.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            buttonRemoveVariableIdentifier.Enabled = false;
            buttonRemoveVariableIdentifier.Location = new System.Drawing.Point(4, 486);
            buttonRemoveVariableIdentifier.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            buttonRemoveVariableIdentifier.Name = "buttonRemoveVariableIdentifier";
            buttonRemoveVariableIdentifier.Size = new System.Drawing.Size(295, 27);
            buttonRemoveVariableIdentifier.TabIndex = 5;
            buttonRemoveVariableIdentifier.Text = "Delete Selected Node";
            buttonRemoveVariableIdentifier.UseVisualStyleBackColor = true;
            buttonRemoveVariableIdentifier.Click += buttonRemoveVariableIdentifier_Click;
            // 
            // treeViewVariables
            // 
            treeViewVariables.AllowDrop = true;
            treeViewVariables.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            treeViewVariables.BackColor = System.Drawing.Color.Silver;
            treeViewVariables.Font = new System.Drawing.Font("Consolas", 8.5F);
            treeViewVariables.HideSelection = false;
            treeViewVariables.Indent = 15;
            treeViewVariables.ItemHeight = 19;
            treeViewVariables.Location = new System.Drawing.Point(4, 22);
            treeViewVariables.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            treeViewVariables.Name = "treeViewVariables";
            treeViewVariables.Size = new System.Drawing.Size(295, 363);
            treeViewVariables.TabIndex = 0;
            treeViewVariables.ItemDrag += treeView_ItemDrag;
            treeViewVariables.AfterSelect += treeViewVariables_AfterSelect;
            treeViewVariables.DragDrop += TreeView_DragDrop;
            treeViewVariables.DragOver += TreeView_DragOver;
            // 
            // panelBoolean
            // 
            panelBoolean.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            panelBoolean.BackColor = System.Drawing.Color.Silver;
            panelBoolean.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            panelBoolean.Controls.Add(radioButtonFalse);
            panelBoolean.Controls.Add(radioButtonTrue);
            panelBoolean.Font = new System.Drawing.Font("Consolas", 9F);
            panelBoolean.Location = new System.Drawing.Point(4, 456);
            panelBoolean.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panelBoolean.Name = "panelBoolean";
            panelBoolean.Size = new System.Drawing.Size(295, 24);
            panelBoolean.TabIndex = 4;
            panelBoolean.Visible = false;
            // 
            // radioButtonFalse
            // 
            radioButtonFalse.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            radioButtonFalse.AutoSize = true;
            radioButtonFalse.Checked = true;
            radioButtonFalse.Location = new System.Drawing.Point(88, 3);
            radioButtonFalse.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            radioButtonFalse.Name = "radioButtonFalse";
            radioButtonFalse.Size = new System.Drawing.Size(60, 18);
            radioButtonFalse.TabIndex = 6;
            radioButtonFalse.TabStop = true;
            radioButtonFalse.Text = "False";
            radioButtonFalse.UseVisualStyleBackColor = true;
            radioButtonFalse.CheckedChanged += radioButtonFalse_CheckedChanged;
            // 
            // radioButtonTrue
            // 
            radioButtonTrue.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            radioButtonTrue.AutoSize = true;
            radioButtonTrue.Location = new System.Drawing.Point(156, 3);
            radioButtonTrue.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            radioButtonTrue.Name = "radioButtonTrue";
            radioButtonTrue.Size = new System.Drawing.Size(53, 18);
            radioButtonTrue.TabIndex = 7;
            radioButtonTrue.Text = "True";
            radioButtonTrue.UseVisualStyleBackColor = true;
            // 
            // textBoxVarNumberValue
            // 
            textBoxVarNumberValue.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            textBoxVarNumberValue.BackColor = System.Drawing.Color.LightGray;
            textBoxVarNumberValue.Font = new System.Drawing.Font("Consolas", 9F);
            textBoxVarNumberValue.Location = new System.Drawing.Point(4, 456);
            textBoxVarNumberValue.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            textBoxVarNumberValue.Name = "textBoxVarNumberValue";
            textBoxVarNumberValue.Size = new System.Drawing.Size(295, 22);
            textBoxVarNumberValue.TabIndex = 4;
            textBoxVarNumberValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            textBoxVarNumberValue.Visible = false;
            textBoxVarNumberValue.TextChanged += textBoxVarNumberValue_TextChanged;
            textBoxVarNumberValue.KeyPress += textBoxVarNumberValue_KeyPress;
            // 
            // textBoxVarStringValue
            // 
            textBoxVarStringValue.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            textBoxVarStringValue.BackColor = System.Drawing.Color.Silver;
            textBoxVarStringValue.Font = new System.Drawing.Font("Consolas", 9F);
            textBoxVarStringValue.Location = new System.Drawing.Point(4, 456);
            textBoxVarStringValue.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            textBoxVarStringValue.Name = "textBoxVarStringValue";
            textBoxVarStringValue.Size = new System.Drawing.Size(295, 22);
            textBoxVarStringValue.TabIndex = 4;
            textBoxVarStringValue.Visible = false;
            textBoxVarStringValue.TextChanged += textBoxTextVarValue_TextChanged;
            // 
            // panelPlaceholder
            // 
            panelPlaceholder.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            panelPlaceholder.BackColor = System.Drawing.Color.Silver;
            panelPlaceholder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            panelPlaceholder.Font = new System.Drawing.Font("Consolas", 9F);
            panelPlaceholder.Location = new System.Drawing.Point(4, 456);
            panelPlaceholder.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panelPlaceholder.Name = "panelPlaceholder";
            panelPlaceholder.Size = new System.Drawing.Size(295, 23);
            panelPlaceholder.TabIndex = 9;
            // 
            // panelNewIdentifier
            // 
            panelNewIdentifier.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            panelNewIdentifier.Controls.Add(comboBoxTableAddOptions);
            panelNewIdentifier.Controls.Add(buttonNewIdentifier);
            panelNewIdentifier.Font = new System.Drawing.Font("Consolas", 9F);
            panelNewIdentifier.Location = new System.Drawing.Point(4, 456);
            panelNewIdentifier.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panelNewIdentifier.Name = "panelNewIdentifier";
            panelNewIdentifier.Size = new System.Drawing.Size(295, 24);
            panelNewIdentifier.TabIndex = 11;
            panelNewIdentifier.Visible = false;
            panelNewIdentifier.VisibleChanged += panelNewIdentifier_VisibleChanged;
            // 
            // comboBoxTableAddOptions
            // 
            comboBoxTableAddOptions.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            comboBoxTableAddOptions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxTableAddOptions.FormattingEnabled = true;
            comboBoxTableAddOptions.Items.AddRange(new object[] { "STRING", "NUMBER", "BOOLEAN", "TABLE" });
            comboBoxTableAddOptions.Location = new System.Drawing.Point(1, 0);
            comboBoxTableAddOptions.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            comboBoxTableAddOptions.Name = "comboBoxTableAddOptions";
            comboBoxTableAddOptions.Size = new System.Drawing.Size(210, 22);
            comboBoxTableAddOptions.TabIndex = 10;
            // 
            // buttonNewIdentifier
            // 
            buttonNewIdentifier.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            buttonNewIdentifier.Font = new System.Drawing.Font("Segoe UI", 9F);
            buttonNewIdentifier.Location = new System.Drawing.Point(219, 0);
            buttonNewIdentifier.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            buttonNewIdentifier.Name = "buttonNewIdentifier";
            buttonNewIdentifier.Size = new System.Drawing.Size(76, 24);
            buttonNewIdentifier.TabIndex = 4;
            buttonNewIdentifier.Text = "Add >>";
            buttonNewIdentifier.UseVisualStyleBackColor = true;
            buttonNewIdentifier.Click += buttonNewIdentifier_Click;
            // 
            // groupBoxScripts
            // 
            groupBoxScripts.BackColor = System.Drawing.Color.Transparent;
            groupBoxScripts.Controls.Add(splitContainerPreconditionOperation);
            groupBoxScripts.Controls.Add(textBoxScriptName);
            groupBoxScripts.Controls.Add(buttonRemoveScript);
            groupBoxScripts.Controls.Add(buttonNewScript);
            groupBoxScripts.Controls.Add(treeViewScripts);
            groupBoxScripts.Dock = System.Windows.Forms.DockStyle.Fill;
            groupBoxScripts.Location = new System.Drawing.Point(0, 0);
            groupBoxScripts.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBoxScripts.Name = "groupBoxScripts";
            groupBoxScripts.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBoxScripts.Size = new System.Drawing.Size(363, 519);
            groupBoxScripts.TabIndex = 6;
            groupBoxScripts.TabStop = false;
            groupBoxScripts.Text = "Custom Scripts";
            // 
            // splitContainerPreconditionOperation
            // 
            splitContainerPreconditionOperation.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            splitContainerPreconditionOperation.IsSplitterFixed = true;
            splitContainerPreconditionOperation.Location = new System.Drawing.Point(4, 456);
            splitContainerPreconditionOperation.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            splitContainerPreconditionOperation.Name = "splitContainerPreconditionOperation";
            // 
            // splitContainerPreconditionOperation.Panel1
            // 
            splitContainerPreconditionOperation.Panel1.Controls.Add(buttonNewPrecondition);
            // 
            // splitContainerPreconditionOperation.Panel2
            // 
            splitContainerPreconditionOperation.Panel2.Controls.Add(buttonNewOperation);
            splitContainerPreconditionOperation.Size = new System.Drawing.Size(356, 24);
            splitContainerPreconditionOperation.SplitterDistance = 175;
            splitContainerPreconditionOperation.SplitterWidth = 7;
            splitContainerPreconditionOperation.TabIndex = 9;
            // 
            // buttonNewPrecondition
            // 
            buttonNewPrecondition.Dock = System.Windows.Forms.DockStyle.Fill;
            buttonNewPrecondition.Location = new System.Drawing.Point(0, 0);
            buttonNewPrecondition.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            buttonNewPrecondition.Name = "buttonNewPrecondition";
            buttonNewPrecondition.Size = new System.Drawing.Size(175, 24);
            buttonNewPrecondition.TabIndex = 3;
            buttonNewPrecondition.Text = "Add New Precondition";
            buttonNewPrecondition.UseVisualStyleBackColor = true;
            buttonNewPrecondition.Click += buttonNewPrecondition_Click;
            // 
            // buttonNewOperation
            // 
            buttonNewOperation.Dock = System.Windows.Forms.DockStyle.Fill;
            buttonNewOperation.Location = new System.Drawing.Point(0, 0);
            buttonNewOperation.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            buttonNewOperation.Name = "buttonNewOperation";
            buttonNewOperation.Size = new System.Drawing.Size(174, 24);
            buttonNewOperation.TabIndex = 4;
            buttonNewOperation.Text = "Add New Operation";
            buttonNewOperation.UseVisualStyleBackColor = true;
            buttonNewOperation.Click += buttonNewOperation_Click;
            // 
            // textBoxScriptName
            // 
            textBoxScriptName.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            textBoxScriptName.BackColor = System.Drawing.Color.Silver;
            textBoxScriptName.Enabled = false;
            textBoxScriptName.Font = new System.Drawing.Font("Consolas", 9F);
            textBoxScriptName.Location = new System.Drawing.Point(4, 426);
            textBoxScriptName.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            textBoxScriptName.Name = "textBoxScriptName";
            textBoxScriptName.Size = new System.Drawing.Size(355, 22);
            textBoxScriptName.TabIndex = 2;
            textBoxScriptName.TextChanged += textBoxScriptName_TextChanged;
            textBoxScriptName.KeyDown += textBoxScriptName_KeyDown;
            // 
            // buttonRemoveScript
            // 
            buttonRemoveScript.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            buttonRemoveScript.Location = new System.Drawing.Point(4, 486);
            buttonRemoveScript.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            buttonRemoveScript.Name = "buttonRemoveScript";
            buttonRemoveScript.Size = new System.Drawing.Size(356, 27);
            buttonRemoveScript.TabIndex = 5;
            buttonRemoveScript.Text = "Delete Selected Node";
            buttonRemoveScript.UseVisualStyleBackColor = true;
            buttonRemoveScript.Click += buttonRemoveScript_Click;
            // 
            // buttonNewScript
            // 
            buttonNewScript.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            buttonNewScript.Location = new System.Drawing.Point(4, 392);
            buttonNewScript.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            buttonNewScript.Name = "buttonNewScript";
            buttonNewScript.Size = new System.Drawing.Size(356, 27);
            buttonNewScript.TabIndex = 1;
            buttonNewScript.Text = "Add New Script Event";
            buttonNewScript.UseVisualStyleBackColor = true;
            buttonNewScript.Click += buttonNewScript_Click;
            // 
            // treeViewScripts
            // 
            treeViewScripts.AllowDrop = true;
            treeViewScripts.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            treeViewScripts.BackColor = System.Drawing.Color.Silver;
            treeViewScripts.Font = new System.Drawing.Font("Consolas", 8.5F);
            treeViewScripts.HideSelection = false;
            treeViewScripts.Indent = 21;
            treeViewScripts.ItemHeight = 19;
            treeViewScripts.Location = new System.Drawing.Point(4, 22);
            treeViewScripts.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            treeViewScripts.Name = "treeViewScripts";
            treeViewScripts.ShowRootLines = false;
            treeViewScripts.Size = new System.Drawing.Size(355, 363);
            treeViewScripts.TabIndex = 0;
            treeViewScripts.ItemDrag += treeView_ItemDrag;
            treeViewScripts.AfterSelect += treeViewScripts_AfterSelect;
            treeViewScripts.DragDrop += TreeView_DragDrop;
            treeViewScripts.DragOver += TreeView_DragOver;
            // 
            // panelScripting
            // 
            panelScripting.AutoScroll = true;
            panelScripting.AutoSize = true;
            panelScripting.Controls.Add(splitContainerOuter);
            panelScripting.Dock = System.Windows.Forms.DockStyle.Fill;
            panelScripting.Location = new System.Drawing.Point(0, 0);
            panelScripting.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panelScripting.Name = "panelScripting";
            panelScripting.Size = new System.Drawing.Size(1353, 519);
            panelScripting.TabIndex = 2;
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
            splitContainerOuter.Panel1.Controls.Add(splitContainerInner);
            splitContainerOuter.Panel1MinSize = 512;
            // 
            // splitContainerOuter.Panel2
            // 
            splitContainerOuter.Panel2.Controls.Add(groupBoxScriptDetails);
            splitContainerOuter.Panel2MinSize = 250;
            splitContainerOuter.Size = new System.Drawing.Size(1353, 519);
            splitContainerOuter.SplitterDistance = 670;
            splitContainerOuter.SplitterWidth = 5;
            splitContainerOuter.TabIndex = 8;
            // 
            // splitContainerInner
            // 
            splitContainerInner.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainerInner.Location = new System.Drawing.Point(0, 0);
            splitContainerInner.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            splitContainerInner.Name = "splitContainerInner";
            // 
            // splitContainerInner.Panel1
            // 
            splitContainerInner.Panel1.Controls.Add(groupBoxVariables);
            splitContainerInner.Panel1MinSize = 254;
            // 
            // splitContainerInner.Panel2
            // 
            splitContainerInner.Panel2.Controls.Add(groupBoxScripts);
            splitContainerInner.Panel2MinSize = 254;
            splitContainerInner.Size = new System.Drawing.Size(670, 519);
            splitContainerInner.SplitterDistance = 302;
            splitContainerInner.SplitterWidth = 5;
            splitContainerInner.TabIndex = 0;
            // 
            // groupBoxScriptDetails
            // 
            groupBoxScriptDetails.Controls.Add(panelComponentDetails);
            groupBoxScriptDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            groupBoxScriptDetails.Location = new System.Drawing.Point(0, 0);
            groupBoxScriptDetails.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBoxScriptDetails.Name = "groupBoxScriptDetails";
            groupBoxScriptDetails.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBoxScriptDetails.Size = new System.Drawing.Size(678, 519);
            groupBoxScriptDetails.TabIndex = 20;
            groupBoxScriptDetails.TabStop = false;
            groupBoxScriptDetails.Text = "Script Component Details";
            // 
            // panelComponentDetails
            // 
            panelComponentDetails.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            panelComponentDetails.Location = new System.Drawing.Point(4, 22);
            panelComponentDetails.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panelComponentDetails.Name = "panelComponentDetails";
            panelComponentDetails.Size = new System.Drawing.Size(670, 491);
            panelComponentDetails.TabIndex = 0;
            // 
            // ScriptControl
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            AutoScroll = true;
            Controls.Add(panelScripting);
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "ScriptControl";
            Size = new System.Drawing.Size(1353, 519);
            groupBoxVariables.ResumeLayout(false);
            groupBoxVariables.PerformLayout();
            panelBoolean.ResumeLayout(false);
            panelBoolean.PerformLayout();
            panelNewIdentifier.ResumeLayout(false);
            groupBoxScripts.ResumeLayout(false);
            groupBoxScripts.PerformLayout();
            splitContainerPreconditionOperation.Panel1.ResumeLayout(false);
            splitContainerPreconditionOperation.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerPreconditionOperation).EndInit();
            splitContainerPreconditionOperation.ResumeLayout(false);
            panelScripting.ResumeLayout(false);
            splitContainerOuter.Panel1.ResumeLayout(false);
            splitContainerOuter.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerOuter).EndInit();
            splitContainerOuter.ResumeLayout(false);
            splitContainerInner.Panel1.ResumeLayout(false);
            splitContainerInner.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerInner).EndInit();
            splitContainerInner.ResumeLayout(false);
            groupBoxScriptDetails.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.GroupBox groupBoxVariables;
        internal DoubleBufferedTreeView treeViewVariables;
        internal System.Windows.Forms.GroupBox groupBoxScripts;
        internal System.Windows.Forms.ComboBox comboBoxVarType;
        internal System.Windows.Forms.TextBox textBoxVarName;
        internal System.Windows.Forms.TextBox textBoxVarStringValue;
        internal System.Windows.Forms.Button buttonNewVariable;
        internal System.Windows.Forms.Button buttonRemoveVariableIdentifier;
        internal System.Windows.Forms.Button buttonNewIdentifier;
        internal System.Windows.Forms.TextBox textBoxVarNumberValue;
        internal System.Windows.Forms.Panel panelScripting;
        internal System.Windows.Forms.GroupBox groupBoxScriptDetails;
        internal DoubleBufferedTreeView treeViewScripts;
        internal System.Windows.Forms.Button buttonNewScript;
        internal System.Windows.Forms.Button buttonRemoveScript;
        internal System.Windows.Forms.TextBox textBoxScriptName;
        internal System.Windows.Forms.SplitContainer splitContainerOuter;
        internal System.Windows.Forms.SplitContainer splitContainerInner;
        internal System.Windows.Forms.Panel panelComponentDetails;
        internal System.Windows.Forms.Button buttonNewOperation;
        internal System.Windows.Forms.Button buttonNewPrecondition;
        internal System.Windows.Forms.SplitContainer splitContainerPreconditionOperation;
        internal System.Windows.Forms.RadioButton radioButtonTrue;
        internal System.Windows.Forms.RadioButton radioButtonFalse;
        internal System.Windows.Forms.Panel panelBoolean;
        internal System.Windows.Forms.Panel panelPlaceholder;
        internal System.Windows.Forms.ComboBox comboBoxTableAddOptions;
        internal System.Windows.Forms.Panel panelNewIdentifier;
    }
}
