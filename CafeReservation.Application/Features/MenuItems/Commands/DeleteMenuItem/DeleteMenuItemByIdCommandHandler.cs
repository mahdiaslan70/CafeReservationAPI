using Dapper;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace CafeReservation.Application.Features.MenuItems.Commands.DeleteMenuItem
{
    public class DeleteMenuItemByIdCommandHandler : IRequestHandler<DeleteMenuItemByIdCommand, bool>
    {

        private readonly string? _connectionString;

        public DeleteMenuItemByIdCommandHandler(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<bool> Handle(DeleteMenuItemByIdCommand request, CancellationToken cancellationToken)
        {
            using var connection = new SqlConnection(_connectionString);
            const string sql = "DELETE FROM MenuItems WHERE Id=@Id";
            int affectedRows = await connection.ExecuteAsync(sql, new { Id = request.Id });

            return affectedRows > 0;
        }
    }
}
