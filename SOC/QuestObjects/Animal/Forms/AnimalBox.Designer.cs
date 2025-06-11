namespace SOC.QuestObjects.Animal
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
            groupBox_main = new System.Windows.Forms.GroupBox();
            label5 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            comboBox_count = new System.Windows.Forms.ComboBox();
            comboBox_typeID = new System.Windows.Forms.ComboBox();
            comboBox_animal = new System.Windows.Forms.ComboBox();
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
            groupBox_main.Controls.Add(label4);
            groupBox_main.Controls.Add(label3);
            groupBox_main.Controls.Add(label2);
            groupBox_main.Controls.Add(comboBox_count);
            groupBox_main.Controls.Add(comboBox_typeID);
            groupBox_main.Controls.Add(comboBox_animal);
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
            groupBox_main.Size = new System.Drawing.Size(313, 211);
            groupBox_main.TabIndex = 2;
            groupBox_main.TabStop = false;
            groupBox_main.Text = "AnimalBox";
            // 
            // label5
            // 
            label5.Location = new System.Drawing.Point(4, 180);
            label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(89, 24);
            label5.TabIndex = 15;
            label5.Text = "Herd Count:";
            label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            label4.Location = new System.Drawing.Point(4, 149);
            label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(89, 24);
            label4.TabIndex = 14;
            label4.Text = "Type ID:";
            label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            label3.Location = new System.Drawing.Point(4, 118);
            label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(89, 24);
            label3.TabIndex = 13;
            label3.Text = "Animal:";
            label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            label2.Location = new System.Drawing.Point(4, 52);
            label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(89, 23);
            label2.TabIndex = 12;
            label2.Text = "Rotation:";
            label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBox_count
            // 
            comboBox_count.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            comboBox_count.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBox_count.Font = new System.Drawing.Font("Consolas", 9F);
            comboBox_count.FormattingEnabled = true;
            comboBox_count.Location = new System.Drawing.Point(99, 180);
            comboBox_count.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            comboBox_count.Name = "comboBox_count";
            comboBox_count.Size = new System.Drawing.Size(202, 22);
            comboBox_count.TabIndex = 7;
            // 
            // comboBox_typeID
            // 
            comboBox_typeID.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            comboBox_typeID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBox_typeID.Font = new System.Drawing.Font("Consolas", 9F);
            comboBox_typeID.FormattingEnabled = true;
            comboBox_typeID.Location = new System.Drawing.Point(99, 149);
            comboBox_typeID.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            comboBox_typeID.Name = "comboBox_typeID";
            comboBox_typeID.Size = new System.Drawing.Size(202, 22);
            comboBox_typeID.TabIndex = 6;
            // 
            // comboBox_animal
            // 
            comboBox_animal.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            comboBox_animal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBox_animal.Font = new System.Drawing.Font("Consolas", 9F);
            comboBox_animal.FormattingEnabled = true;
            comboBox_animal.Location = new System.Drawing.Point(99, 118);
            comboBox_animal.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            comboBox_animal.Name = "comboBox_animal";
            comboBox_animal.Size = new System.Drawing.Size(202, 22);
            comboBox_animal.TabIndex = 5;
            comboBox_animal.SelectedIndexChanged += comboBox_animal_selectedIndexChanged;
            // 
            // checkBox_target
            // 
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
            label1.TabIndex = 3;
            label1.Text = "Coordinates:";
            label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBox_zcoord
            // 
            textBox_zcoord.Font = new System.Drawing.Font("Consolas", 9F);
            textBox_zcoord.Location = new System.Drawing.Point(239, 22);
            textBox_zcoord.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            textBox_zcoord.Name = "textBox_zcoord";
            textBox_zcoord.Size = new System.Drawing.Size(62, 22);
            textBox_zcoord.TabIndex = 3;
            // 
            // textBox_ycoord
            // 
            textBox_ycoord.Font = new System.Drawing.Font("Consolas", 9F);
            textBox_ycoord.Location = new System.Drawing.Point(169, 22);
            textBox_ycoord.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            textBox_ycoord.Name = "textBox_ycoord";
            textBox_ycoord.Size = new System.Drawing.Size(62, 22);
            textBox_ycoord.TabIndex = 2;
            // 
            // textBox_xcoord
            // 
            textBox_xcoord.Font = new System.Drawing.Font("Consolas", 9F);
            textBox_xcoord.Location = new System.Drawing.Point(99, 22);
            textBox_xcoord.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            textBox_xcoord.Name = "textBox_xcoord";
            textBox_xcoord.Size = new System.Drawing.Size(62, 22);
            textBox_xcoord.TabIndex = 1;
            // 
            // AnimalBox
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(groupBox_main);
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "AnimalBox";
            Size = new System.Drawing.Size(313, 211);
            groupBox_main.ResumeLayout(false);
            groupBox_main.PerformLayout();
            ResumeLayout(false);

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
