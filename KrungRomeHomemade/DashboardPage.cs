using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace KrungRomeHomemade
{
    public partial class DashboardPage : Form
    {
        public DashboardPage()
        {
            InitializeComponent();
            this.BackColor = System.Drawing.Color.FromArgb(255, 235, 205); // สีครีมอุ่น

            Label lblTitle = new Label();
            lblTitle.Text = "📊 สรุปภาพรวมยอดขาย";
            lblTitle.Font = new System.Drawing.Font("Segoe UI", 18, System.Drawing.FontStyle.Bold);
            lblTitle.AutoSize = true;
            lblTitle.Location = new System.Drawing.Point(40, 30);
            this.Controls.Add(lblTitle);

            // 🔹 Progress Bar จำลองยอดขาย
            Guna2ProgressBar bar1 = new Guna2ProgressBar();
            bar1.Location = new System.Drawing.Point(50, 100);
            bar1.Size = new System.Drawing.Size(400, 25);
            bar1.Value = 75;
            bar1.ProgressColor = System.Drawing.Color.SaddleBrown;
            this.Controls.Add(bar1);

            Label lbl = new Label();
            lbl.Text = "ยอดขายเดือนนี้ 75% ของเป้าหมาย";
            lbl.Font = new System.Drawing.Font("Segoe UI", 11, System.Drawing.FontStyle.Regular);
            lbl.AutoSize = true;
            lbl.Location = new System.Drawing.Point(50, 130);
            this.Controls.Add(lbl);
        }

        private void DashboardPage_Load(object sender, EventArgs e)
        {

        }


        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }

        private void guna2ProgressBar1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
