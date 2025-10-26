using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KrungRomeHomemade
{
    public partial class Krung_Rome_Homemade : Form
    {
        public Krung_Rome_Homemade()
        {
            InitializeComponent();
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
            this.WindowState = FormWindowState.Maximized; // เต็มจอ

            // ✅ เพิ่มหัวข้อหน้า Home
            Label lblHeading = new Label();
            lblHeading.Text = "Fresh Baked Daily with Love";
            lblHeading.Font = new Font("Segoe UI", 22, FontStyle.Bold);
            lblHeading.ForeColor = Color.FromArgb(80, 50, 30);
            lblHeading.AutoSize = true;
            lblHeading.TextAlign = ContentAlignment.MiddleCenter;
            lblHeading.Location = new Point(
                (this.ClientSize.Width - 500) / 2, // จัดให้อยู่กลาง
                250
            );
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

            LoadProductCards(); // ✅ โหลดสินค้าเข้าหน้า
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
            flowProducts.Controls.Clear();

            using (var conn = new MySql.Data.MySqlClient.MySqlConnection("server=localhost;user id=root;password=;database=krungrome_db;"))
            {
                conn.Open();
                string sql = "SELECT name, price, image, description FROM products ORDER BY product_id ASC";

                using (var cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, conn))
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        // ✅ การ์ดสินค้า 1 ชิ้น
                        Panel card = new Panel()
                        {
                            Width = 260,
                            Height = 340,
                            BackColor = Color.White,
                            Margin = new Padding(20),
                            BorderStyle = BorderStyle.None
                        };

                        // ✅ เพิ่มเงาให้ดูนูน
                        card.Paint += (s, e) =>
                        {
                            ControlPaint.DrawBorder(e.Graphics,
                                card.ClientRectangle,
                                Color.LightGray, 2, ButtonBorderStyle.Solid,
                                Color.LightGray, 2, ButtonBorderStyle.Solid,
                                Color.LightGray, 2, ButtonBorderStyle.Solid,
                                Color.LightGray, 2, ButtonBorderStyle.Solid);
                        };


                        // ✅ รูปสินค้า
                        PictureBox pic = new PictureBox()
                        {
                            Image = BytesToImage(rd["image"]),
                            SizeMode = PictureBoxSizeMode.Zoom,
                            Dock = DockStyle.Top,
                            Height = 180
                        };

                        // ✅ ชื่อสินค้า
                        Label lblName = new Label()
                        {
                            Text = rd["name"].ToString(),
                            Dock = DockStyle.Top,
                            Height = 35,
                            Font = new Font("Segoe UI Semibold", 12, FontStyle.Regular),
                            TextAlign = ContentAlignment.MiddleCenter,
                            ForeColor = Color.FromArgb(80, 50, 30)
                        };

                        // ✅ คำอธิบาย
                        Label lblDesc = new Label()
                        {
                            Text = rd["description"].ToString(),
                            Dock = DockStyle.Top,
                            Height = 40,
                            Font = new Font("Segoe UI", 9, FontStyle.Italic),
                            ForeColor = Color.FromArgb(140, 110, 80),
                            TextAlign = ContentAlignment.MiddleCenter
                        };



                        // ✅ ราคา
                        Label lblPrice = new Label()
                        {
                            Text = $"฿{rd["price"]}",
                            Dock = DockStyle.Top,
                            Height = 30,
                            Font = new Font("Segoe UI Semibold", 11, FontStyle.Bold),
                            ForeColor = Color.FromArgb(180, 120, 60),
                            TextAlign = ContentAlignment.MiddleCenter
                        };

                        // ✅ ปุ่ม Add to Cart (Guna2)
                        Guna.UI2.WinForms.Guna2Button btnAdd = new Guna.UI2.WinForms.Guna2Button()
                        {
                            Text = "🛒 Add to Cart",
                            Dock = DockStyle.Bottom,
                            Height = 40,
                            BorderRadius = 20,
                            FillColor = Color.FromArgb(200, 150, 90),
                            Font = new Font("Segoe UI Semibold", 10),
                            ForeColor = Color.White,
                            Cursor = Cursors.Hand
                        };

                        // ✅ ตั้งค่า HoverState แยกออกมา
                        btnAdd.HoverState.FillColor = Color.FromArgb(230, 180, 100);


                        // ✅ ใส่ทั้งหมดใน card
                        card.Controls.Add(btnAdd);
                        card.Controls.Add(lblPrice);
                        card.Controls.Add(lblDesc);
                        card.Controls.Add(lblName);
                        card.Controls.Add(pic);

                        // ✅ เพิ่มลงใน FlowLayoutPanel
                        flowProducts.Controls.Add(card);

                        flowProducts.WrapContents = true;
                        flowProducts.FlowDirection = FlowDirection.LeftToRight;
                        flowProducts.Padding = new Padding(100, 20, 100, 20);
                        flowProducts.AutoScroll = true;

                    }
                }
            }
        }

        // ✅ แปลง byte[] → Image
        private Image BytesToImage(object dbValue)
        {
            if (dbValue == null || dbValue is DBNull) return null;
            var bytes = (byte[])dbValue;
            using (var ms = new System.IO.MemoryStream(bytes))
            using (var temp = Image.FromStream(ms))
                return new Bitmap(temp);
        }

    }
}
