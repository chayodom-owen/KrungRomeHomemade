using System.Collections.Generic;

namespace KrungRomeHomemade
{
    public static class GlobalCart
    {
        // ✅ รายการสินค้าทั้งหมดในตะกร้า
        public static List<Cartitem> Items = new List<Cartitem>();
    }

    // ✅ คลาส CartItem (ข้อมูลของสินค้าหนึ่งชิ้น)
    public class Cartitem
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public System.Drawing.Image Image { get; set; }

        public decimal Total => Price * Quantity;

        public Cartitem(string name, decimal price, int quantity, System.Drawing.Image image)
        {
            Name = name;
            Price = price;
            Quantity = quantity;
            Image = image;
        }
    }
}
