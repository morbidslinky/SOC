﻿namespace SOC.QuestObjects.Animal
{
    partial class AnimalBox
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
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox_count = new System.Windows.Forms.ComboBox();
            this.comboBox_typeID = new System.Windows.Forms.ComboBox();
            this.comboBox_animal = new System.Windows.Forms.ComboBox();
            this.checkBox_target = new System.Windows.Forms.CheckBox();
            this.textBox_rot = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_zcoord = new System.Windows.Forms.TextBox();
            this.textBox_ycoord = new System.Windows.Forms.TextBox();
            this.textBox_xcoord = new System.Windows.Forms.TextBox();
            this.groupBox_main.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox_main
            // 
            this.groupBox_main.BackColor = System.Drawing.Color.DarkGray;
            this.groupBox_main.Controls.Add(this.label5);
            this.groupBox_main.Controls.Add(this.label4);
            this.groupBox_main.Controls.Add(this.label3);
            this.groupBox_main.Controls.Add(this.label2);
            this.groupBox_main.Controls.Add(this.comboBox_count);
            this.groupBox_main.Controls.Add(this.comboBox_typeID);
            this.groupBox_main.Controls.Add(this.comboBox_animal);
            this.groupBox_main.Controls.Add(this.checkBox_target);
            this.groupBox_main.Controls.Add(this.textBox_rot);
            this.groupBox_main.Controls.Add(this.label1);
            this.groupBox_main.Controls.Add(this.textBox_zcoord);
            this.groupBox_main.Controls.Add(this.textBox_ycoord);
            this.groupBox_main.Controls.Add(this.textBox_xcoord);
            this.groupBox_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox_main.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox_main.Location = new System.Drawing.Point(0, 0);
            this.groupBox_main.Name = "groupBox_main";
            this.groupBox_main.Size = new System.Drawing.Size(268, 183);
            this.groupBox_main.TabIndex = 2;
            this.groupBox_main.TabStop = false;
            this.groupBox_main.Text = "AnimalBox";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(3, 156);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 21);
            this.label5.TabIndex = 15;
            this.label5.Text = "Herd Count:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(3, 129);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 21);
            this.label4.TabIndex = 14;
            this.label4.Text = "Type ID:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(3, 102);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 21);
            this.label3.TabIndex = 13;
            this.label3.Text = "Animal:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(3, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 20);
            this.label2.TabIndex = 12;
            this.label2.Text = "Rotation:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBox_count
            // 
            this.comboBox_count.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox_count.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_count.FormattingEnabled = true;
            this.comboBox_count.Location = new System.Drawing.Point(85, 156);
            this.comboBox_count.Name = "comboBox_count";
            this.comboBox_count.Size = new System.Drawing.Size(174, 21);
            this.comboBox_count.TabIndex = 10;
            // 
            // comboBox_typeID
            // 
            this.comboBox_typeID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox_typeID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_typeID.FormattingEnabled = true;
            this.comboBox_typeID.Location = new System.Drawing.Point(85, 129);
            this.comboBox_typeID.Name = "comboBox_typeID";
            this.comboBox_typeID.Size = new System.Drawing.Size(174, 21);
            this.comboBox_typeID.TabIndex = 9;
            // 
            // comboBox_animal
            // 
            this.comboBox_animal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox_animal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_animal.FormattingEnabled = true;
            this.comboBox_animal.Location = new System.Drawing.Point(85, 102);
            this.comboBox_animal.Name = "comboBox_animal";
            this.comboBox_animal.Size = new System.Drawing.Size(174, 21);
            this.comboBox_animal.TabIndex = 8;
            this.comboBox_animal.SelectedIndexChanged += new System.EventHandler(this.comboBox_animal_selectedIndexChanged);
            // 
            // checkBox_target
            // 
            this.checkBox_target.AutoSize = true;
            this.checkBox_target.Location = new System.Drawing.Point(85, 75);
            this.checkBox_target.Name = "checkBox_target";
            this.checkBox_target.Size = new System.Drawing.Size(68, 17);
            this.checkBox_target.TabIndex = 5;
            this.checkBox_target.Text = "Is Target";
            this.checkBox_target.UseVisualStyleBackColor = true;
            // 
            // textBox_rot
            // 
            this.textBox_rot.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_rot.Location = new System.Drawing.Point(85, 45);
            this.textBox_rot.Name = "textBox_rot";
            this.textBox_rot.Size = new System.Drawing.Size(174, 20);
            this.textBox_rot.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Coordinates:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBox_zcoord
            // 
            this.textBox_zcoord.Location = new System.Drawing.Point(205, 19);
            this.textBox_zcoord.Name = "textBox_zcoord";
            this.textBox_zcoord.Size = new System.Drawing.Size(54, 20);
            this.textBox_zcoord.TabIndex = 2;
            // 
            // textBox_ycoord
            // 
            this.textBox_ycoord.Location = new System.Drawing.Point(145, 19);
            this.textBox_ycoord.Name = "textBox_ycoord";
            this.textBox_ycoord.Size = new System.Drawing.Size(54, 20);
            this.textBox_ycoord.TabIndex = 1;
            // 
            // textBox_xcoord
            // 
            this.textBox_xcoord.Location = new System.Drawing.Point(85, 19);
            this.textBox_xcoord.Name = "textBox_xcoord";
            this.textBox_xcoord.Size = new System.Drawing.Size(54, 20);
            this.textBox_xcoord.TabIndex = 0;
            // 
            // AnimalBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox_main);
            this.Name = "AnimalBox";
            this.Size = new System.Drawing.Size(268, 183);
            this.groupBox_main.ResumeLayout(false);
            this.groupBox_main.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox_main;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.ComboBox comboBox_count;
        public System.Windows.Forms.ComboBox comboBox_typeID;
        public System.Windows.Forms.ComboBox comboBox_animal;
        public System.Windows.Forms.CheckBox checkBox_target;
        public System.Windows.Forms.TextBox textBox_rot;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox textBox_zcoord;
        public System.Windows.Forms.TextBox textBox_ycoord;
        public System.Windows.Forms.TextBox textBox_xcoord;
    }
}
