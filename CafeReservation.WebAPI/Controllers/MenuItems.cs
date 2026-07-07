using CafeReservation.Application.Features.MenuItems.Commands.CreateMenuItem;
using CafeReservation.Application.Features.MenuItems.Commands.DeleteMenuItem;
using CafeReservation.Application.Features.MenuItems.Commands.UpdateMenuItem;
using CafeReservation.Application.Features.MenuItems.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CafeReservation.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenuItemsController : ControllerBase
    {

        private readonly IMediator _mediator;

        public MenuItemsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateItem([FromBody] CreateMenuItemCommand command)
        {
            var id = _mediator.Send(command);

            return Ok(new { message = "Item was added to menu successfully !", ItemId = id });

        }

        [HttpGet]
        public async Task<IActionResult> GetAllItems()
        {
            var items = await _mediator.Send(new GetMenuItemsQuery());

            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetItemById(int id)
        {
            var item = await _mediator.Send(new GetMenuItemByIdQuery(id));

            if (item == null)
                return NotFound(new { message = "item was not found !" });

            return Ok(item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(int id, [FromBody] UpdateMenuItemRequest request)
        {
            var command = new UpdateMenuItemCommand(id, request.Name, request.Description, request.Price);

            bool successful = await _mediator.Send(command);

            if (successful)
                return Ok(new { message = "item was updated successfully !" });
            else
                return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemById(int id)
        {
            bool successful = await _mediator.Send(new DeleteMenuItemByIdCommand(id));
            if (successful)
                return Ok(new { message = "item was deleted successfully !" });
            else
                return NotFound();
        }
    }
}
