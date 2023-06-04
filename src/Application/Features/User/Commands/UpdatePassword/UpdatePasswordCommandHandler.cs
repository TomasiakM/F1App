using Application.Interfaces;
using MediatR;

namespace Application.Features.User.Commands.UpdatePassword;
internal class UpdatePasswordCommandHandler : IRequestHandler<UpdatePasswordCommand, Unit>
{
    private readonly IUserService _userService;

    public UpdatePasswordCommandHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<Unit> Handle(UpdatePasswordCommand request, CancellationToken cancellationToken)
    {
        await _userService.UpdatePassword(request.Password, request.NewPassword, cancellationToken);

        return Unit.Value;
    }
}
