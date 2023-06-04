using Domain.DDD;

namespace Domain.Aggregates.Users.ValueObjects;
public sealed class UserId : ValueObject
{
    public Guid Value { get; init; }

    private UserId()
    {
        Value = Guid.NewGuid();
    }

    private UserId(Guid id)
    {
        Value = id;
    }

    public static UserId Create()
    {
        return new UserId(Guid.NewGuid());
    }

    public static UserId Create(Guid id)
    {
        return new UserId(id);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
