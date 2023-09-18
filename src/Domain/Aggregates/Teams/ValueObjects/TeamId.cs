using Domain.DDD;

namespace Domain.Aggregates.Teams.ValueObjects;
public sealed class TeamId : ValueObject
{
    public Guid Value { get; init; }

    private TeamId() => Value = Guid.NewGuid();
    private TeamId(Guid value) => Value = value;

    public static TeamId Create() => new();
    public static TeamId Create(Guid value) => new(value);

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
