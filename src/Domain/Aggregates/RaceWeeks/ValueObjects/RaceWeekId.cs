using Domain.DDD;

namespace Domain.Aggregates.RaceWeeks.ValueObjects;
public sealed class RaceWeekId : ValueObject
{
    public Guid Value { get; init; }

    private RaceWeekId() => Value = Guid.NewGuid();
    private RaceWeekId(Guid value) => Value = value;

    public static RaceWeekId Create() => new();
    public static RaceWeekId Create(Guid value) => new(value);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
