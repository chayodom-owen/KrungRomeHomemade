using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace KrungRomeHomemade
{
    public static class CartData
    {
        public static List<CartItem> Items { get; } = new List<CartItem>();

        public static decimal Subtotal => Items.Sum(i => i.Price * i.Quantity);

        public static void Clear()
        {
            Items.Clear();
        }
    }

    // ✅ คลาสสินค้า (เก็บรายละเอียดแต่ละชิ้น)
    public class CartItem
    {
        public string Name { get; set; }          // ชื่อสินค้า
        public decimal Price { get; set; }        // ราคาต่อชิ้น
        public int Quantity { get; set; }         // จำนวน
        public Image Image { get; set; }          // รูปภาพสินค้า
    }
}
