using Domain.Entities;
using Domain.Repositories.Users;
using Domain.Shared;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Users;

public class UserCommandRepository : IUserCommandRepository
{
    private ApplicationDbContext _context;

    public UserCommandRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Response<User?>> AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        
        return new Response<User?>(
            true, 
            "User added", 
            user, 
            null);
    }

    public async Task<Response<User?>> UpdateAsync(User user)
    {
        var exist = await _context.Users
            .Include(x => x.Roles)
            .FirstOrDefaultAsync(x => x.Id == user.Id);

        if (exist == null)
            return new Response<User?>(false, "User not found", null, null);

        exist.ChangeFirstName(user.FirstName.ToString());
        exist.ChangeLastName(user.LastName.ToString());
        exist.ChangeEmail(user.Email.Address);
        exist.SetPassword(user.PasswordHash.ToString());
        
        var rolesToRemove = exist.Roles
            .Where(rExist => !user.Roles.Any(rNew => rNew.Name == rExist.Name))
            .ToList();

        foreach (var roleToRemove in rolesToRemove)
        {
            exist.RemoveRole(roleToRemove);
        }
        
        foreach (var newRole in user.Roles)
        {
            exist.AddOrUpdateRole(newRole);
        }

        await _context.SaveChangesAsync();

        return new Response<User?>(true, "User updated", exist, null);
    }

    public async Task<Response<User?>> RemoveAsync(User user)
    {
        var exist = await _context
            .Users
            .FindAsync(user.Id);
        
        if (exist == null)
            return new Response<User?>(
                false,
                "User not found",
                null,
                null);
        
        _context.Users.Remove(exist);
        await _context.SaveChangesAsync();
        
        return new Response<User?>(true, "User deleted", null, null);
    }
}