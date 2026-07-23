using CafeReservation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CafeReservation.Application.Common.Interfaces.Authentication
{
    public interface IJwtProvider
    {
        Task<string> Generate(User user);
    }
}
