using CafeReservation.Application.Features.Users.Queries;
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
        Task<IEnumerable<UserInfoDTO>> GetAllUsersWithRolesAsync(CancellationToken cancellationToken);
        Task<UserInfoDTO?> GetUserInfoByEmailAsync(string email, CancellationToken cancellationToken);
        Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken);
        //Task<IEnumerable<string>> GetUserRolesAsync(string email, CancellationToken cancellationToken);
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
