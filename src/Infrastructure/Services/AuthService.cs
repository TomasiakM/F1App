using Application.Dtos.Auth.Responses;
using Application.Interfaces;
using Domain.Aggregates.Roles;
using Domain.Aggregates.Users;
using Domain.Exceptions;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Domain.Aggregates.Users.ValueObjects;
using Domain.Interfaces;
using Domain.Aggregates.Users.Exceptions;

namespace Infrastructure.Services;
internal class AuthService : IAuthService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenService _tokenService;
    private readonly IHashService _hashService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IDateProvider _dateProvider;
    private readonly IUserService _userService;

    public AuthService(IUnitOfWork unitOfWork, ITokenService tokenService, IHashService hashService, IHttpContextAccessor httpContextAccessor, IDateProvider dateProvider, IUserService userService)
    {
        _unitOfWork = unitOfWork;
        _tokenService = tokenService;
        _hashService = hashService;
        _httpContextAccessor = httpContextAccessor;
        _dateProvider = dateProvider;
        _userService = userService;
    }

    public async Task<AuthResponse> Login(string username, string password, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Users
            .FindAsync(e => e.Username == username, cancellationToken);

        if (user is null || !_hashService.Validate(password, user.Password))
        {
            throw new UnauthorizedException();
        }

        if(user.GetActiveBan(_dateProvider) is Ban ban)
        {
            throw new UserIsBannedException(ban); 
        }

        await AttachRefreshCookie(user);

        return CreateAuthResponse(user);
    }

    public async Task Logout()
    {
        await _httpContextAccessor.HttpContext!
            .SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }

    public async Task<AuthResponse> RefreshToken(CancellationToken cancellationToken)
    {
        var userId = _userService.GetUserId();
        var user = await _unitOfWork.Users.GetAsync(userId, cancellationToken);

        if (user is null)
        {
            throw new UnauthorizedException();
        }

        if (user.GetActiveBan(_dateProvider) is Ban ban)
        {
            await Logout();

            throw new UserIsBannedException(ban);
        }

        return CreateAuthResponse(user);
    }

    private async Task AttachRefreshCookie(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.Value.ToString())
        };

        var claimsIdentity = new ClaimsIdentity(
            claims, CookieAuthenticationDefaults.AuthenticationScheme);

        var authProperties = new AuthenticationProperties
        {
            AllowRefresh = true,
            IsPersistent = true,
        };


        await _httpContextAccessor.HttpContext!.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperties);
    }

    private AuthResponse CreateAuthResponse(User user)
    {
        return new AuthResponse(
            user.Id.Value.ToString(),
            user.Username,
            user.Image,
            Role.GetRolesByIds(user.RoleIds.ToList()).Select(e => e.Name).ToList(),
            _tokenService.GenerateAccessToken(user));
    }
}
