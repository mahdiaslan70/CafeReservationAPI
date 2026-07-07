using CafeReservation.Application.Features.MenuItems.Queries;
using CafeReservation.Infrastructure.Persistence;
using Dapper;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace CafeReservation.Application.Features.MenuItems.Commands.UpdateMenuItem
{
    public class UpdateMenuItemCommandHandler : IRequestHandler<UpdateMenuItemCommand, bool>
    {
        private readonly string? _connectionString;
        public UpdateMenuItemCommandHandler(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<bool> Handle(UpdateMenuItemCommand request, CancellationToken cancellationToken)
        {
            using var connection = new SqlConnection(_connectionString);

            const string sql = @"
                UPDATE MenuItems
                SET Name = @Name,
                    Description = @Description,
                    Price = @Price
                WHERE Id=@Id";

            int affectedRows = await connection.ExecuteAsync(sql, request);

            return affectedRows > 0;
        }
    }
}
