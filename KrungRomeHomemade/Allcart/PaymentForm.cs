using System;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

// ✅ ไลบรารี PDF
using iTextSharp.text;
using iTextSharp.text.pdf;
using KrungRomeHomemade.Allcart;
using MySql.Data.MySqlClient;

namespace KrungRomeHomemade
{
    public partial class PaymentForm : Form
    {
        private string slipPath = "";

        public PaymentForm()
        {
            InitializeComponent();
        }

        private void PaymentForm_Load(object sender, EventArgs e)
        {
            // ✅ แสดงราคารวม
            lblTotalPrice.Text = $"฿ {CartData.Subtotal:N2}";

            // ✅ โหลดสินค้าจากตะกร้า
            LoadCartItems();
        }

        // ✅ ฟังก์ชันแสดงรายการสินค้า
        private void LoadCartItems()
        {
            flowCartItems.Controls.Clear();

            foreach (var item in CartData.Items)
            {
                // 🧁 ใช้ Guna2Panel เพื่อให้มีขอบโค้งมน + เงา + สีขอบ
                var panelItem = new Guna.UI2.WinForms.Guna2Panel();
                panelItem.Width = flowCartItems.Width - 40;
                panelItem.Height = 100;
                panelItem.BorderRadius = 20;
                panelItem.FillColor = Color.FromArgb(255, 249, 244); // สีครีมอ่อน
                panelItem.BorderColor = Color.FromArgb(90, 64, 51);  // ✅ เพิ่มสีขอบ
                panelItem.BorderThickness = 1;                       // ✅ ความหนาของขอบ
                panelItem.ShadowDecoration.Enabled = true;
                panelItem.ShadowDecoration.Depth = 10;
                panelItem.ShadowDecoration.Color = Color.FromArgb(220, 200, 180);
                panelItem.Margin = new Padding(10, 8, 10, 8);

                // 🖼️ รูปสินค้า
                PictureBox pic = new PictureBox();
                pic.Image = item.Image;
                pic.SizeMode = PictureBoxSizeMode.Zoom;
                pic.Size = new Size(70, 70);
                pic.Location = new Point(20, 15);
                panelItem.Controls.Add(pic);

                // 🧁 ชื่อสินค้า
                Label lblName = new Label();
                lblName.Text = item.Name;
                lblName.Font = new System.Drawing.Font("FC Minimal", 15, FontStyle.Bold);
                lblName.ForeColor = Color.FromArgb(80, 45, 25);
                lblName.Location = new Point(110, 25);
                lblName.AutoSize = true;
                panelItem.Controls.Add(lblName);

                // 🔢 จำนวนสินค้า
                Label lblQty = new Label();
                lblQty.Text = $"จำนวน {item.Quantity} ชิ้น";
                lblQty.Font = new System.Drawing.Font("FC Issara Rounded [Non-cml.] SmB", 9.5f, FontStyle.Regular);

                lblQty.ForeColor = Color.FromArgb(100, 70, 50);
                lblQty.Location = new Point(110, 55);
                lblQty.AutoSize = true;
                panelItem.Controls.Add(lblQty);

                // 💰 ราคาสินค้า (จัดไว้ด้านขวา)
                Label lblPrice = new Label();
                lblPrice.Text = $"฿{item.Price * item.Quantity:N2}";
                lblPrice.Font = new System.Drawing.Font("FC Minimal", 14, FontStyle.Bold);
                lblPrice.ForeColor = Color.FromArgb(197, 138, 84);
                lblPrice.AutoSize = true;
                lblPrice.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                lblPrice.Location = new Point(panelItem.Width - 130, 38);
                panelItem.Controls.Add(lblPrice);

                flowCartItems.Controls.Add(panelItem);
            }
        }



        private void btnUploadSlip_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                slipPath = ofd.FileName;
                MessageBox.Show("แนบสลิปเรียบร้อย ✅", "สำเร็จ");
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {


            if (SessionData.Username == "Guest")
            {
                MessageBox.Show("กรุณาเข้าสู่ระบบก่อนทำการชำระเงิน ⚠️",
                    "ต้องล็อกอินก่อน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            if (string.IsNullOrEmpty(slipPath))
            {
                MessageBox.Show("กรุณาแนบสลิปก่อนบันทึกการชำระเงิน ⚠️");
                return;
            }

            try
            {
                string orderId = "KR " + DateTime.Now.ToString("yyMMddHHmm");
                string dateNow = DateTime.Now.ToString("dd/MM/yyyy");
                string timeNow = DateTime.Now.ToString("HH:mm");
                string customer = SessionData.Username; // ✅ ดึงจากตัวแปรเก็บชื่อผู้ใช้หลังล็อกอิน
                decimal totalPayment = CartData.Subtotal;

                // ✅ 1. สร้าง PDF
                GeneratePaymentReceipt(orderId, dateNow, timeNow, customer, totalPayment);

                // ✅ 2. สำเนาไฟล์สลิปไปเก็บในโฟลเดอร์ SlipUploads
                string slipFolder = @"C:\Project_C#\SlipUploads";
                if (!Directory.Exists(slipFolder))
                    Directory.CreateDirectory(slipFolder);

                string slipFileName = Path.GetFileName(slipPath);
                string newSlipPath = Path.Combine(slipFolder, slipFileName);
                File.Copy(slipPath, newSlipPath, true);

                // ✅ 3. บันทึกลงฐานข้อมูล
                string connectionString = "server=localhost;user id=root;password=;database=krungrome_db;";
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = @"INSERT INTO receipts (order_id, username, total_payment, file_path, slip_image, created_at)
                           VALUES (@order_id, @username, @total_payment, @file_path, @slip_image, @created_at)";

                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@order_id", orderId);
                        cmd.Parameters.AddWithValue("@username", customer);
                        cmd.Parameters.AddWithValue("@total_payment", totalPayment);
                        cmd.Parameters.AddWithValue("@file_path", $@"C:\Project_C#\PDFPayment\Receipt_{orderId.Replace(" ", "_")}.pdf");
                        cmd.Parameters.AddWithValue("@created_at", DateTime.Now);

                        // ✅ อ่านรูปสลิปเป็น byte[]
                        byte[] slipBytes = File.ReadAllBytes(newSlipPath);
                        cmd.Parameters.Add("@slip_image", MySqlDbType.LongBlob).Value = slipBytes;

                        cmd.ExecuteNonQuery();
                        conn.Close(); // ปิด connection หลังบันทึกสำเร็จ

                    }
                }

                MessageBox.Show("✅ บันทึกข้อมูลใบเสร็จและสลิปสำเร็จ!", "KrungRome Bakery", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CartData.Items.Clear();
                Application.Exit();
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ เกิดข้อผิดพลาดในการบันทึกข้อมูล\n" + ex.Message);
            }
        }



        private void GeneratePaymentReceipt(string orderId, string dateNow, string timeNow, string customer, decimal totalPayment)
        {
            try
            {
                string templatePath = Path.Combine(Application.StartupPath, "Assets", "Images", "ReceiptTemplate.png");
                // ✅ กำหนดโฟลเดอร์เก็บ PDF
                string saveFolder = @"C:\Project_C#\PDFPayment";

                // ✅ ถ้าโฟลเดอร์ยังไม่มี ให้สร้างขึ้นอัตโนมัติ
                if (!Directory.Exists(saveFolder))
                {
                    Directory.CreateDirectory(saveFolder);
                }

                // ✅ สร้าง path สำหรับไฟล์ใบเสร็จ
                string outputPath = Path.Combine(saveFolder, $"Receipt_{orderId.Replace(" ", "_")}.pdf");


                using (FileStream fs = new FileStream(outputPath, FileMode.Create))
                using (Document doc = new Document(PageSize.A5, 36, 36, 36, 36))
                {
                    PdfWriter writer = PdfWriter.GetInstance(doc, fs);
                    doc.Open();

                    // ✅ โหลดฟอนต์ไทย
                    string fontPath = Path.Combine(Application.StartupPath, "Assets", "Fonts", "Sarabun-Regular.ttf");
                    BaseFont bf = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                    PdfContentByte cb = writer.DirectContent;
                    cb.BeginText();
                    cb.SetFontAndSize(bf, 13);

                    // ✅ เพิ่มพื้นหลังหน้าแรก
                    iTextSharp.text.Image bg = iTextSharp.text.Image.GetInstance(templatePath);
                    bg.SetAbsolutePosition(0, 0);
                    bg.ScaleToFit(PageSize.A5.Width, PageSize.A5.Height);
                    doc.Add(bg);

                    // ✅ ส่วนหัวใบเสร็จ
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, orderId, 30, 436, 0);
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, dateNow + "  " + timeNow, 275, 436, 0);

                    // ✅ ตัวแปรช่วยจัดการหน้า
                    int startY = 380;
                    int lineHeight = 18;
                    int bottomLimit = 220;
                    int totalItems = CartData.Items.Count;
                    int currentItem = 0;

                    foreach (var item in CartData.Items)
                    {
                        currentItem++;
                        bool isLastItem = (currentItem == totalItems);

                        string productLine = $"{item.Name} x {item.Quantity}";
                        string priceLine = $"{item.Price * item.Quantity:N2} ฿";

                        // 🔹 ถ้าพิมพ์ถึงขอบล่างแล้วแต่ยังมีสินค้าต่อ -> สร้างหน้าใหม่
                        if (startY < bottomLimit)
                        {
                            // ✅ ก่อนจะขึ้นหน้าใหม่ ให้พิมพ์ footer หน้าเก่าก่อน
                            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, customer, 300, 190, 0);
                            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, $"{totalPayment:N2} ฿", 300, 170, 0);

                            cb.EndText();
                            doc.NewPage();

                            // ✅ วางพื้นหลังใหม่ในหน้าถัดไป
                            iTextSharp.text.Image bg2 = iTextSharp.text.Image.GetInstance(templatePath);
                            bg2.SetAbsolutePosition(0, 0);
                            bg2.ScaleToFit(PageSize.A5.Width, PageSize.A5.Height);
                            doc.Add(bg2);

                            // ✅ เขียนหน้าใหม่
                            cb = writer.DirectContent;
                            cb.BeginText();
                            cb.SetFontAndSize(bf, 13);
                            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "- Continued -", 40, 436, 0);

                            startY = 380;
                        }

                        // 🔹 พิมพ์สินค้าแต่ละบรรทัด
                        cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, productLine, 40, startY, 0);
                        cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, priceLine, 380, startY, 0);
                        startY -= lineHeight;

                        // ✅ ถ้าเป็นสินค้าชิ้นสุดท้าย → แสดง footer ท้ายใบเสร็จ
                        if (isLastItem)
                        {
                            // ถ้าใกล้ขอบล่างให้ขึ้นหน้าใหม่ก่อน
                            if (startY < 200)
                            {
                                cb.EndText();
                                doc.NewPage();

                                iTextSharp.text.Image bgFinal = iTextSharp.text.Image.GetInstance(templatePath);
                                bgFinal.SetAbsolutePosition(0, 0);
                                bgFinal.ScaleToFit(PageSize.A5.Width, PageSize.A5.Height);
                                doc.Add(bgFinal);

                                cb = writer.DirectContent;
                                cb.BeginText();
                                cb.SetFontAndSize(bf, 13);

                                startY = 300;
                            }

                            // ✅ Footer หน้าสุดท้าย
                            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, customer, 300, 190, 0);
                            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, $"{totalPayment:N2} ฿", 300, 170, 0);

                            // ✅ เพิ่มข้อความยืนยันและขอบคุณ
                        }
                    }

                    // ✅ ปิดการเขียน PDF อย่างปลอดภัย
                    cb.EndText();
                    doc.Close();
                }

                // ✅ เปิดไฟล์ PDF อัตโนมัติ
                System.Diagnostics.Process.Start(outputPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ ไม่สามารถสร้างใบเสร็จได้\n" + ex.Message);
            }
        }


        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void BtnUpload_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void CartItems_Paint(object sender, PaintEventArgs e)
        {

        }

        private void flowCartItems_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
