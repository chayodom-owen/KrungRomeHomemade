namespace KrungRomeHomemade.Adminpage
{
    partial class SlipViewForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.picSlip = new Guna.UI2.WinForms.Guna2PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picSlip)).BeginInit();
            this.SuspendLayout();
            // 
            // picSlip
            // 
            this.picSlip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picSlip.ImageRotate = 0F;
            this.picSlip.Location = new System.Drawing.Point(0, 0);
            this.picSlip.Name = "picSlip";
            this.picSlip.Size = new System.Drawing.Size(457, 530);
            this.picSlip.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picSlip.TabIndex = 0;
            this.picSlip.TabStop = false;
            this.picSlip.Click += new System.EventHandler(this.picSlip_Click);
            // 
            // SlipViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(457, 530);
            this.Controls.Add(this.picSlip);
            this.Name = "SlipViewForm";
            this.Text = "SlipViewForm";
            ((System.ComponentModel.ISupportInitialize)(this.picSlip)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2PictureBox picSlip;
    }
}