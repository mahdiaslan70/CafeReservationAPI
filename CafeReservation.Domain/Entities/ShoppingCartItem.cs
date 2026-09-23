using System;
using System.Collections.Generic;
using System.Text;

namespace CafeReservation.Domain.Entities
{
    public class ShoppingCartItem
    {
        public int Id { get; private set; }
        public Guid CartId { get; private set; }
        public int MenuItemId { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public decimal Price { get; private set; }
        public ShoppingCart Cart { get; private set; } = null!;
        public int Quantity { get; private set; }

        private ShoppingCartItem() { }

        internal ShoppingCartItem(int menuItemId, string name, decimal price, int quantity)
        {
            MenuItemId = menuItemId;
            Name = name;
            Price = price;
            Quantity = quantity > 0 ? quantity : 1;
        }

        internal void IncreaseQuantity(int amount)
        {
            Quantity += amount;
        }


    }
}
