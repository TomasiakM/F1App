using Application.Dtos.Auth.Responses;

namespace Application.Interfaces;
public interface IAuthService
{
    Task<AuthResponse> Login(string username, string password, CancellationToken cancellationToken);
    Task<AuthResponse> RefreshToken(CancellationToken cancellationToken);
    Task Logout();
}
