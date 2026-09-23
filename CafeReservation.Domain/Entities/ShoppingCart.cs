using System;
using System.Collections.Generic;
using System.Text;

namespace CafeReservation.Domain.Entities
{
    public class ShoppingCart
    {
        public Guid Id { get; private set; }
        public Guid? UserId { get; private set; }
        public Guid? GuestId { get; private set; }
        public decimal TotalPrice => _items.Sum(item => item.Price * item.Quantity);

        private readonly List<ShoppingCartItem> _items = new();
        public IReadOnlyCollection<ShoppingCartItem> Items => _items.AsReadOnly();

        private ShoppingCart()
        {

        }

        public static ShoppingCart Create(
            Guid? userId,
            Guid? guestId)
        {
            var cart = new ShoppingCart
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                GuestId = guestId,

            };

            return cart;
        }

        public void AddToCart(
            int menuItemId,
            string name,
            decimal price,
            int quantity)
        {
            var existingItem = _items.FirstOrDefault(item => item.MenuItemId == menuItemId);

            if (existingItem != null)
            {
                existingItem.IncreaseQuantity(quantity);
            }

            else
            {
                _items.Add(new ShoppingCartItem(menuItemId, name, price, quantity));
            }
        }

        public void RemoveFromCart(int itemId)
        {
            var item = _items.FirstOrDefault(item => item.Id == itemId);
            if (item != null)
            {
                _items.Remove(item);
            }
        }
    }


}
