using CafeReservation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace CafeReservation.Application.Common.Interfaces.ShoppingCart
{
    public interface IShoppingCartRepository
    {
        Task<Domain.Entities.ShoppingCart?> GetCartAsync(Guid? userId, Guid? guestId, CancellationToken cancellationToken);
        Task AddAsync(Domain.Entities.ShoppingCart Cart, CancellationToken cancellationToken);
        Task RemoveAsync(Guid cartId, CancellationToken cancellationToken);
    }
}
