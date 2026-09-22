using CafeReservation.Application.Features.ShoppingCart;
using System;
using System.Collections.Generic;
using System.Text;

namespace CafeReservation.Application.Common.Interfaces.ShoppingCart
{
    public interface ICartQueryService
    {
        Task<ShoppingCartDTO?> GetCartAsync(Guid? userId, Guid? guestId);
    }
}
