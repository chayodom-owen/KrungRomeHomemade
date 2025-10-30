using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace KrungRomeHomemade
{
    public partial class CartPage : Form
    {
        public CartPage()
        {
            InitializeComponent();
            this.Load += CartPage_Load;   // ผูกโหลด
        }

        private void CartPage_Load(object sender, EventArgs e)
        {

        }

        private void btnBack_Click(object sender, EventArgs e)
        {

            this.Close(); // ปิด Cart เพื่อให้ฟอร์มหลักเรียก ShowHome()
        }

  
        private void btnContinue_Click(object sender, EventArgs e)
        {

        }

        private void panelRight_Paint(object sender, PaintEventArgs e)
        {
            panelRight.BorderRadius = 10;
        }

        private void panelLeft_Paint(object sender, PaintEventArgs e)
        {

        }


        private void flowCartItems_Paint(object sender, PaintEventArgs e)
        {

        }

        private void picProduct_Click(object sender, EventArgs e)
        {

        }

        private void panelItem1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnPlus_Click(object sender, EventArgs e)
        {

        }

        private void lblQty_Click(object sender, EventArgs e)
        {

        }

        private void btnMinus_Click(object sender, EventArgs e)
        {

        }

        private void lblSubtotalValue_Click(object sender, EventArgs e)
        {

        }

        private void btnCheckout_Click(object sender, EventArgs e)
        {

        }

        private void lblPrice_Click(object sender, EventArgs e)
        {

        }
    }
}

