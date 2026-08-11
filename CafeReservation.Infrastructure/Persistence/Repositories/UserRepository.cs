using CafeReservation.Application.Common.Interfaces.Persistence;
using CafeReservation.Domain.Entities;
using Dapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CafeReservation.Infrastructure.Persistence.Repositories
{

    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IDbConnection _dbConnection;
        public UserRepository(ApplicationDbContext context, IDbConnection dbConnection)
        {
            _context = context;
            _dbConnection = dbConnection;
        }
        public async Task AddAsync(User user, CancellationToken cancellationToken)
        {
            await _context.Users.AddAsync(user, cancellationToken);
            await _context.SaveChangesAsync();
        }

        public async Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<bool> IsEmailUniqueAsync(string email, CancellationToken cancellationToken)
        {
            return !await _context.Users.AnyAsync(u => u.Email == email, cancellationToken);

        }

        public async Task<IEnumerable<string>> GetUserRolesAsync(User user)
        {
            string sql = @"SELECT Roles.Name
                        FROM Roles r
                        INNER JOIN UserRoles ur
                        ON ur.RoleId = r.Id
                        INNER JOIN Users u ON UserRoles.UserId = u.Id
                        WHERE u.Id = @UserId";

            var roles = await _dbConnection.QueryAsync<string>(sql, new { UserId = user.Id });

            return roles;
        }
    }
}
