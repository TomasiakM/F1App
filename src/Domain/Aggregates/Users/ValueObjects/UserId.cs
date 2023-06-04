using Domain.DDD;

namespace Domain.Aggregates.Users.ValueObjects;
public sealed class UserId : ValueObject
{
    public Guid Value { get; init; }

    private UserId() => Value = Guid.NewGuid();
    private UserId(Guid value) => Value = value;

    public static UserId Create() => new(Guid.NewGuid());
    public static UserId Create(Guid value) => new(value);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
