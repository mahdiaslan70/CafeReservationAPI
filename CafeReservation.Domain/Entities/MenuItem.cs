using System;
using System.Collections.Generic;
using System.Text;

namespace CafeReservation.Domain.Entities
{
    public class MenuItem
    {
        public int Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public decimal Price { get; private set; }
        public bool IsAvailable { get; private set; }

        public MenuItem(string name, string description, decimal price)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("نام آیتم کافه نمیتواند خالی باشد");

            if (price < 0)
                throw new ArgumentException("قیمت نمیتواند عدد منفی باشد");

            Name = name;
            Description = description;
            Price = price;
            IsAvailable = true;

        }


        public void UpdatePrice(decimal newPrice)
        {
            if (newPrice < 0)
                throw new ArgumentException("قیمت جدید نمیتواند منفی باشد");
            Price = newPrice;
        }

        public void ChangeAvailability(bool isAvailable)
        {
            IsAvailable = isAvailable;
        }
    }
}
