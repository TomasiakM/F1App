using Application.Interfaces;
using MediatR;

namespace Application.Features.User.Commands.Register;
internal class RegisterCommandHandler : IRequestHandler<RegisterCommand, Unit>
{
    public readonly IUserService _userService;

    public RegisterCommandHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<Unit> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        await _userService.Register(
            request.Username,
            request.Password,
            request.Email,
            cancellationToken);

        return Unit.Value;
    }
}
