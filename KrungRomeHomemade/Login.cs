using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KrungRomeHomemade.Allcart;
using MySql.Data.MySqlClient;

namespace KrungRomeHomemade
{
    public partial class Login : Form
    {

        public static class GlobalUser
{
    public static string Username { get; set; }
}



        bool isNavigating = false;

        public Login()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            // ✅ ซ่อนรหัสผ่านเมื่อเริ่มต้น
            Password.UseSystemPasswordChar = true;
        }

        private void Int_Username(object sender, EventArgs e) { }
        private void Int_Password(object sender, EventArgs e) { }

        private void Login_Click(object sender, EventArgs e) { }

        private void Click_ForgotPassword(object sender, EventArgs e)
        {
            Forgot_Password forgotForm = new Forgot_Password();
            forgotForm.Show();
            this.Hide(); // ✅ ใช้ Hide() ไม่ใช่ Close()
        }

        private void Click_Createaccount(object sender, EventArgs e)
        {
            isNavigating = true;
            Create_Account createAccountForm = new Create_Account();
            createAccountForm.Show();
            this.Hide(); // ✅ เปลี่ยนจาก Close() เป็น Hide()
        }

        private void Close_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "คุณต้องการปิดโปรแกรม กรุงโรมโฮมเมด จริงหรือไม่?",
                "ยืนยันการปิดโปรแกรม",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit(); // ✅ ปิดโปรแกรมทั้งหมด
            }
        }

        // 🔹 ปุ่ม Show/Hide Password
        private void btnShowConfirm_Click2(object sender, EventArgs e)
        {
            Password.UseSystemPasswordChar = !Password.UseSystemPasswordChar;
            btnShowConfirm2.Text = Password.UseSystemPasswordChar ? "Show" : "Hide";
        }

        // 🔹 ปุ่ม Login (ตรวจสอบฐานข้อมูล)
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string usernameOrEmail = Username.Text.Trim();
            string password = Password.Text.Trim();

            if (string.IsNullOrWhiteSpace(usernameOrEmail) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("กรุณากรอกชื่อผู้ใช้หรืออีเมล และรหัสผ่าน",
                    "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string connectionString = "server=localhost;user id=root;password=;database=krungrome_db;";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    string query = "SELECT * FROM users WHERE (username = @Username OR email = @Username) AND password = @Password";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Username", usernameOrEmail);
                    cmd.Parameters.AddWithValue("@Password", password);

                    MySqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        reader.Read();
                        string user = reader["username"].ToString();

                        // ✅ เก็บชื่อผู้ใช้ไว้ใน SessionData
                        SessionData.Username = user;

                        // ✅ ถ้าชื่อผู้ใช้คือ admin → เปิดหน้า Admin
                        if (user.ToLower() == "admin")
                        {
                            MessageBox.Show("เข้าสู่ระบบในฐานะผู้ดูแลระบบสำเร็จ!",
                                "Admin Access", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Admin adminForm = new Admin();
                            adminForm.Show();
                            this.Hide();
                        }
                        else
                        {
                            // ✅ ถ้าเป็นผู้ใช้ทั่วไป → เปิดหน้า Home
                            MessageBox.Show("เข้าสู่ระบบสำเร็จ! ยินดีต้อนรับคุณ " + user,
                                "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Krung_Rome_Homemade home = new Krung_Rome_Homemade();
                            home.Show();
                            this.Hide();
                        }
                    }
                    else
                    {
                        MessageBox.Show("ชื่อผู้ใช้/อีเมล หรือรหัสผ่านไม่ถูกต้อง",
                            "เข้าสู่ระบบล้มเหลว", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("เกิดข้อผิดพลาด: " + ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        // 🔹 เมื่อพิมพ์ชื่อผู้ใช้แล้วกด Enter → ให้โฟกัสไปช่องรหัสผ่าน
        private void Username_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // ป้องกันเสียง "ติ๊ด"
                Password.Focus();
            }
        }

        // 🔹 เมื่อพิมพ์รหัสผ่านแล้วกด Enter → ให้กดปุ่ม Login อัตโนมัติ
        private void Password_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnLogin.PerformClick(); // สั่งกดปุ่ม Login
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }
    }
}
