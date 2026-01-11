using System;
using System.Windows.Forms;
using KrungRomeHomemade.Adminpage; // 👈 เพิ่มบรรทัดนี้

namespace KrungRomeHomemade
{
    public partial class AdminPage : Form
    {
        private Form activePage = null; // ✅ เก็บหน้าปัจจุบันไว้ป้องกันการเปิดซ้ำ

        public AdminPage()
        {
            InitializeComponent();

            // ✅ กำหนดขนาดคงที่
            this.Size = new System.Drawing.Size(1500, 800);
            this.MaximumSize = new System.Drawing.Size(1500, 800);
            this.MinimumSize = new System.Drawing.Size(1500, 800);

            // ✅ ทำให้ฟอร์มอยู่ด้านบนสุดตอนเปิด

        }

        private void Admin_Load(object sender, EventArgs e)
        {


            // ✅ โหลด Dashboard เป็นหน้าเริ่มต้นทันที
            LoadPage(new ReportPage());
        }

        // 🔹 ฟังก์ชันโหลดหน้า
        private void LoadPage(Form page)
        {
            // ถ้ามีหน้าเดิมอยู่แล้ว → ปิดก่อน
            if (activePage != null)
                activePage.Close();

            activePage = page;
            page.TopLevel = false;
            page.Dock = DockStyle.Fill;

            // ✅ เคลียร์ของเดิมออกก่อนทุกครั้ง
            PanelMain.Controls.Clear();
            PanelMain.Controls.Add(page);

            page.Show();
        }

        // 📊 ปุ่ม Dashboard
        private void btnDashboard_Click_1(object sender, EventArgs e)
        {
            LoadPage(new DashboardPage());
        }

        // 📦 ปุ่ม Product
        private void btnProduct_Click(object sender, EventArgs e)
        {
            LoadPage(new ProductPage());
        }

        // 🛒 ปุ่ม Order
        private void btnOrder_Click(object sender, EventArgs e)
        {
            LoadPage(new OrderPage());
        }

        // 📋 ปุ่ม Report
        private void btnReport_Click(object sender, EventArgs e)
        {
            LoadPage(new ReportPage());
        }

        // 🚪 ปุ่มออกจากระบบ
        private void Close_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "คุณต้องการออกจากระบบหรือไม่?",
                "ยืนยันการออกจากระบบ",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // กลับไปหน้า Login แทนการปิดโปรแกรม
                Login login = new Login();
                login.Show();
                this.Hide();
            }
        }

        // 🎨 สำหรับการวาด Panel (ไม่จำเป็นก็ลบทิ้งได้)
        private void PanelMain_Paint(object sender, PaintEventArgs e) { }
        private void PanelMenu_Paint(object sender, PaintEventArgs e) { }
    }
}
