using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;          // ⬅️ เพิ่มที่บนไฟล์
using System.Globalization;

namespace KrungRomeHomemade
{
    public partial class Krung_Rome_Homemade : Form
    {
        public Krung_Rome_Homemade()
        {
            InitializeComponent();
            // ✅ ป้องกันอาการกระพริบเมื่อเลื่อน FlowLayoutPanel
            typeof(Panel).InvokeMember("DoubleBuffered",
                System.Reflection.BindingFlags.SetProperty |
                System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.NonPublic,
                null, flowProducts, new object[] { true });
            this.Load += Krung_Rome_Homemade_Load; // ✅ โหลดตอนเปิดหน้า
        }


        private void KrungRomeHomemade(object sender, EventArgs e)
        {

        }

        private void Close_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "คุณต้องการปิดโปรแกรม กรุงโรมโฮมเมด จริงหรือไม่?",
                "ยืนยันการปิดโปรแกรม",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit(); // ✅ ปิดโปรแกรมทั้งหมด
            }
        }

        private void panelTop_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }


        private void panelMain_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2PictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void flowProducts_Paint(object sender, PaintEventArgs e)
        {

        }

        // ✅ ฟังก์ชันเมื่อโหลดหน้า Home
        private void Krung_Rome_Homemade_Load(object sender, EventArgs e)
        {
            // ✅ กำหนดขนาดหน้าจอให้เท่าเดิมเสมอ
            this.Width = 1500;
            this.Height = 800;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = true;

            // ✅ จัดให้อยู่ตรงกลางจอแน่นอน
            this.StartPosition = FormStartPosition.Manual;
            Rectangle screen = Screen.FromPoint(Cursor.Position).WorkingArea;
            this.Location = new Point(
                (screen.Width - this.Width) / 2,
                (screen.Height - this.Height) / 2
            );

            // ✅ ตั้งชื่อ/หัวข้อ
            this.Text = "KrungRome Homemade Bakery";

            // ✅ เพิ่มหัวข้อหน้า Home
            Label lblHeading = new Label();
            lblHeading.Text = "Fresh Baked Daily with Love";
            lblHeading.Font = new Font("Segoe UI", 22, FontStyle.Bold);
            lblHeading.ForeColor = Color.FromArgb(80, 50, 30);
            lblHeading.AutoSize = true;
            lblHeading.TextAlign = ContentAlignment.MiddleCenter;
            lblHeading.Location = new Point((this.ClientSize.Width - 500) / 2, 250);
            lblHeading.Anchor = AnchorStyles.Top;
            this.Controls.Add(lblHeading);



            Label lblDesc = new Label();
            lblDesc.Text = "Discover our artisan selection of French pastries, breads, and desserts — handcrafted using traditional techniques and the finest ingredients.";
            lblDesc.Font = new Font("Segoe UI", 10);
            lblDesc.ForeColor = Color.FromArgb(110, 80, 60);
            lblDesc.AutoSize = false;
            lblDesc.Size = new Size(this.ClientSize.Width - 200, 60);
            lblDesc.TextAlign = ContentAlignment.MiddleCenter;
            lblDesc.Location = new Point(100, 300);
            lblDesc.Anchor = AnchorStyles.Top;
            this.Controls.Add(lblDesc);


            LoadProductCards();

            try
            {
                using (var conn = new MySql.Data.MySqlClient.MySqlConnection("server=localhost;user id=root;password=;database=krungrome_db;"))
                {
                    conn.Open();
                    MessageBox.Show("✅ เชื่อมต่อฐานข้อมูลสำเร็จ!");
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ เชื่อมต่อฐานข้อมูลไม่สำเร็จ\n" + ex.Message);
            }
        }



        // ✅ โหลดสินค้าจากฐานข้อมูล
        private void LoadProductCards()
        {
            flowProducts.SuspendLayout();
            flowProducts.Controls.Clear();
            flowProducts.FlowDirection = FlowDirection.LeftToRight;
            flowProducts.WrapContents = true;
            flowProducts.AutoScroll = true;
            flowProducts.BackColor = ColorTranslator.FromHtml("#FFF9F4");
            flowProducts.Padding = new Padding(40, 30, 40, 30);

            string cs = "server=localhost;user id=root;password=;database=krungrome_db;";
            using (var conn = new MySql.Data.MySqlClient.MySqlConnection(cs))
            {
                conn.Open();
                string sql = "SELECT name, price, image, description FROM products ORDER BY product_id ASC";

                using (var cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, conn))
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        var card = new Guna.UI2.WinForms.Guna2ShadowPanel
                        {
                            Width = 260,
                            Height = 500,
                            BackColor = Color.Transparent,
                            FillColor = Color.White,
                            Radius = 7,
                            ShadowColor = Color.FromArgb(242, 238, 232),
                            ShadowDepth = 6,
                            ShadowShift = 3,
                            ShadowStyle = Guna.UI2.WinForms.Guna2ShadowPanel.ShadowMode.ForwardDiagonal,
                            Margin = new Padding(35, 25, 35, 30)
                        };

                        var img = BytesToImage(rd["image"]);
                        img = CropToAspect(img, 1.0 / 1.0);

                        var pic = new Guna.UI2.WinForms.Guna2PictureBox
                        {
                            Image = img,
                            Width = 230,
                            Height = 230,
                            SizeMode = PictureBoxSizeMode.Zoom,
                            BorderRadius = 6,
                            UseTransparentBackground = true,
                            BackColor = Color.White,
                            Location = new Point((card.Width - 232) / 2,15)
                        };

                        var name = new Label
                        {
                            Text = (rd["name"]?.ToString() ?? "").Trim(),
                            Font = new Font("TA Chailai", 22, FontStyle.Bold),
                            ForeColor = Color.FromArgb(90, 60, 40),
                            TextAlign = ContentAlignment.MiddleCenter,
                            AutoSize = false,
                            Size = new Size(card.Width, 40),
                            Location = new Point(0, pic.Bottom + 15)
                        };

                        // 🧁 ข้อความคำอธิบายสินค้า (description)
                        string descText = (rd["description"]?.ToString() ?? "").Trim();

                        // ✅ ตัดเฉพาะกรณียาวมากจริง ๆ
                        if (descText.Length > 300)
                            descText = descText.Substring(0, 297).Trim() + "...";

                        // ✅ สร้าง Label คำอธิบายสินค้า
                        var desc = new Label
                        {
                            Text = descText,
                            Font = new Font("Segoe UI", 9.5f, FontStyle.Regular),
                            ForeColor = Color.FromArgb(130, 100, 70),
                            TextAlign = ContentAlignment.TopCenter,
                            AutoSize = false, // ❌ ปิด AutoSize
                            Size = new Size(card.Width - 30, 70), // ✅ กำหนดความสูงเท่ากันทุกใบ
                            Location = new Point(15, name.Bottom + 0),
                            BackColor = Color.Transparent
                        };
                        desc.UseCompatibleTextRendering = true;
                        desc.AutoEllipsis = true; // ✅ แสดง "..." ถ้าเกินความสูง


                        // 🏷️ ราคาสินค้า (มีแค่บล็อกนี้พอ)
                        decimal.TryParse(rd["price"]?.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal price);
                        var priceLabel = new Label
                        {
                            Text = $"฿ {price:N0}",
                            Font = new Font("TA Chailai", 25, FontStyle.Bold),
                            ForeColor = Color.FromArgb(170, 110, 50),
                            TextAlign = ContentAlignment.MiddleCenter,
                            AutoSize = false,
                            Size = new Size(card.Width, 35),
                            Location = new Point(0, desc.Bottom + 10) // ✅ ทุกใบจะอยู่ระดับเดียวกัน
                        };


                        var btnAdd = new Guna.UI2.WinForms.Guna2Button
                        {
                            Text = "🛒  Add to Cart",
                            Height = 38,
                            Width = 200,
                            BorderRadius = 18,
                            FillColor = Color.FromArgb(200, 150, 90),
                            Font = new Font("TA Chailai", 18.5f),
                            ForeColor = Color.White,
                            Cursor = Cursors.Hand,
                            Location = new Point((card.Width - 200) / 2, priceLabel.Bottom + 15)
                        };


                        // ✅ เพิ่มเรียงลำดับถูกต้อง (รูปอยู่บนสุด)
                        card.Controls.Add(pic);
                        card.Controls.Add(name);
                        card.Controls.Add(desc);
                        card.Controls.Add(priceLabel);
                        card.Controls.Add(btnAdd);
                        pic.BringToFront();

                        flowProducts.Controls.Add(card);
                    }

                }
            }

            flowProducts.ResumeLayout();

            // ✅ จัดกึ่งกลางสินค้าให้แน่นอน
            flowProducts.Layout += (s, e) =>
            {
                int cardWidth = 240;
                int spacing = 30;
                int totalCardWidth = cardWidth + spacing;
                int visibleWidth = flowProducts.ClientSize.Width;

                int numPerRow = Math.Max(1, visibleWidth / totalCardWidth);
                int usedWidth = numPerRow * totalCardWidth - spacing;
                int padding = Math.Max(0, (visibleWidth - usedWidth) / 2);

                flowProducts.Padding = new Padding(padding, 30, padding, 30);
            };
        }

        // ✅ แปลง byte[] → Image
        private Image BytesToImage(object dbValue)
        {
            if (dbValue == null || dbValue is DBNull)
                return null;

            var bytes = (byte[])dbValue;

            // 🔸 ถ้าไม่มีข้อมูลเลย
            if (bytes.Length == 0)
                return null;

            try
            {
                using (var ms = new System.IO.MemoryStream(bytes))
                {
                    return Image.FromStream(ms);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️ ไม่สามารถอ่านภาพได้: {ex.Message}");
                return null; // คืนค่า null แทนการ throw error
            }
        }


        // ✅ ฟังก์ชันครอบภาพให้อัตราส่วนเท่ากันทุกภาพ (4:5)
        private Image CropToAspect(Image img, double targetRatio = 4.0 / 5.0)
        {
            if (img == null) return null;

            int width = img.Width;
            int height = img.Height;
            double currentRatio = (double)width / height;

            if (Math.Abs(currentRatio - targetRatio) < 0.01)
                return img;

            Rectangle cropArea;

            if (currentRatio > targetRatio)
            {
                int newWidth = (int)(height * targetRatio);
                int x = (width - newWidth) / 2;
                cropArea = new Rectangle(x, 0, newWidth, height);
            }
            else
            {
                int newHeight = (int)(width / targetRatio);
                int y = (height - newHeight) / 2;
                cropArea = new Rectangle(0, y, width, newHeight);
            }

            Bitmap cropped = new Bitmap(cropArea.Width, cropArea.Height);
            using (Graphics g = Graphics.FromImage(cropped))
            {
                g.DrawImage(img, new Rectangle(0, 0, cropped.Width, cropped.Height), cropArea, GraphicsUnit.Pixel);
            }
            return cropped;
        }

        private void btnVN_Click(object sender, EventArgs e) { }

        private void btnAB_Click(object sender, EventArgs e) { }

        private void btnPA_Click(object sender, EventArgs e) { }

        private void panelCategory_Paint(object sender, PaintEventArgs e) { }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim().ToLower();
            if (string.IsNullOrEmpty(keyword))
            {
                LoadProductCards(); // ถ้าไม่มีคำค้น ให้แสดงทั้งหมด
                return;
            }

            flowProducts.Controls.Clear();

            using (var conn = new MySql.Data.MySqlClient.MySqlConnection("server=localhost;user id=root;password=;database=krungrome_db;"))
            {
                conn.Open();
                string sql = @"SELECT name, price, image, description 
                       FROM products 
                       WHERE LOWER(name) LIKE @kw OR LOWER(description) LIKE @kw";
                using (var cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@kw", "%" + keyword + "%");
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            var card = new Guna2ShadowPanel
                            {
                                Width = 260,
                                Height = 450,
                                BackColor = Color.Transparent,
                                FillColor = Color.White,
                                Radius = 7,
                                ShadowColor = Color.FromArgb(242, 238, 232),
                                ShadowDepth = 6,
                                ShadowShift = 3,
                                ShadowStyle = Guna2ShadowPanel.ShadowMode.ForwardDiagonal,
                                Margin = new Padding(35, 25, 35, 30)
                            };

                            var img = BytesToImage(rd["image"]);
                            img = CropToAspect(img, 1.0 / 1.0);

                            var pic = new Guna2PictureBox
                            {
                                Image = img,
                                Width = 230,
                                Height = 230,
                                SizeMode = PictureBoxSizeMode.Zoom,
                                BorderRadius = 6,
                                UseTransparentBackground = true,
                                BackColor = Color.White,
                                Location = new Point((card.Width - 232) / 2, 15)
                            };

                            var name = new Label
                            {
                                Text = (rd["name"]?.ToString() ?? "").Trim(),
                                Font = new Font("TA Chailai", 16, FontStyle.Bold),
                                ForeColor = Color.FromArgb(90, 60, 40),
                                TextAlign = ContentAlignment.MiddleCenter,
                                AutoSize = false,
                                Size = new Size(card.Width, 40),
                                Location = new Point(0, pic.Bottom + 10)
                            };

                            string descText = (rd["description"]?.ToString() ?? "").Trim();
                            if (descText.Length > 45) descText = descText.Substring(0, 42) + "...";
                            var desc = new Label
                            {
                                Text = descText,
                                Font = new Font("Segoe UI", 8.8f),
                                ForeColor = Color.FromArgb(130, 100, 70),
                                TextAlign = ContentAlignment.MiddleCenter,
                                AutoSize = false,
                                Size = new Size(card.Width - 20, 30),
                                Location = new Point(10, name.Bottom + 5)
                            };

                            decimal.TryParse(rd["price"]?.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal price);
                            var priceLabel = new Label
                            {
                                Text = $"฿{price:N0}",
                                Font = new Font("Segoe UI Semibold", 11, FontStyle.Bold),
                                ForeColor = Color.FromArgb(170, 110, 50),
                                TextAlign = ContentAlignment.MiddleCenter,
                                AutoSize = false,
                                Size = new Size(card.Width, 30),
                                Location = new Point(0, desc.Bottom + 5)
                            };

                            // ✅ เพิ่มปุ่ม Add to Cart เหมือนฟังก์ชันหลัก
                            var btnAdd = new Guna2Button
                            {
                                Text = "🛒  Add to Cart",
                                Height = 38,
                                Width = 200,
                                BorderRadius = 18,
                                FillColor = Color.FromArgb(200, 150, 90),
                                Font = new Font("Segoe UI Semibold", 9.5f),
                                ForeColor = Color.White,
                                Cursor = Cursors.Hand,
                                Location = new Point((card.Width - 200) / 2, card.Height - 55)
                            };
                            btnAdd.HoverState.FillColor = Color.FromArgb(230, 180, 110);

                            // ✅ เรียงเหมือนเดิม (รูปอยู่บนสุด)
                            card.Controls.Add(pic);
                            card.Controls.Add(name);
                            card.Controls.Add(desc);
                            card.Controls.Add(priceLabel);
                            card.Controls.Add(btnAdd);
                            pic.BringToFront();

                            flowProducts.Controls.Add(card);
                        }
                    }
                }
            }
        }

        private void btnCart_Click(object sender, EventArgs e)
        {

        }
    }
}
