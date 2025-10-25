using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions; // ✅ สำหรับตรวจสอบรูปแบบอีเมล
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace KrungRomeHomemade
{
    public partial class Create_Account : Form
    {
        public Create_Account()
        {
            InitializeComponent();
        }

        // 🔹 เมื่อกดปุ่ม Create Account
        private void Click_CA(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();
            string confirm = txtConfirm.Text.Trim();

            // ✅ ตรวจสอบช่องว่าง
            if (username == "" || email == "" || password == "" || confirm == "")
            {
                MessageBox.Show("กรุณากรอกข้อมูลให้ครบทุกช่อง", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ✅ ตรวจสอบรูปแบบอีเมล
            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("กรุณากรอกอีเมลให้ถูกต้อง เช่น example@gmail.com", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ✅ ตรวจสอบความยาวรหัสผ่าน
            if (password.Length < 8)
            {
                MessageBox.Show("รหัสผ่านต้องมีอย่างน้อย 8 ตัวอักษร", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ✅ ตรวจสอบรหัสผ่านตรงกัน
            if (password != confirm)
            {
                MessageBox.Show("รหัสผ่านไม่ตรงกัน", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 🔹 สร้าง Connection String
            string connectionString = "server=localhost;user id=root;password=;database=krungrome_db;";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // ✅ ตรวจสอบว่าชื่อผู้ใช้ซ้ำไหม
                    string checkQuery = "SELECT COUNT(*) FROM users WHERE username = @username";
                    MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn);
                    checkCmd.Parameters.AddWithValue("@username", username);
                    int count = Convert.ToInt32(checkCmd.ExecuteScalar());
                    if (count > 0)
                    {
                        MessageBox.Show("ชื่อผู้ใช้นี้ถูกใช้ไปแล้ว กรุณาเปลี่ยนชื่อใหม่", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // ✅ตรวจสอบอีเมลซ้ำ
                    string checkEmailQuery = "SELECT COUNT(*) FROM users WHERE email = @Email";
                    MySqlCommand checkEmailCmd = new MySqlCommand(checkEmailQuery, conn);
                    checkEmailCmd.Parameters.AddWithValue("@Email", email);
                    int emailCount = Convert.ToInt32(checkEmailCmd.ExecuteScalar());
                    if (emailCount > 0)
                    {
                        MessageBox.Show("อีเมลนี้ถูกใช้ไปแล้ว กรุณาใช้อีเมลอื่น", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // ✅ พิ่มข้อมูลใหม่ในฐานข้อมูล
                    string insertQuery = "INSERT INTO users (username, email, password) VALUES (@username, @Email, @password)";
                    MySqlCommand cmd = new MySqlCommand(insertQuery, conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@password", password);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("สมัครสมาชิกสำเร็จแล้ว!", "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // 🔹 กลับไปหน้า Login
                    Login login = new Login();
                    login.Show();
                    this.Hide();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("เกิดข้อผิดพลาด: " + ex.Message);
                }
            }

        }

        // 🔹 ปุ่ม Show/Hide Password
        private void btnShowPassword_Click(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !txtPassword.UseSystemPasswordChar;
            btnShowPassword.Text = txtPassword.UseSystemPasswordChar ? "Show" : "Hide";
        }

        // 🔹 ปุ่ม Show/Hide Confirm Password
        private void BtnShowConfirm_Click(object sender, EventArgs e)
        {
            txtConfirm.UseSystemPasswordChar = !txtConfirm.UseSystemPasswordChar;
            btnShowConfirm.Text = txtConfirm.UseSystemPasswordChar ? "Show" : "Hide";
        }

        // 🔹 ปุ่มกลับไปหน้า Login
        private void Click_btlogin(object sender, EventArgs e)
        {
            Login newLogin = new Login();
            newLogin.Show();
            this.Hide();
        }

        // 🔹 เมื่อโหลดฟอร์ม (เริ่มต้นซ่อนรหัสผ่าน)
        private void Create_Account_Load(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = true;
            txtConfirm.UseSystemPasswordChar = true;
        }

        // (อันนี้พวก TextBox event ไม่จำเป็นต้องใส่อะไร ถ้าไม่ใช้)
        private void txtPassword_TextChanged(object sender, EventArgs e) { }
        private void txtConfirm_TextChanged(object sender, EventArgs e) { }
        private void txtEmail_TextChanged(object sender, EventArgs e) { }
        private void txtUsername_TextChanged(object sender, EventArgs e) { }
    }
}
