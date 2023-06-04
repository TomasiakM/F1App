using Application.Dtos.Auth.Responses;
using MediatR;

namespace Application.Features.Auth.Queries.Login;
public record LoginQuery(
    string Username,
    string Password) : IRequest<AuthResponse>;
