using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using CafeReservation.Domain.Entities;
using CafeReservation.Domain.Repositories;
using CafeReservation.Infrastructure.Persistence;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace CafeReservation.Infrastructure.Persistence.Repositories
{
    public class MenuItemRepository : IMenuItemRepository
    {
        private readonly string _connectionString;
        private readonly ApplicationDbContext _context;

        public MenuItemRepository(ApplicationDbContext context, IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new ArgumentNullException("Connection string not found !");

            _context = context;

        }

        public async Task<int> AddAsync(MenuItem menuItem)
        {
            await _context.MenuItems.AddAsync(menuItem);
            await _context.SaveChangesAsync();

            return menuItem.Id;
        }

        public async Task<IEnumerable<MenuItem>> GetAllAsync()
        {
            const string sql = "SELECT Id, Name, Description, Price, IsAvailable FROM MenuItems";

            using IDbConnection connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<MenuItem>(sql);
        }

    }
}
