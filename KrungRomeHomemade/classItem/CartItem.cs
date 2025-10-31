using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace KrungRomeHomemade
{
    public class CartItem
    {
        public string Name { get; set; }      // ชื่อสินค้า
        public decimal Price { get; set; }    // ราคาต่อชิ้น
        public int Quantity { get; set; }     // จำนวน
        public Image Image { get; set; }      // รูปสินค้า
    }

}