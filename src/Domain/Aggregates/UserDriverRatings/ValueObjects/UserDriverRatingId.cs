using Domain.DDD;

namespace Domain.Aggregates.UserDriverRatings.ValueObjects;
public sealed class UserDriverRatingId : ValueObject
{
    public Guid Value { get; private set; }

    private UserDriverRatingId() => Value = Guid.NewGuid();
    private UserDriverRatingId(Guid value) => Value = value;

    public static UserDriverRatingId Create() => new();
    public static UserDriverRatingId Create(Guid value) => new(value);

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
