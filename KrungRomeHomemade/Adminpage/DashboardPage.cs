using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using MySql.Data.MySqlClient;

namespace KrungRomeHomemade
{
    public partial class DashboardPage : Form
    {
        public DashboardPage()
        {
            InitializeComponent();

            // ✅ ขนาดคงที่
            this.Size = new Size(1500, 800);
            this.MinimumSize = new Size(1500, 800);
            this.MaximumSize = new Size(1500, 800);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = ColorTranslator.FromHtml("#FFF9F4");
            this.FormBorderStyle = FormBorderStyle.None;

            // โหลดข้อมูลทันทีเมื่อเปิด
            this.Load += DashboardPage_Load;
        }

        private void DashboardPage_Load(object sender, EventArgs e)
        {
            string connStr = "server=localhost;user id=root;password=;database=krungrome_db;";
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                conn.Open();

                // =======================================================
                // 1. 🟢 ยอดขายวันนี้ (Sales Today)
                // =======================================================
                MySqlCommand cmdSales = new MySqlCommand("SELECT SUM(total_payment) FROM receipts WHERE DATE(created_at) = CURDATE()", conn);
                object salesResult = cmdSales.ExecuteScalar();

                // ถ้าไม่มีค่า (Null) ให้เป็น 0
                decimal todaySales = (salesResult == DBNull.Value) ? 0 : Convert.ToDecimal(salesResult);
                lblSalesValue.Text = $"฿ {todaySales:N2}";

                // =======================================================
                // 2. 🛒 ออเดอร์วันนี้ (Orders Today)
                // =======================================================
                MySqlCommand cmdOrders = new MySqlCommand("SELECT COUNT(*) FROM receipts WHERE DATE(created_at) = CURDATE()", conn);
                int todayOrders = Convert.ToInt32(cmdOrders.ExecuteScalar());
                lblOrdersValue.Text = todayOrders.ToString();

                // =======================================================
                // 3. 📦 สินค้าทั้งหมด (Total Products)
                // =======================================================
                MySqlCommand cmdProducts = new MySqlCommand("SELECT COUNT(*) FROM products", conn);
                int totalProducts = Convert.ToInt32(cmdProducts.ExecuteScalar());
                lblProductsValue.Text = totalProducts.ToString();

                // =======================================================
                // 4. ⚠️ สินค้าใกล้หมด (Low Stock)
                // =======================================================
                MySqlCommand cmdLowStock = new MySqlCommand("SELECT COUNT(*) FROM products WHERE stock <= 5", conn);
                int lowStockCount = Convert.ToInt32(cmdLowStock.ExecuteScalar());
                lblLowStockValue.Text = lowStockCount.ToString();

                // 🔹 สินค้าขายดี 5 อันดับแรก (แก้ไข: เพิ่มชื่อสินค้า)
                MySqlCommand cmdTopSales = new MySqlCommand(@"
    SELECT 
        ROW_NUMBER() OVER (ORDER BY SUM(qty) DESC) AS 'อันดับ',
        product_name AS 'ชื่อสินค้า',  -- ✅ เพิ่มบรรทัดนี้
        SUM(qty) AS 'จำนวนขาย',
        SUM(qty * price) AS 'ยอดรวม (฿)'
    FROM order_items
    GROUP BY product_id, product_name -- ✅ ต้อง Group By ชื่อด้วย
    ORDER BY SUM(qty) DESC
    LIMIT 5", conn);

                MySqlDataAdapter adapterTop = new MySqlDataAdapter(cmdTopSales);
                DataTable dtTop = new DataTable();
                adapterTop.Fill(dtTop);
                dgvTopSales.DataSource = dtTop;
                // ตั้งค่าความกว้างช่อง
                dgvTopSales.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                // คอลัมน์ 0: อันดับ
                dgvTopSales.Columns[0].FillWeight = 15;
                dgvTopSales.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                // คอลัมน์ 1: ชื่อสินค้า (ให้กว้างที่สุด)
                dgvTopSales.Columns[1].FillWeight = 50;
                dgvTopSales.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft; // ชิดซ้ายให้อ่านง่าย

                // คอลัมน์ 2: จำนวนขาย
                dgvTopSales.Columns[2].FillWeight = 15;
                dgvTopSales.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                // คอลัมน์ 3: ยอดรวม
                dgvTopSales.Columns[3].FillWeight = 20;
                dgvTopSales.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvTopSales.Columns[3].DefaultCellStyle.Format = "N2"; // ใส่จุดทศนิยม
                // 🔸 ปรับหัวตารางให้สวยงาม
                dgvTopSales.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#7B4B28");
                dgvTopSales.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dgvTopSales.ColumnHeadersDefaultCellStyle.Font = new Font("FC Minimal", 16, FontStyle.Bold);
                dgvTopSales.DefaultCellStyle.Font = new Font("FC Minimal", 14);
                dgvTopSales.DefaultCellStyle.ForeColor = ColorTranslator.FromHtml("#5A3E2B");
                dgvTopSales.DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#FBECDC");
                dgvTopSales.EnableHeadersVisualStyles = false;

                // 🔸 ปรับสไตล์ DataGridView (ปรับขนาดให้เล็กลงเท่าตาราง Low Stock)
                dgvTopSales.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvTopSales.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

                // 🔥 ลดความสูงแถว (จาก 40 -> 30)
                dgvTopSales.RowTemplate.Height = 30;
                // 🔥 ลดความสูงหัวตาราง (จาก 45 -> 35)
                dgvTopSales.ColumnHeadersHeight = 35;

                // 🔹 สีหัวตาราง (สีน้ำตาลเดิม) + 🔥 ลดขนาดฟอนต์ (18 -> 14)
                dgvTopSales.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#7B4B28");
                dgvTopSales.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dgvTopSales.ColumnHeadersDefaultCellStyle.Font = new Font("FC Minimal", 14, FontStyle.Bold);
                dgvTopSales.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                // 🔹 สีเนื้อหา + 🔥 ลดขนาดฟอนต์ (16 -> 12)
                dgvTopSales.DefaultCellStyle.BackColor = Color.White;
                dgvTopSales.DefaultCellStyle.ForeColor = ColorTranslator.FromHtml("#5A3E2B");
                dgvTopSales.DefaultCellStyle.Font = new Font("FC Minimal", 12, FontStyle.Regular);
                dgvTopSales.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                // 🔹 สีตอนเลือกแถว (เหมือนเดิม)
                dgvTopSales.DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#FBECDC");
                dgvTopSales.DefaultCellStyle.SelectionForeColor = ColorTranslator.FromHtml("#5A3E2B");

                dgvTopSales.EnableHeadersVisualStyles = false;
                dgvTopSales.GridColor = ColorTranslator.FromHtml("#D9C6B0");

                // 🔹 ไม่ให้ผู้ใช้เพิ่ม/แก้ไข/ลบ
                dgvTopSales.ReadOnly = true;
                dgvTopSales.AllowUserToAddRows = false;
                dgvTopSales.AllowUserToResizeRows = false;
                dgvTopSales.RowHeadersVisible = false;

                // 🔹 ปรับระยะขอบในเซลล์
                dgvTopSales.DefaultCellStyle.Padding = new Padding(0, 5, 0, 5);
                dgvTopSales.Margin = new Padding(50, 20, 50, 50);

                MySqlCommand cmdLowStockList = new MySqlCommand(@"
                    SELECT 
                        product_id AS 'รหัสสินค้า',
                        name AS 'ชื่อสินค้า',
                        stock AS 'คงเหลือ'
                    FROM products
                    WHERE stock <= 5
                    ORDER BY stock ASC
                    LIMIT 10", conn); // แสดงแค่ 10 รายการแรกที่วิกฤตสุด

                MySqlDataAdapter adapterLow = new MySqlDataAdapter(cmdLowStockList);
                DataTable dtLow = new DataTable();
                adapterLow.Fill(dtLow);
                dgvLowStock.DataSource = dtLow;

                dgvLowStock.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                // 🔥 ลดความสูงแถว
                dgvLowStock.RowTemplate.Height = 30;
                dgvLowStock.ColumnHeadersHeight = 35;

                dgvLowStock.EnableHeadersVisualStyles = false;
                dgvLowStock.RowHeadersVisible = false;
                dgvLowStock.ReadOnly = true;
                dgvLowStock.AllowUserToAddRows = false;
                dgvLowStock.AllowUserToResizeRows = false;

                // สีหัวตาราง (แดงเลือดหมู) + 🔥 ลดขนาดฟอนต์หัวข้อ (14)
                dgvLowStock.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#C0392B");
                dgvLowStock.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dgvLowStock.ColumnHeadersDefaultCellStyle.Font = new Font("FC Minimal", 14, FontStyle.Bold);
                dgvLowStock.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                // สีเนื้อหา + 🔥 ลดขนาดฟอนต์เนื้อหา (12)
                dgvLowStock.DefaultCellStyle.BackColor = Color.White;
                dgvLowStock.DefaultCellStyle.ForeColor = ColorTranslator.FromHtml("#C0392B");
                dgvLowStock.DefaultCellStyle.Font = new Font("FC Minimal", 12, FontStyle.Regular);
                dgvLowStock.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvLowStock.DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#FADBD8");
                dgvLowStock.DefaultCellStyle.SelectionForeColor = ColorTranslator.FromHtml("#922B21");

                // กำหนดความกว้าง (เหมือนเดิม)
                dgvLowStock.Columns[0].FillWeight = 20;
                dgvLowStock.Columns[1].FillWeight = 60;
                dgvLowStock.Columns[2].FillWeight = 20;

                dgvLowStock.GridColor = ColorTranslator.FromHtml("#E6B0AA");

                // ถ้าไม่มีสินค้าใกล้หมด ให้ซ่อนตารางหรือขึ้นข้อความ
                if (dtLow.Rows.Count == 0)
                {
                    // อาจจะซ่อนตาราง หรือแสดง Label ว่า "✅ สต๊อกปกติ" แทน
                    // dgvLowStock.Visible = false;
                }

            }
        }

        private void btnVN_Click(object sender, EventArgs e) { }
        private void lblSalesValue_Click(object sender, EventArgs e) { }
        private void lblOrdersValue_Click(object sender, EventArgs e) { }
        private void lblProductsValue_Click(object sender, EventArgs e) { }
        private void lblLowStockValue_Click(object sender, EventArgs e) { }

        private void dgvRecentOrders_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvTopSales_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvLowStock_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
