using Domain.Entities;
using Domain.Repositories.Users;
using Domain.Shared;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Users;

public class UserQueryRepository : IUserQueryRepository
{
    private ApplicationDbContext _context;

    public UserQueryRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<Response<User?>> GetByIdAsync(Guid id)
    {
        var user = await _context
            .Users
            .AsNoTracking()
            .Include(x => x.Roles)
            .FirstOrDefaultAsync(x => x.Id == id);
        
        if (user == null)
            return new Response<User?>(
                false, 
                "User not found", 
                null,
                null);
        
        return new Response<User?>(true, "User found", user, null);
    }

    public async Task<Response<User?>> GetByEmailAsync(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return new Response<User?>(
                false,
                "Email inválido",
                null,
                null);

        var normalizedEmail = email.Trim().ToLower();

        var user = await _context
            .Users
            .AsNoTracking()
            .Include(x => x.Roles)
            .FirstOrDefaultAsync(x => x.Email.Address.ToLower() == normalizedEmail);

        if (user == null)
            return new Response<User?>(
                false,
                "User not found",
                null,
                null);

        return new Response<User?>(
            true,
            "User found",
            user,
            null);
    }
}