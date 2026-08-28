using CafeReservation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace CafeReservation.Application.Common.Interfaces.Authentication
{
    public interface ICurrentUserService
    {
        Guid? UserId { get; }
        List<string> Roles { get; }
        bool IsInRole(string roleName);

    }
}
