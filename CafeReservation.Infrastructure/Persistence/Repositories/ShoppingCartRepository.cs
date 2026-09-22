using CafeReservation.Application.Common.Interfaces.ShoppingCart;
using CafeReservation.Domain.Entities;
using Dapper;
using FluentValidation.Validators;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CafeReservation.Infrastructure.Persistence.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly ApplicationDbContext _context;


        public ShoppingCartRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(ShoppingCart cart, CancellationToken cancellationToken)
        {
            bool exists = await _context.ShoppingCarts.AnyAsync(item => item.Id == cart.Id);
            if (!exists)
            {
                await _context.ShoppingCarts.AddAsync(cart, cancellationToken);
            }
        }

        public async Task<ShoppingCart?> GetCartAsync(Guid? userId, Guid? guestId, CancellationToken cancellationToken)
        {
            if (userId == null && guestId == null) return null;

            return await _context.ShoppingCarts
                .Include(sc => sc.Items)
                .Where(sc => (userId != null && sc.UserId == userId) ||
                (guestId != null && sc.GuestId == guestId))
                .OrderByDescending(sc => sc.UserId != null)
                .FirstOrDefaultAsync(cancellationToken);

        }

        public async Task RemoveAsync(Guid cartId, CancellationToken cancellationToken)
        {
            var cart = await _context.ShoppingCarts.FirstOrDefaultAsync(cart => cart.Id == cartId, cancellationToken);

            if (cart != null)
            {
                _context.ShoppingCarts.Remove(cart);
            }

        }
    }
}
