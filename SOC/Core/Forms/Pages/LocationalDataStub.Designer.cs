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
            this.panelStub = new System.Windows.Forms.Panel();
            this.textBoxCoords = new ScrollPassthroughTextBox();
            this.labelStub = new System.Windows.Forms.Label();
            this.panelStub.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelStub
            // 
            this.panelStub.AutoScroll = true;
            this.panelStub.Controls.Add(this.textBoxCoords);
            this.panelStub.Controls.Add(this.labelStub);
            this.panelStub.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelStub.Location = new System.Drawing.Point(0, 0);
            this.panelStub.Name = "panelStub";
            this.panelStub.Size = new System.Drawing.Size(350, 115);
            this.panelStub.TabIndex = 24;
            // 
            // textBoxCoords
            // 
            this.textBoxCoords.AcceptsReturn = true;
            this.textBoxCoords.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxCoords.BackColor = System.Drawing.Color.Silver;
            this.textBoxCoords.Location = new System.Drawing.Point(3, 21);
            this.textBoxCoords.Multiline = true;
            this.textBoxCoords.Name = "textBoxCoords";
            this.textBoxCoords.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxCoords.Size = new System.Drawing.Size(344, 91);
            this.textBoxCoords.TabIndex = 17;
            // 
            // labelStub
            // 
            this.labelStub.AutoSize = true;
            this.labelStub.Location = new System.Drawing.Point(3, 5);
            this.labelStub.Name = "labelStub";
            this.labelStub.Size = new System.Drawing.Size(237, 13);
            this.labelStub.TabIndex = 16;
            this.labelStub.Text = "Locations: {pos={X, Y, Z},rotY=Y-Axis Rotation,},";
            // 
            // LocationalDataStub
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelStub);
            this.Name = "LocationalDataStub";
            this.Size = new System.Drawing.Size(350, 115);
            this.panelStub.ResumeLayout(false);
            this.panelStub.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel panelStub;
        public System.Windows.Forms.TextBox textBoxCoords;
        public System.Windows.Forms.Label labelStub;
    }
}
