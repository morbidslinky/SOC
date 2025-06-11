namespace SOC.QuestObjects.Camera
{
    partial class CameraBox
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
            checkBox_weapon = new System.Windows.Forms.CheckBox();
            label2 = new System.Windows.Forms.Label();
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
            groupBox_main.Controls.Add(checkBox_weapon);
            groupBox_main.Controls.Add(label2);
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
            groupBox_main.Size = new System.Drawing.Size(313, 113);
            groupBox_main.TabIndex = 1;
            groupBox_main.TabStop = false;
            groupBox_main.Text = "CameraBox";
            // 
            // checkBox_weapon
            // 
            checkBox_weapon.Anchor = System.Windows.Forms.AnchorStyles.Top;
            checkBox_weapon.AutoSize = true;
            checkBox_weapon.Font = new System.Drawing.Font("Consolas", 9F);
            checkBox_weapon.Location = new System.Drawing.Point(200, 87);
            checkBox_weapon.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            checkBox_weapon.Name = "checkBox_weapon";
            checkBox_weapon.Size = new System.Drawing.Size(68, 18);
            checkBox_weapon.TabIndex = 7;
            checkBox_weapon.TabStop = false;
            checkBox_weapon.Text = "Weapon";
            checkBox_weapon.UseVisualStyleBackColor = true;
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
            // CameraBox
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(groupBox_main);
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "CameraBox";
            Size = new System.Drawing.Size(313, 113);
            groupBox_main.ResumeLayout(false);
            groupBox_main.PerformLayout();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox_main;
        public System.Windows.Forms.CheckBox checkBox_weapon;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.CheckBox checkBox_target;
        public System.Windows.Forms.TextBox textBox_rot;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox textBox_zcoord;
        public System.Windows.Forms.TextBox textBox_ycoord;
        public System.Windows.Forms.TextBox textBox_xcoord;
    }
}
