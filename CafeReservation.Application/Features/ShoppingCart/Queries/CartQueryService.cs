using CafeReservation.Application.Common.Interfaces.ShoppingCart;
using CafeReservation.Domain.Entities;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CafeReservation.Application.Features.ShoppingCart.Queries
{
    public class CartQueryService : ICartQueryService
    {

        private readonly IDbConnection _dbConnection;

        public CartQueryService(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public async Task<ShoppingCartDTO?> GetCartAsync(Guid? userId, Guid? guestId)
        {
            if (userId == null && guestId == null) return null;

            string sql = @"SELECT 
                        sc.Id,
                        sc.UserId,
                        sc.GuestId,
                        sci.Id,
                        sci.CartId,
                        sci.Name,
                        sci.Price,
                        sci.Quantity
                        FROM ShoppingCarts sc 
                        LEFT JOIN ShoppingCartItems sci ON sc.Id = sci.CartId
                        WHERE (sc.UserId = @UserId OR sc.GuestId = @GuestId)";

            var command = new CommandDefinition(sql, new { UserId = userId, GuestId = guestId });

            var cartDictionary = new Dictionary<Guid, ShoppingCartDTO>();

            await _dbConnection.QueryAsync<ShoppingCartDTO, ShoppingCartItemDTO, ShoppingCartDTO>(
                command,
                (cart, item) =>
                {
                    if (!cartDictionary.TryGetValue(cart.Id, out var currentCart))
                    {
                        currentCart = cart;
                        cartDictionary.Add(currentCart.Id, currentCart);
                    }

                    if (item != null)
                    {
                        currentCart.Items.Add(item);
                    }

                    return currentCart;

                },
                splitOn: "Id");

            return cartDictionary.Values.FirstOrDefault();
        }
    }
}
