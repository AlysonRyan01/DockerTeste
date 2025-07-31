using Domain.Shared;
using MediatR;

namespace Application.Requests.Auth.Queries;

public record ValidateTokenQuery(string Token) : IRequest<Response<bool>>;