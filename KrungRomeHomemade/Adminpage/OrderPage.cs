using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Guna.UI2.WinForms;
using KrungRomeHomemade.Adminpage;

namespace KrungRomeHomemade
{
    public partial class OrderPage : Form
    {
        // เก็บ Connection String ไว้ที่นี่
        string connectionString = "server=localhost;user id=root;password=;database=krungrome_db;";

        public OrderPage()
        {
            InitializeComponent();
            // ❌ ลบบรรทัด LoadOrders(); ออกจากตรงนี้ครับ
            // เราจะให้มันไปทำงานตอนหน้าจอโหลดเสร็จ (OrderPage_Load) แทน เพื่อกัน Error
        }

        // ✅ ฟังก์ชันดึงรูปสลิปจากฐานข้อมูลตาม Order ID
        private Image GetSlipFromDb(string orderId)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                // ดึงข้อมูล slip_image จากตาราง receipts
                string sql = "SELECT slip_image FROM receipts WHERE order_id = @oid";

                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@oid", orderId);

                    // ดึงข้อมูลออกมา (มันจะเป็น byte[])
                    object result = cmd.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        byte[] imgBytes = (byte[])result;

                        // แปลง byte[] กลับเป็น Image
                        using (System.IO.MemoryStream ms = new System.IO.MemoryStream(imgBytes))
                        {
                            return Image.FromStream(ms);
                        }
                    }
                }
            }
            return null; // ถ้าไม่มีรูป
        }


        private void OrderPage_Load(object sender, EventArgs e)
        {
            // 1. สร้างคอลัมน์ก่อน (สำคัญมาก!)
            dgvOrders.AutoGenerateColumns = false;
            SetupOrderTable();

            // 2. ค่อยโหลดข้อมูลใส่เข้าไป
            LoadOrders();
        }

        // ✅ ฟังก์ชันสร้างคอลัมน์และตกแต่งตาราง
        // ✅ ฟังก์ชันสร้างคอลัมน์และตกแต่งตาราง (แก้ไขเพิ่มปุ่มดูรายการ)
        private void SetupOrderTable()
        {
            dgvOrders.Columns.Clear();
            dgvOrders.BackgroundColor = Color.FromArgb(255, 250, 240);
            dgvOrders.RowHeadersVisible = false;

            // ตกแต่งหัวตาราง
            dgvOrders.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(197, 138, 84);
            dgvOrders.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvOrders.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvOrders.ColumnHeadersDefaultCellStyle.Font = new Font("FC Minimal", 15, FontStyle.Bold);
            dgvOrders.EnableHeadersVisualStyles = false;
            dgvOrders.ColumnHeadersHeight = 50;

            // ตกแต่งแถวข้อมูล
            dgvOrders.DefaultCellStyle.Font = new Font("FC Minimal", 14);
            dgvOrders.DefaultCellStyle.ForeColor = Color.FromArgb(80, 60, 40);
            dgvOrders.DefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 240, 220);
            dgvOrders.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvOrders.RowTemplate.Height = 55;

            // 1. Order ID
            var colId = new DataGridViewTextBoxColumn();
            colId.HeaderText = "Order ID";
            colId.DataPropertyName = "order_id";
            colId.Width = 180;
            dgvOrders.Columns.Add(colId);

            // 2. Customer
            var colUser = new DataGridViewTextBoxColumn();
            colUser.HeaderText = "Customer";
            colUser.DataPropertyName = "username";
            colUser.Width = 200;
            dgvOrders.Columns.Add(colUser);

            // 3. Total
            var colTotal = new DataGridViewTextBoxColumn();
            colTotal.HeaderText = "Total (฿)";
            colTotal.DataPropertyName = "total_payment";
            colTotal.Width = 150;
            colTotal.DefaultCellStyle.Format = "N2";
            colTotal.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvOrders.Columns.Add(colTotal);

            // 4. Date
            var colDate = new DataGridViewTextBoxColumn();
            colDate.HeaderText = "Order Date";
            colDate.DataPropertyName = "created_at";
            colDate.Width = 180;
            colDate.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvOrders.Columns.Add(colDate);

            // -----------------------------------------------------
            // 5. 🔴 เพิ่มปุ่ม "ดูรายการ" (View Details)
            // -----------------------------------------------------
            DataGridViewButtonColumn btnView = new DataGridViewButtonColumn();
            btnView.HeaderText = "Details";     // หัวข้อ
            btnView.Text = "🔎 ดูสลิป";      // ข้อความบนปุ่ม
            btnView.UseColumnTextForButtonValue = true; // บังคับให้โชว์ข้อความนี้ทุกปุ่ม
            btnView.Name = "btnView";           // ตั้งชื่อไว้เรียกใช้
            btnView.Width = 120;

            // ปรับสีปุ่ม (อาจจะไม่แสดงผลถ้าใช้ GunaDataGrid บางเวอร์ชัน แต่ใส่ไว้ก่อน)
            btnView.DefaultCellStyle.BackColor = Color.White;
            btnView.DefaultCellStyle.ForeColor = Color.Black;

            dgvOrders.Columns.Add(btnView);

            dgvOrders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        // ✅ ฟังก์ชันโหลดข้อมูล (รวมการค้นหาไว้ในตัวเดียว)
        // แก้ไขให้รับพารามิเตอร์วันที่ (ค่าเริ่มต้นเป็น null คือไม่กรอง)
        // แก้ไขให้รับพารามิเตอร์วันที่ (ค่าเริ่มต้นเป็น null คือไม่กรอง)
        private void LoadOrders(string search = "", DateTime? start = null, DateTime? end = null)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                // เริ่มต้น SQL
                string sql = "SELECT order_id, username, total_payment, created_at FROM receipts WHERE 1=1";

                // 1. เงื่อนไขค้นหาข้อความ
                if (!string.IsNullOrEmpty(search))
                {
                    sql += " AND (order_id LIKE @search OR username LIKE @search)";
                }

                // 2. เงื่อนไขค้นหาวันที่ (ถ้ามีการส่งค่ามา)
                if (start != null && end != null)
                {
                    sql += " AND DATE(created_at) BETWEEN DATE(@start) AND DATE(@end)";
                }

                sql += " ORDER BY created_at DESC";

                using (var cmd = new MySqlCommand(sql, conn))
                {
                    // ใส่พารามิเตอร์
                    if (!string.IsNullOrEmpty(search))
                    {
                        cmd.Parameters.AddWithValue("@search", "%" + search + "%");
                    }

                    if (start != null && end != null)
                    {
                        cmd.Parameters.AddWithValue("@start", start.Value);
                        cmd.Parameters.AddWithValue("@end", end.Value);
                    }

                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvOrders.DataSource = dt;
                }
            }
        }

        private void txtSearchOrder_TextChanged(object sender, EventArgs e)
        {
            // ค้นหาชื่อ โดยไม่สนวันที่ (ส่ง null ไป)
            LoadOrders(txtSearchOrder.Text.Trim(), null, null);
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // 1. ตรวจสอบว่ากดโดนปุ่ม "ดูสลิป" (btnView)
            if (e.RowIndex >= 0 && dgvOrders.Columns[e.ColumnIndex].Name == "btnView")
            {
                string orderId = dgvOrders.Rows[e.RowIndex].Cells[0].Value.ToString();

                try
                {
                    // 2. ดึงรูปจาก Database
                    Image slip = GetSlipFromDb(orderId);

                    if (slip != null)
                    {
                        // ✅ 3. ถ้ามีรูป -> เปิดหน้าต่างโชว์รูปทันที (ลบ MessageBox เดิมออกแล้ว)
                        SlipViewForm viewForm = new SlipViewForm(slip, orderId);
                        viewForm.StartPosition = FormStartPosition.CenterScreen;
                        viewForm.ShowDialog();
                    }
                    else
                    {
                        // ❌ 4. ถ้าไม่มีรูป -> แจ้งเตือน
                        MessageBox.Show("ไม่พบรูปสลิปสำหรับออเดอร์นี้ (อาจจะยังไม่ได้แนบ)", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("เกิดข้อผิดพลาดในการโหลดรูป: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dtpStart_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dtpEnd_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btnFilterDate_Click(object sender, EventArgs e)
        {
            // ดึงค่าจากปฏิทิน แล้วส่งไปโหลดข้อมูล
            LoadOrders(txtSearchOrder.Text.Trim(), dtpStart.Value, dtpEnd.Value);
        }
    }
}