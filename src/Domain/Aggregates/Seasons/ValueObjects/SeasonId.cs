using Domain.DDD;

namespace Domain.Aggregates.Seasons.ValueObjects;
public sealed class SeasonId : ValueObject
{
    public Guid Value { get; init; }

    private SeasonId() => Value = Guid.NewGuid();
    private SeasonId(Guid value) => Value = value;

    public static SeasonId Create() => new();
    public static SeasonId Create(Guid value) => new(value);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
