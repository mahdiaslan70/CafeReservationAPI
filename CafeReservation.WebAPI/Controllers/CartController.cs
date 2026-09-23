using CafeReservation.Application.Features.ShoppingCart.Commands;
using CafeReservation.Application.Features.ShoppingCart.Queries;
using CafeReservation.WebAPI.Contracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CafeReservation.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CartController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Index(GetCartQuery request)
        {
            var cart = await _mediator.Send(request);

            return Ok(cart);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddToCart([FromBody] AddItemToCartRequest request)
        {
            Guid? userId = null;
            Guid? guestId = null;

            if (User.Identity?.IsAuthenticated == true)
            {
                string? userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (Guid.TryParse(userIdStr, out var parsedUserId))
                {
                    userId = parsedUserId;
                }
            }

            else if (Request.Headers.TryGetValue("X", out var headerGuestId))
            {
                if (Guid.TryParse(headerGuestId, out var parsedGuestId))
                {
                    guestId = parsedGuestId;
                }
            }

            if (userId == null && guestId == null)
            {
                return BadRequest();
            }

            var command = new AddItemToCartCommand(userId, guestId, request.MenuItemId, request.Quantity);
            await _mediator.Send(command);

            return Ok();
        }
    }
}
