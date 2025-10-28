using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KrungRomeHomemade
{
    internal static class Program
    {
        /// <summary>
        /// จุดเริ่มต้นของโปรแกรม
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Admin()); // ✅ ใช้ชื่อฟอร์มให้ตรงกับไฟล์ LoginForm.cs
        }
    }
}
