namespace KrungRomeHomemade
{
    partial class Krung_Rome_Homemade
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
            this.flowProducts = new System.Windows.Forms.FlowLayoutPanel();
            this.panelMain = new Guna.UI2.WinForms.Guna2Panel();
            this.panelTop = new Guna.UI2.WinForms.Guna2Panel();
            this.btnAll = new Guna.UI2.WinForms.Guna2Button();
            this.panelCategory = new Guna.UI2.WinForms.Guna2Panel();
            this.btnSS = new Guna.UI2.WinForms.Guna2Button();
            this.btnPA = new Guna.UI2.WinForms.Guna2Button();
            this.btnAB = new Guna.UI2.WinForms.Guna2Button();
            this.btnVN = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Button2 = new Guna.UI2.WinForms.Guna2Button();
            this.btnCart = new Guna.UI2.WinForms.Guna2Button();
            this.txtSearch = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnClose = new Guna.UI2.WinForms.Guna2Button();
            this.guna2PictureBox3 = new Guna.UI2.WinForms.Guna2PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.mySqlCommand1 = new MySql.Data.MySqlClient.MySqlCommand();
            this.panelMain.SuspendLayout();
            this.panelTop.SuspendLayout();
            this.panelCategory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // flowProducts
            // 
            this.flowProducts.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(249)))), ((int)(((byte)(244)))));
            this.flowProducts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowProducts.Location = new System.Drawing.Point(0, 255);
            this.flowProducts.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.flowProducts.Name = "flowProducts";
            this.flowProducts.Padding = new System.Windows.Forms.Padding(53, 25, 53, 25);
            this.flowProducts.Size = new System.Drawing.Size(1540, 506);
            this.flowProducts.TabIndex = 2;
            this.flowProducts.Paint += new System.Windows.Forms.PaintEventHandler(this.flowProducts_Paint);
            // 
            // panelMain
            // 
            this.panelMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(243)))), ((int)(((byte)(238)))));
            this.panelMain.Controls.Add(this.flowProducts);
            this.panelMain.Controls.Add(this.panelTop);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelMain.Name = "panelMain";
            this.panelMain.ShadowDecoration.BorderRadius = 62;
            this.panelMain.Size = new System.Drawing.Size(1540, 761);
            this.panelMain.TabIndex = 3;
            this.panelMain.Paint += new System.Windows.Forms.PaintEventHandler(this.panelMain_Paint);
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(250)))), ((int)(((byte)(246)))));
            this.panelTop.BorderColor = System.Drawing.Color.White;
            this.panelTop.Controls.Add(this.btnAll);
            this.panelTop.Controls.Add(this.panelCategory);
            this.panelTop.Controls.Add(this.guna2Button2);
            this.panelTop.Controls.Add(this.btnCart);
            this.panelTop.Controls.Add(this.txtSearch);
            this.panelTop.Controls.Add(this.btnClose);
            this.panelTop.Controls.Add(this.guna2PictureBox3);
            this.panelTop.Controls.Add(this.label2);
            this.panelTop.Controls.Add(this.label1);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelTop.Name = "panelTop";
            this.panelTop.ShadowDecoration.BorderRadius = 20;
            this.panelTop.ShadowDecoration.Color = System.Drawing.Color.IndianRed;
            this.panelTop.ShadowDecoration.Enabled = true;
            this.panelTop.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(5, 5, 5, 20);
            this.panelTop.Size = new System.Drawing.Size(1540, 255);
            this.panelTop.TabIndex = 0;
            this.panelTop.Paint += new System.Windows.Forms.PaintEventHandler(this.panelTop_Paint);
            // 
            // btnAll
            // 
            this.btnAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(249)))), ((int)(((byte)(244)))));
            this.btnAll.BorderRadius = 20;
            this.btnAll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAll.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnAll.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnAll.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnAll.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnAll.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(249)))), ((int)(((byte)(244)))));
            this.btnAll.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold);
            this.btnAll.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(119)))), ((int)(((byte)(91)))));
            this.btnAll.HoverState.BorderColor = System.Drawing.Color.Transparent;
            this.btnAll.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnAll.HoverState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(60)))), ((int)(((byte)(40)))));
            this.btnAll.Location = new System.Drawing.Point(1164, 32);
            this.btnAll.Margin = new System.Windows.Forms.Padding(4);
            this.btnAll.Name = "btnAll";
            this.btnAll.Size = new System.Drawing.Size(94, 37);
            this.btnAll.TabIndex = 22;
            this.btnAll.Text = "Home";
            this.btnAll.Click += new System.EventHandler(this.btnAll_Click);
            // 
            // panelCategory
            // 
            this.panelCategory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(249)))), ((int)(((byte)(244)))));
            this.panelCategory.Controls.Add(this.btnSS);
            this.panelCategory.Controls.Add(this.btnPA);
            this.panelCategory.Controls.Add(this.btnAB);
            this.panelCategory.Controls.Add(this.btnVN);
            this.panelCategory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(252)))), ((int)(((byte)(247)))));
            this.panelCategory.Location = new System.Drawing.Point(0, 81);
            this.panelCategory.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelCategory.Name = "panelCategory";
            this.panelCategory.Size = new System.Drawing.Size(1540, 190);
            this.panelCategory.TabIndex = 1;
            this.panelCategory.Paint += new System.Windows.Forms.PaintEventHandler(this.panelCategory_Paint);
            // 
            // btnSS
            // 
            this.btnSS.BackColor = System.Drawing.Color.Transparent;
            this.btnSS.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(200)))), ((int)(((byte)(166)))));
            this.btnSS.BorderRadius = 20;
            this.btnSS.BorderThickness = 2;
            this.btnSS.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSS.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnSS.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnSS.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnSS.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnSS.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(249)))), ((int)(((byte)(244)))));
            this.btnSS.Font = new System.Drawing.Font("TA Chailai", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSS.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(79)))), ((int)(((byte)(43)))));
            this.btnSS.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(164)))), ((int)(((byte)(106)))));
            this.btnSS.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(242)))), ((int)(((byte)(227)))));
            this.btnSS.Image = global::KrungRomeHomemade.Properties.Resources.Sandwich___Savory;
            this.btnSS.ImageOffset = new System.Drawing.Point(0, 15);
            this.btnSS.ImageSize = new System.Drawing.Size(50, 50);
            this.btnSS.Location = new System.Drawing.Point(1158, 17);
            this.btnSS.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSS.Name = "btnSS";
            this.btnSS.ShadowDecoration.BorderRadius = 20;
            this.btnSS.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(191)))), ((int)(((byte)(165)))));
            this.btnSS.ShadowDecoration.Enabled = true;
            this.btnSS.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(3, 3, 3, 5);
            this.btnSS.Size = new System.Drawing.Size(319, 146);
            this.btnSS.TabIndex = 8;
            this.btnSS.Text = "Sandwich / Savory";
            this.btnSS.Tile = true;
            this.btnSS.Click += new System.EventHandler(this.btnSS_Click);
            // 
            // btnPA
            // 
            this.btnPA.BackColor = System.Drawing.Color.Transparent;
            this.btnPA.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(201)))), ((int)(((byte)(166)))));
            this.btnPA.BorderRadius = 20;
            this.btnPA.BorderThickness = 2;
            this.btnPA.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPA.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnPA.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnPA.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnPA.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnPA.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(249)))), ((int)(((byte)(244)))));
            this.btnPA.Font = new System.Drawing.Font("TA Chailai", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPA.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(79)))), ((int)(((byte)(43)))));
            this.btnPA.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(164)))), ((int)(((byte)(106)))));
            this.btnPA.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(242)))), ((int)(((byte)(227)))));
            this.btnPA.Image = global::KrungRomeHomemade.Properties.Resources.Pâtisserie;
            this.btnPA.ImageOffset = new System.Drawing.Point(0, 15);
            this.btnPA.ImageSize = new System.Drawing.Size(50, 50);
            this.btnPA.Location = new System.Drawing.Point(786, 17);
            this.btnPA.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnPA.Name = "btnPA";
            this.btnPA.ShadowDecoration.BorderRadius = 20;
            this.btnPA.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(191)))), ((int)(((byte)(165)))));
            this.btnPA.ShadowDecoration.Enabled = true;
            this.btnPA.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(3, 3, 3, 5);
            this.btnPA.Size = new System.Drawing.Size(319, 146);
            this.btnPA.TabIndex = 6;
            this.btnPA.Text = "Patisserie";
            this.btnPA.Tile = true;
            this.btnPA.Click += new System.EventHandler(this.btnPA_Click);
            // 
            // btnAB
            // 
            this.btnAB.BackColor = System.Drawing.Color.Transparent;
            this.btnAB.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(201)))), ((int)(((byte)(166)))));
            this.btnAB.BorderRadius = 20;
            this.btnAB.BorderThickness = 2;
            this.btnAB.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAB.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnAB.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnAB.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnAB.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnAB.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(249)))), ((int)(((byte)(244)))));
            this.btnAB.Font = new System.Drawing.Font("TA Chailai", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(79)))), ((int)(((byte)(43)))));
            this.btnAB.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(164)))), ((int)(((byte)(106)))));
            this.btnAB.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(242)))), ((int)(((byte)(227)))));
            this.btnAB.Image = global::KrungRomeHomemade.Properties.Resources.Artisan_Breads;
            this.btnAB.ImageOffset = new System.Drawing.Point(0, 15);
            this.btnAB.ImageSize = new System.Drawing.Size(50, 50);
            this.btnAB.Location = new System.Drawing.Point(420, 17);
            this.btnAB.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnAB.Name = "btnAB";
            this.btnAB.ShadowDecoration.BorderRadius = 20;
            this.btnAB.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(191)))), ((int)(((byte)(165)))));
            this.btnAB.ShadowDecoration.Enabled = true;
            this.btnAB.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(3, 3, 3, 5);
            this.btnAB.Size = new System.Drawing.Size(319, 146);
            this.btnAB.TabIndex = 4;
            this.btnAB.Text = "Artisan Breads";
            this.btnAB.Tile = true;
            this.btnAB.Click += new System.EventHandler(this.btnAB_Click);
            // 
            // btnVN
            // 
            this.btnVN.BackColor = System.Drawing.Color.Transparent;
            this.btnVN.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(201)))), ((int)(((byte)(166)))));
            this.btnVN.BorderRadius = 20;
            this.btnVN.BorderThickness = 2;
            this.btnVN.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnVN.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnVN.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnVN.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnVN.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnVN.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(249)))), ((int)(((byte)(244)))));
            this.btnVN.Font = new System.Drawing.Font("TA Chailai", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVN.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(79)))), ((int)(((byte)(43)))));
            this.btnVN.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(164)))), ((int)(((byte)(106)))));
            this.btnVN.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(242)))), ((int)(((byte)(227)))));
            this.btnVN.Image = global::KrungRomeHomemade.Properties.Resources.Viennoiserie;
            this.btnVN.ImageOffset = new System.Drawing.Point(0, 15);
            this.btnVN.ImageSize = new System.Drawing.Size(45, 45);
            this.btnVN.Location = new System.Drawing.Point(57, 17);
            this.btnVN.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnVN.Name = "btnVN";
            this.btnVN.ShadowDecoration.BorderRadius = 20;
            this.btnVN.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(191)))), ((int)(((byte)(165)))));
            this.btnVN.ShadowDecoration.Enabled = true;
            this.btnVN.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(3, 3, 3, 5);
            this.btnVN.Size = new System.Drawing.Size(319, 146);
            this.btnVN.TabIndex = 3;
            this.btnVN.Text = "Viennoiserie";
            this.btnVN.Tile = true;
            this.btnVN.Click += new System.EventHandler(this.btnVN_Click);
            // 
            // guna2Button2
            // 
            this.guna2Button2.BorderRadius = 10;
            this.guna2Button2.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button2.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button2.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2Button2.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2Button2.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(250)))), ((int)(((byte)(245)))));
            this.guna2Button2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2Button2.ForeColor = System.Drawing.Color.White;
            this.guna2Button2.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(192)))), ((int)(((byte)(204)))));
            this.guna2Button2.Image = global::KrungRomeHomemade.Properties.Resources.user;
            this.guna2Button2.ImageSize = new System.Drawing.Size(24, 24);
            this.guna2Button2.Location = new System.Drawing.Point(1319, 29);
            this.guna2Button2.Margin = new System.Windows.Forms.Padding(4);
            this.guna2Button2.Name = "guna2Button2";
            this.guna2Button2.Size = new System.Drawing.Size(43, 42);
            this.guna2Button2.TabIndex = 21;
            // 
            // btnCart
            // 
            this.btnCart.BorderRadius = 10;
            this.btnCart.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnCart.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnCart.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnCart.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnCart.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(250)))), ((int)(((byte)(245)))));
            this.btnCart.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnCart.ForeColor = System.Drawing.Color.White;
            this.btnCart.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(192)))), ((int)(((byte)(204)))));
            this.btnCart.Image = global::KrungRomeHomemade.Properties.Resources.ตะกร้า;
            this.btnCart.ImageSize = new System.Drawing.Size(24, 24);
            this.btnCart.Location = new System.Drawing.Point(1257, 32);
            this.btnCart.Margin = new System.Windows.Forms.Padding(4);
            this.btnCart.Name = "btnCart";
            this.btnCart.Size = new System.Drawing.Size(43, 42);
            this.btnCart.TabIndex = 20;
            this.btnCart.Click += new System.EventHandler(this.btnCart_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(220)))), ((int)(((byte)(210)))));
            this.txtSearch.BorderRadius = 12;
            this.txtSearch.BorderThickness = 2;
            this.txtSearch.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtSearch.DefaultText = "";
            this.txtSearch.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtSearch.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtSearch.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtSearch.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtSearch.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(60)))), ((int)(((byte)(40)))));
            this.txtSearch.Font = new System.Drawing.Font("Harlow Solid Italic", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.ForeColor = System.Drawing.Color.Empty;
            this.txtSearch.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(60)))), ((int)(((byte)(40)))));
            this.txtSearch.IconLeft = global::KrungRomeHomemade.Properties.Resources.ค้นหา1;
            this.txtSearch.IconLeftOffset = new System.Drawing.Point(15, 0);
            this.txtSearch.IconLeftSize = new System.Drawing.Size(25, 25);
            this.txtSearch.Location = new System.Drawing.Point(476, 28);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(60)))), ((int)(((byte)(40)))));
            this.txtSearch.PlaceholderText = "Search your sweets...";
            this.txtSearch.SelectedText = "";
            this.txtSearch.Size = new System.Drawing.Size(678, 48);
            this.txtSearch.TabIndex = 18;
            this.txtSearch.TextOffset = new System.Drawing.Point(10, 3);
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.BorderColor = System.Drawing.Color.Transparent;
            this.btnClose.BorderRadius = 15;
            this.btnClose.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnClose.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnClose.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnClose.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnClose.FillColor = System.Drawing.Color.IndianRed;
            this.btnClose.Font = new System.Drawing.Font("Torsilp Nantarat", 12F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.HoverState.BorderColor = System.Drawing.Color.White;
            this.btnClose.HoverState.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnClose.HoverState.FillColor = System.Drawing.Color.GhostWhite;
            this.btnClose.HoverState.ForeColor = System.Drawing.Color.IndianRed;
            this.btnClose.Location = new System.Drawing.Point(1386, 29);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnClose.Name = "btnClose";
            this.btnClose.PressedColor = System.Drawing.Color.Transparent;
            this.btnClose.Size = new System.Drawing.Size(62, 39);
            this.btnClose.TabIndex = 17;
            this.btnClose.Text = "X";
            this.btnClose.Click += new System.EventHandler(this.Close_Click);
            // 
            // guna2PictureBox3
            // 
            this.guna2PictureBox3.BackColor = System.Drawing.Color.Transparent;
            this.guna2PictureBox3.FillColor = System.Drawing.Color.Transparent;
            this.guna2PictureBox3.Image = global::KrungRomeHomemade.Properties.Resources.LOGO_Krung_Rome_Homemade21;
            this.guna2PictureBox3.ImageRotate = 0F;
            this.guna2PictureBox3.Location = new System.Drawing.Point(109, 25);
            this.guna2PictureBox3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.guna2PictureBox3.Name = "guna2PictureBox3";
            this.guna2PictureBox3.Size = new System.Drawing.Size(53, 49);
            this.guna2PictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.guna2PictureBox3.TabIndex = 16;
            this.guna2PictureBox3.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(60)))), ((int)(((byte)(40)))));
            this.label2.Location = new System.Drawing.Point(159, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(294, 25);
            this.label2.TabIndex = 15;
            this.label2.Text = "KrungRome Homemade Bakery";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(60)))), ((int)(((byte)(40)))));
            this.label1.Location = new System.Drawing.Point(83, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 30);
            this.label1.TabIndex = 0;
            // 
            // mySqlCommand1
            // 
            this.mySqlCommand1.CacheAge = 0;
            this.mySqlCommand1.Connection = null;
            this.mySqlCommand1.EnableCaching = false;
            this.mySqlCommand1.Transaction = null;
            // 
            // Krung_Rome_Homemade
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(1540, 761);
            this.Controls.Add(this.panelMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1942, 800);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1534, 800);
            this.Name = "Krung_Rome_Homemade";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "  ";
            this.Load += new System.EventHandler(this.KrungRomeHomemade);
            this.panelMain.ResumeLayout(false);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelCategory.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.FlowLayoutPanel flowProducts;
        private Guna.UI2.WinForms.Guna2Panel panelMain;
        private Guna.UI2.WinForms.Guna2Panel panelTop;
        private Guna.UI2.WinForms.Guna2Button btnAll;
        private Guna.UI2.WinForms.Guna2Panel panelCategory;
        private Guna.UI2.WinForms.Guna2Button btnSS;
        private Guna.UI2.WinForms.Guna2Button btnPA;
        private Guna.UI2.WinForms.Guna2Button btnAB;
        private Guna.UI2.WinForms.Guna2Button btnVN;
        private Guna.UI2.WinForms.Guna2Button guna2Button2;
        private Guna.UI2.WinForms.Guna2Button btnCart;
        private Guna.UI2.WinForms.Guna2TextBox txtSearch;
        private Guna.UI2.WinForms.Guna2Button btnClose;
        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private MySql.Data.MySqlClient.MySqlCommand mySqlCommand1;
    }
}