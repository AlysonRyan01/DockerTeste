using Domain.Entities;

namespace Application.Interfaces;

public interface IJwtService
{
    Task<string> Generate(User user);
    Task<bool> ValidateToken(string token);
}