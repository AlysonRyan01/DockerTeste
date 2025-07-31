using Application.Requests.Auth.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Endpoints;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this WebApplication app)
    {
        app.MapGet("/v1/auth/validate", async (ISender handler,[FromHeader(Name = "Authorization")] string authHeader) =>
        {
            var token = authHeader?.Replace("Bearer ", "", StringComparison.OrdinalIgnoreCase);
            
            if (string.IsNullOrEmpty(token))
                return Results.BadRequest();
            
            var request = new ValidateTokenQuery(token);
            var result = await handler.Send(request);
            
            if (!result.Success)
                return Results.BadRequest(result);
            
            return Results.Ok(result);
        });
    }
}