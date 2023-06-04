using Domain.DDD;
using Domain.Interfaces;

namespace Domain.Aggregates.Users.ValueObjects;
public sealed class Ban : ValueObject
{
    public DateTimeOffset End { get; init; }
    public string Reason { get; init; }

    private Ban(DateTimeOffset end, string reason)
    {
        End = end;
        Reason = reason;
    }

    public static Ban Create(DateTimeOffset end, string reason)
    {
        return new(end, reason);
    }

    public bool IsActive(IDateProvider dateProvider)
    {
        return dateProvider.UtcNow.CompareTo(End) == -1;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        throw new NotImplementedException();
    }
}
