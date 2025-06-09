namespace SOC.QuestObjects.GeoTrap
{
    partial class GeoTrapBox
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
            checkBoxPlayerOnlyTrigger = new System.Windows.Forms.CheckBox();
            textBox_zscale = new System.Windows.Forms.TextBox();
            textBox_yscale = new System.Windows.Forms.TextBox();
            textBox_xscale = new System.Windows.Forms.TextBox();
            label5 = new System.Windows.Forms.Label();
            radioButton_sphere = new System.Windows.Forms.RadioButton();
            radioButton_box = new System.Windows.Forms.RadioButton();
            label4 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            comboBox_geotrap = new System.Windows.Forms.ComboBox();
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
            groupBox_main.Controls.Add(checkBoxPlayerOnlyTrigger);
            groupBox_main.Controls.Add(textBox_zscale);
            groupBox_main.Controls.Add(textBox_yscale);
            groupBox_main.Controls.Add(textBox_xscale);
            groupBox_main.Controls.Add(label5);
            groupBox_main.Controls.Add(radioButton_sphere);
            groupBox_main.Controls.Add(radioButton_box);
            groupBox_main.Controls.Add(label4);
            groupBox_main.Controls.Add(label3);
            groupBox_main.Controls.Add(label2);
            groupBox_main.Controls.Add(comboBox_geotrap);
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
            groupBox_main.Size = new System.Drawing.Size(313, 206);
            groupBox_main.TabIndex = 1;
            groupBox_main.TabStop = false;
            groupBox_main.Text = "GeoTrapBox";
            // 
            // checkBoxPlayerOnlyTrigger
            // 
            checkBoxPlayerOnlyTrigger.AutoSize = true;
            checkBoxPlayerOnlyTrigger.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            checkBoxPlayerOnlyTrigger.Location = new System.Drawing.Point(99, 180);
            checkBoxPlayerOnlyTrigger.Name = "checkBoxPlayerOnlyTrigger";
            checkBoxPlayerOnlyTrigger.Size = new System.Drawing.Size(152, 19);
            checkBoxPlayerOnlyTrigger.TabIndex = 29;
            checkBoxPlayerOnlyTrigger.Text = "Only Player Can Trigger:";
            checkBoxPlayerOnlyTrigger.UseVisualStyleBackColor = true;
            checkBoxPlayerOnlyTrigger.CheckedChanged += checkBoxPlayerOnlyTrigger_CheckedChanged;
            // 
            // textBox_zscale
            // 
            textBox_zscale.Anchor = System.Windows.Forms.AnchorStyles.Top;
            textBox_zscale.Location = new System.Drawing.Point(239, 149);
            textBox_zscale.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            textBox_zscale.Name = "textBox_zscale";
            textBox_zscale.Size = new System.Drawing.Size(62, 23);
            textBox_zscale.TabIndex = 10;
            // 
            // textBox_yscale
            // 
            textBox_yscale.Anchor = System.Windows.Forms.AnchorStyles.Top;
            textBox_yscale.Location = new System.Drawing.Point(169, 149);
            textBox_yscale.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            textBox_yscale.Name = "textBox_yscale";
            textBox_yscale.Size = new System.Drawing.Size(62, 23);
            textBox_yscale.TabIndex = 9;
            // 
            // textBox_xscale
            // 
            textBox_xscale.Anchor = System.Windows.Forms.AnchorStyles.Top;
            textBox_xscale.Location = new System.Drawing.Point(99, 149);
            textBox_xscale.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            textBox_xscale.Name = "textBox_xscale";
            textBox_xscale.Size = new System.Drawing.Size(62, 23);
            textBox_xscale.TabIndex = 8;
            // 
            // label5
            // 
            label5.Location = new System.Drawing.Point(4, 84);
            label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(89, 23);
            label5.TabIndex = 28;
            label5.Text = "Shape:";
            label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // radioButton_sphere
            // 
            radioButton_sphere.AutoSize = true;
            radioButton_sphere.Location = new System.Drawing.Point(200, 87);
            radioButton_sphere.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            radioButton_sphere.Name = "radioButton_sphere";
            radioButton_sphere.Size = new System.Drawing.Size(61, 19);
            radioButton_sphere.TabIndex = 6;
            radioButton_sphere.TabStop = true;
            radioButton_sphere.Text = "Sphere";
            radioButton_sphere.UseVisualStyleBackColor = true;
            // 
            // radioButton_box
            // 
            radioButton_box.AutoSize = true;
            radioButton_box.Location = new System.Drawing.Point(99, 87);
            radioButton_box.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            radioButton_box.Name = "radioButton_box";
            radioButton_box.Size = new System.Drawing.Size(45, 19);
            radioButton_box.TabIndex = 5;
            radioButton_box.TabStop = true;
            radioButton_box.Text = "Box";
            radioButton_box.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            label4.Location = new System.Drawing.Point(4, 149);
            label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(89, 24);
            label4.TabIndex = 25;
            label4.Text = "Scale:";
            label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            label3.Location = new System.Drawing.Point(4, 118);
            label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(89, 24);
            label3.TabIndex = 24;
            label3.Text = "GeoTrap:";
            label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            label2.Location = new System.Drawing.Point(4, 52);
            label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(89, 23);
            label2.TabIndex = 23;
            label2.Text = "Rotation:";
            label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBox_geotrap
            // 
            comboBox_geotrap.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            comboBox_geotrap.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBox_geotrap.FormattingEnabled = true;
            comboBox_geotrap.Location = new System.Drawing.Point(99, 118);
            comboBox_geotrap.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            comboBox_geotrap.Name = "comboBox_geotrap";
            comboBox_geotrap.Size = new System.Drawing.Size(202, 23);
            comboBox_geotrap.TabIndex = 7;
            comboBox_geotrap.SelectedIndexChanged += comboBox_geotrap_SelectedIndexChanged;
            // 
            // textBox_rot
            // 
            textBox_rot.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            textBox_rot.Location = new System.Drawing.Point(99, 52);
            textBox_rot.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            textBox_rot.Name = "textBox_rot";
            textBox_rot.Size = new System.Drawing.Size(202, 23);
            textBox_rot.TabIndex = 4;
            // 
            // label1
            // 
            label1.Location = new System.Drawing.Point(4, 22);
            label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(89, 23);
            label1.TabIndex = 18;
            label1.Text = "Coordinates:";
            label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBox_zcoord
            // 
            textBox_zcoord.Anchor = System.Windows.Forms.AnchorStyles.Top;
            textBox_zcoord.Location = new System.Drawing.Point(239, 22);
            textBox_zcoord.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            textBox_zcoord.Name = "textBox_zcoord";
            textBox_zcoord.Size = new System.Drawing.Size(62, 23);
            textBox_zcoord.TabIndex = 3;
            // 
            // textBox_ycoord
            // 
            textBox_ycoord.Anchor = System.Windows.Forms.AnchorStyles.Top;
            textBox_ycoord.Location = new System.Drawing.Point(169, 22);
            textBox_ycoord.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            textBox_ycoord.Name = "textBox_ycoord";
            textBox_ycoord.Size = new System.Drawing.Size(62, 23);
            textBox_ycoord.TabIndex = 2;
            // 
            // textBox_xcoord
            // 
            textBox_xcoord.Anchor = System.Windows.Forms.AnchorStyles.Top;
            textBox_xcoord.Location = new System.Drawing.Point(99, 22);
            textBox_xcoord.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            textBox_xcoord.Name = "textBox_xcoord";
            textBox_xcoord.Size = new System.Drawing.Size(62, 23);
            textBox_xcoord.TabIndex = 1;
            // 
            // GeoTrapBox
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(groupBox_main);
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "GeoTrapBox";
            Size = new System.Drawing.Size(313, 206);
            groupBox_main.ResumeLayout(false);
            groupBox_main.PerformLayout();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox_main;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.ComboBox comboBox_geotrap;
        public System.Windows.Forms.TextBox textBox_rot;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox textBox_zcoord;
        public System.Windows.Forms.TextBox textBox_ycoord;
        public System.Windows.Forms.TextBox textBox_xcoord;
        public System.Windows.Forms.TextBox textBox_zscale;
        public System.Windows.Forms.TextBox textBox_yscale;
        public System.Windows.Forms.TextBox textBox_xscale;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.RadioButton radioButton_box;
        public System.Windows.Forms.RadioButton radioButton_sphere;
        public System.Windows.Forms.CheckBox checkBoxPlayerOnlyTrigger;
    }
}
