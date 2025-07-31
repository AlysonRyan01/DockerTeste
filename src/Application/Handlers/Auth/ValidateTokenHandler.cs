using Application.Interfaces;
using Application.Requests.Auth.Queries;
using Domain.Shared;
using MediatR;

namespace Application.Handlers.Auth;

public class ValidateTokenHandler : IRequestHandler<ValidateTokenQuery, Response<bool>>
{
    private readonly IJwtService _jwtService;
    
    public ValidateTokenHandler(IJwtService jwtService)
    {
        _jwtService = jwtService;
    }
    
    public async Task<Response<bool>> Handle(
        ValidateTokenQuery request, 
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(request.Token))
            return new Response<bool>(false, "Token vazio", false, null);
        
        var result = await _jwtService.ValidateToken(request.Token);

        return result == false 
            ? new Response<bool>(false, "Token invalido", false, null) 
            : new Response<bool>(true, "Token validado com sucesso!", true, null);
    }
}