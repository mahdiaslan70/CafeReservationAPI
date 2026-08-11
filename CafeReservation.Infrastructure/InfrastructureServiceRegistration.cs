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
using CafeReservation.Application.Common.Interfaces.Persistence;
using System.Data;
using Microsoft.Data.SqlClient;

namespace CafeReservation.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IDbConnection>(sp => 
            new SqlConnection(configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.SectionName));

            services.AddScoped<IMenuItemRepository, MenuItemRepository>();
            services.AddSingleton<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddSingleton<IJwtProvider, JwtProvider>();

            return services;
        }
    }
}
