using CafeReservation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CafeReservation.Application.Common.Interfaces.Persistence
{
    public interface IUserRepository
    {
        Task<bool> IsEmailUniqueAsync(string email, CancellationToken cancellationToken);

        Task AddAsync(User user, CancellationToken cancellationToken);
    }
}
