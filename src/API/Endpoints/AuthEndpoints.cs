using Application.Requests.Auth.Queries;
using Domain.Shared;
using MediatR;

namespace API.Endpoints;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this WebApplication app)
    {
        app.MapGet("/v1/auth/validate", async (ISender handler, HttpContext context) =>
        {
            if (!context.Request.Cookies.TryGetValue("access_token", out var accessToken) ||
                string.IsNullOrEmpty(accessToken))
            {
                return Results.Unauthorized();
            }

            var request = new ValidateTokenQuery(accessToken);
            var result = await handler.Send(request);

            if (!result.Success)
            {
                context.Response.Cookies.Append("access_token", "", new CookieOptions
                {
                    HttpOnly = true,
                    Secure = false,
                    SameSite = SameSiteMode.Lax,
                    Expires = DateTimeOffset.UtcNow.AddDays(-1)
                });
                
                return Results.Unauthorized();
            }
            
            var claims = context.User.Claims
                .Select(c => new ClaimDto { Type = c.Type, Value = c.Value })
                .ToList();

            return Results.Ok(new Response<List<ClaimDto>>(true, "Autenticado com sucesso!", claims, null));
        });
    }
}

public class ClaimDto
{
    public string Type { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
}