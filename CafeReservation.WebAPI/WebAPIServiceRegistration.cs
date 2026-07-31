using CafeReservation.Infrastructure.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;
using System.Runtime.CompilerServices;
using System.Text;

namespace CafeReservation.WebAPI
{
    public static class WebAPIServiceRegister
    {
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtOptions = configuration.GetSection(JwtOptions.SectionName).Get<JwtOptions>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtOptions.Issuer,
                        ValidAudience = jwtOptions.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Secret)),
                        ClockSkew = TimeSpan.Zero

                    };
                });

            return services;
        }


        public static IServiceCollection AddOpenApiDocumentation(this IServiceCollection services)
        {
            services.AddOpenApi(options =>
                options.AddDocumentTransformer((document, context, cancelationToken) =>
                {
                    var securityScheme = new OpenApiSecurityScheme
                    {
                        Name = "Authorization",
                        Type = SecuritySchemeType.Http,
                        In = ParameterLocation.Header,
                        BearerFormat = "JWT",
                        Description = "Enter JWT value !"
                    };

                    document.Components ??= new OpenApiComponents();
                    document.Components.SecuritySchemes["Bearer"] = securityScheme;

                    var schemeReference = new OpenApiSecuritySchemeReference("Bearer", document);

                    var securityRequirement = new OpenApiSecurityRequirement
                    {
                        [schemeReference] = new List<String>()
                    };

                    document.Security ??= new List<OpenApiSecurityRequirement>();
                    document.Security.Add(securityRequirement);

                    return Task.CompletedTask;
                }));

            return services;

        }
    }
}
