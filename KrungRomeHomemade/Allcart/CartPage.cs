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
    public partial class CartPage : Form
    {
        public CartPage()
        {
            InitializeComponent();
            this.Load += CartPage_Load;   // ผูกโหลด
        }

        private void CartPage_Load(object sender, EventArgs e)
        {
            LoadCartItems();

        }

        // ✅ โหลดรายการสินค้าในตะกร้า
        public void LoadCartItems()
        {
            // ✅ ตั้งค่า FlowLayoutPanel ให้เลื่อนแนวตั้งเท่านั้น
            flowCartItems.FlowDirection = FlowDirection.TopDown;
            flowCartItems.WrapContents = false;
            flowCartItems.AutoScroll = true;
            flowCartItems.HorizontalScroll.Enabled = false;
            flowCartItems.HorizontalScroll.Visible = false;
            flowCartItems.VerticalScroll.Visible = true;

            flowCartItems.Controls.Clear();

            foreach (var item in CartData.Items)
            {
                // 🔸 Panel พื้นหลังสินค้า
                Guna.UI2.WinForms.Guna2Panel panel = new Guna.UI2.WinForms.Guna2Panel();
                panel.Width = flowCartItems.ClientSize.Width - 40; // ลดอีกนิดเพื่อกันเกินขวา
                panel.Height = 120;
                panel.BorderColor = Color.FromArgb(90, 64, 51);
                panel.BorderThickness = 1;
                panel.BorderRadius = 15;
                panel.BackColor = Color.FromArgb(249, 245, 241);
                panel.Margin = new Padding(10, 0, 0, 10);

                // 🖼️ รูปสินค้า
                PictureBox pic = new PictureBox();
                pic.Image = item.Image;
                pic.SizeMode = PictureBoxSizeMode.Zoom;
                pic.Location = new Point(20, 15);
                pic.Size = new Size(100, 100);
                panel.Controls.Add(pic);

                // 🧁 ชื่อสินค้า
                Label lblName = new Label();
                lblName.Text = item.Name;
                lblName.Font = new Font("FC Minimal", 15, FontStyle.Bold);
                lblName.ForeColor = Color.FromArgb(80, 45, 25);
                lblName.Location = new Point(140, 25);
                lblName.AutoSize = true;
                panel.Controls.Add(lblName);

                // ➖ ปุ่มลดจำนวน
                Guna.UI2.WinForms.Guna2Button btnMinus = new Guna.UI2.WinForms.Guna2Button();
                btnMinus.Text = "-";
                btnMinus.Font = new Font("FC Minimal", 12, FontStyle.Bold);
                btnMinus.Size = new Size(35, 35);
                btnMinus.Location = new Point(140, 70);
                btnMinus.BorderRadius = 8;
                btnMinus.FillColor = Color.FromArgb(249, 245, 241);
                btnMinus.ForeColor = Color.FromArgb(120, 70, 40);
                btnMinus.Click += (s, e) =>
                {
                    if (item.Quantity > 1) item.Quantity--;
                    LoadCartItems(); // รีเฟรช
                };
                panel.Controls.Add(btnMinus);

                // 🔢 จำนวน
                Label lblQty = new Label();
                lblQty.Text = item.Quantity.ToString();
                lblQty.Font = new Font("FC Minimal", 13);
                lblQty.ForeColor = Color.FromArgb(80, 45, 25);
                lblQty.AutoSize = true;
                lblQty.Location = new Point(185, 75);
                panel.Controls.Add(lblQty);

                // ➕ ปุ่มเพิ่มจำนวน
                Guna.UI2.WinForms.Guna2Button btnPlus = new Guna.UI2.WinForms.Guna2Button();
                btnPlus.Text = "+";
                btnPlus.Font = new Font("FC Minimal", 12, FontStyle.Bold);
                btnPlus.Size = new Size(35, 35);
                btnPlus.Location = new Point(215, 70);
                btnPlus.BorderRadius = 8;
                btnPlus.FillColor = Color.FromArgb(249, 245, 241);
                btnPlus.ForeColor = Color.FromArgb(120, 70, 40);
                btnPlus.Click += (s, e) =>
                {
                    item.Quantity++;
                    LoadCartItems(); // รีเฟรช
                };
                panel.Controls.Add(btnPlus);

                // 💰 ราคารวม (ด้านขวา)
                Label lblTotal = new Label();
                lblTotal.Text = $"฿{item.Price * item.Quantity:N0}";
                lblTotal.Font = new Font("FC Minimal", 14, FontStyle.Bold);
                lblTotal.ForeColor = Color.FromArgb(197, 138, 84);
                lblTotal.AutoSize = true;
                lblTotal.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                lblTotal.Location = new Point(panel.Width - 100, 50);
                panel.Controls.Add(lblTotal);

                // ✅ เพิ่ม panel ลง FlowLayout
                flowCartItems.Controls.Add(panel);
            }

            // ✅ อัปเดตยอดรวม
            lblSubtotalValue.Text = $"฿{CartData.Subtotal:N0}";
        }



        private void btnBack_Click(object sender, EventArgs e)
        {

            this.Close(); // ปิด Cart เพื่อให้ฟอร์มหลักเรียก ShowHome()
        }

  
        private void btnContinue_Click(object sender, EventArgs e)
        {
            this.Close(); // ปิด Cart เพื่อให้ฟอร์มหลักเรียก ShowHome()

        }

        private void panelRight_Paint(object sender, PaintEventArgs e)
        {
            panelRight.BorderRadius = 10;
        }

        private void panelLeft_Paint(object sender, PaintEventArgs e)
        {

        }


        private void flowCartItems_Paint(object sender, PaintEventArgs e)
        {

        }

        private void picProduct_Click(object sender, EventArgs e)
        {

        }

        private void panelItem1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnPlus_Click(object sender, EventArgs e)
        {

        }

        private void lblQty_Click(object sender, EventArgs e)
        {

        }

        private void btnMinus_Click(object sender, EventArgs e)
        {

        }

        private void lblSubtotalValue_Click(object sender, EventArgs e)
        {

        }

        private void btnCheckout_Click(object sender, EventArgs e)
        {
            // 🔸 เปิดหน้าชำระเงิน (PaymentForm)
            PaymentForm payment = new PaymentForm();
            payment.TopLevel = false;
            payment.FormBorderStyle = FormBorderStyle.None;
            payment.Dock = DockStyle.Fill;

            // 🔸 ค้นหา panel หลักจากฟอร์มแม่ (Krung_Rome_Homemade)
            Form parentForm = this.Parent?.FindForm();
            if (parentForm != null)
            {
                Panel panelMain = parentForm.Controls.Find("panelMain", true).FirstOrDefault() as Panel;
                if (panelMain != null)
                {
                    panelMain.Controls.Clear();   // ล้างหน้าเดิม (CartPage)
                    panelMain.Controls.Add(payment); // แสดง PaymentForm แทน
                    payment.Show();
                }
            }
        }





        private void lblPrice_Click(object sender, EventArgs e)
        {

        }

        private void lblDesc_Click(object sender, EventArgs e)
        {

        }

        private void lblCartTitle_Click(object sender, EventArgs e)
        {

        }
    }
}

