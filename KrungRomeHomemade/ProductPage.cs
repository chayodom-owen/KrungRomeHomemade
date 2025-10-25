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
    public partial class ProductPage : Form
    {
        public ProductPage()
        {
            InitializeComponent();
            this.BackColor = System.Drawing.Color.Beige;

            Label lbl = new Label();
            lbl.Text = "📦 รายการสินค้า";
            lbl.Font = new System.Drawing.Font("Segoe UI", 18, System.Drawing.FontStyle.Bold);
            lbl.AutoSize = true;
            lbl.Location = new System.Drawing.Point(30, 20);
            this.Controls.Add(lbl);

            // 🔹 ตารางสินค้า
            DataGridView dgv = new DataGridView();
            dgv.Location = new System.Drawing.Point(30, 80);
            dgv.Size = new System.Drawing.Size(600, 300);
            dgv.DataSource = GetMockProducts();
            this.Controls.Add(dgv);
        }
        private DataTable GetMockProducts()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("รหัสสินค้า");
            dt.Columns.Add("ชื่อสินค้า");
            dt.Columns.Add("ราคา (บาท)");
            dt.Columns.Add("คงเหลือ");

            dt.Rows.Add("P001", "ขนมปังฝรั่งเศส", "35", "20");
            dt.Rows.Add("P002", "ครัวซองต์เนยสด", "45", "15");
            dt.Rows.Add("P003", "บาแก็ตกรอบ", "50", "10");

            return dt;
        }
        private void ProductPage_Load(object sender, EventArgs e)
        {

        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
