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
    public partial class OrderPage : Form
    {
        public OrderPage()
        {
            InitializeComponent();
            this.BackColor = System.Drawing.Color.BlanchedAlmond;

            Label lbl = new Label();
            lbl.Text = "🧾 รายการสั่งซื้อวันนี้";
            lbl.Font = new System.Drawing.Font("Segoe UI", 18, System.Drawing.FontStyle.Bold);
            lbl.AutoSize = true;
            lbl.Location = new System.Drawing.Point(30, 20);
            this.Controls.Add(lbl);

            // 🔹 รายการออเดอร์
            ListView list = new ListView();
            list.View = View.Details;
            list.FullRowSelect = true;
            list.Columns.Add("รหัสออเดอร์", 100);
            list.Columns.Add("ลูกค้า", 150);
            list.Columns.Add("ยอดรวม (บาท)", 120);
            list.Columns.Add("สถานะ", 100);
            list.Items.Add(new ListViewItem(new[] { "O1001", "คุณอรอนงค์", "185", "ชำระแล้ว" }));
            list.Items.Add(new ListViewItem(new[] { "O1002", "คุณปวีณา", "90", "รอชำระ" }));
            list.Location = new System.Drawing.Point(30, 80);
            list.Size = new System.Drawing.Size(600, 300);
            this.Controls.Add(list);
        }

        private void OrderPage_Load(object sender, EventArgs e)
        {

        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
