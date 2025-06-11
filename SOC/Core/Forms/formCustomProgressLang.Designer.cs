namespace SOC.UI
{
    partial class formCustomProgressLang
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            textBoxLangId = new System.Windows.Forms.TextBox();
            textBoxLangValue = new System.Windows.Forms.TextBox();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            buttonCreateEntry = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // textBoxLangId
            // 
            textBoxLangId.BackColor = System.Drawing.Color.LightGray;
            textBoxLangId.Font = new System.Drawing.Font("Consolas", 9F);
            textBoxLangId.Location = new System.Drawing.Point(14, 29);
            textBoxLangId.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            textBoxLangId.Name = "textBoxLangId";
            textBoxLangId.Size = new System.Drawing.Size(363, 22);
            textBoxLangId.TabIndex = 0;
            textBoxLangId.Leave += textBoxLangId_Leave;
            // 
            // textBoxLangValue
            // 
            textBoxLangValue.BackColor = System.Drawing.Color.LightGray;
            textBoxLangValue.Font = new System.Drawing.Font("Consolas", 9F);
            textBoxLangValue.Location = new System.Drawing.Point(14, 78);
            textBoxLangValue.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            textBoxLangValue.Name = "textBoxLangValue";
            textBoxLangValue.Size = new System.Drawing.Size(363, 22);
            textBoxLangValue.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(10, 10);
            label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(87, 15);
            label1.TabIndex = 2;
            label1.Text = "Unique LangId:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(10, 60);
            label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(121, 15);
            label2.TabIndex = 3;
            label2.Text = "In-game Notification:";
            // 
            // buttonCreateEntry
            // 
            buttonCreateEntry.Location = new System.Drawing.Point(284, 110);
            buttonCreateEntry.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            buttonCreateEntry.Name = "buttonCreateEntry";
            buttonCreateEntry.Size = new System.Drawing.Size(94, 27);
            buttonCreateEntry.TabIndex = 4;
            buttonCreateEntry.Text = "Create Entry";
            buttonCreateEntry.UseVisualStyleBackColor = true;
            buttonCreateEntry.Click += buttonCreateEntry_Click;
            // 
            // formCustomProgressLang
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.DarkGray;
            ClientSize = new System.Drawing.Size(392, 148);
            Controls.Add(buttonCreateEntry);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(textBoxLangValue);
            Controls.Add(textBoxLangId);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "formCustomProgressLang";
            ShowIcon = false;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Custom Progress Notification";
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxLangId;
        private System.Windows.Forms.TextBox textBoxLangValue;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonCreateEntry;
    }
}