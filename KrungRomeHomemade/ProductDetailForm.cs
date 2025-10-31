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
using System.IO; // สำหรับจัดการรูปภาพ
using KrungRomeHomemade;

namespace KrungRomeHomemade
{
    public partial class ProductDetailForm : Form
    {
        private int quantity = 1;
        private decimal pricePerItem;

        // ✅ เพิ่ม Constructor เปล่า เพื่อให้ Designer และการรันทดสอบทำงานได้
        public ProductDetailForm()
        {
            InitializeComponent();
        }

        // ✅ Constructor หลัก (ใช้ตอนเปิด popup แสดงรายละเอียดสินค้า)
        public ProductDetailForm(string name, string description, Image image, decimal price)
        {
            InitializeComponent();

            // 🧁 ตั้งค่าข้อมูลสินค้า
            lblName.Text = name;
            lblDesc.Text = description;
            picProduct.Image = image;
            pricePerItem = price;

            lblPrice.Text = $"ราคาต่อชิ้น ฿ {pricePerItem:N0}";
            lblQty.Text = quantity.ToString();
            lblTotal.Text = $"ราคารวม ฿ {pricePerItem * quantity:N0}";
        }

        private void ProductDetailForm_Load(object sender, EventArgs e)
        {

        }

        private void picProduct_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void lblName_Click(object sender, EventArgs e)
        {

        }

        private void btnPlus_Click(object sender, EventArgs e)
        {
            quantity++;
            lblQty.Text = quantity.ToString();
            UpdateTotal();
        }

        private void btnMinus_Click(object sender, EventArgs e)
        {
            if (quantity > 1)
            {
                quantity--;
                lblQty.Text = quantity.ToString();
                UpdateTotal();
            }
        }

        private void UpdateTotal()

        {
            lblTotal.Text = $"ราคารวม ฿ {pricePerItem * quantity:N0}";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // ✅ สร้างอ็อบเจกต์สินค้าใหม่
            var item = new CartItem
            {
                Name = lblName.Text,
                Price = pricePerItem,
                Quantity = quantity,
                Image = picProduct.Image,
            };

            // ✅ ตรวจว่ามีสินค้านี้อยู่ในตะกร้าแล้วหรือยัง
            var existing = CartData.Items.FirstOrDefault(i => i.Name == item.Name);
            if (existing != null)
            {
                existing.Quantity += quantity; // ถ้ามีแล้ว เพิ่มจำนวน
            }
            else
            {
                CartData.Items.Add(item); // ถ้ายังไม่มี เพิ่มใหม่ในตะกร้า
            }

            // ✅ แสดงข้อความยืนยัน
            MessageBox.Show(
                $"เพิ่ม {lblName.Text} จำนวน {quantity} ชิ้น ลงตะกร้าเรียบร้อยแล้ว!",
                "เพิ่มสินค้าสำเร็จ",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );

            this.Close();

            // 🧺 เมื่อปิดหน้ารายละเอียดแล้ว ให้ CartPage โหลดใหม่อัตโนมัติ
            foreach (Form form in Application.OpenForms)
            {
                if (form is CartPage cartPage)
                {
                    cartPage.LoadCartItems(); // รีโหลดข้อมูลในตะกร้า
                    break;
                }
            }

            // เมื่อปิดหน้ารายละเอียดแล้ว ให้หน้า CartPage โหลดใหม่อัตโนมัติ (กรณีเปิดอยู่)
            foreach (Form form in Application.OpenForms)
            {
                if (form is CartPage cartPage)
                {
                    cartPage.LoadCartItems();
                    break;
                }
            }

        }



        private void lblDesc_Click(object sender, EventArgs e)
        {

        }

        private void lblPrice_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            

        }
    }
}
