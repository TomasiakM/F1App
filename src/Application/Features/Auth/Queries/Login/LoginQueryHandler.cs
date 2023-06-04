using Application.Dtos.Auth.Responses;
using Application.Interfaces;
using MediatR;

namespace Application.Features.Auth.Queries.Login;
internal class LoginQueryHandler : IRequestHandler<LoginQuery, AuthResponse>
{
    private readonly IAuthService _authService;

    public LoginQueryHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<AuthResponse> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        return await _authService.Login(request.Username, request.Password, cancellationToken);
    }
}
