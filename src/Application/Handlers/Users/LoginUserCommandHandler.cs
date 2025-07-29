using Application.Interfaces;
using Application.Requests.Users.Commands;
using Domain.Repositories.Users;
using Domain.Shared;
using MediatR;

namespace Application.Handlers.Users;

public sealed class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Response<string>>
{
    private readonly IUserQueryRepository _userQueryRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtService _jwtService;

    public LoginUserCommandHandler(
        IUserQueryRepository userQueryRepository,
        IPasswordHasher passwordHasher,
        IJwtService jwtService)
    {
        _userQueryRepository = userQueryRepository;
        _passwordHasher = passwordHasher;
        _jwtService = jwtService;
    }
    
    public async Task<Response<string>> Handle(
        LoginUserCommand request, 
        CancellationToken cancellationToken = default)
    {
        var query = await _userQueryRepository.GetByEmailAsync(request.Email);
        if (!query.Success || query.Data == null)
            return new Response<string>(false, "Email or password invalid", null, null);

        if (!_passwordHasher.VerifyHashedPassword(query.Data.PasswordHash.Value, request.Password))
            return new Response<string>(false, "Email or password invalid", null, null);

        var user = query.Data;
        var token = await _jwtService.Generate(user);
        
        return new Response<string>(true, "User logged in", token, null);
    }
}