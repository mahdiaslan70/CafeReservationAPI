using CafeReservation.Application.Features.Authentication.Commands.Register;
using CafeReservation.Application.Features.Authentication.Queries.Login;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens.Experimental;

namespace CafeReservation.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthenticationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterCommand command)
        {
            var userId = await _mediator.Send(command);

            return Ok(new { Message = "User registered successfully !", UserId = userId });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginQuery query)
        {
            var token = await _mediator.Send(query);

            if (token == null)
                return BadRequest(new { Message = "Invalid Username or Password !" });

            return Ok(new { Message = "Login was successful !", Token = token });
        }
    }
}
