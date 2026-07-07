using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CafeReservation.Application.Features.MenuItems.Queries
{
    //public class GetMenuItemByIdQuery : IRequest<MenuItemDTO>
    //{
    //    public int Id { get; set; }
    //}

    public record GetMenuItemByIdQuery(int Id) : IRequest<MenuItemDTO>;
}
