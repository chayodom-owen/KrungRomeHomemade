using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Microsoft.VisualBasic;  // ✅ สำหรับใช้ InputBox

namespace KrungRomeHomemade
{
    public partial class Forgot_Password : Form
    {
        public Forgot_Password()
        {
            InitializeComponent();
        }

        private void Forgot_Password_Load(object sender, EventArgs e)
        {
        }

        private void Text_forgot(object sender, EventArgs e)
        {
        }

        // 🔹 ปุ่มกลับไปหน้า Login
        private void Click_Backtologin(object sender, EventArgs e)
        {
            Form login = Application.OpenForms["Login"];

            if (login != null)
            {
                login.Show();
                this.Hide();
            }
            else
            {
                Login newLogin = new Login();
                newLogin.Show();
                this.Hide();
            }
        }

        // 🔹 ปุ่ม Submit (รีเซ็ตรหัสผ่าน)
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();

            // ✅ ตรวจสอบว่ากรอกอีเมลหรือยัง
            if (string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("กรุณากรอกอีเมลก่อน", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ✅ ตรวจสอบรูปแบบอีเมลเบื้องต้น
            if (!email.Contains("@") || !email.Contains("."))
            {
                MessageBox.Show("รูปแบบอีเมลไม่ถูกต้อง", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string connectionString = "server=localhost;user id=root;password=;database=krungrome_db;";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // 🔎 ตรวจสอบว่ามีอีเมลนี้ในระบบไหม
                    string query = "SELECT COUNT(*) FROM users WHERE email = @Email";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Email", email);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());

                    if (count == 0)
                    {
                        MessageBox.Show("ไม่พบบัญชีที่ใช้อีเมลนี้ในระบบ", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // ✨ ถ้ามีอีเมลในระบบ → ขอรหัสใหม่
                    string newPassword = Interaction.InputBox("กรุณากรอกรหัสผ่านใหม่:", "ตั้งรหัสใหม่", "");

                    if (string.IsNullOrWhiteSpace(newPassword))
                    {
                        MessageBox.Show("การรีเซ็ตรหัสผ่านถูกยกเลิก", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    if (newPassword.Length < 8)
                    {
                        MessageBox.Show("รหัสผ่านต้องมีอย่างน้อย 8 ตัวอักษร", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // ✅ อัปเดตรหัสผ่านใหม่ในฐานข้อมูล
                    string updateQuery = "UPDATE users SET password = @Password WHERE email = @Email";
                    MySqlCommand updateCmd = new MySqlCommand(updateQuery, conn);
                    updateCmd.Parameters.AddWithValue("@Password", newPassword);
                    updateCmd.Parameters.AddWithValue("@Email", email);
                    updateCmd.ExecuteNonQuery();

                    MessageBox.Show("รีเซ็ตรหัสผ่านสำเร็จแล้ว!", "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // 🔙 กลับไปหน้า Login
                    Login login = new Login();
                    login.Show();
                    this.Hide();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("เกิดข้อผิดพลาด: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
