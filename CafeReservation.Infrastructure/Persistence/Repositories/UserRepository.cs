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

        public async Task<IEnumerable<UserInfoDTO>> GetAllUsersInfoAsync(CancellationToken cancellationToken)
        {
            var sql = @"SELECT
                      u.Id,
                      u.FirstName,
                      u.LastName,
                      u.Email,
                      r.Id,
                      r.Name 
                      FROM Users u 
                      LEFT JOIN UserRoles ur ON u.Id = ur.UserId 
                      LEFT JOIN Roles r ON ur.RoleId = r.Id";

            var command = new CommandDefinition(sql, cancellationToken: cancellationToken);

            var usersDictionary = new Dictionary<Guid, UserInfoDTO>();

            await _dbConnection.QueryAsync<UserInfoDTO, Role, UserInfoDTO>(
               command,
               (user, role) =>
               {
                   if (!usersDictionary.TryGetValue(user.Id, out var currentUser))
                   {
                       currentUser = user;
                       usersDictionary.Add(currentUser.Id, currentUser);
                   }

                   if (role != null && !string.IsNullOrWhiteSpace(role.Name))
                   {
                       currentUser.Roles.Add(role.Name);
                   }

                   return currentUser;
               },
               splitOn: "Id"
               );
            return usersDictionary.Values;

        }

        public async Task<UserInfoDTO?> GetUserInfoByEmailAsync(string email, CancellationToken cancellationToken)
        {
            string sql = @"SELECT
                            u.Id,
                            u.FirstName,
                            u.LastName,
                            u.Email,
                            r.Name 
                            FROM Users u 
                            LEFT JOIN UserRoles ur ON u.Id = ur.UserId
                            LEFT JOIN Roles r ON r.Id = ur.RoleId
                            WHERE u.Email = @Email";



            var command = new CommandDefinition(sql, new { Email = email });

            var usersDictionary = new Dictionary<Guid, UserInfoDTO>();

            var userInfo = await _dbConnection.QueryAsync<UserInfoDTO, Role, UserInfoDTO>(
                command,
                (user, role) =>
                {
                    if (!usersDictionary.TryGetValue(user.Id, out var currentUser))
                    {
                        currentUser = user;
                        usersDictionary.Add(currentUser.Id, currentUser);
                    }

                    if (role != null && !string.IsNullOrWhiteSpace(role.Name))
                    {
                        currentUser.Roles.Add(role.Name);
                    }

                    return currentUser;
                });

            return usersDictionary.Values.FirstOrDefault();
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
