using Application.Interfaces;
using MediatR;

namespace Application.Features.Auth.Queries.Logout;
internal class LogoutQueryHandler : IRequestHandler<LogoutQuery>
{
    private readonly IAuthService _authService;

    public LogoutQueryHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task Handle(LogoutQuery request, CancellationToken cancellationToken)
    {
        await _authService.Logout();
    }
}
