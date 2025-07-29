using Domain.Entities;
using Domain.Shared;

namespace Domain.Repositories.Users;

public interface IUserCommandRepository
{
    Task<Response<User?>> AddAsync(User user);
    Task<Response<User?>> UpdateAsync(User user);
    Task<Response<User?>> RemoveAsync(User user);
}