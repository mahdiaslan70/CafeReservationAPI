using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CafeReservation.Application.Features.MenuItems.Commands.DeleteMenuItem
{
    public record DeleteMenuItemByIdCommand(int Id) : IRequest<bool>;
}
