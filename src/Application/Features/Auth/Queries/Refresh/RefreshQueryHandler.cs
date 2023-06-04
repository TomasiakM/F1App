using Application.Dtos.Auth.Responses;
using Application.Interfaces;
using MediatR;

namespace Application.Features.Auth.Queries.Refresh;
internal class RefreshQueryHandler : IRequestHandler<RefreshQuery, AuthResponse>
{
    private readonly IAuthService _authService;

    public RefreshQueryHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<AuthResponse> Handle(RefreshQuery request, CancellationToken cancellationToken)
    {
        return await _authService.RefreshToken(cancellationToken);
    }
}
