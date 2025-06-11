namespace SOC.Forms.Pages
{
    partial class LocationalDataStub
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
            panelStub = new System.Windows.Forms.Panel();
            textBoxCoords = new ScrollPassthroughTextBox();
            labelStub = new System.Windows.Forms.Label();
            panelStub.SuspendLayout();
            SuspendLayout();
            // 
            // panelStub
            // 
            panelStub.AutoScroll = true;
            panelStub.Controls.Add(textBoxCoords);
            panelStub.Controls.Add(labelStub);
            panelStub.Dock = System.Windows.Forms.DockStyle.Fill;
            panelStub.Location = new System.Drawing.Point(0, 0);
            panelStub.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panelStub.Name = "panelStub";
            panelStub.Size = new System.Drawing.Size(408, 133);
            panelStub.TabIndex = 24;
            // 
            // textBoxCoords
            // 
            textBoxCoords.AcceptsReturn = true;
            textBoxCoords.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            textBoxCoords.BackColor = System.Drawing.Color.Silver;
            textBoxCoords.Font = new System.Drawing.Font("Consolas", 9F);
            textBoxCoords.Location = new System.Drawing.Point(4, 24);
            textBoxCoords.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            textBoxCoords.Multiline = true;
            textBoxCoords.Name = "textBoxCoords";
            textBoxCoords.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            textBoxCoords.Size = new System.Drawing.Size(401, 104);
            textBoxCoords.TabIndex = 17;
            // 
            // labelStub
            // 
            labelStub.AutoSize = true;
            labelStub.Location = new System.Drawing.Point(4, 6);
            labelStub.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            labelStub.Name = "labelStub";
            labelStub.Size = new System.Drawing.Size(261, 15);
            labelStub.TabIndex = 16;
            labelStub.Text = "Locations: {pos={X, Y, Z},rotY=Y-Axis Rotation,},";
            // 
            // LocationalDataStub
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(panelStub);
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "LocationalDataStub";
            Size = new System.Drawing.Size(408, 133);
            panelStub.ResumeLayout(false);
            panelStub.PerformLayout();
            ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel panelStub;
        public System.Windows.Forms.Label labelStub;
        public ScrollPassthroughTextBox textBoxCoords;
    }
}
