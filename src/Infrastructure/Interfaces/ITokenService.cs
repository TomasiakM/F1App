using Domain.Aggregates.Users;

namespace Infrastructure.Interfaces;
internal interface ITokenService
{
    string GenerateAccessToken(User user);
}
