using System;
using System.Drawing; // ✅ ต้องมีอันนี้เพื่อใช้ Image
using System.Windows.Forms;

namespace KrungRomeHomemade.Adminpage
{
    public partial class SlipViewForm : Form
    {
        // ❌ ลบ Constructor เดิมทิ้ง (public SlipViewForm() { ... })

        // ✅ สร้าง Constructor ใหม่ ที่รับ "รูปภาพ" และ "ชื่อออเดอร์" เข้ามา
        public SlipViewForm(Image slipImage, string title)
        {
            InitializeComponent();

            // ตั้งชื่อหัวหน้าต่าง
            this.Text = "หลักฐานการโอนเงิน - Order ID: " + title;

            // เอารูปที่รับมา ใส่เข้าไปใน PictureBox
            if (slipImage != null)
            {
                picSlip.Image = slipImage;
            }
        }

        // (Event นี้ปล่อยว่างไว้ก็ได้ หรือลบทิ้งก็ได้ถ้าไม่ได้ใช้)
        private void picSlip_Click(object sender, EventArgs e)
        {
            // อาจจะใส่โค้ดขยายรูป หรือ Save รูปตรงนี้ในอนาคต
        }
    }
}