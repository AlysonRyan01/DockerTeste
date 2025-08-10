using Domain.Shared;

namespace API.Endpoints;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this WebApplication app)
    {
        app.MapGet("/v1/auth/validate", (HttpContext context) =>
        {
            var claims = context.User.Claims
                .Select(c => new ClaimDto { Type = c.Type, Value = c.Value })
                .ToList();
            
            return Results.Ok(
                new Response<List<ClaimDto>>(true, "Autenticado com sucesso!", claims, null));
        }).RequireAuthorization();
    }
}

public class ClaimDto
{
    public string Type { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
}