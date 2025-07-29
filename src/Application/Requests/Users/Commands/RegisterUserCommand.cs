using Domain.Entities;
using Domain.Shared;
using MediatR;

namespace Application.Requests.Users.Commands;

public sealed record RegisterUserCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password) : IRequest<Response<User>>;