using CafeReservation.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CafeReservation.Application.Features.Users.Commands.Roles
{
    public record AssignRoleToUserCommand(
        string Email ,
        RoleType RoleToAssign) : IRequest<bool>;
}
