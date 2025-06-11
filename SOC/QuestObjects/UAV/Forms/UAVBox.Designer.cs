namespace SOC.QuestObjects.UAV
{
    partial class UAVBox
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
            label5 = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            comboBox_defense = new System.Windows.Forms.ComboBox();
            comboBox_weapon = new System.Windows.Forms.ComboBox();
            checkBox_docile = new System.Windows.Forms.CheckBox();
            label4 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            comboBox_aRoute = new System.Windows.Forms.ComboBox();
            comboBox_dRoute = new System.Windows.Forms.ComboBox();
            checkBox_target = new System.Windows.Forms.CheckBox();
            textBox_rot = new System.Windows.Forms.TextBox();
            label1 = new System.Windows.Forms.Label();
            textBox_zcoord = new System.Windows.Forms.TextBox();
            textBox_ycoord = new System.Windows.Forms.TextBox();
            textBox_xcoord = new System.Windows.Forms.TextBox();
            groupBox_main.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox_main
            // 
            groupBox_main.BackColor = System.Drawing.Color.DarkGray;
            groupBox_main.Controls.Add(label5);
            groupBox_main.Controls.Add(label6);
            groupBox_main.Controls.Add(comboBox_defense);
            groupBox_main.Controls.Add(comboBox_weapon);
            groupBox_main.Controls.Add(checkBox_docile);
            groupBox_main.Controls.Add(label4);
            groupBox_main.Controls.Add(label3);
            groupBox_main.Controls.Add(label2);
            groupBox_main.Controls.Add(comboBox_aRoute);
            groupBox_main.Controls.Add(comboBox_dRoute);
            groupBox_main.Controls.Add(checkBox_target);
            groupBox_main.Controls.Add(textBox_rot);
            groupBox_main.Controls.Add(label1);
            groupBox_main.Controls.Add(textBox_zcoord);
            groupBox_main.Controls.Add(textBox_ycoord);
            groupBox_main.Controls.Add(textBox_xcoord);
            groupBox_main.Dock = System.Windows.Forms.DockStyle.Fill;
            groupBox_main.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            groupBox_main.Location = new System.Drawing.Point(0, 0);
            groupBox_main.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox_main.Name = "groupBox_main";
            groupBox_main.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox_main.Size = new System.Drawing.Size(313, 242);
            groupBox_main.TabIndex = 0;
            groupBox_main.TabStop = false;
            groupBox_main.Text = "UAVBox";
            // 
            // label5
            // 
            label5.Location = new System.Drawing.Point(5, 211);
            label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(89, 24);
            label5.TabIndex = 8;
            label5.Text = "Armor Rank:";
            label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            label6.Location = new System.Drawing.Point(5, 180);
            label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(89, 24);
            label6.TabIndex = 9;
            label6.Text = "Weapon:";
            label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBox_defense
            // 
            comboBox_defense.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            comboBox_defense.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBox_defense.Font = new System.Drawing.Font("Consolas", 9F);
            comboBox_defense.FormattingEnabled = true;
            comboBox_defense.Items.AddRange(new object[] { "DEFAULT", "9", "10", "11" });
            comboBox_defense.Location = new System.Drawing.Point(99, 211);
            comboBox_defense.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            comboBox_defense.Name = "comboBox_defense";
            comboBox_defense.Size = new System.Drawing.Size(202, 22);
            comboBox_defense.TabIndex = 11;
            // 
            // comboBox_weapon
            // 
            comboBox_weapon.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            comboBox_weapon.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBox_weapon.Font = new System.Drawing.Font("Consolas", 9F);
            comboBox_weapon.FormattingEnabled = true;
            comboBox_weapon.Items.AddRange(new object[] { "DEVELOP_LEVEL_LMG_0", "DEVELOP_LEVEL_LMG_1", "DEVELOP_LEVEL_LMG_2" });
            comboBox_weapon.Location = new System.Drawing.Point(99, 180);
            comboBox_weapon.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            comboBox_weapon.Name = "comboBox_weapon";
            comboBox_weapon.Size = new System.Drawing.Size(202, 22);
            comboBox_weapon.TabIndex = 10;
            // 
            // checkBox_docile
            // 
            checkBox_docile.Anchor = System.Windows.Forms.AnchorStyles.Top;
            checkBox_docile.AutoSize = true;
            checkBox_docile.Font = new System.Drawing.Font("Consolas", 9F);
            checkBox_docile.Location = new System.Drawing.Point(200, 87);
            checkBox_docile.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            checkBox_docile.Name = "checkBox_docile";
            checkBox_docile.Size = new System.Drawing.Size(68, 18);
            checkBox_docile.TabIndex = 7;
            checkBox_docile.TabStop = false;
            checkBox_docile.Text = "Docile";
            checkBox_docile.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            label4.Location = new System.Drawing.Point(2, 149);
            label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(91, 24);
            label4.TabIndex = 0;
            label4.Text = "Combat Route:";
            label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            label3.Location = new System.Drawing.Point(5, 118);
            label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(89, 24);
            label3.TabIndex = 0;
            label3.Text = "Sneak Route:";
            label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            label2.Location = new System.Drawing.Point(4, 52);
            label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(89, 23);
            label2.TabIndex = 0;
            label2.Text = "Rotation:";
            label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBox_aRoute
            // 
            comboBox_aRoute.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            comboBox_aRoute.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBox_aRoute.Font = new System.Drawing.Font("Consolas", 9F);
            comboBox_aRoute.FormattingEnabled = true;
            comboBox_aRoute.Location = new System.Drawing.Point(99, 149);
            comboBox_aRoute.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            comboBox_aRoute.Name = "comboBox_aRoute";
            comboBox_aRoute.Size = new System.Drawing.Size(202, 22);
            comboBox_aRoute.TabIndex = 6;
            // 
            // comboBox_dRoute
            // 
            comboBox_dRoute.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            comboBox_dRoute.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBox_dRoute.Font = new System.Drawing.Font("Consolas", 9F);
            comboBox_dRoute.FormattingEnabled = true;
            comboBox_dRoute.Location = new System.Drawing.Point(99, 118);
            comboBox_dRoute.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            comboBox_dRoute.Name = "comboBox_dRoute";
            comboBox_dRoute.Size = new System.Drawing.Size(202, 22);
            comboBox_dRoute.TabIndex = 5;
            // 
            // checkBox_target
            // 
            checkBox_target.Anchor = System.Windows.Forms.AnchorStyles.Top;
            checkBox_target.AutoSize = true;
            checkBox_target.Font = new System.Drawing.Font("Consolas", 9F);
            checkBox_target.Location = new System.Drawing.Point(99, 87);
            checkBox_target.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            checkBox_target.Name = "checkBox_target";
            checkBox_target.Size = new System.Drawing.Size(89, 18);
            checkBox_target.TabIndex = 0;
            checkBox_target.TabStop = false;
            checkBox_target.Text = "Is Target";
            checkBox_target.UseVisualStyleBackColor = true;
            // 
            // textBox_rot
            // 
            textBox_rot.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            textBox_rot.Font = new System.Drawing.Font("Consolas", 9F);
            textBox_rot.Location = new System.Drawing.Point(99, 52);
            textBox_rot.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            textBox_rot.Name = "textBox_rot";
            textBox_rot.Size = new System.Drawing.Size(202, 22);
            textBox_rot.TabIndex = 4;
            // 
            // label1
            // 
            label1.Location = new System.Drawing.Point(4, 22);
            label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(89, 23);
            label1.TabIndex = 0;
            label1.Text = "Coordinates:";
            label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBox_zcoord
            // 
            textBox_zcoord.Anchor = System.Windows.Forms.AnchorStyles.Top;
            textBox_zcoord.Font = new System.Drawing.Font("Consolas", 9F);
            textBox_zcoord.Location = new System.Drawing.Point(239, 22);
            textBox_zcoord.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            textBox_zcoord.Name = "textBox_zcoord";
            textBox_zcoord.Size = new System.Drawing.Size(62, 22);
            textBox_zcoord.TabIndex = 3;
            // 
            // textBox_ycoord
            // 
            textBox_ycoord.Anchor = System.Windows.Forms.AnchorStyles.Top;
            textBox_ycoord.Font = new System.Drawing.Font("Consolas", 9F);
            textBox_ycoord.Location = new System.Drawing.Point(169, 22);
            textBox_ycoord.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            textBox_ycoord.Name = "textBox_ycoord";
            textBox_ycoord.Size = new System.Drawing.Size(62, 22);
            textBox_ycoord.TabIndex = 2;
            // 
            // textBox_xcoord
            // 
            textBox_xcoord.Anchor = System.Windows.Forms.AnchorStyles.Top;
            textBox_xcoord.Font = new System.Drawing.Font("Consolas", 9F);
            textBox_xcoord.Location = new System.Drawing.Point(99, 22);
            textBox_xcoord.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            textBox_xcoord.Name = "textBox_xcoord";
            textBox_xcoord.Size = new System.Drawing.Size(62, 22);
            textBox_xcoord.TabIndex = 1;
            // 
            // UAVBox
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(groupBox_main);
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "UAVBox";
            Size = new System.Drawing.Size(313, 242);
            groupBox_main.ResumeLayout(false);
            groupBox_main.PerformLayout();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox_main;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.ComboBox comboBox_aRoute;
        public System.Windows.Forms.ComboBox comboBox_dRoute;
        public System.Windows.Forms.CheckBox checkBox_target;
        public System.Windows.Forms.TextBox textBox_rot;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox textBox_zcoord;
        public System.Windows.Forms.TextBox textBox_ycoord;
        public System.Windows.Forms.TextBox textBox_xcoord;
        public System.Windows.Forms.CheckBox checkBox_docile;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.ComboBox comboBox_defense;
        public System.Windows.Forms.ComboBox comboBox_weapon;
    }
}
