using CafeReservation.Application;
using CafeReservation.Application.Common.Behaviors;
using CafeReservation.Application.Features.MenuItems.Commands.CreateMenuItem;
using CafeReservation.Infrastructure;
using DbUp;
using FluentValidation;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "CafeReservation API v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
