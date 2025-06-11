namespace SOC.QuestObjects.Enemy
{
    partial class EnemyBox
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
            checkBox_balaclava = new System.Windows.Forms.CheckBox();
            checkBox_zombie = new System.Windows.Forms.CheckBox();
            groupBox_main = new System.Windows.Forms.GroupBox();
            comboBox_power = new System.Windows.Forms.ComboBox();
            button_removepower = new System.Windows.Forms.Button();
            label_power = new System.Windows.Forms.Label();
            comboBox_skill = new System.Windows.Forms.ComboBox();
            listBox_power = new System.Windows.Forms.ListBox();
            label_skill = new System.Windows.Forms.Label();
            comboBox_staff = new System.Windows.Forms.ComboBox();
            label_staff = new System.Windows.Forms.Label();
            comboBox_body = new System.Windows.Forms.ComboBox();
            comboBox_cautionroute = new System.Windows.Forms.ComboBox();
            label_cautionroute = new System.Windows.Forms.Label();
            comboBox_sneakroute = new System.Windows.Forms.ComboBox();
            label_sneakroute = new System.Windows.Forms.Label();
            label_body = new System.Windows.Forms.Label();
            checkBox_target = new System.Windows.Forms.CheckBox();
            checkBox_armor = new System.Windows.Forms.CheckBox();
            checkBox_spawn = new System.Windows.Forms.CheckBox();
            button_SneakToCaution = new System.Windows.Forms.Button();
            button_CautionToSneak = new System.Windows.Forms.Button();
            button_SwapRoutes = new System.Windows.Forms.Button();
            groupBox_main.SuspendLayout();
            SuspendLayout();
            // 
            // checkBox_balaclava
            // 
            checkBox_balaclava.AutoSize = true;
            checkBox_balaclava.Font = new System.Drawing.Font("Consolas", 9F);
            checkBox_balaclava.Location = new System.Drawing.Point(200, 46);
            checkBox_balaclava.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            checkBox_balaclava.Name = "checkBox_balaclava";
            checkBox_balaclava.Size = new System.Drawing.Size(89, 18);
            checkBox_balaclava.TabIndex = 0;
            checkBox_balaclava.TabStop = false;
            checkBox_balaclava.Text = "Balaclava";
            checkBox_balaclava.UseVisualStyleBackColor = true;
            checkBox_balaclava.Click += checkBox_balaclava_Click;
            // 
            // checkBox_zombie
            // 
            checkBox_zombie.AutoSize = true;
            checkBox_zombie.Font = new System.Drawing.Font("Consolas", 9F);
            checkBox_zombie.Location = new System.Drawing.Point(99, 47);
            checkBox_zombie.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            checkBox_zombie.Name = "checkBox_zombie";
            checkBox_zombie.Size = new System.Drawing.Size(68, 18);
            checkBox_zombie.TabIndex = 0;
            checkBox_zombie.TabStop = false;
            checkBox_zombie.Text = "Zombie";
            checkBox_zombie.UseVisualStyleBackColor = true;
            // 
            // groupBox_main
            // 
            groupBox_main.BackColor = System.Drawing.Color.DarkGray;
            groupBox_main.Controls.Add(comboBox_power);
            groupBox_main.Controls.Add(button_removepower);
            groupBox_main.Controls.Add(label_power);
            groupBox_main.Controls.Add(comboBox_skill);
            groupBox_main.Controls.Add(listBox_power);
            groupBox_main.Controls.Add(label_skill);
            groupBox_main.Controls.Add(comboBox_staff);
            groupBox_main.Controls.Add(label_staff);
            groupBox_main.Controls.Add(comboBox_body);
            groupBox_main.Controls.Add(comboBox_cautionroute);
            groupBox_main.Controls.Add(label_cautionroute);
            groupBox_main.Controls.Add(comboBox_sneakroute);
            groupBox_main.Controls.Add(label_sneakroute);
            groupBox_main.Controls.Add(label_body);
            groupBox_main.Controls.Add(checkBox_target);
            groupBox_main.Controls.Add(checkBox_armor);
            groupBox_main.Controls.Add(checkBox_spawn);
            groupBox_main.Controls.Add(checkBox_zombie);
            groupBox_main.Controls.Add(checkBox_balaclava);
            groupBox_main.Controls.Add(button_SneakToCaution);
            groupBox_main.Controls.Add(button_CautionToSneak);
            groupBox_main.Controls.Add(button_SwapRoutes);
            groupBox_main.Dock = System.Windows.Forms.DockStyle.Fill;
            groupBox_main.Location = new System.Drawing.Point(0, 0);
            groupBox_main.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox_main.Name = "groupBox_main";
            groupBox_main.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox_main.Size = new System.Drawing.Size(313, 368);
            groupBox_main.TabIndex = 0;
            groupBox_main.TabStop = false;
            groupBox_main.Text = "EnemyBox";
            // 
            // comboBox_power
            // 
            comboBox_power.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            comboBox_power.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBox_power.Font = new System.Drawing.Font("Consolas", 9F);
            comboBox_power.FormattingEnabled = true;
            comboBox_power.Location = new System.Drawing.Point(99, 147);
            comboBox_power.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            comboBox_power.Name = "comboBox_power";
            comboBox_power.Size = new System.Drawing.Size(202, 22);
            comboBox_power.TabIndex = 7;
            comboBox_power.SelectedIndexChanged += comboBox_power_selectedIndexChanged;
            // 
            // button_removepower
            // 
            button_removepower.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            button_removepower.Location = new System.Drawing.Point(238, 245);
            button_removepower.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            button_removepower.Name = "button_removepower";
            button_removepower.Size = new System.Drawing.Size(64, 27);
            button_removepower.TabIndex = 10;
            button_removepower.Text = "Remove";
            button_removepower.UseVisualStyleBackColor = true;
            button_removepower.Click += button_removepower_Click;
            // 
            // label_power
            // 
            label_power.Location = new System.Drawing.Point(4, 151);
            label_power.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label_power.Name = "label_power";
            label_power.Size = new System.Drawing.Size(89, 15);
            label_power.TabIndex = 19;
            label_power.Text = "Gear | Tactics:";
            label_power.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBox_skill
            // 
            comboBox_skill.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            comboBox_skill.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBox_skill.Font = new System.Drawing.Font("Consolas", 9F);
            comboBox_skill.FormattingEnabled = true;
            comboBox_skill.Location = new System.Drawing.Point(99, 337);
            comboBox_skill.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            comboBox_skill.Name = "comboBox_skill";
            comboBox_skill.Size = new System.Drawing.Size(202, 22);
            comboBox_skill.TabIndex = 13;
            // 
            // listBox_power
            // 
            listBox_power.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            listBox_power.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            listBox_power.Font = new System.Drawing.Font("Consolas", 9F);
            listBox_power.FormattingEnabled = true;
            listBox_power.ItemHeight = 14;
            listBox_power.Location = new System.Drawing.Point(99, 175);
            listBox_power.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            listBox_power.Name = "listBox_power";
            listBox_power.Size = new System.Drawing.Size(203, 58);
            listBox_power.TabIndex = 8;
            listBox_power.SelectedIndexChanged += listBox_power_selectedIndexChanged;
            // 
            // label_skill
            // 
            label_skill.Location = new System.Drawing.Point(4, 340);
            label_skill.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label_skill.Name = "label_skill";
            label_skill.Size = new System.Drawing.Size(89, 15);
            label_skill.TabIndex = 16;
            label_skill.Text = "Skill:";
            label_skill.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBox_staff
            // 
            comboBox_staff.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            comboBox_staff.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBox_staff.Font = new System.Drawing.Font("Consolas", 9F);
            comboBox_staff.FormattingEnabled = true;
            comboBox_staff.Location = new System.Drawing.Point(99, 308);
            comboBox_staff.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            comboBox_staff.Name = "comboBox_staff";
            comboBox_staff.Size = new System.Drawing.Size(202, 22);
            comboBox_staff.TabIndex = 12;
            // 
            // label_staff
            // 
            label_staff.Location = new System.Drawing.Point(4, 312);
            label_staff.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label_staff.Name = "label_staff";
            label_staff.Size = new System.Drawing.Size(89, 15);
            label_staff.TabIndex = 14;
            label_staff.Text = "Staff Type:";
            label_staff.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBox_body
            // 
            comboBox_body.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            comboBox_body.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBox_body.Font = new System.Drawing.Font("Consolas", 9F);
            comboBox_body.FormattingEnabled = true;
            comboBox_body.Location = new System.Drawing.Point(99, 279);
            comboBox_body.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            comboBox_body.Name = "comboBox_body";
            comboBox_body.Size = new System.Drawing.Size(202, 22);
            comboBox_body.TabIndex = 11;
            // 
            // comboBox_cautionroute
            // 
            comboBox_cautionroute.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            comboBox_cautionroute.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBox_cautionroute.Font = new System.Drawing.Font("Consolas", 9F);
            comboBox_cautionroute.FormattingEnabled = true;
            comboBox_cautionroute.Location = new System.Drawing.Point(119, 105);
            comboBox_cautionroute.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            comboBox_cautionroute.Name = "comboBox_cautionroute";
            comboBox_cautionroute.Size = new System.Drawing.Size(163, 22);
            comboBox_cautionroute.TabIndex = 4;
            // 
            // label_cautionroute
            // 
            label_cautionroute.Location = new System.Drawing.Point(4, 108);
            label_cautionroute.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label_cautionroute.Name = "label_cautionroute";
            label_cautionroute.Size = new System.Drawing.Size(91, 15);
            label_cautionroute.TabIndex = 11;
            label_cautionroute.Text = "Caution Route:";
            label_cautionroute.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBox_sneakroute
            // 
            comboBox_sneakroute.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            comboBox_sneakroute.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBox_sneakroute.Font = new System.Drawing.Font("Consolas", 9F);
            comboBox_sneakroute.FormattingEnabled = true;
            comboBox_sneakroute.Location = new System.Drawing.Point(119, 75);
            comboBox_sneakroute.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            comboBox_sneakroute.Name = "comboBox_sneakroute";
            comboBox_sneakroute.Size = new System.Drawing.Size(163, 22);
            comboBox_sneakroute.TabIndex = 3;
            // 
            // label_sneakroute
            // 
            label_sneakroute.Location = new System.Drawing.Point(4, 81);
            label_sneakroute.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label_sneakroute.Name = "label_sneakroute";
            label_sneakroute.Size = new System.Drawing.Size(91, 15);
            label_sneakroute.TabIndex = 9;
            label_sneakroute.Text = "Sneak Route:";
            label_sneakroute.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_body
            // 
            label_body.Location = new System.Drawing.Point(4, 283);
            label_body.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label_body.Name = "label_body";
            label_body.Size = new System.Drawing.Size(89, 15);
            label_body.TabIndex = 8;
            label_body.Text = "Body:";
            label_body.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // checkBox_target
            // 
            checkBox_target.AutoSize = true;
            checkBox_target.Font = new System.Drawing.Font("Consolas", 9F);
            checkBox_target.Location = new System.Drawing.Point(200, 24);
            checkBox_target.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            checkBox_target.Name = "checkBox_target";
            checkBox_target.Size = new System.Drawing.Size(89, 18);
            checkBox_target.TabIndex = 0;
            checkBox_target.TabStop = false;
            checkBox_target.Text = "Is Target";
            checkBox_target.UseVisualStyleBackColor = true;
            // 
            // checkBox_armor
            // 
            checkBox_armor.AutoSize = true;
            checkBox_armor.Font = new System.Drawing.Font("Consolas", 9F);
            checkBox_armor.Location = new System.Drawing.Point(99, 249);
            checkBox_armor.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            checkBox_armor.Name = "checkBox_armor";
            checkBox_armor.Size = new System.Drawing.Size(103, 18);
            checkBox_armor.TabIndex = 9;
            checkBox_armor.Text = "Heavy Armor";
            checkBox_armor.UseVisualStyleBackColor = true;
            checkBox_armor.Click += checkBox_armor_Click;
            // 
            // checkBox_spawn
            // 
            checkBox_spawn.AutoSize = true;
            checkBox_spawn.Font = new System.Drawing.Font("Consolas", 9F);
            checkBox_spawn.Location = new System.Drawing.Point(99, 24);
            checkBox_spawn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            checkBox_spawn.Name = "checkBox_spawn";
            checkBox_spawn.Size = new System.Drawing.Size(61, 18);
            checkBox_spawn.TabIndex = 1;
            checkBox_spawn.Text = "Spawn";
            checkBox_spawn.UseVisualStyleBackColor = true;
            checkBox_spawn.CheckedChanged += checkBox_spawn_CheckedChanged;
            // 
            // button_SneakToCaution
            // 
            button_SneakToCaution.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            button_SneakToCaution.Location = new System.Drawing.Point(286, 75);
            button_SneakToCaution.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            button_SneakToCaution.Name = "button_SneakToCaution";
            button_SneakToCaution.Size = new System.Drawing.Size(16, 27);
            button_SneakToCaution.TabIndex = 5;
            button_SneakToCaution.Text = "↓";
            button_SneakToCaution.UseVisualStyleBackColor = true;
            button_SneakToCaution.Click += SneakToCaution_Button_Clicked;
            // 
            // button_CautionToSneak
            // 
            button_CautionToSneak.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            button_CautionToSneak.Location = new System.Drawing.Point(286, 104);
            button_CautionToSneak.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            button_CautionToSneak.Name = "button_CautionToSneak";
            button_CautionToSneak.Size = new System.Drawing.Size(16, 27);
            button_CautionToSneak.TabIndex = 6;
            button_CautionToSneak.Text = "↑";
            button_CautionToSneak.UseVisualStyleBackColor = true;
            button_CautionToSneak.Click += CautionToSneak_Button_Clicked;
            // 
            // button_SwapRoutes
            // 
            button_SwapRoutes.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            button_SwapRoutes.Location = new System.Drawing.Point(99, 75);
            button_SwapRoutes.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            button_SwapRoutes.Name = "button_SwapRoutes";
            button_SwapRoutes.Size = new System.Drawing.Size(16, 55);
            button_SwapRoutes.TabIndex = 2;
            button_SwapRoutes.Text = "↕";
            button_SwapRoutes.UseVisualStyleBackColor = true;
            button_SwapRoutes.Click += SwapRoute_Button_Clicked;
            // 
            // EnemyBox
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(groupBox_main);
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "EnemyBox";
            Size = new System.Drawing.Size(313, 368);
            groupBox_main.ResumeLayout(false);
            groupBox_main.PerformLayout();
            ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.GroupBox groupBox_main;
        public System.Windows.Forms.CheckBox checkBox_target;
        public System.Windows.Forms.CheckBox checkBox_spawn;
        public System.Windows.Forms.CheckBox checkBox_balaclava;
        public System.Windows.Forms.CheckBox checkBox_zombie;
        public System.Windows.Forms.CheckBox checkBox_armor;
        public System.Windows.Forms.Label label_body;
        public System.Windows.Forms.ComboBox comboBox_body;
        public System.Windows.Forms.ComboBox comboBox_cautionroute;
        public System.Windows.Forms.Label label_cautionroute;
        public System.Windows.Forms.ComboBox comboBox_sneakroute;
        public System.Windows.Forms.Label label_sneakroute;
        public System.Windows.Forms.ComboBox comboBox_skill;
        public System.Windows.Forms.Label label_skill;
        public System.Windows.Forms.ComboBox comboBox_staff;
        public System.Windows.Forms.Label label_staff;
        public System.Windows.Forms.ComboBox comboBox_power;
        public System.Windows.Forms.Button button_removepower;
        public System.Windows.Forms.Label label_power;
        public System.Windows.Forms.ListBox listBox_power;
        public System.Windows.Forms.Button button_SneakToCaution;
        public System.Windows.Forms.Button button_CautionToSneak;
        public System.Windows.Forms.Button button_SwapRoutes;
    }
}
