using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services;

public class JwtService : IJwtService
{
    private readonly IConfiguration _configuration;

    public JwtService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<string> Generate(User user)
    {
        var handler = new JwtSecurityTokenHandler();

        var secret = _configuration["Jwt:SecretKey"];

        if (string.IsNullOrWhiteSpace(secret))
            throw new Exception("O SecretKey da API não pode estar vazia");

        var key = Encoding.ASCII.GetBytes(secret);

        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(key),
            SecurityAlgorithms.HmacSha256Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = await GenerateClaims(user),
            SigningCredentials = credentials,
            Expires = DateTime.UtcNow.AddHours(14),

        };

        var token = handler.CreateToken(tokenDescriptor);

        return handler.WriteToken(token);

    }

    private Task<ClaimsIdentity> GenerateClaims(User user)
    {
        var ci = new ClaimsIdentity();
        
        ci.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
        ci.AddClaim(new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"));
        ci.AddClaim(new Claim(ClaimTypes.Email, user.Email.Address));

        foreach (var role in user.Roles.Where(x => x.IsActive()))
        {
            ci.AddClaim(new Claim(ClaimTypes.Role, role.Name));
        }
        
        return Task.FromResult(ci);
    }
}