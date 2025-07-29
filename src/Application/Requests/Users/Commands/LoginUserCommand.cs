using Domain.Shared;
using MediatR;

namespace Application.Requests.Users.Commands;

public sealed record LoginUserCommand(
    string Email, 
    string Password) 
    : IRequest<Response<string>>;