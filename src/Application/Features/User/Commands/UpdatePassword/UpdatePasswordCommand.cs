using MediatR;

namespace Application.Features.User.Commands.UpdatePassword;
public record UpdatePasswordCommand(
    string Password,
    string NewPassword) : IRequest<Unit>;
