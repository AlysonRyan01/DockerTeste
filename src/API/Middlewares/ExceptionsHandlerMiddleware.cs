using System.Text.Json;
using Application.Exceptions;
using Domain.Exceptions;
using Domain.Shared;

namespace API.Middlewares;

public class ExceptionsHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionsHandlerMiddleware> _logger;
    private readonly IHostEnvironment _env;

    public ExceptionsHandlerMiddleware(RequestDelegate next, ILogger<ExceptionsHandlerMiddleware> logger, IHostEnvironment env)
    {
        _next = next;
        _logger = logger;
        _env = env;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ocorreu uma exceção: {Message}", ex.Message);
            
            context.Response.ContentType = "application/json";
            string message;
            
            switch (ex)
            {
                case var _ when ex is BaseCommandException:
                case var _ when ex is BaseQueryException:
                case var _ when ex is BaseValueObjectException:
                    message = ex.Message;
                    break;

                default:
                    message = _env.IsDevelopment()
                        ? $"Erro inesperado: {ex.Message}\n{ex.StackTrace}"
                        : ex.Message;
                    break;
            }
            
            var response = new Response<object>(
                false, 
                message, 
                null, 
                null);

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            
            var json = JsonSerializer.Serialize(response, options);
            await context.Response.WriteAsync(json);
        }
    }
}