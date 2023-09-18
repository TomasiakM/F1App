using Domain.DDD;

namespace Domain.Aggregates.Drivers.ValueObjects;

public sealed class DriverId : ValueObject
{
    public Guid Value { get; init; }

    private DriverId() => Value = Guid.NewGuid();
    private DriverId(Guid value) => Value = value;

    public static DriverId Create() => new();
    public static DriverId Create(Guid value) => new(value);

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}