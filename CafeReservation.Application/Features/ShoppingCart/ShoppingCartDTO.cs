using CafeReservation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CafeReservation.Application.Features.ShoppingCart
{
    public class ShoppingCartDTO
    {
        public Guid Id { get; set; }
        public Guid? UserId { get; set; }
        public Guid? GuestId { get; set; }
        public decimal TotalPrice => Items.Sum(item => item.Price * item.Quantity);
        public List<ShoppingCartItemDTO> Items { get; set; } = new List<ShoppingCartItemDTO>();
    }
}
