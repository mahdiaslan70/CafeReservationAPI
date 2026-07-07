using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using CafeReservation.Domain.Repositories;
using MediatR;

namespace CafeReservation.Application.Features.MenuItems.Commands.CreateMenuItem
{
    internal class CreateMenuItemCommandHandler : IRequestHandler<CreateMenuItemCommand, int>
    {
        private readonly IMenuItemRepository _menuItemRepository;
        public CreateMenuItemCommandHandler(IMenuItemRepository menuItemRepository)
        {
            _menuItemRepository = menuItemRepository;
        }
        public async Task<int> Handle(CreateMenuItemCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
                throw new ArgumentNullException("نام آیتم نمیتواند خالی باشد");

            var menuItem = new Domain.Entities.MenuItem(request.Name, request.Description, request.Price);

            var registerId = await _menuItemRepository.AddAsync(menuItem);

            return registerId;
        }
    }
}
