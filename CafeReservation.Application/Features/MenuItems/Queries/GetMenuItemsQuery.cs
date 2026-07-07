using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CafeReservation.Application.Features.MenuItems.Queries
{
    public class GetMenuItemsQuery : IRequest<IEnumerable<MenuItemDTO>>
    {

    }
}
