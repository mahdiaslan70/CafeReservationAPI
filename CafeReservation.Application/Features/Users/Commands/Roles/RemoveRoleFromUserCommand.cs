using CafeReservation.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CafeReservation.Application.Features.Users.Commands.Roles
{
    public record RemoveRoleFromUserCommand(
        string Email,
        RoleType RoleToRemove) : IRequest<bool>;

}
