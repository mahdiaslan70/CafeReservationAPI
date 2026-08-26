using CafeReservation.Application.Common.Interfaces.Persistence;
using CafeReservation.Application.Features.Users.Commands.Roles;
using CafeReservation.Application.Features.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CafeReservation.WebAPI.Controllers
{
    //[Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AllUsersInfo(GetAllUsersInfoQuery query, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpPost("info")]
        public async Task<IActionResult> UserInfo([FromBody] GetUserInfoQuery query, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpPost("assign-role")]
        public async Task<IActionResult> AssignRole([FromBody] AssignRoleToUserCommand command, CancellationToken cancellationToken)
        {
            await _mediator.Send(command);

            return Ok(new { Message = "Role was assigned to user successfully !" });
        }

        [HttpPost("remove-role")]
        public async Task<IActionResult> RemoveRole([FromBody] RemoveRoleFromUserCommand command, CancellationToken cancellationToken)
        {
            await _mediator.Send(command);

            return Ok(new { Message = "Role was removed from user successfully !" });
        }


    }
}
