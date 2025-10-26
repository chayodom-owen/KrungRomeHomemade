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

namespace KrungRomeHomemade
{
    public partial class ProductPage : Form
    {
        string connectionString = "server=localhost;user id=root;password=;database=krungrome_db;";

        public ProductPage()
        {
            InitializeComponent();
            dataGridProducts.AutoGenerateColumns = false; // ✅ ป้องกันการสร้างคอลัมน์ซ้ำ
        }

        private void ProductPage_Load(object sender, EventArgs e)
        {


            dataGridProducts.Columns.Clear();

            dataGridProducts.Columns.Add("product_id", "รหัสสินค้า");
            dataGridProducts.Columns.Add("name", "ชื่อสินค้า");
            dataGridProducts.Columns.Add("category", "หมวดหมู่");
            dataGridProducts.Columns.Add("price", "ราคา (บาท)");
            dataGridProducts.Columns.Add("stock", "คงเหลือ");

            DataGridViewImageColumn imgCol = new DataGridViewImageColumn();
            imgCol.Name = "image";
            imgCol.HeaderText = "รูปสินค้า";
            imgCol.ImageLayout = DataGridViewImageCellLayout.Zoom;
            imgCol.ValueType = typeof(Image);
            imgCol.DefaultCellStyle.NullValue = null;
            dataGridProducts.Columns.Add(imgCol);

            dataGridProducts.RowTemplate.Height = 100;
            dataGridProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // ✅ ป้องกัน error จากเซลล์รูปภาพ
            dataGridProducts.DataError += (s, ev) => { ev.ThrowException = false; };

            // ✅ ปิดการแสดงสัญลักษณ์ ❌ ในทุกกรณี
            dataGridProducts.ShowCellErrors = false;
            dataGridProducts.ShowEditingIcon = false;
            dataGridProducts.ShowRowErrors = false;

            // ✅ เพิ่มหมวดหมู่ใน ComboBox
            cmbCategory.Items.Clear();
            cmbCategory.Items.AddRange(new string[]
            {
        "เวียนนัวเซอรี (Viennoiserie)",
        "ขนมปังยุโรป (Artisan Breads)",
        "ปาทิสเซอรี (Pâtisserie)",
        "แซนด์วิช / ของคาว (Sandwich & Savory)"
            });
            cmbCategory.SelectedIndex = -1;
            dataGridProducts.CellFormatting += (s, ev) =>
            {
                if (dataGridProducts.Columns[ev.ColumnIndex].Name == "image" && ev.Value == null)
                {
                    ev.FormattingApplied = true; // ไม่ให้มัน render ❌
                }
            };

            dataGridProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridProducts.MultiSelect = false;
            dataGridProducts.AllowUserToAddRows = false;

            // โหลดข้อมูลสินค้าจากฐานข้อมูลเมื่อเปิดหน้า
            LoadProductsFromDb();

        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            // ✅ อ่านค่าจากช่องกรอก
            string name = txtName.Text.Trim();
            string category = cmbCategory.Text.Trim();
            string price = txtPrice.Text.Trim();
            string stock = txtStock.Text.Trim();

            // ✅ ตรวจว่ากรอกครบไหม
            if (name == "" || category == "" || price == "" || stock == "")
            {
                MessageBox.Show("กรุณากรอกข้อมูลให้ครบ", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ✅ ตรวจว่ามีรูปภาพไหม
            if (pictureBoxProduct.Image == null)
            {
                MessageBox.Show("กรุณาเลือกรูปภาพก่อน", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ✅ กำหนด prefix ของหมวดหมู่สินค้า
            string prefix = "";
            switch (category)
            {
                case "เวียนนัวเซอรี (Viennoiserie)": prefix = "VN"; break;
                case "ขนมปังยุโรป (Artisan Breads)": prefix = "AB"; break;
                case "ปาทิสเซอรี (Pâtisserie)": prefix = "PA"; break;
                case "แซนด์วิช / ของคาว (Sandwich & Savory)": prefix = "SS"; break;
                default: prefix = "XX"; break;
            }

            string productId = txtProductId.Text.Trim();
            byte[] imgBytes = ImageToBytes(pictureBoxProduct.Image);

            try
            {
                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    // 🧩 ตรวจว่ามีชื่อสินค้าซ้ำหรือไม่
                    string checkSql = "SELECT product_id FROM products WHERE LOWER(TRIM(name)) = LOWER(TRIM(@name)) LIMIT 1";
                    using (var checkCmd = new MySqlCommand(checkSql, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@name", name);
                        var result = checkCmd.ExecuteScalar();

                        if (result != null)
                        {
                            // ✅ ถ้ามีสินค้าชื่อซ้ำ
                            string existingId = result.ToString();

                            DialogResult confirm = MessageBox.Show(
                                $"พบสินค้าชื่อเดียวกันในระบบ (รหัส: {existingId})\nต้องการอัปเดตข้อมูลสินค้านี้หรือไม่?",
                                "ยืนยันการอัปเดตสินค้า",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question);

                            if (confirm == DialogResult.Yes)
                            {
                                // ✅ ถ้าเปลี่ยนหมวดหมู่ ให้สร้างรหัสใหม่อัตโนมัติ
                                if (!existingId.StartsWith(prefix))
                                {
                                    string getLastSql = "SELECT product_id FROM products WHERE product_id LIKE @prefix ORDER BY product_id DESC LIMIT 1";
                                    using (var getCmd = new MySqlCommand(getLastSql, conn))
                                    {
                                        getCmd.Parameters.AddWithValue("@prefix", prefix + "%");
                                        var last = getCmd.ExecuteScalar();

                                        if (last != null)
                                        {
                                            string lastId = last.ToString();
                                            int lastNum = int.Parse(lastId.Substring(2));
                                            productId = prefix + (lastNum + 1).ToString("D3");
                                        }
                                        else
                                        {
                                            productId = prefix + "001";
                                        }
                                    }
                                }

                                // 🔄 อัปเดตสินค้าเดิม
                                string updateSql = @"UPDATE products 
                                    SET product_id=@id,
                                        category=@category,
                                        price=@price,
                                        stock=@stock,
                                        image=@image 
                                       WHERE TRIM(LOWER(name)) = LOWER(@name)";

                                using (var cmd = new MySqlCommand(updateSql, conn))
                                {
                                    cmd.Parameters.AddWithValue("@id", productId);
                                    cmd.Parameters.AddWithValue("@name", name);
                                    cmd.Parameters.AddWithValue("@category", category);
                                    cmd.Parameters.AddWithValue("@price", price);
                                    cmd.Parameters.AddWithValue("@stock", stock);
                                    cmd.Parameters.AddWithValue("@image", imgBytes);
                                    cmd.ExecuteNonQuery();
                                }

                                MessageBox.Show($"🟡 แก้ไขสินค้าเดิมสำเร็จ! (รหัสใหม่: {productId})",
                                    "อัปเดตเรียบร้อย", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("❎ ยกเลิกการอัปเดตสินค้า", "ยกเลิก",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        }
                        else
                        {
                            // 🆕 ถ้าไม่มีชื่อซ้ำ → เพิ่มสินค้าใหม่
                            string newProductId;

                            string getLastSql = "SELECT product_id FROM products WHERE product_id LIKE @prefix ORDER BY product_id DESC LIMIT 1";
                            using (var getCmd = new MySqlCommand(getLastSql, conn))
                            {
                                getCmd.Parameters.AddWithValue("@prefix", prefix + "%");
                                var last = getCmd.ExecuteScalar();

                                if (last != null)
                                {
                                    string lastId = last.ToString();
                                    int lastNum = int.Parse(lastId.Substring(2));
                                    newProductId = prefix + (lastNum + 1).ToString("D3");
                                }
                                else
                                {
                                    newProductId = prefix + "001";
                                }
                            }

                            string insertSql = @"INSERT INTO products (product_id, name, category, price, stock, image)
                                         VALUES (@id, @name, @category, @price, @stock, @image)";
                            using (var cmd = new MySqlCommand(insertSql, conn))
                            {
                                cmd.Parameters.AddWithValue("@id", newProductId);
                                cmd.Parameters.AddWithValue("@name", name);
                                cmd.Parameters.AddWithValue("@category", category);
                                cmd.Parameters.AddWithValue("@price", price);
                                cmd.Parameters.AddWithValue("@stock", stock);
                                cmd.Parameters.AddWithValue("@image", imgBytes);
                                cmd.ExecuteNonQuery();
                            }

                            MessageBox.Show($"✅ เพิ่มสินค้าใหม่สำเร็จ! (รหัส: {newProductId})",
                                "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }

                // ✅ โหลดข้อมูลใหม่หลังจากเพิ่ม/แก้ไข
                LoadProductsFromDb();
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ เกิดข้อผิดพลาด: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // ✅ สร้างรหัสอัตโนมัติใหม่ทุกครั้งหลังเพิ่มเสร็จ
            cmbCategory_SelectedIndexChanged(null, null);

            // 🧼 เคลียร์ฟอร์มหลังเพิ่ม/อัปเดตเสร็จ
            txtName.Clear();
            txtPrice.Clear();
            txtStock.Clear();
            cmbCategory.SelectedIndex = -1;
            pictureBoxProduct.Image = null;
            txtProductId.Clear();
            dataGridProducts.ClearSelection();
        }







        private void btnDeleteProduct_Click(object sender, EventArgs e)
        {
            // ✅ ตรวจสอบว่ามีการเลือกแถวหรือไม่
            if (dataGridProducts.SelectedRows.Count == 0)
            {
                MessageBox.Show("กรุณาเลือกสินค้าที่ต้องการลบก่อน", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ✅ ดึง product_id ของสินค้าที่เลือก
            DataGridViewRow selectedRow = dataGridProducts.SelectedRows[0];
            string productId = selectedRow.Cells["product_id"].Value?.ToString();

            if (string.IsNullOrEmpty(productId))
            {
                MessageBox.Show("❌ ไม่พบรหัสสินค้า!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // ✅ ยืนยันการลบ
            DialogResult result = MessageBox.Show(
                $"คุณแน่ใจหรือไม่ว่าต้องการลบสินค้ารหัส {productId} ?",
                "ยืนยันการลบ",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.No)
                return;

            try
            {
                // ✅ ลบข้อมูลออกจากฐานข้อมูล
                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "DELETE FROM products WHERE product_id = @id";
                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", productId);
                        cmd.ExecuteNonQuery();
                    }
                }

                // ✅ ลบออกจาก DataGridView ด้วย
                dataGridProducts.Rows.Remove(selectedRow);

                MessageBox.Show("✅ ลบสินค้าเรียบร้อยแล้ว!", "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ เกิดข้อผิดพลาดขณะลบสินค้า: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // ✅ เคลียร์ช่องกรอก
            txtName.Clear();
            txtPrice.Clear();
            txtStock.Clear();
            cmbCategory.SelectedIndex = -1;
            pictureBoxProduct.Image = null;
        }


        private void btnEditProduct_Click(object sender, EventArgs e)
        {
            if (dataGridProducts.SelectedRows.Count == 0)
            {
                MessageBox.Show("กรุณาเลือกสินค้าที่ต้องการแก้ไขก่อน", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ✅ ดึงข้อมูลจากช่องกรอก
            string name = txtName.Text.Trim();
            string category = cmbCategory.Text.Trim();
            string price = txtPrice.Text.Trim();
            string stock = txtStock.Text.Trim();

            if (name == "" || category == "" || price == "" || stock == "")
            {
                MessageBox.Show("กรุณากรอกข้อมูลให้ครบทุกช่องก่อนแก้ไข", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ✅ ดึงรหัสสินค้าเดิม
            DataGridViewRow selectedRow = dataGridProducts.SelectedRows[0];
            string oldProductId = selectedRow.Cells["product_id"].Value?.ToString();

            // ✅ สร้าง prefix ใหม่ตามหมวดหมู่
            string prefix = "";
            switch (category)
            {
                case "เวียนนัวเซอรี (Viennoiserie)": prefix = "VN"; break;
                case "ขนมปังยุโรป (Artisan Breads)": prefix = "AB"; break;
                case "ปาทิสเซอรี (Pâtisserie)": prefix = "PA"; break;
                case "แซนด์วิช / ของคาว (Sandwich & Savory)": prefix = "SS"; break;
                default: prefix = "XX"; break;
            }

            // ✅ ตรวจว่ารหัสใหม่ควรเริ่มจากอะไร
            string newProductId = oldProductId;
            if (!oldProductId.StartsWith(prefix))
            {
                try
                {
                    using (var conn = new MySqlConnection(connectionString))
                    {
                        conn.Open();
                        string sqlGet = "SELECT product_id FROM products WHERE product_id LIKE @prefix ORDER BY product_id DESC LIMIT 1";
                        using (var cmd = new MySqlCommand(sqlGet, conn))
                        {
                            cmd.Parameters.AddWithValue("@prefix", prefix + "%");
                            var result = cmd.ExecuteScalar();

                            if (result != null)
                            {
                                string lastId = result.ToString();
                                int lastNum = int.Parse(lastId.Substring(2));
                                newProductId = prefix + (lastNum + 1).ToString("D3");
                            }
                            else
                            {
                                newProductId = prefix + "001";
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("⚠️ อ่านรหัสใหม่ไม่สำเร็จ: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // ✅ แปลงรูปเป็น byte[]
            byte[] imgBytes = ImageToBytes(pictureBoxProduct.Image);

            // ✅ อัปเดตลงฐานข้อมูล
            try
            {
                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    string sql = @"UPDATE products 
                           SET product_id = @newId,
                               name = @name,
                               category = @category,
                               price = @price,
                               stock = @stock,
                               image = @image 
                           WHERE product_id = @oldId";

                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@newId", newProductId);
                        cmd.Parameters.AddWithValue("@oldId", oldProductId);
                        cmd.Parameters.AddWithValue("@name", name);
                        cmd.Parameters.AddWithValue("@category", category);
                        cmd.Parameters.AddWithValue("@price", price);
                        cmd.Parameters.AddWithValue("@stock", stock);
                        cmd.Parameters.AddWithValue("@image", imgBytes);
                        cmd.ExecuteNonQuery();
                    }
                }

                // ✅ อัปเดตใน DataGridView ด้วย
                selectedRow.Cells["product_id"].Value = newProductId;
                selectedRow.Cells["name"].Value = name;
                selectedRow.Cells["category"].Value = category;
                selectedRow.Cells["price"].Value = price;
                selectedRow.Cells["stock"].Value = stock;
                selectedRow.Cells["image"].Value = pictureBoxProduct.Image;

                MessageBox.Show($"✅ อัปเดตสินค้าสำเร็จ! (รหัสใหม่: {newProductId})", "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ เกิดข้อผิดพลาดขณะอัปเดตสินค้า: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // ✅ รีโหลดตาราง
            LoadProductsFromDb();

            // ✅ เคลียร์ช่องกรอก
            txtName.Clear();
            txtPrice.Clear();
            txtStock.Clear();
            cmbCategory.SelectedIndex = -1;
            pictureBoxProduct.Image = null;
            txtProductId.Clear();
        }



        private void txtStock_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPrice_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            string category = cmbCategory.Text.Trim();
            string prefix = "";

            // ✅ ตั้ง prefix ตามหมวดหมู่
            switch (category)
            {
                case "เวียนนัวเซอรี (Viennoiserie)": prefix = "VN"; break;
                case "ขนมปังยุโรป (Artisan Breads)": prefix = "AB"; break;
                case "ปาทิสเซอรี (Pâtisserie)": prefix = "PA"; break;
                case "แซนด์วิช / ของคาว (Sandwich & Savory)": prefix = "SS"; break;
                default: prefix = "XX"; break;
            }

            // ✅ ดึงรหัสล่าสุดของหมวดหมู่จากฐานข้อมูลแบบเรียลไทม์
            try
            {
                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "SELECT product_id FROM products WHERE product_id LIKE @prefix ORDER BY product_id DESC LIMIT 1";
                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@prefix", prefix + "%");
                        var result = cmd.ExecuteScalar();

                        string nextId;
                        if (result != null)
                        {
                            string lastId = result.ToString(); // เช่น VN005
                            int lastNumber = int.Parse(lastId.Substring(2)); // 5
                            nextId = prefix + (lastNumber + 1).ToString("D3"); // VN006
                        }
                        else
                        {
                            nextId = prefix + "001"; // ถ้าไม่มีข้อมูลในหมวดนี้เลย
                        }

                        // ✅ แสดงรหัสในกล่องข้อความ
                        txtProductId.Text = nextId;
                    }
                }
            }
            catch (Exception ex)
            {
                txtProductId.Text = "XX000";
                MessageBox.Show("⚠️ ไม่สามารถสร้างรหัสสินค้าได้: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void lblTitle_Click(object sender, EventArgs e)
        {

        }

        private void lblCategory_Click(object sender, EventArgs e)
        {

        }

        private void lblPrice_Click(object sender, EventArgs e)
        {

        }

        private void lblStock_Click(object sender, EventArgs e)
        {

        }

        private void dataGridProducts_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void lblName_Click(object sender, EventArgs e)
        {

        }

        private void lblTitle_Click_1(object sender, EventArgs e)
        {

        }

        private void dataGridProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridProducts.Rows[e.RowIndex];
                txtName.Text = row.Cells["name"].Value?.ToString();
                cmbCategory.Text = row.Cells["category"].Value?.ToString();
                txtPrice.Text = row.Cells["price"].Value?.ToString();
                txtStock.Text = row.Cells["stock"].Value?.ToString();

                if (row.Cells["image"].Value is Image img)
                {
                    pictureBoxProduct.Image = img;
                }
                else
                {
                    pictureBoxProduct.Image = null;
                }

                // ✅ ถ้าคลิกที่คอลัมน์รูป → เปลี่ยนภาพ
                if (dataGridProducts.Columns[e.ColumnIndex].Name == "image")
                {
                    OpenFileDialog ofd = new OpenFileDialog();
                    ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        Image newImg = Image.FromFile(ofd.FileName);
                        row.Cells["image"].Value = newImg;
                        pictureBoxProduct.Image = newImg;
                    }
                }
            }
        }



        private void btnSelectImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                // 🧩 โหลดภาพแบบ “clone” ทันที ป้องกันไฟล์ถูกล็อก
                using (var tempImg = Image.FromFile(ofd.FileName))
                {
                    pictureBoxProduct.Image = new Bitmap(tempImg);
                }
            }
        }


        private void pictureBoxProduct_Click(object sender, EventArgs e)
        {

        }

        // ✅ แปลงรูปภาพเป็น byte[] อย่างปลอดภัย (แก้ GDI+ error)
        private byte[] ImageToBytes(Image img)
        {
            if (img == null) return null;

            using (var ms = new MemoryStream())
            {
                // 🧩 สร้างสำเนาของภาพใหม่แทนการใช้ไฟล์เดิม (กัน GDI+ error)
                using (var clone = new Bitmap(img))
                {
                    clone.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                }
                return ms.ToArray();
            }
        }


        // ✅ แปลง byte[] จากฐานข้อมูลกลับเป็น Image (แบบปลอดภัย ไม่ล็อก Stream)
        private Image BytesToImage(object dbValue)
        {
            if (dbValue == null || dbValue is DBNull) return null;

            var bytes = (byte[])dbValue;

            using (var ms = new MemoryStream(bytes))
            {
                // 🧩 สร้างสำเนาใหม่ของภาพเพื่อไม่ให้ Stream ถูกล็อก
                using (var tempImg = Image.FromStream(ms))
                {
                    return new Bitmap(tempImg);
                }
            }
        }


        // ✅ โหลดข้อมูลจากฐานข้อมูลมาแสดงใน DataGridView
        private void LoadProductsFromDb()
        {
            dataGridProducts.Rows.Clear();

            try
            {
                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    string sql = "SELECT product_id, name, category, price, stock, image FROM products ORDER BY product_id ASC";

                    using (var cmd = new MySqlCommand(sql, conn))
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            Image img = BytesToImage(rd["image"]);

                            // 🟢 ใช้รหัสจริงจากฐานข้อมูล
                            string productId = rd["product_id"].ToString();

                            dataGridProducts.Rows.Add(
                                productId,
                                rd["name"].ToString(),
                                rd["category"].ToString(),
                                rd["price"].ToString(),
                                rd["stock"].ToString(),
                                img
                            );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("โหลดข้อมูลล้มเหลว: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtProductId_TextChanged(object sender, EventArgs e)
        {

        }
    }
}