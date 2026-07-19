using CafeReservation.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using CafeReservation.Domain.Repositories;
using CafeReservation.Infrastructure.Persistence.Repositories;
using CafeReservation.Application.Common.Interfaces.Authentication;
using CafeReservation.Infrastructure.Authentication;

namespace CafeReservation.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IMenuItemRepository, MenuItemRepository>();

            services.AddSingleton<IPasswordHasher, PasswordHasher>();

            return services;
        }
    }
}
