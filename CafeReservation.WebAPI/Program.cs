using CafeReservation.Application;
using CafeReservation.Application.Common.Behaviors;
using CafeReservation.Application.Features.MenuItems.Commands.CreateMenuItem;
using CafeReservation.Infrastructure;
using CafeReservation.WebAPI;
using CafeReservation.WebAPI.Middleware;
using DbUp;
using FluentValidation;
using Scalar.AspNetCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddOpenApiDocumentation();

builder.Services.AddMediatR(cfg =>
{

    cfg.RegisterServicesFromAssembly(typeof(IAssemblyMarker).Assembly);
    cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
});

builder.Services.AddValidatorsFromAssemblyContaining(typeof(IAssemblyMarker));

EnsureDatabase.For.SqlDatabase(connectionString);

var upgrader = DeployChanges.To.SqlDatabase(connectionString)
    .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
    .LogToConsole()
    .Build();

var result = upgrader.PerformUpgrade();

if (!result.Successful)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine($"An error occured while creating Database : {result.Error}");
    Console.ResetColor();
}

else
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("Database updated successfully !");
    Console.ResetColor();
}

var app = builder.Build();

app.UseMiddleware(typeof(ExeptionHandlingMiddleware));

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.MapScalarApiReference(configureOptions =>

    configureOptions.WithTheme(ScalarTheme.BluePlanet)
    
    );

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
