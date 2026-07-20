using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CafeReservation.Application.Features.Authentication.Commands.Register
{
    public record RegisterCommand(
        string FirstName,
        string LastName,
        string Email,
        string Password) : IRequest<Guid>;
}
