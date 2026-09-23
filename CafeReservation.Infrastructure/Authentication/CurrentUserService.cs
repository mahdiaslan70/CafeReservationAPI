using CafeReservation.Application.Common.Interfaces.Authentication;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace CafeReservation.Infrastructure.Authentication
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private ClaimsPrincipal? User => _httpContextAccessor.HttpContext?.User;
        public Guid? UserId
        {
            get
            {
                var userIdClaim = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                return Guid.TryParse(userIdClaim, out var id) ? id : null;
            }
        }

        public List<string> Roles
        {
            get
            {
                return User?.FindAll(ClaimTypes.Role)
                    .Select(c => c.Value)
                    .ToList() ?? new List<string>();
            }
        }

       // public Guid? ShoppingCartId => User?.FindFirst(ClaimTypes.)
        public bool IsInRole(string roleName) =>
            User?.IsInRole(roleName) ?? false;
    }
}
