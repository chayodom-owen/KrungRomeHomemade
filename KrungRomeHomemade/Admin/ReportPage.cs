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
    public partial class ReportPage : Form
    {
        public ReportPage()
        {
            InitializeComponent();
            this.BackColor = System.Drawing.Color.Wheat;

            Label lbl = new Label();
            lbl.Text = "📊 สร้างรายงานยอดขาย";
            lbl.Font = new System.Drawing.Font("Segoe UI", 18, System.Drawing.FontStyle.Bold);
            lbl.AutoSize = true;
            lbl.Location = new System.Drawing.Point(30, 20);
            this.Controls.Add(lbl);

            Guna2Button btn = new Guna2Button();
            btn.Text = "สร้างรายงาน PDF";
            btn.FillColor = System.Drawing.Color.SaddleBrown;
            btn.ForeColor = System.Drawing.Color.White;
            btn.Font = new System.Drawing.Font("Segoe UI", 12, System.Drawing.FontStyle.Bold);
            btn.Size = new System.Drawing.Size(200, 45);
            btn.Location = new System.Drawing.Point(40, 100);
            btn.Click += (s, e) => {
                MessageBox.Show("สร้างรายงานสำเร็จ! (จำลอง)", "Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
            };
            this.Controls.Add(btn);
        }

        private void ReportPage_Load(object sender, EventArgs e)
        {

        }
    }
}
