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

        private readonly ICartQueryService _queryService;

        public GetCartQueryHandler(ICartQueryService queryService)
        {
            _queryService = queryService;
        }
        public async Task<ShoppingCartDTO> Handle(GetCartQuery query, CancellationToken cancellationToken)
        {
            var user = await _queryService.GetCartAsync(query.UserId, query.GuestId);

            if (user == null)
            {
                throw new KeyNotFoundException("User not found !");
            }

            return user;
        }

    }
}
