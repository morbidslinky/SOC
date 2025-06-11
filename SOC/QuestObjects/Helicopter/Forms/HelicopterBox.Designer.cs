namespace SOC.QuestObjects.Helicopter
{
    partial class HelicopterBox
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
            groupBox_main = new System.Windows.Forms.GroupBox();
            checkBox_spawn = new System.Windows.Forms.CheckBox();
            label1 = new System.Windows.Forms.Label();
            comboBox_cRoute = new System.Windows.Forms.ComboBox();
            label4 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            comboBox_dRoute = new System.Windows.Forms.ComboBox();
            comboBox_class = new System.Windows.Forms.ComboBox();
            checkBox_target = new System.Windows.Forms.CheckBox();
            groupBox_main.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox_main
            // 
            groupBox_main.BackColor = System.Drawing.Color.DarkGray;
            groupBox_main.Controls.Add(checkBox_spawn);
            groupBox_main.Controls.Add(label1);
            groupBox_main.Controls.Add(comboBox_cRoute);
            groupBox_main.Controls.Add(label4);
            groupBox_main.Controls.Add(label3);
            groupBox_main.Controls.Add(comboBox_dRoute);
            groupBox_main.Controls.Add(comboBox_class);
            groupBox_main.Controls.Add(checkBox_target);
            groupBox_main.Dock = System.Windows.Forms.DockStyle.Fill;
            groupBox_main.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            groupBox_main.Location = new System.Drawing.Point(0, 0);
            groupBox_main.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox_main.Name = "groupBox_main";
            groupBox_main.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox_main.Size = new System.Drawing.Size(313, 194);
            groupBox_main.TabIndex = 2;
            groupBox_main.TabStop = false;
            groupBox_main.Text = "HelicopterBox";
            // 
            // checkBox_spawn
            // 
            checkBox_spawn.AutoSize = true;
            checkBox_spawn.Font = new System.Drawing.Font("Consolas", 9F);
            checkBox_spawn.Location = new System.Drawing.Point(110, 21);
            checkBox_spawn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            checkBox_spawn.Name = "checkBox_spawn";
            checkBox_spawn.Size = new System.Drawing.Size(61, 18);
            checkBox_spawn.TabIndex = 23;
            checkBox_spawn.TabStop = false;
            checkBox_spawn.Text = "Spawn";
            checkBox_spawn.UseVisualStyleBackColor = true;
            checkBox_spawn.CheckedChanged += checkBox_spawn_CheckedChanged;
            // 
            // label1
            // 
            label1.Location = new System.Drawing.Point(10, 137);
            label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(92, 24);
            label1.TabIndex = 22;
            label1.Text = "Caution Route:";
            label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBox_cRoute
            // 
            comboBox_cRoute.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            comboBox_cRoute.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBox_cRoute.Font = new System.Drawing.Font("Consolas", 9F);
            comboBox_cRoute.FormattingEnabled = true;
            comboBox_cRoute.Location = new System.Drawing.Point(10, 163);
            comboBox_cRoute.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            comboBox_cRoute.Name = "comboBox_cRoute";
            comboBox_cRoute.Size = new System.Drawing.Size(291, 22);
            comboBox_cRoute.TabIndex = 21;
            // 
            // label4
            // 
            label4.Location = new System.Drawing.Point(10, 84);
            label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(92, 24);
            label4.TabIndex = 20;
            label4.Text = "Sneak Route:";
            label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            label3.Location = new System.Drawing.Point(10, 50);
            label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(92, 24);
            label3.TabIndex = 19;
            label3.Text = "Class:";
            label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBox_dRoute
            // 
            comboBox_dRoute.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            comboBox_dRoute.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBox_dRoute.Font = new System.Drawing.Font("Consolas", 9F);
            comboBox_dRoute.FormattingEnabled = true;
            comboBox_dRoute.Location = new System.Drawing.Point(10, 110);
            comboBox_dRoute.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            comboBox_dRoute.Name = "comboBox_dRoute";
            comboBox_dRoute.Size = new System.Drawing.Size(291, 22);
            comboBox_dRoute.TabIndex = 6;
            // 
            // comboBox_class
            // 
            comboBox_class.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            comboBox_class.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBox_class.Font = new System.Drawing.Font("Consolas", 9F);
            comboBox_class.FormattingEnabled = true;
            comboBox_class.Items.AddRange(new object[] { "DEFAULT", "BLACK", "RED" });
            comboBox_class.Location = new System.Drawing.Point(110, 50);
            comboBox_class.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            comboBox_class.Name = "comboBox_class";
            comboBox_class.Size = new System.Drawing.Size(192, 22);
            comboBox_class.TabIndex = 5;
            // 
            // checkBox_target
            // 
            checkBox_target.AutoSize = true;
            checkBox_target.Font = new System.Drawing.Font("Consolas", 9F);
            checkBox_target.Location = new System.Drawing.Point(210, 21);
            checkBox_target.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            checkBox_target.Name = "checkBox_target";
            checkBox_target.Size = new System.Drawing.Size(89, 18);
            checkBox_target.TabIndex = 0;
            checkBox_target.TabStop = false;
            checkBox_target.Text = "Is Target";
            checkBox_target.UseVisualStyleBackColor = true;
            // 
            // HelicopterBox
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(groupBox_main);
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "HelicopterBox";
            Size = new System.Drawing.Size(313, 194);
            groupBox_main.ResumeLayout(false);
            groupBox_main.PerformLayout();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox_main;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.ComboBox comboBox_dRoute;
        public System.Windows.Forms.ComboBox comboBox_class;
        public System.Windows.Forms.CheckBox checkBox_target;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.ComboBox comboBox_cRoute;
        public System.Windows.Forms.CheckBox checkBox_spawn;
    }
}
