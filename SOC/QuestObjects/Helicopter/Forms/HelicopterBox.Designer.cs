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
            this.groupBox_main = new System.Windows.Forms.GroupBox();
            this.checkBox_spawn = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox_cRoute = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox_dRoute = new System.Windows.Forms.ComboBox();
            this.comboBox_class = new System.Windows.Forms.ComboBox();
            this.checkBox_target = new System.Windows.Forms.CheckBox();
            this.groupBox_main.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox_main
            // 
            this.groupBox_main.BackColor = System.Drawing.Color.DarkGray;
            this.groupBox_main.Controls.Add(this.checkBox_spawn);
            this.groupBox_main.Controls.Add(this.label1);
            this.groupBox_main.Controls.Add(this.comboBox_cRoute);
            this.groupBox_main.Controls.Add(this.label4);
            this.groupBox_main.Controls.Add(this.label3);
            this.groupBox_main.Controls.Add(this.comboBox_dRoute);
            this.groupBox_main.Controls.Add(this.comboBox_class);
            this.groupBox_main.Controls.Add(this.checkBox_target);
            this.groupBox_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox_main.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox_main.Location = new System.Drawing.Point(0, 0);
            this.groupBox_main.Name = "groupBox_main";
            this.groupBox_main.Size = new System.Drawing.Size(268, 168);
            this.groupBox_main.TabIndex = 2;
            this.groupBox_main.TabStop = false;
            this.groupBox_main.Text = "HelicopterBox";
            // 
            // checkBox_spawn
            // 
            this.checkBox_spawn.AutoSize = true;
            this.checkBox_spawn.Location = new System.Drawing.Point(94, 18);
            this.checkBox_spawn.Name = "checkBox_spawn";
            this.checkBox_spawn.Size = new System.Drawing.Size(59, 17);
            this.checkBox_spawn.TabIndex = 23;
            this.checkBox_spawn.TabStop = false;
            this.checkBox_spawn.Text = "Spawn";
            this.checkBox_spawn.UseVisualStyleBackColor = true;
            this.checkBox_spawn.CheckedChanged += new System.EventHandler(this.checkBox_spawn_CheckedChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(9, 119);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 21);
            this.label1.TabIndex = 22;
            this.label1.Text = "Caution Route:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBox_cRoute
            // 
            this.comboBox_cRoute.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox_cRoute.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_cRoute.FormattingEnabled = true;
            this.comboBox_cRoute.Location = new System.Drawing.Point(9, 141);
            this.comboBox_cRoute.Name = "comboBox_cRoute";
            this.comboBox_cRoute.Size = new System.Drawing.Size(250, 21);
            this.comboBox_cRoute.TabIndex = 21;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(9, 73);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 21);
            this.label4.TabIndex = 20;
            this.label4.Text = "Sneak Route:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(9, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 21);
            this.label3.TabIndex = 19;
            this.label3.Text = "Class:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBox_dRoute
            // 
            this.comboBox_dRoute.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox_dRoute.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_dRoute.FormattingEnabled = true;
            this.comboBox_dRoute.Location = new System.Drawing.Point(9, 95);
            this.comboBox_dRoute.Name = "comboBox_dRoute";
            this.comboBox_dRoute.Size = new System.Drawing.Size(250, 21);
            this.comboBox_dRoute.TabIndex = 6;
            // 
            // comboBox_class
            // 
            this.comboBox_class.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox_class.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_class.FormattingEnabled = true;
            this.comboBox_class.Items.AddRange(new object[] {
            "DEFAULT",
            "BLACK",
            "RED"});
            this.comboBox_class.Location = new System.Drawing.Point(94, 43);
            this.comboBox_class.Name = "comboBox_class";
            this.comboBox_class.Size = new System.Drawing.Size(165, 21);
            this.comboBox_class.TabIndex = 5;
            // 
            // checkBox_target
            // 
            this.checkBox_target.AutoSize = true;
            this.checkBox_target.Location = new System.Drawing.Point(180, 18);
            this.checkBox_target.Name = "checkBox_target";
            this.checkBox_target.Size = new System.Drawing.Size(68, 17);
            this.checkBox_target.TabIndex = 0;
            this.checkBox_target.TabStop = false;
            this.checkBox_target.Text = "Is Target";
            this.checkBox_target.UseVisualStyleBackColor = true;
            // 
            // HelicopterBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox_main);
            this.Name = "HelicopterBox";
            this.Size = new System.Drawing.Size(268, 168);
            this.groupBox_main.ResumeLayout(false);
            this.groupBox_main.PerformLayout();
            this.ResumeLayout(false);

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
