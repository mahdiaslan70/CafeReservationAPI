using CafeReservation.Application.Common.Interfaces.Authentication;
using CafeReservation.Application.Common.Interfaces.ShoppingCart;
using CafeReservation.Domain.Entities;
using CafeReservation.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CafeReservation.Application.Features.ShoppingCart.Commands
{
    public class AddItemToCartCommandHandler : IRequestHandler<AddItemToCartCommand, bool>
    {
        private readonly IMenuItemRepository _menuItemRepository;
        private readonly IShoppingCartRepository _shoppingCartRepository;


        public AddItemToCartCommandHandler(
            IMenuItemRepository menuItemRepository,
            IShoppingCartRepository shoppingCartRepository)
        {
            _menuItemRepository = menuItemRepository;
            _shoppingCartRepository = shoppingCartRepository;
        }
        public async Task<bool> Handle(AddItemToCartCommand command, CancellationToken cancellationToken)
        {
            var cart = await _shoppingCartRepository.GetCartAsync(command.UserId, command.GuestId, cancellationToken);

            if (cart == null)
            {
                cart = Domain.Entities.ShoppingCart.Create(command.UserId, command.GuestId);
                await _shoppingCartRepository.AddAsync(cart, cancellationToken);
            }

            var item = await _menuItemRepository.GetItemAsync(command.MenuItemId);

            if (item == null)
            {
                throw new KeyNotFoundException();
            }

            cart.AddToCart(command.MenuItemId, item.Name, item.Price, command.Quantity);

            await _shoppingCartRepository.SaveChangesAsync(cancellationToken);

            return true;

        }
    }
}
