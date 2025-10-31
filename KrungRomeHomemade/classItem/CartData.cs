using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
}
