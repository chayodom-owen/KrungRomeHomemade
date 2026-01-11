using System;
using System.Windows.Forms;

namespace KrungRomeHomemade
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Login());  // ✅ เปลี่ยนชื่อฟอร์มตรงนี้ตามหน้าที่ต้องการให้เปิดก่อน
        }
    }
}
