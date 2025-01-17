namespace SOC.UI
{
    partial class DetailsDisplay
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
            this.flowPanelDetails = new System.Windows.Forms.FlowLayoutPanel();
            this.labelFlowHeight = new System.Windows.Forms.Label();
            this.flowPanelDetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowPanelDetails
            // 
            this.flowPanelDetails.AutoScroll = true;
            this.flowPanelDetails.Controls.Add(this.labelFlowHeight);
            this.flowPanelDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowPanelDetails.Location = new System.Drawing.Point(0, 0);
            this.flowPanelDetails.Name = "flowPanelDetails";
            this.flowPanelDetails.Size = new System.Drawing.Size(267, 452);
            this.flowPanelDetails.TabIndex = 0;
            this.flowPanelDetails.WrapContents = false;
            this.flowPanelDetails.Layout += new System.Windows.Forms.LayoutEventHandler(this.flowPanelDetails_Layout);
            // 
            // labelFlowHeight
            // 
            this.labelFlowHeight.Location = new System.Drawing.Point(0, 0);
            this.labelFlowHeight.Margin = new System.Windows.Forms.Padding(0);
            this.labelFlowHeight.Name = "labelFlowHeight";
            this.labelFlowHeight.Size = new System.Drawing.Size(0, 452);
            this.labelFlowHeight.TabIndex = 0;
            // 
            // DetailsDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flowPanelDetails);
            this.Name = "DetailsDisplay";
            this.Size = new System.Drawing.Size(267, 452);
            this.flowPanelDetails.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowPanelDetails;
        private System.Windows.Forms.Label labelFlowHeight;
    }
}
