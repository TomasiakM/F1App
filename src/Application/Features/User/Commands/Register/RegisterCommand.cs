using MediatR;

namespace Application.Features.User.Commands.Register;
public record RegisterCommand(
    string Username,
    string Password,
    string Email) : IRequest<Unit>;
