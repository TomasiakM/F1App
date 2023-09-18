using Domain.DDD;

namespace Domain.Aggregates.Tracks.ValueObjects;
public sealed class TrackId : ValueObject
{
    public Guid Value { get; init; }

    private TrackId() => Value = Guid.NewGuid();
    private TrackId(Guid value) => Value = value;

    public static TrackId Create() => new();
    public static TrackId Create(Guid value) => new(value);

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
