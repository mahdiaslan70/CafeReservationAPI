using CafeReservation.Application.Features.MenuItems.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CafeReservation.Application.Features.MenuItems.Commands.UpdateMenuItem
{
    public record UpdateMenuItemRequest(string Name, string Description, decimal Price);
    public record UpdateMenuItemCommand(int Id, string Name, string Description, decimal Price) : IRequest<bool>;


}
