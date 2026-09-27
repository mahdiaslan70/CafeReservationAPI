using CafeReservation.Application.Common.Interfaces.Authentication;
using CafeReservation.Application.Common.Interfaces.ShoppingCart;
using MediatR;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace CafeReservation.Application.Features.ShoppingCart.Queries
{

    public record GetCartQuery(Guid? UserId, Guid? GuestId) : IRequest<ShoppingCartDTO>;
    public class GetCartQueryHandler : IRequestHandler<GetCartQuery, ShoppingCartDTO>
    {

        private readonly ICartQueryService _cartQueryService;

        public GetCartQueryHandler(ICartQueryService cartQueryService)
        {
            _cartQueryService = cartQueryService;
        }
        public async Task<ShoppingCartDTO> Handle(GetCartQuery query, CancellationToken cancellationToken)
        {
            var cart = await _cartQueryService.GetCartAsync(query.UserId, query.GuestId);

            if (cart == null)
            {
                throw new KeyNotFoundException("Cart was not found!");
            }

            return cart;
        }

    }
}
