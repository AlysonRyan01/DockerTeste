using API.Endpoints;
using API.Middlewares;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace API.DependencyInjection;

public static class AppExtensions
{
    public static void AddAuthorization(this WebApplication app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
    }
    
    public static void AddEndpoints(this WebApplication app)
    {
        app.MapUserEndpoint();
    }
    
    public static void AddSwagger(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
    }
    
    public static void AddMigrations(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            db.Database.Migrate();
        }
    }
    
    public static void AddExceptionMiddleware(this WebApplication app)
    {
        app.UseMiddleware<ExceptionsHandlerMiddleware>();
    }
}