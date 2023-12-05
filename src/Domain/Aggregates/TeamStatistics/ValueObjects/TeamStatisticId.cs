using Domain.DDD;

namespace Domain.Aggregates.TeamStatistics.ValueObjects;
public sealed class TeamStatisticId : ValueObject
{
    public Guid Value { get; init; }

    private TeamStatisticId() => Value = Guid.NewGuid();
    private TeamStatisticId(Guid value) => Value = value;

    public static TeamStatisticId Create() => new();
    public static TeamStatisticId Create(Guid value) => new(value);

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
