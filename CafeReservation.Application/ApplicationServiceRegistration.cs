using CafeReservation.Application.Common.Interfaces.ShoppingCart;
using CafeReservation.Application.Features.ShoppingCart.Queries;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace CafeReservation.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<ICartQueryService, CartQueryService>();

            return services;
        }
    }
}
