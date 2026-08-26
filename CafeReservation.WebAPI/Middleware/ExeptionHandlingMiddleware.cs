using FluentValidation;
using System.ComponentModel;
using System.Net;
using System.Text.Json;

namespace CafeReservation.WebAPI.Middleware
{
    public class ExeptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExeptionHandlingMiddleware> _logger;

        public ExeptionHandlingMiddleware(RequestDelegate next, ILogger<ExeptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException validationExeption)
            {
                _logger.LogWarning("An error occured while validating !");
                await HandleValidationExeptionAsync(context, validationExeption);

            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "An unknown error occured !");
                await HandleGenericExceptionAsync(context, ex);
            }
        }

        public async Task HandleValidationExeptionAsync(HttpContext context, ValidationException validationExeption)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            var errors = validationExeption.Errors
                .GroupBy(ve => ve.PropertyName)
                .ToDictionary(
                g => g.Key,
                g => g.Select(e => e.ErrorMessage.ToArray())
                );

            var response = new
            {
                Title = "Some validation errors occured !",
                Status = (int)HttpStatusCode.BadRequest,
                Errors = errors
            };

            var json = JsonSerializer.Serialize(response);

            await context.Response.WriteAsync(json);
        }

        public async Task HandleGenericExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = new
            {
                Title = "Some errors occured in server !",
                Status = (int)HttpStatusCode.InternalServerError,
                Errors = ex.Message
            };

            var json = JsonSerializer.Serialize(response);

            await context.Response.WriteAsync(json);

        }
    }
}
