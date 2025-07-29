using Domain.Entities;
using Domain.Shared;

namespace Domain.Repositories.Users;

public interface IUserQueryRepository
{
    Task<Response<User?>> GetByIdAsync(Guid id);
    Task<Response<User?>> GetByEmailAsync(string email);
}