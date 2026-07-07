using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace CafeReservation.Application.Features.MenuItems.Queries
{
    public class GetMenuItemByIdQuryHandler : IRequestHandler<GetMenuItemByIdQuery, MenuItemDTO>
    {
        private readonly string? _connectionString;

        public GetMenuItemByIdQuryHandler(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<MenuItemDTO> Handle(GetMenuItemByIdQuery request, CancellationToken cancellationToken)
        {
            var connection = new SqlConnection(_connectionString);
            var sql = "SELECT Id, Name, Description, Price FROM MenuItems WHERE Id=@Id";

            MenuItemDTO? item = await connection.QueryFirstOrDefaultAsync<MenuItemDTO>(
                sql,
                new { Id = request.Id });


            return item;
        }


    }
}
