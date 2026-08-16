using CafeReservation.Application.Common.Interfaces.Persistence;
using CafeReservation.Application.Features.Users.Queries;
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

        public async Task<IEnumerable<UserInfoDTO>> GetAllUsersWithRolesAsync(CancellationToken cancellationToken)
        {
            var sql = @"SELECT
                      u.Id,
                      u.FirstName,
                      u.LastName,
                      u.Email,
                      r.Name AS RoleName
                      FROM Users u 
                      LEFT JOIN UserRoles ur ON u.Id = ur.UserId 
                      LEFT JOIN Roles r ON ur.RoleId = r.Id";

            var command = new CommandDefinition(sql, cancellationToken: cancellationToken);
            var flatResults = await _dbConnection.QueryAsync<FlatUserRole>(command);

            return flatResults
                .GroupBy(x => new { x.Id, x.FirstName, x.LastName, x.Email })
                .Select(g => new UserInfoDTO(
                    g.Key.Id.ToString(),
                    g.Key.FirstName,
                    g.Key.LastName,
                    g.Key.Email,
                    g.Select(r => r.RoleName).ToList()));



        }

        public async Task<UserInfoDTO?> GetUserInfoByEmailAsync(string email, CancellationToken cancellationToken)
        {
            string sql = @"SELECT
                            u.Id,
                            u.FirstName,
                            u.LastName,
                            u.Email,
                            r.Name AS RoleName
                            FROM Users u 
                            LEFT JOIN UserRoles ur ON u.Id = ur.UserId
                            LEFT JOIN Roles r ON r.Id = ur.RoleId
                            WHERE u.Email = @Email";

            var command = new CommandDefinition(sql, new { Email = email });

            var flatResult = await _dbConnection.QueryAsync<FlatUserRole>(command);

            return flatResult.GroupBy(x => new { x.Id, x.FirstName, x.LastName, x.Email })
                .Select(g =>
                new UserInfoDTO(
                    g.Key.Id.ToString(),
                    g.Key.FirstName,
                    g.Key.LastName,
                    g.Key.Email,
                    g.Select(x => x.RoleName).ToList())).FirstOrDefault();

        }

        public async Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
        {
            return await _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
        }

        

        public async Task<bool> IsEmailUniqueAsync(string email, CancellationToken cancellationToken)
        {

            return !await _context.Users.AnyAsync(u => u.Email == email, cancellationToken);

        }

        //public async Task<IEnumerable<string>> GetUserRolesAsync(string email, CancellationToken cancellationToken)
        //{
        //    string sql = @"SELECT r.Name
        //                FROM Roles r
        //                INNER JOIN UserRoles ur
        //                ON ur.RoleId = r.Id
        //                INNER JOIN Users u ON ur.UserId = u.Id
        //                WHERE u.Email = @Email";
        //    var command = new CommandDefinition(sql, new { Email = email }, cancellationToken: cancellationToken);

        //    return await _dbConnection.QueryAsync<string>(command);


        //}

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }



        public record FlatUserRole(
            Guid Id,
            string FirstName,
            string LastName,
            string Email,
            string RoleName);
    }
}
