using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using MySql.Data.MySqlClient;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace KrungRomeHomemade.Adminpage
{
    public partial class ReportPage : Form
    {
        // 1. ประกาศตัวแปร
        string connectionString = "server=localhost;user id=root;password=;database=krungrome_db;";
        private DataTable dtReport = new DataTable();

        public ReportPage()
        {
            InitializeComponent();
        }

        private void ReportPage_Load(object sender, EventArgs e)
        {
            // ตั้งค่าวันเริ่มต้น
            dtpStart.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpEnd.Value = DateTime.Now;
        }

        // ===========================================================
        // 1. ฟังก์ชันดึงข้อมูล (Search)
        // ===========================================================
        private void LoadSalesReport()
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string sql = @"SELECT 
                                    order_id AS 'เลขที่บิล',
                                    DATE_FORMAT(created_at, '%d/%m/%Y %H:%i') AS 'วันที่ขาย',
                                    username AS 'ลูกค้า',
                                    total_payment AS 'ยอดเงิน (บาท)'
                                   FROM receipts
                                   WHERE DATE(created_at) BETWEEN DATE(@start) AND DATE(@end)
                                   ORDER BY created_at DESC";

                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@start", dtpStart.Value);
                        cmd.Parameters.AddWithValue("@end", dtpEnd.Value);

                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        dtReport = new DataTable();
                        da.Fill(dtReport);

                        // แสดงผลลงตาราง
                        if (dgvReport != null)
                        {
                            dgvReport.DataSource = dtReport;

                            // ==========================================
                            // 🎨 ปรับแต่งตารางให้สวยงาม (Grid Styling)
                            // ==========================================

                            // 1. ตั้งค่าพื้นฐาน
                            dgvReport.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                            dgvReport.BackgroundColor = Color.FromArgb(255, 250, 240); // สีพื้นหลังครีมอ่อน
                            dgvReport.BorderStyle = BorderStyle.None;
                            dgvReport.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                            dgvReport.RowHeadersVisible = false; // ซ่อนแถวซ้ายสุด
                            dgvReport.EnableHeadersVisualStyles = false; // ⚠️ สำคัญ! ต้องปิดเพื่อให้แก้สีหัวตารางได้

                            // 2. ปรับหัวตาราง (Header) - สีน้ำตาล
                            dgvReport.ColumnHeadersHeight = 50;
                            dgvReport.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#7B4B28"); // สีน้ำตาลเข้ม
                            dgvReport.ColumnHeadersDefaultCellStyle.ForeColor = Color.White; // ตัวหนังสือขาว
                            dgvReport.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("FC Minimal", 16, FontStyle.Bold);
                            dgvReport.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; // หัวข้ออยู่กลางเสมอ

                            // 3. ปรับเนื้อหาในตาราง (Rows)
                            dgvReport.RowTemplate.Height = 45;
                            dgvReport.DefaultCellStyle.Font = new System.Drawing.Font("FC Minimal", 14);
                            dgvReport.DefaultCellStyle.ForeColor = Color.FromArgb(80, 60, 40);
                            dgvReport.DefaultCellStyle.BackColor = Color.White;
                            dgvReport.DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#FBECDC"); // สีตอนเลือก
                            dgvReport.DefaultCellStyle.SelectionForeColor = Color.Black;

                            // 4. จัดระเบียบคอลัมน์ (Column Alignment & Width)

                            // คอลัมน์ 0: เลขที่บิล (ชิดซ้าย + เว้นระยะ)
                            if (dgvReport.Columns["เลขที่บิล"] != null)
                            {
                                dgvReport.Columns["เลขที่บิล"].FillWeight = 25;
                                dgvReport.Columns["เลขที่บิล"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                                dgvReport.Columns["เลขที่บิล"].DefaultCellStyle.Padding = new Padding(20, 0, 0, 0); // เว้นซ้าย 20px
                            }

                            // คอลัมน์ 1: วันที่ขาย (กึ่งกลาง)
                            if (dgvReport.Columns["วันที่ขาย"] != null)
                            {
                                dgvReport.Columns["วันที่ขาย"].FillWeight = 25;
                                dgvReport.Columns["วันที่ขาย"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            }

                            // คอลัมน์ 2: ลูกค้า (ชิดซ้าย + เว้นระยะ)
                            if (dgvReport.Columns["ลูกค้า"] != null)
                            {
                                dgvReport.Columns["ลูกค้า"].FillWeight = 25;
                                dgvReport.Columns["ลูกค้า"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                                dgvReport.Columns["ลูกค้า"].DefaultCellStyle.Padding = new Padding(20, 0, 0, 0);
                            }

                            // คอลัมน์ 3: ยอดเงิน (ชิดขวา + มีทศนิยม)
                            if (dgvReport.Columns["ยอดเงิน (บาท)"] != null)
                            {
                                dgvReport.Columns["ยอดเงิน (บาท)"].FillWeight = 25;
                                dgvReport.Columns["ยอดเงิน (บาท)"].DefaultCellStyle.Format = "N2"; // มีลูกน้ำและจุดทศนิยม
                                dgvReport.Columns["ยอดเงิน (บาท)"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                                dgvReport.Columns["ยอดเงิน (บาท)"].DefaultCellStyle.Padding = new Padding(0, 0, 20, 0); // เว้นขวา 20px
                            }
                        }
                    }
                    CalculateTotal();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("เกิดข้อผิดพลาด: " + ex.Message);
                }
            }
        }

        private void CalculateTotal()
        {
            if (dtReport.Rows.Count > 0)
            {
                object sumObj = dtReport.Compute("Sum([ยอดเงิน (บาท)])", "");
                decimal total = (sumObj == DBNull.Value) ? 0 : Convert.ToDecimal(sumObj);

                if (lblTotalSales != null)
                    lblTotalSales.Text = $"ยอดขายรวม: {total:N2} บาท";
            }
            else
            {
                if (lblTotalSales != null)
                    lblTotalSales.Text = "ยอดขายรวม: 0.00 บาท";
            }
        }

        // ===========================================================
        // 2. ฟังก์ชัน Export CSV
        // ===========================================================
        private void ExportToCSV()
        {
            if (dtReport.Rows.Count == 0)
            {
                MessageBox.Show("ไม่มีข้อมูลให้ Export", "แจ้งเตือน");
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "CSV File|*.csv";
            sfd.FileName = $"SalesReport_{DateTime.Now:yyyyMMdd}.csv";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    StringBuilder sb = new StringBuilder();
                    string[] columnNames = dtReport.Columns.Cast<DataColumn>().Select(column => column.ColumnName).ToArray();
                    sb.AppendLine(string.Join(",", columnNames));

                    foreach (DataRow row in dtReport.Rows)
                    {
                        string[] fields = row.ItemArray.Select(field => "\"" + field.ToString() + "\"").ToArray();
                        sb.AppendLine(string.Join(",", fields));
                    }

                    File.WriteAllText(sfd.FileName, sb.ToString(), Encoding.UTF8);
                    MessageBox.Show("✅ Export สำเร็จ!", "เรียบร้อย");
                    System.Diagnostics.Process.Start(sfd.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Export ไม่สำเร็จ: " + ex.Message);
                }
            }
        }

        // ===========================================================
        // 3. ฟังก์ชันพิมพ์ PDF
        // ===========================================================
        // ===========================================================
        // 3. ฟังก์ชันพิมพ์ใบสรุปยอด (PDF Report) - แก้ไข Font Ambiguous
        // ===========================================================
        private void PrintSalesReport()
        {
            if (dtReport.Rows.Count == 0)
            {
                MessageBox.Show("ไม่มีข้อมูลให้พิมพ์ กรุณากดค้นหาก่อน", "แจ้งเตือน");
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "PDF File|*.pdf";
            sfd.FileName = $"SalesReport_{DateTime.Now:yyyyMMdd_HHmm}.pdf";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // ใช้ FileStream สร้างไฟล์
                    using (FileStream fs = new FileStream(sfd.FileName, FileMode.Create))
                    using (Document doc = new Document(PageSize.A4, 36, 36, 50, 50))
                    {
                        PdfWriter.GetInstance(doc, fs);
                        doc.Open();

                        // -----------------------------------------------------------------------
                        // 🔴 จุดที่แก้ไข: ระบุชื่อเต็ม iTextSharp.text.Font เพื่อไม่ให้ชนกับ System.Drawing
                        // -----------------------------------------------------------------------
                        string fontPath = Path.Combine(Application.StartupPath, "Assets", "Fonts", "Sarabun-Regular.ttf");
                        string fontBoldPath = Path.Combine(Application.StartupPath, "Assets", "Fonts", "Sarabun-Bold.ttf");

                        BaseFont bf = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                        BaseFont bfBold = BaseFont.CreateFont(fontBoldPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);

                        // ใช้ iTextSharp.text.Font แบบระบุชื่อเต็ม
                        iTextSharp.text.Font fontTitle = new iTextSharp.text.Font(bfBold, 20);
                        iTextSharp.text.Font fontHeader = new iTextSharp.text.Font(bfBold, 14);
                        iTextSharp.text.Font fontNormal = new iTextSharp.text.Font(bf, 12);
                        iTextSharp.text.Font fontSmall = new iTextSharp.text.Font(bf, 10);
                        // -----------------------------------------------------------------------

                        // ส่วนหัวกระดาษ
                        Paragraph title = new Paragraph("Sales Report", fontTitle);
                        title.Alignment = Element.ALIGN_CENTER;
                        doc.Add(title);

                        Paragraph subTitle = new Paragraph("KrungRome Homemade Bakery", fontHeader);
                        subTitle.Alignment = Element.ALIGN_CENTER;
                        doc.Add(subTitle);

                        doc.Add(new Paragraph("\n"));

                        string dateRange = $"ช่วงวันที่: {dtpStart.Value:dd/MM/yyyy} - {dtpEnd.Value:dd/MM/yyyy}";
                        doc.Add(new Paragraph(dateRange, fontNormal));
                        doc.Add(new Paragraph($"พิมพ์โดย: Admin | เวลา: {DateTime.Now:dd/MM/yyyy HH:mm}", fontSmall));

                        doc.Add(new Paragraph("\n"));

                        // สร้างตาราง (4 คอลัมน์)
                        PdfPTable table = new PdfPTable(4);
                        table.WidthPercentage = 100;
                        table.SetWidths(new float[] { 20f, 25f, 30f, 25f });

                        // หัวตาราง
                        string[] headers = { "เลขที่บิล", "วันที่ขาย", "ลูกค้า", "ยอดเงิน (บาท)" };
                        foreach (string h in headers)
                        {
                            PdfPCell cell = new PdfPCell(new Phrase(h, fontHeader));
                            cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            cell.BackgroundColor = new BaseColor(197, 138, 84);
                            cell.Padding = 8;
                            cell.Phrase.Font.Color = BaseColor.WHITE;
                            table.AddCell(cell);
                        }

                        // เนื้อหาตาราง
                        decimal totalSum = 0;
                        foreach (DataRow row in dtReport.Rows)
                        {
                            PdfPCell c1 = new PdfPCell(new Phrase(row["เลขที่บิล"].ToString(), fontNormal));
                            c1.HorizontalAlignment = Element.ALIGN_LEFT;
                            c1.Padding = 6;
                            table.AddCell(c1);

                            PdfPCell c2 = new PdfPCell(new Phrase(row["วันที่ขาย"].ToString(), fontNormal));
                            c2.HorizontalAlignment = Element.ALIGN_CENTER;
                            c2.Padding = 6;
                            table.AddCell(c2);

                            PdfPCell c3 = new PdfPCell(new Phrase(row["ลูกค้า"].ToString(), fontNormal));
                            c3.HorizontalAlignment = Element.ALIGN_LEFT;
                            c3.Padding = 6;
                            table.AddCell(c3);

                            decimal amount = Convert.ToDecimal(row["ยอดเงิน (บาท)"]);
                            totalSum += amount;
                            PdfPCell c4 = new PdfPCell(new Phrase(amount.ToString("N2"), fontNormal));
                            c4.HorizontalAlignment = Element.ALIGN_RIGHT;
                            c4.Padding = 6;
                            table.AddCell(c4);
                        }

                        // ... (หลังจาก doc.Add(table);) ...

                        doc.Add(table);

                        // =======================================================
                        // ✅ ปรับปรุงส่วนสรุปยอดท้ายกระดาษ
                        // =======================================================

                        // 1. สร้าง Paragraph สำหรับยอดรวม
                        Paragraph totalPara = new Paragraph($"Grand Total {totalSum:N2} THB", fontTitle);
                        // 2. จัดตำแหน่งชิดขวา
                        totalPara.Alignment = Element.ALIGN_RIGHT;

                        // 3. 🔥 เพิ่มระยะห่างจากตาราง (SpacingBefore)
                        // ยิ่งเลขเยอะ ยิ่งห่างจากตารางมาก (ลองปรับดูได้ครับ เช่น 20f, 30f)
                        totalPara.SpacingBefore = 30f;

                        // (แถม) กำหนดระยะบรรทัดไม่ให้สระจม (กรณีฟอนต์ใหญ่)
                        totalPara.MultipliedLeading = 1.2f;

                        // 4. เพิ่มลงในเอกสาร
                        doc.Add(totalPara);

                        doc.Close();
                    }

                    System.Diagnostics.Process.Start(sfd.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("เกิดข้อผิดพลาดในการสร้าง PDF: " + ex.Message);
                }
            }
        }

        // ===========================================================
        // 4. เชื่อมปุ่ม
        // ===========================================================
        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadSalesReport();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            ExportToCSV();
        }

        // ✅ แก้ไขปุ่ม Print ให้เหลืออันเดียวที่ถูกต้อง
        private void btnPrint_Click(object sender, EventArgs e)
        {
            PrintSalesReport();
        }

        // ⚠️ Event ว่างๆ (ห้ามลบ)
        private void dtpStart_ValueChanged(object sender, EventArgs e) { }
        private void dtpEnd_ValueChanged(object sender, EventArgs e) { }
        private void dgvReport_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void lblTotalSales_Click(object sender, EventArgs e) { }
    }
}