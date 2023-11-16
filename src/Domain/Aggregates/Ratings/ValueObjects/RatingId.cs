using Domain.DDD;

namespace Domain.Aggregates.Ratings.ValueObjects;
public sealed class RatingId : ValueObject
{
    public Guid Value { get; private set; }

    private RatingId() => Value = Guid.NewGuid();
    private RatingId(Guid value) => Value = value;

    public static RatingId Create() => new();
    public static RatingId Create(Guid value) => new(value);

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
