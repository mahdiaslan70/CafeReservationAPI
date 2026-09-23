using System;
using System.Collections.Generic;
using System.Text;

namespace CafeReservation.Application.Features.ShoppingCart
{
    public class ShoppingCartItemDTO
    {
        public int Id { get; set; }
        public Guid CartId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; } 
    }
}
