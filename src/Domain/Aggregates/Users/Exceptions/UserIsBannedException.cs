using Domain.Aggregates.Users.ValueObjects;
using Domain.Exceptions;

namespace Domain.Aggregates.Users.Exceptions;
public sealed class UserIsBannedException : ForbiddenException
{
    public Ban Ban { get; init; }

    public UserIsBannedException(Ban ban)
        : base($"Ban aktywny do {ban.End.ToUniversalTime()}")
    {
        Ban = ban;
    }
}
