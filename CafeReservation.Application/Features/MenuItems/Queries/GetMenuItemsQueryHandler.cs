using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using Dapper;

namespace CafeReservation.Application.Features.MenuItems.Queries
{
    public class GetMenuItemsQueryHandler : IRequestHandler<GetMenuItemsQuery, IEnumerable<MenuItemDTO>>
    {
        private readonly string? _connectionString;

        public GetMenuItemsQueryHandler(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<MenuItemDTO>> Handle(GetMenuItemsQuery query, CancellationToken cancellationToken)
        {
            var connection = new SqlConnection(_connectionString);

            var sql = "SELECT Id, Name, Description, Price FROM MenuItems";

            return await connection.QueryAsync<MenuItemDTO>(sql);
        }
    }
}
