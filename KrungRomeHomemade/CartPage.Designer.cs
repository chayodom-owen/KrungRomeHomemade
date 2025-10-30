namespace KrungRomeHomemade
{
    partial class CartPage
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
            this.btnBack = new Guna.UI2.WinForms.Guna2Button();
            this.panelLeft = new Guna.UI2.WinForms.Guna2Panel();
            this.flowCartItems = new System.Windows.Forms.FlowLayoutPanel();
            this.lblCartTitle = new System.Windows.Forms.Label();
            this.panelItem1 = new Guna.UI2.WinForms.Guna2Panel();
            this.lblPrice = new System.Windows.Forms.Label();
            this.lblQty = new System.Windows.Forms.Label();
            this.btnPlus = new Guna.UI2.WinForms.Guna2Button();
            this.btnMinus = new Guna.UI2.WinForms.Guna2Button();
            this.lblDesc = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.picProduct = new Guna.UI2.WinForms.Guna2PictureBox();
            this.panelRight = new Guna.UI2.WinForms.Guna2Panel();
            this.btnContinue = new Guna.UI2.WinForms.Guna2Button();
            this.btnCheckout = new Guna.UI2.WinForms.Guna2Button();
            this.lblSubtotalValue = new System.Windows.Forms.Label();
            this.lblSubtotalText = new System.Windows.Forms.Label();
            this.lblSummaryTitle = new System.Windows.Forms.Label();
            this.panelLeft.SuspendLayout();
            this.flowCartItems.SuspendLayout();
            this.panelItem1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picProduct)).BeginInit();
            this.panelRight.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnBack
            // 
            this.btnBack.BorderRadius = 15;
            this.btnBack.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnBack.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnBack.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnBack.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnBack.FillColor = System.Drawing.Color.Transparent;
            this.btnBack.Font = new System.Drawing.Font("FC Issara [Non-commercial] Semi", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBack.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(46)))), ((int)(((byte)(30)))));
            this.btnBack.Location = new System.Drawing.Point(31, 23);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(235, 41);
            this.btnBack.TabIndex = 0;
            this.btnBack.Text = "<- กลับไปเลือกซื้อสินค้า";
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // panelLeft
            // 
            this.panelLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.panelLeft.Controls.Add(this.flowCartItems);
            this.panelLeft.Controls.Add(this.btnBack);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeft.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(46)))), ((int)(((byte)(30)))));
            this.panelLeft.Location = new System.Drawing.Point(0, 0);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Padding = new System.Windows.Forms.Padding(40, 70, 40, 20);
            this.panelLeft.Size = new System.Drawing.Size(906, 687);
            this.panelLeft.TabIndex = 1;
            this.panelLeft.Paint += new System.Windows.Forms.PaintEventHandler(this.panelLeft_Paint);
            // 
            // flowCartItems
            // 
            this.flowCartItems.AutoScroll = true;
            this.flowCartItems.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.flowCartItems.Controls.Add(this.lblCartTitle);
            this.flowCartItems.Controls.Add(this.panelItem1);
            this.flowCartItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowCartItems.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowCartItems.Location = new System.Drawing.Point(40, 70);
            this.flowCartItems.Name = "flowCartItems";
            this.flowCartItems.Padding = new System.Windows.Forms.Padding(50, 20, 10, 20);
            this.flowCartItems.Size = new System.Drawing.Size(826, 597);
            this.flowCartItems.TabIndex = 3;
            this.flowCartItems.WrapContents = false;
            this.flowCartItems.Paint += new System.Windows.Forms.PaintEventHandler(this.flowCartItems_Paint);
            // 
            // lblCartTitle
            // 
            this.lblCartTitle.AutoSize = true;
            this.lblCartTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblCartTitle.Font = new System.Drawing.Font("FC Issara [Non-commercial] Semi", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCartTitle.Location = new System.Drawing.Point(53, 20);
            this.lblCartTitle.Name = "lblCartTitle";
            this.lblCartTitle.Padding = new System.Windows.Forms.Padding(0, 0, 0, 20);
            this.lblCartTitle.Size = new System.Drawing.Size(213, 75);
            this.lblCartTitle.TabIndex = 1;
            this.lblCartTitle.Text = "ตะกร้าสินค้า";
            // 
            // panelItem1
            // 
            this.panelItem1.BackColor = System.Drawing.Color.Transparent;
            this.panelItem1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(64)))), ((int)(((byte)(51)))));
            this.panelItem1.BorderRadius = 20;
            this.panelItem1.BorderThickness = 1;
            this.panelItem1.Controls.Add(this.lblPrice);
            this.panelItem1.Controls.Add(this.lblQty);
            this.panelItem1.Controls.Add(this.btnPlus);
            this.panelItem1.Controls.Add(this.btnMinus);
            this.panelItem1.Controls.Add(this.lblDesc);
            this.panelItem1.Controls.Add(this.lblName);
            this.panelItem1.Controls.Add(this.picProduct);
            this.panelItem1.Location = new System.Drawing.Point(53, 98);
            this.panelItem1.Name = "panelItem1";
            this.panelItem1.Padding = new System.Windows.Forms.Padding(0, 15, 0, 15);
            this.panelItem1.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(203)))), ((int)(((byte)(194)))));
            this.panelItem1.Size = new System.Drawing.Size(735, 140);
            this.panelItem1.TabIndex = 2;
            this.panelItem1.Paint += new System.Windows.Forms.PaintEventHandler(this.panelItem1_Paint);
            // 
            // lblPrice
            // 
            this.lblPrice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPrice.AutoSize = true;
            this.lblPrice.BackColor = System.Drawing.Color.Transparent;
            this.lblPrice.Font = new System.Drawing.Font("FC Issara [Non-commercial] Semi", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(138)))), ((int)(((byte)(84)))));
            this.lblPrice.Location = new System.Drawing.Point(628, 91);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(70, 24);
            this.lblPrice.TabIndex = 8;
            this.lblPrice.Text = "฿130.00";
            this.lblPrice.Click += new System.EventHandler(this.lblPrice_Click);
            // 
            // lblQty
            // 
            this.lblQty.AutoSize = true;
            this.lblQty.BackColor = System.Drawing.Color.Transparent;
            this.lblQty.Font = new System.Drawing.Font("FC Issara [Non-commercial] Semi", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQty.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(46)))), ((int)(((byte)(30)))));
            this.lblQty.Location = new System.Drawing.Point(204, 91);
            this.lblQty.Name = "lblQty";
            this.lblQty.Size = new System.Drawing.Size(19, 24);
            this.lblQty.TabIndex = 7;
            this.lblQty.Text = "2";
            this.lblQty.Click += new System.EventHandler(this.lblQty_Click);
            // 
            // btnPlus
            // 
            this.btnPlus.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(64)))), ((int)(((byte)(51)))));
            this.btnPlus.BorderRadius = 8;
            this.btnPlus.BorderThickness = 1;
            this.btnPlus.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnPlus.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnPlus.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnPlus.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnPlus.FillColor = System.Drawing.Color.White;
            this.btnPlus.Font = new System.Drawing.Font("FC Issara [Non-commercial] Semi", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPlus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(108)))), ((int)(((byte)(46)))));
            this.btnPlus.Location = new System.Drawing.Point(238, 88);
            this.btnPlus.Name = "btnPlus";
            this.btnPlus.Size = new System.Drawing.Size(30, 30);
            this.btnPlus.TabIndex = 6;
            this.btnPlus.Text = "+";
            this.btnPlus.Click += new System.EventHandler(this.btnPlus_Click);
            // 
            // btnMinus
            // 
            this.btnMinus.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(64)))), ((int)(((byte)(51)))));
            this.btnMinus.BorderRadius = 8;
            this.btnMinus.BorderThickness = 1;
            this.btnMinus.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnMinus.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnMinus.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnMinus.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnMinus.FillColor = System.Drawing.Color.White;
            this.btnMinus.Font = new System.Drawing.Font("FC Issara [Non-commercial] Semi", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMinus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(108)))), ((int)(((byte)(46)))));
            this.btnMinus.Location = new System.Drawing.Point(154, 88);
            this.btnMinus.Name = "btnMinus";
            this.btnMinus.Size = new System.Drawing.Size(30, 30);
            this.btnMinus.TabIndex = 5;
            this.btnMinus.Text = "-";
            this.btnMinus.Click += new System.EventHandler(this.btnMinus_Click);
            // 
            // lblDesc
            // 
            this.lblDesc.AutoSize = true;
            this.lblDesc.BackColor = System.Drawing.Color.Transparent;
            this.lblDesc.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDesc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(64)))), ((int)(((byte)(51)))));
            this.lblDesc.Location = new System.Drawing.Point(160, 45);
            this.lblDesc.Name = "lblDesc";
            this.lblDesc.Size = new System.Drawing.Size(289, 15);
            this.lblDesc.TabIndex = 4;
            this.lblDesc.Text = "Buttery flaky French croissant made with pure butter";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.BackColor = System.Drawing.Color.Transparent;
            this.lblName.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(150, 20);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(157, 21);
            this.lblName.TabIndex = 3;
            this.lblName.Text = "Croissant au Beurre";
            // 
            // picProduct
            // 
            this.picProduct.BorderRadius = 10;
            this.picProduct.ImageRotate = 0F;
            this.picProduct.Location = new System.Drawing.Point(15, 10);
            this.picProduct.Name = "picProduct";
            this.picProduct.Size = new System.Drawing.Size(120, 120);
            this.picProduct.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picProduct.TabIndex = 0;
            this.picProduct.TabStop = false;
            this.picProduct.Click += new System.EventHandler(this.picProduct_Click);
            // 
            // panelRight
            // 
            this.panelRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panelRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.panelRight.BorderColor = System.Drawing.Color.Black;
            this.panelRight.BorderRadius = 20;
            this.panelRight.BorderThickness = 1;
            this.panelRight.Controls.Add(this.btnContinue);
            this.panelRight.Controls.Add(this.btnCheckout);
            this.panelRight.Controls.Add(this.lblSubtotalValue);
            this.panelRight.Controls.Add(this.lblSubtotalText);
            this.panelRight.Controls.Add(this.lblSummaryTitle);
            this.panelRight.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(64)))), ((int)(((byte)(51)))));
            this.panelRight.Location = new System.Drawing.Point(958, 90);
            this.panelRight.Name = "panelRight";
            this.panelRight.Padding = new System.Windows.Forms.Padding(20);
            this.panelRight.Size = new System.Drawing.Size(431, 314);
            this.panelRight.TabIndex = 2;
            this.panelRight.Paint += new System.Windows.Forms.PaintEventHandler(this.panelRight_Paint);
            // 
            // btnContinue
            // 
            this.btnContinue.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(138)))), ((int)(((byte)(84)))));
            this.btnContinue.BorderRadius = 20;
            this.btnContinue.BorderThickness = 1;
            this.btnContinue.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnContinue.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnContinue.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnContinue.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnContinue.FillColor = System.Drawing.Color.White;
            this.btnContinue.Font = new System.Drawing.Font("TA Chailai", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnContinue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(64)))), ((int)(((byte)(51)))));
            this.btnContinue.Location = new System.Drawing.Point(76, 233);
            this.btnContinue.Name = "btnContinue";
            this.btnContinue.Size = new System.Drawing.Size(284, 44);
            this.btnContinue.TabIndex = 8;
            this.btnContinue.Text = "เลือกซื้อสินค้าเพิ่มเติม";
            this.btnContinue.Click += new System.EventHandler(this.btnContinue_Click);
            // 
            // btnCheckout
            // 
            this.btnCheckout.BorderRadius = 20;
            this.btnCheckout.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnCheckout.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnCheckout.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnCheckout.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnCheckout.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(138)))), ((int)(((byte)(84)))));
            this.btnCheckout.Font = new System.Drawing.Font("TA Chailai", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCheckout.ForeColor = System.Drawing.Color.White;
            this.btnCheckout.Location = new System.Drawing.Point(76, 174);
            this.btnCheckout.Name = "btnCheckout";
            this.btnCheckout.Size = new System.Drawing.Size(284, 44);
            this.btnCheckout.TabIndex = 7;
            this.btnCheckout.Text = "ดำเนินการชำระเงิน";
            this.btnCheckout.Click += new System.EventHandler(this.btnCheckout_Click);
            // 
            // lblSubtotalValue
            // 
            this.lblSubtotalValue.AutoSize = true;
            this.lblSubtotalValue.BackColor = System.Drawing.Color.Transparent;
            this.lblSubtotalValue.Font = new System.Drawing.Font("FC Issara [Non-commercial] Semi", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtotalValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(64)))), ((int)(((byte)(51)))));
            this.lblSubtotalValue.Location = new System.Drawing.Point(262, 108);
            this.lblSubtotalValue.Name = "lblSubtotalValue";
            this.lblSubtotalValue.Size = new System.Drawing.Size(105, 31);
            this.lblSubtotalValue.TabIndex = 4;
            this.lblSubtotalValue.Text = "฿ 280.00";
            this.lblSubtotalValue.Click += new System.EventHandler(this.lblSubtotalValue_Click);
            // 
            // lblSubtotalText
            // 
            this.lblSubtotalText.AutoSize = true;
            this.lblSubtotalText.BackColor = System.Drawing.Color.Transparent;
            this.lblSubtotalText.Font = new System.Drawing.Font("FC Issara [Non-commercial] Semi", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtotalText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(64)))), ((int)(((byte)(51)))));
            this.lblSubtotalText.Location = new System.Drawing.Point(65, 108);
            this.lblSubtotalText.Name = "lblSubtotalText";
            this.lblSubtotalText.Size = new System.Drawing.Size(142, 31);
            this.lblSubtotalText.TabIndex = 3;
            this.lblSubtotalText.Text = "ราคารวมสินค้า";
            // 
            // lblSummaryTitle
            // 
            this.lblSummaryTitle.AutoSize = true;
            this.lblSummaryTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblSummaryTitle.Font = new System.Drawing.Font("FC Issara [Non-commercial] Semi", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSummaryTitle.Location = new System.Drawing.Point(23, 20);
            this.lblSummaryTitle.Name = "lblSummaryTitle";
            this.lblSummaryTitle.Size = new System.Drawing.Size(250, 55);
            this.lblSummaryTitle.TabIndex = 2;
            this.lblSummaryTitle.Text = "สรุปยอดสั่งซื้อ";
            // 
            // CartPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.ClientSize = new System.Drawing.Size(1500, 687);
            this.Controls.Add(this.panelRight);
            this.Controls.Add(this.panelLeft);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CartPage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " ";
            this.Load += new System.EventHandler(this.CartPage_Load);
            this.panelLeft.ResumeLayout(false);
            this.flowCartItems.ResumeLayout(false);
            this.flowCartItems.PerformLayout();
            this.panelItem1.ResumeLayout(false);
            this.panelItem1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picProduct)).EndInit();
            this.panelRight.ResumeLayout(false);
            this.panelRight.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Button btnBack;
        private Guna.UI2.WinForms.Guna2Panel panelLeft;
        private Guna.UI2.WinForms.Guna2Panel panelRight;
        private System.Windows.Forms.Label lblCartTitle;
        private System.Windows.Forms.Label lblSummaryTitle;
        private System.Windows.Forms.Label lblSubtotalValue;
        private System.Windows.Forms.Label lblSubtotalText;
        private Guna.UI2.WinForms.Guna2Button btnContinue;
        private Guna.UI2.WinForms.Guna2Button btnCheckout;
        private Guna.UI2.WinForms.Guna2Panel panelItem1;
        private Guna.UI2.WinForms.Guna2PictureBox picProduct;
        private System.Windows.Forms.Label lblDesc;
        private System.Windows.Forms.Label lblName;
        private Guna.UI2.WinForms.Guna2Button btnMinus;
        private System.Windows.Forms.Label lblQty;
        private Guna.UI2.WinForms.Guna2Button btnPlus;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.FlowLayoutPanel flowCartItems;
    }
}