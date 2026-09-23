using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CafeReservation.Application.Features.ShoppingCart.Commands
{
    public record AddItemToCartCommand(
        Guid? UserId,
        Guid? GuestId,
        int MenuItemId,
        int Quantity) : IRequest<bool>;
}
