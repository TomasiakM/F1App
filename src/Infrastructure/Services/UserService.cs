using Application.Interfaces;
using Domain.Aggregates.Users;
using Domain.Aggregates.Users.Exceptions;
using Domain.Aggregates.Users.ValueObjects;
using Domain.Exceptions;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Infrastructure.Services;
internal sealed class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHashService _hashService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserService(IUnitOfWork unitOfWork, IHashService hashService, IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _hashService = hashService;
        _httpContextAccessor = httpContextAccessor;
    }

    public UserId GetUserId()
    {
        var id = _httpContextAccessor.HttpContext!.User.Claims
            .FirstOrDefault(e => e.Type == ClaimTypes.NameIdentifier)?.Value;

        if (id is null)
        {
            throw new UnauthorizedException();
        }

        return UserId.Create(new Guid(id));
    }

    public async Task Register(string username, string password, string email, CancellationToken cancellationToken = default)
    {
        var hashedPassword = _hashService.Hash(password);
        var user = User.Create(username, hashedPassword, email);

        _unitOfWork.Users.Add(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdatePassword(string password, string newPassword, CancellationToken cancellationToken)
    {
        var userId = GetUserId();
        var user = await _unitOfWork.Users.GetAsync(userId);

        if (user is null || !_hashService.Validate(password, user.Password))
        {
            throw new InvalidPasswordException();
        }

        var hashedPassord = _hashService.Hash(newPassword);
        user.UpdatePassword(hashedPassord);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
