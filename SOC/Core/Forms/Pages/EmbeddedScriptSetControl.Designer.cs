namespace SOC.UI
{
    partial class EmbeddedScriptSetControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EmbeddedScriptSetControl));
            this.buttonLoadScript = new System.Windows.Forms.Button();
            this.groupBoxScriptSet = new System.Windows.Forms.GroupBox();
            this.textEmptyHint = new System.Windows.Forms.TextBox();
            this.checkedListBoxScripts = new System.Windows.Forms.CheckedListBox();
            this.buttonSaveScript = new System.Windows.Forms.Button();
            this.groupBoxScriptSet.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonLoadScript
            // 
            this.buttonLoadScript.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonLoadScript.Location = new System.Drawing.Point(0, 379);
            this.buttonLoadScript.Name = "buttonLoadScript";
            this.buttonLoadScript.Size = new System.Drawing.Size(575, 21);
            this.buttonLoadScript.TabIndex = 12;
            this.buttonLoadScript.Text = "Import Script(s) From Xml...";
            this.buttonLoadScript.UseVisualStyleBackColor = true;
            this.buttonLoadScript.Click += new System.EventHandler(this.buttonLoadScript_Click);
            // 
            // groupBoxScriptSet
            // 
            this.groupBoxScriptSet.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxScriptSet.BackColor = System.Drawing.Color.Silver;
            this.groupBoxScriptSet.Controls.Add(this.checkedListBoxScripts);
            this.groupBoxScriptSet.Controls.Add(this.textEmptyHint);
            this.groupBoxScriptSet.Location = new System.Drawing.Point(0, 5);
            this.groupBoxScriptSet.Name = "groupBoxScriptSet";
            this.groupBoxScriptSet.Size = new System.Drawing.Size(575, 368);
            this.groupBoxScriptSet.TabIndex = 11;
            this.groupBoxScriptSet.TabStop = false;
            // 
            // textEmptyHint
            // 
            this.textEmptyHint.AcceptsReturn = true;
            this.textEmptyHint.AcceptsTab = true;
            this.textEmptyHint.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textEmptyHint.BackColor = System.Drawing.Color.LightGray;
            this.textEmptyHint.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textEmptyHint.Enabled = false;
            this.textEmptyHint.Location = new System.Drawing.Point(3, 16);
            this.textEmptyHint.Multiline = true;
            this.textEmptyHint.Name = "textEmptyHint";
            this.textEmptyHint.ReadOnly = true;
            this.textEmptyHint.Size = new System.Drawing.Size(569, 346);
            this.textEmptyHint.TabIndex = 0;
            this.textEmptyHint.Text = resources.GetString("textEmptyHint.Text");
            this.textEmptyHint.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // checkedListBoxScripts
            // 
            this.checkedListBoxScripts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkedListBoxScripts.BackColor = System.Drawing.Color.LightGray;
            this.checkedListBoxScripts.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkedListBoxScripts.FormattingEnabled = true;
            this.checkedListBoxScripts.Location = new System.Drawing.Point(3, 16);
            this.checkedListBoxScripts.Name = "checkedListBoxScripts";
            this.checkedListBoxScripts.Size = new System.Drawing.Size(569, 349);
            this.checkedListBoxScripts.TabIndex = 0;
            // 
            // buttonSaveScript
            // 
            this.buttonSaveScript.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSaveScript.Location = new System.Drawing.Point(0, 405);
            this.buttonSaveScript.Name = "buttonSaveScript";
            this.buttonSaveScript.Size = new System.Drawing.Size(575, 23);
            this.buttonSaveScript.TabIndex = 13;
            this.buttonSaveScript.Text = "Export Script(s) To Xml...";
            this.buttonSaveScript.UseVisualStyleBackColor = true;
            this.buttonSaveScript.Click += new System.EventHandler(this.buttonSaveScript_Click);
            // 
            // EmbeddedScriptSetControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonLoadScript);
            this.Controls.Add(this.groupBoxScriptSet);
            this.Controls.Add(this.buttonSaveScript);
            this.Name = "EmbeddedScriptSetControl";
            this.Size = new System.Drawing.Size(575, 431);
            this.groupBoxScriptSet.ResumeLayout(false);
            this.groupBoxScriptSet.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonLoadScript;
        private System.Windows.Forms.GroupBox groupBoxScriptSet;
        private System.Windows.Forms.Button buttonSaveScript;
        private System.Windows.Forms.CheckedListBox checkedListBoxScripts;
        private System.Windows.Forms.TextBox textEmptyHint;
    }
}
