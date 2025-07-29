using Application.Interfaces;
using Application.Requests.Users.Commands;
using Domain.Entities;
using Domain.Repositories.Users;
using Domain.Shared;
using MediatR;

namespace Application.Handlers.Users;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Response<User>>
{
    private readonly IUserCommandRepository _userCommandRepository;
    private readonly IUserQueryRepository _userQueryRepository;
    private readonly IPasswordHasher _passwordHasher;

    public RegisterUserCommandHandler(
        IUserCommandRepository userCommandRepository, 
        IUserQueryRepository userQueryRepository,
        IPasswordHasher passwordHasher)
    {
        _userCommandRepository = userCommandRepository;
        _userQueryRepository = userQueryRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<Response<User>> Handle(
        RegisterUserCommand request, 
        CancellationToken cancellationToken = default)
    {
        if (request.Password.Length < 6)
            return new Response<User>(false, "Password must be at least 6 characters long", null, null);
        
        var verifyUser = await _userQueryRepository.GetByEmailAsync(request.Email);

        if (verifyUser.Data != null)
            return new Response<User>(
                false, 
                "User already exists", 
                null, 
                null);
        
        var passwordHash = _passwordHasher.HashPassword(request.Password);

        var user = User.Create(
            request.FirstName, 
            request.LastName, 
            request.Email, 
            passwordHash);
        
        var createResult = await _userCommandRepository.AddAsync(user);
        if (!createResult.Success)
            return new Response<User>(false, createResult.Message, null, createResult.Errors);
        
        return new Response<User>(
            true, 
            "User created successfully", 
            user, 
            null);
    }
}