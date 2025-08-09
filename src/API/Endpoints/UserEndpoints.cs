using Application.Requests.Users.Commands;
using MediatR;

namespace API.Endpoints;

public static class UserEndpoints
{
    public static void MapUserEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/v1/login", async (
            HttpContext context,
            LoginUserCommand command,
            ISender handler) =>
        {
            var result = await handler.Send(command);
            if (!result.Success || result.Data == null)
                return Results.BadRequest(result);
            
            context.Response.Cookies.Append("access_token", result.Data, new CookieOptions
            {
                HttpOnly = true,
                Secure = false,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddMinutes(30)
            });
            
            return Results.Ok(result);
        });

        app.MapPost("/v1/register", async (
            RegisterUserCommand command, 
            ISender handler) =>
        {
            var result = await handler.Send(command);
            if (!result.Success)
                return Results.BadRequest(result);
            
            return Results.Ok(result);
        });
    }
}