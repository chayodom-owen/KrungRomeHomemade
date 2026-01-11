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
using System.IO; // For image handling

namespace KrungRomeHomemade
{
    public partial class ProductPage : Form
    {
        bool isLoadingFromGrid = false;
        string connectionString = "server=localhost;user id=root;password=;database=krungrome_db;";

        public ProductPage()
        {
            InitializeComponent();
            dataGridProducts.AutoGenerateColumns = false;
        }

        private void ProductPage_Load(object sender, EventArgs e)
        {
            dataGridProducts.Columns.Clear();
            dataGridProducts.Columns.Add("product_id", "รหัสสินค้า");
            dataGridProducts.Columns.Add("name", "ชื่อสินค้า");
            dataGridProducts.Columns.Add("category", "หมวดหมู่");
            dataGridProducts.Columns.Add("price", "ราคา (บาท)");
            dataGridProducts.Columns.Add("stock", "คงเหลือ");
            dataGridProducts.Columns.Add("description", "คำอธิบายสินค้า");

            DataGridViewImageColumn imgCol = new DataGridViewImageColumn();
            imgCol.Name = "image";
            imgCol.HeaderText = "รูปสินค้า";
            imgCol.ImageLayout = DataGridViewImageCellLayout.Zoom;
            imgCol.ValueType = typeof(Image);
            imgCol.DefaultCellStyle.NullValue = null;
            dataGridProducts.Columns.Add(imgCol);

            dataGridProducts.RowTemplate.Height = 100;
            dataGridProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridProducts.DataError += (s, ev) => { ev.ThrowException = false; };
            dataGridProducts.ShowCellErrors = false;
            dataGridProducts.ShowEditingIcon = false;
            dataGridProducts.ShowRowErrors = false;

            // Load Categories from DB
            LoadCategoriesToComboBox();

            cmbCategory.SelectedIndex = -1;
            dataGridProducts.CellFormatting += (s, ev) =>
            {
                if (dataGridProducts.Columns[ev.ColumnIndex].Name == "image" && ev.Value == null)
                {
                    ev.FormattingApplied = true;
                }
            };

            dataGridProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridProducts.MultiSelect = false;
            dataGridProducts.AllowUserToAddRows = false;

            LoadProductsFromDb();
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            string category = cmbCategory.Text.Trim();
            string price = txtPrice.Text.Trim();
            string stock = txtStock.Text.Trim();

            if (name == "" || category == "" || price == "" || stock == "")
            {
                MessageBox.Show("กรุณากรอกข้อมูลให้ครบ", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (pictureBoxProduct.Image == null)
            {
                MessageBox.Show("กรุณาเลือกรูปภาพก่อน", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ✅ Get prefix dynamically from DB
            string prefix = GetCategoryPrefix(category);

            string productId = txtProductId.Text.Trim();
            byte[] imgBytes = ImageToBytes(pictureBoxProduct.Image);

            try
            {
                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    string checkSql = "SELECT product_id FROM products WHERE LOWER(TRIM(name)) = LOWER(TRIM(@name)) LIMIT 1";
                    using (var checkCmd = new MySqlCommand(checkSql, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@name", name);
                        var result = checkCmd.ExecuteScalar();

                        if (result != null)
                        {
                            string existingId = result.ToString();
                            DialogResult confirm = MessageBox.Show(
                                $"พบสินค้าชื่อเดียวกันในระบบ (รหัส: {existingId})\nต้องการอัปเดตข้อมูลสินค้านี้หรือไม่?",
                                "ยืนยันการอัปเดตสินค้า",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question);

                            if (confirm == DialogResult.Yes)
                            {
                                // Logic to generate new ID if needed (omitted for brevity, keeping existing logic safe)
                                // Assuming we keep the same ID or recalculate if category changed significantly

                                string updateSql = @"UPDATE products 
                                         SET category=@category,
                                             price=@price,
                                             stock=@stock,
                                             image=@image,
                                             description=@desc
                                         WHERE TRIM(LOWER(name)) = LOWER(@name)";

                                using (var cmd = new MySqlCommand(updateSql, conn))
                                {
                                    cmd.Parameters.AddWithValue("@name", name);
                                    cmd.Parameters.AddWithValue("@category", category);
                                    cmd.Parameters.AddWithValue("@price", price);
                                    cmd.Parameters.AddWithValue("@stock", stock);
                                    cmd.Parameters.AddWithValue("@image", imgBytes);
                                    cmd.Parameters.AddWithValue("@desc", txtDescription.Text.Trim());
                                    cmd.ExecuteNonQuery();
                                }
                                MessageBox.Show($"🟡 แก้ไขสินค้าเดิมสำเร็จ! (รหัส: {existingId})", "อัปเดตเรียบร้อย", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                return;
                            }
                        }
                        else
                        {
                            // Add New Product
                            string newProductId;
                            // Calculate next ID based on Prefix
                            string getLastSql = "SELECT product_id FROM products WHERE product_id LIKE @prefix ORDER BY product_id DESC LIMIT 1";
                            using (var getCmd = new MySqlCommand(getLastSql, conn))
                            {
                                getCmd.Parameters.AddWithValue("@prefix", prefix + "%");
                                var last = getCmd.ExecuteScalar();

                                if (last != null)
                                {
                                    string lastId = last.ToString();
                                    string numberPart = lastId.Substring(prefix.Length); // Smart substring
                                    int lastNum = int.Parse(numberPart);
                                    newProductId = prefix + (lastNum + 1).ToString("D3");
                                }
                                else
                                {
                                    newProductId = prefix + "001";
                                }
                            }

                            string insertSql = @"INSERT INTO products (product_id, name, category, price, stock, image, description)
                                                VALUES (@id, @name, @category, @price, @stock, @image, @desc)";

                            using (var cmd = new MySqlCommand(insertSql, conn))
                            {
                                cmd.Parameters.AddWithValue("@id", newProductId);
                                cmd.Parameters.AddWithValue("@name", name);
                                cmd.Parameters.AddWithValue("@category", category);
                                cmd.Parameters.AddWithValue("@price", price);
                                cmd.Parameters.AddWithValue("@stock", stock);
                                cmd.Parameters.AddWithValue("@image", imgBytes);
                                cmd.Parameters.AddWithValue("@desc", txtDescription.Text.Trim());
                                cmd.ExecuteNonQuery();
                            }

                            MessageBox.Show($"✅ เพิ่มสินค้าใหม่สำเร็จ! (รหัส: {newProductId})", "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                LoadProductsFromDb();
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ เกิดข้อผิดพลาด: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Reset
            cmbCategory_SelectedIndexChanged(null, null);
            txtName.Clear(); txtPrice.Clear(); txtStock.Clear();
            cmbCategory.SelectedIndex = -1; pictureBoxProduct.Image = null; txtProductId.Clear();
            dataGridProducts.ClearSelection();
        }

        private void btnDeleteProduct_Click(object sender, EventArgs e)
        {
            if (dataGridProducts.SelectedRows.Count == 0)
            {
                MessageBox.Show("กรุณาเลือกสินค้าที่ต้องการลบก่อน", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow selectedRow = dataGridProducts.SelectedRows[0];
            string productId = selectedRow.Cells["product_id"].Value?.ToString();

            if (string.IsNullOrEmpty(productId)) return;

            DialogResult result = MessageBox.Show($"คุณแน่ใจหรือไม่ว่าต้องการลบสินค้ารหัส {productId} ?", "ยืนยันการลบ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.No) return;

            try
            {
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
                dataGridProducts.Rows.Remove(selectedRow);
                MessageBox.Show("✅ ลบสินค้าเรียบร้อยแล้ว!", "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtName.Clear(); txtPrice.Clear(); txtStock.Clear();
                cmbCategory.SelectedIndex = -1; pictureBoxProduct.Image = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ เกิดข้อผิดพลาดขณะลบสินค้า: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEditProduct_Click(object sender, EventArgs e)
        {
            if (dataGridProducts.SelectedRows.Count == 0)
            {
                MessageBox.Show("กรุณาเลือกสินค้าที่ต้องการแก้ไขก่อน", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string name = txtName.Text.Trim();
            string category = cmbCategory.Text.Trim();
            string price = txtPrice.Text.Trim();
            string stock = txtStock.Text.Trim();

            if (name == "" || category == "" || price == "" || stock == "")
            {
                MessageBox.Show("กรุณากรอกข้อมูลให้ครบทุกช่องก่อนแก้ไข", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow selectedRow = dataGridProducts.SelectedRows[0];
            string oldProductId = selectedRow.Cells["product_id"].Value?.ToString();

            // ✅ Get Prefix from DB
            string prefix = GetCategoryPrefix(category);

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
                                string numberPart = lastId.Substring(prefix.Length);
                                int lastNum = int.Parse(numberPart);
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

            byte[] imgBytes = ImageToBytes(pictureBoxProduct.Image);

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
            LoadProductsFromDb();
            txtName.Clear(); txtPrice.Clear(); txtStock.Clear();
            cmbCategory.SelectedIndex = -1; pictureBoxProduct.Image = null; txtProductId.Clear();
        }

        // ✅ Calculate ID automatically when Category Changes
        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isLoadingFromGrid) return;

            string category = cmbCategory.Text.Trim();
            if (string.IsNullOrEmpty(category)) return;

            // 1. Get Prefix from DB
            string prefix = GetCategoryPrefix(category);

            try
            {
                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    // 2. Find last ID
                    string sql = "SELECT product_id FROM products WHERE product_id LIKE @prefix ORDER BY product_id DESC LIMIT 1";
                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@prefix", prefix + "%");
                        var result = cmd.ExecuteScalar();

                        string nextId;
                        if (result != null)
                        {
                            string lastId = result.ToString();
                            // 3. Smart substring based on prefix length
                            string numberPart = lastId.Substring(prefix.Length);
                            int lastNumber = int.Parse(numberPart);
                            nextId = prefix + (lastNumber + 1).ToString("D3");
                        }
                        else
                        {
                            nextId = prefix + "001";
                        }
                        txtProductId.Text = nextId;
                    }
                }
            }
            catch (Exception ex)
            {
                txtProductId.Text = "XX000";
                // MessageBox.Show("⚠️ Error: " + ex.Message);
            }
        }

        private void dataGridProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                isLoadingFromGrid = true;
                DataGridViewRow row = dataGridProducts.Rows[e.RowIndex];

                txtName.Text = row.Cells["name"].Value?.ToString();
                cmbCategory.Text = row.Cells["category"].Value?.ToString();
                txtPrice.Text = row.Cells["price"].Value?.ToString();
                txtStock.Text = row.Cells["stock"].Value?.ToString();
                txtDescription.Text = row.Cells["description"].Value?.ToString();
                txtProductId.Text = row.Cells["product_id"].Value?.ToString();

                if (row.Cells["image"].Value is Image img)
                    pictureBoxProduct.Image = img;
                else
                    pictureBoxProduct.Image = null;

                isLoadingFromGrid = false;
            }
        }

        private void btnSelectImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                using (var tempImg = Image.FromFile(ofd.FileName))
                {
                    pictureBoxProduct.Image = new Bitmap(tempImg);
                }
            }
        }

        private byte[] ImageToBytes(Image img)
        {
            if (img == null) return null;
            using (var ms = new MemoryStream())
            {
                using (var clone = new Bitmap(img))
                {
                    clone.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                }
                return ms.ToArray();
            }
        }

        private Image BytesToImage(object dbValue)
        {
            if (dbValue == null || dbValue is DBNull) return null;
            try
            {
                var bytes = (byte[])dbValue;
                if (bytes.Length < 50) return null;
                using (var ms = new MemoryStream(bytes))
                {
                    return new Bitmap(Image.FromStream(ms));
                }
            }
            catch { return null; }
        }

        private void LoadProductsFromDb()
        {
            dataGridProducts.Rows.Clear();
            try
            {
                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "SELECT product_id, name, category, price, stock, image, description FROM products ORDER BY product_id ASC";
                    using (var cmd = new MySqlCommand(sql, conn))
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            Image img = BytesToImage(rd["image"]);
                            string desc = rd["description"]?.ToString();
                            dataGridProducts.Rows.Add(
                                rd["product_id"].ToString(),
                                rd["name"].ToString(),
                                rd["category"].ToString(),
                                rd["price"].ToString(),
                                rd["stock"].ToString(),
                                desc,
                                img
                            );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("โหลดข้อมูลล้มเหลว: " + ex.Message);
            }
        }

        // ✅ 1. Category Management - Add
        private void Addcategory_Click(object sender, EventArgs e)
        {
            string newCategoryName = "";
            string newPrefix = "";

            // Call the new 2-field input box
            DialogResult result = ShowCategoryInputBox(ref newCategoryName, ref newPrefix);

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(newCategoryName) && !string.IsNullOrWhiteSpace(newPrefix))
            {
                try
                {
                    using (var conn = new MySqlConnection(connectionString))
                    {
                        conn.Open();
                        // Save Name AND Prefix
                        string sql = "INSERT INTO categories (category_name, category_prefix) VALUES (@name, @prefix)";
                        using (var cmd = new MySqlCommand(sql, conn))
                        {
                            cmd.Parameters.AddWithValue("@name", newCategoryName.Trim());
                            cmd.Parameters.AddWithValue("@prefix", newPrefix.Trim().ToUpper()); // Force Uppercase
                            cmd.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show($"✅ เพิ่มหมวดหมู่ '{newCategoryName}' (รหัส: {newPrefix}) เรียบร้อยแล้ว!", "สำเร็จ");

                    LoadCategoriesToComboBox();
                    cmbCategory.SelectedItem = newCategoryName.Trim();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("❌ เกิดข้อผิดพลาด: " + ex.Message);
                }
            }
        }

        // ✅ 2. Category Management - Delete
        private void btnDeleteCategory_Click(object sender, EventArgs e)
        {
            if (cmbCategory.SelectedIndex == -1 || string.IsNullOrEmpty(cmbCategory.Text))
            {
                MessageBox.Show("กรุณาเลือกหมวดหมู่ที่ต้องการลบจากรายการ", "แจ้งเตือน");
                return;
            }

            string selectedCategory = cmbCategory.Text.Trim();
            DialogResult result = MessageBox.Show($"คุณแน่ใจหรือไม่ว่าต้องการลบหมวดหมู่ '{selectedCategory}' ?", "ยืนยันการลบ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    using (var conn = new MySqlConnection(connectionString))
                    {
                        conn.Open();

                        string checkSql = "SELECT COUNT(*) FROM products WHERE category = @catName";
                        using (var checkCmd = new MySqlCommand(checkSql, conn))
                        {
                            checkCmd.Parameters.AddWithValue("@catName", selectedCategory);
                            int productCount = Convert.ToInt32(checkCmd.ExecuteScalar());

                            if (productCount > 0)
                            {
                                MessageBox.Show($"❌ ไม่สามารถลบหมวดหมู่ '{selectedCategory}' ได้\nเนื่องจากมีสินค้า {productCount} รายการ อยู่ในหมวดหมู่นี้", "ลบไม่ได้", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }

                        string deleteSql = "DELETE FROM categories WHERE category_name = @catName";
                        using (var delCmd = new MySqlCommand(deleteSql, conn))
                        {
                            delCmd.Parameters.AddWithValue("@catName", selectedCategory);
                            delCmd.ExecuteNonQuery();
                            MessageBox.Show("✅ ลบหมวดหมู่เรียบร้อยแล้ว", "สำเร็จ");

                            LoadCategoriesToComboBox();
                            cmbCategory.SelectedIndex = -1;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("เกิดข้อผิดพลาด: " + ex.Message);
                }
            }
        }

        // ✅ 3. Input Box with 2 Fields (Name + Prefix)
        public static DialogResult ShowCategoryInputBox(ref string catName, ref string catPrefix)
        {
            Form form = new Form();
            Label lblName = new Label() { Text = "ชื่อหมวดหมู่:", Left = 10, Top = 20, Width = 200 };
            TextBox txtName = new TextBox() { Left = 10, Top = 45, Width = 260, Text = catName };

            Label lblPrefix = new Label() { Text = "รหัสย่อ (2 ตัว เช่น AB, VN):", Left = 10, Top = 80, Width = 200 };
            TextBox txtPrefix = new TextBox() { Left = 10, Top = 105, Width = 100, MaxLength = 2, CharacterCasing = CharacterCasing.Upper, Text = catPrefix };

            Button buttonOk = new Button() { Text = "ตกลง", Left = 100, Width = 80, Top = 150, DialogResult = DialogResult.OK };
            Button buttonCancel = new Button() { Text = "ยกเลิก", Left = 190, Width = 80, Top = 150, DialogResult = DialogResult.Cancel };

            form.Text = "เพิ่มหมวดหมู่ใหม่";
            form.ClientSize = new Size(300, 200);
            form.Controls.AddRange(new Control[] { lblName, txtName, lblPrefix, txtPrefix, buttonOk, buttonCancel });
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;

            DialogResult dialogResult = form.ShowDialog();

            catName = txtName.Text.Trim();
            catPrefix = txtPrefix.Text.Trim();
            return dialogResult;
        }

        // ✅ 4. Load Categories
        private void LoadCategoriesToComboBox()
        {
            cmbCategory.Items.Clear();
            try
            {
                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "SELECT category_name FROM categories ORDER BY category_name ASC";
                    using (var cmd = new MySqlCommand(sql, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cmbCategory.Items.Add(reader["category_name"].ToString());
                        }
                    }
                }
            }
            catch
            {
                // Handle error silently or set default items if DB fails
            }
        }

        // ✅ 5. Get Prefix from DB
        private string GetCategoryPrefix(string categoryName)
        {
            string prefix = "XX";

            try
            {
                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "SELECT category_prefix FROM categories WHERE category_name = @name LIMIT 1";
                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@name", categoryName);
                        var result = cmd.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            prefix = result.ToString();
                        }
                        else
                        {
                            // Fallback: First 2 letters
                            if (categoryName.Length >= 2) prefix = categoryName.Substring(0, 2).ToUpper();
                        }
                    }
                }
            }
            catch { }
            return prefix;
        }

        // Empty Event Handlers
        private void txtProductId_TextChanged(object sender, EventArgs e) { }
        private void txtStock_TextChanged(object sender, EventArgs e) { }
        private void txtPrice_TextChanged(object sender, EventArgs e) { }
        private void lblTitle_Click(object sender, EventArgs e) { }
        private void lblCategory_Click(object sender, EventArgs e) { }
        private void lblPrice_Click(object sender, EventArgs e) { }
        private void lblStock_Click(object sender, EventArgs e) { }
        private void dataGridProducts_CellContentClick_1(object sender, DataGridViewCellEventArgs e) { }
        private void lblName_Click(object sender, EventArgs e) { }
        private void lblTitle_Click_1(object sender, EventArgs e) { }
        private void pictureBoxProduct_Click(object sender, EventArgs e) { }
    }
}