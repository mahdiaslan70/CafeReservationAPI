using CafeReservation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CafeReservation.Application.Features.Users.Queries
{
    public record UserInfoDTO(
        string Id,
        string FirstName,
        string LastName,
        string Email,
        List<string> Roles);
}
