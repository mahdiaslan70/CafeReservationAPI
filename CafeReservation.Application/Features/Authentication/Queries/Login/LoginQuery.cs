using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CafeReservation.Application.Features.Authentication.Queries.Login
{
    public record LoginQuery(string email, string password) : IRequest<string>;
}
