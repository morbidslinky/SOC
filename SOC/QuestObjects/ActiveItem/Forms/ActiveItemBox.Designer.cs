namespace SOC.QuestObjects.ActiveItem
{
    partial class ActiveItemBox
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
            checkBox_target = new System.Windows.Forms.CheckBox();
            textBox_wrot = new System.Windows.Forms.TextBox();
            textBox_zrot = new System.Windows.Forms.TextBox();
            textBox_yrot = new System.Windows.Forms.TextBox();
            label5 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            comboBox_activeItem = new System.Windows.Forms.ComboBox();
            textBox_xrot = new System.Windows.Forms.TextBox();
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
            groupBox_main.Controls.Add(checkBox_target);
            groupBox_main.Controls.Add(textBox_wrot);
            groupBox_main.Controls.Add(textBox_zrot);
            groupBox_main.Controls.Add(textBox_yrot);
            groupBox_main.Controls.Add(label5);
            groupBox_main.Controls.Add(label2);
            groupBox_main.Controls.Add(comboBox_activeItem);
            groupBox_main.Controls.Add(textBox_xrot);
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
            groupBox_main.Size = new System.Drawing.Size(313, 144);
            groupBox_main.TabIndex = 4;
            groupBox_main.TabStop = false;
            groupBox_main.Text = "ItemBox";
            // 
            // checkBox_target
            // 
            checkBox_target.AutoSize = true;
            checkBox_target.Font = new System.Drawing.Font("Consolas", 9F);
            checkBox_target.Location = new System.Drawing.Point(99, 115);
            checkBox_target.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            checkBox_target.Name = "checkBox_target";
            checkBox_target.Size = new System.Drawing.Size(89, 18);
            checkBox_target.TabIndex = 0;
            checkBox_target.TabStop = false;
            checkBox_target.Text = "Is Target";
            checkBox_target.UseVisualStyleBackColor = true;
            // 
            // textBox_wrot
            // 
            textBox_wrot.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            textBox_wrot.Font = new System.Drawing.Font("Consolas", 9F);
            textBox_wrot.Location = new System.Drawing.Point(257, 52);
            textBox_wrot.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            textBox_wrot.Name = "textBox_wrot";
            textBox_wrot.Size = new System.Drawing.Size(45, 22);
            textBox_wrot.TabIndex = 7;
            // 
            // textBox_zrot
            // 
            textBox_zrot.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            textBox_zrot.Font = new System.Drawing.Font("Consolas", 9F);
            textBox_zrot.Location = new System.Drawing.Point(204, 52);
            textBox_zrot.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            textBox_zrot.Name = "textBox_zrot";
            textBox_zrot.Size = new System.Drawing.Size(45, 22);
            textBox_zrot.TabIndex = 6;
            // 
            // textBox_yrot
            // 
            textBox_yrot.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            textBox_yrot.Font = new System.Drawing.Font("Consolas", 9F);
            textBox_yrot.Location = new System.Drawing.Point(152, 52);
            textBox_yrot.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            textBox_yrot.Name = "textBox_yrot";
            textBox_yrot.Size = new System.Drawing.Size(45, 22);
            textBox_yrot.TabIndex = 5;
            // 
            // label5
            // 
            label5.Location = new System.Drawing.Point(4, 82);
            label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(89, 24);
            label5.TabIndex = 15;
            label5.Text = "Active Item:";
            label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            // comboBox_activeItem
            // 
            comboBox_activeItem.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            comboBox_activeItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBox_activeItem.Font = new System.Drawing.Font("Consolas", 9F);
            comboBox_activeItem.FormattingEnabled = true;
            comboBox_activeItem.Location = new System.Drawing.Point(99, 82);
            comboBox_activeItem.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            comboBox_activeItem.Name = "comboBox_activeItem";
            comboBox_activeItem.Size = new System.Drawing.Size(202, 22);
            comboBox_activeItem.TabIndex = 8;
            // 
            // textBox_xrot
            // 
            textBox_xrot.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            textBox_xrot.Font = new System.Drawing.Font("Consolas", 9F);
            textBox_xrot.Location = new System.Drawing.Point(99, 52);
            textBox_xrot.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            textBox_xrot.Name = "textBox_xrot";
            textBox_xrot.Size = new System.Drawing.Size(45, 22);
            textBox_xrot.TabIndex = 4;
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
            // ActiveItemBox
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(groupBox_main);
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "ActiveItemBox";
            Size = new System.Drawing.Size(313, 144);
            groupBox_main.ResumeLayout(false);
            groupBox_main.PerformLayout();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox_main;
        public System.Windows.Forms.CheckBox checkBox_target;
        public System.Windows.Forms.TextBox textBox_wrot;
        public System.Windows.Forms.TextBox textBox_zrot;
        public System.Windows.Forms.TextBox textBox_yrot;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.ComboBox comboBox_activeItem;
        public System.Windows.Forms.TextBox textBox_xrot;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox textBox_zcoord;
        public System.Windows.Forms.TextBox textBox_ycoord;
        public System.Windows.Forms.TextBox textBox_xcoord;
    }
}
