using Domain.DDD;

namespace Domain.Aggregates.DriverStatistics.ValueObjects;
public sealed class DriverStatisticId : ValueObject
{
    public Guid Value { get; init; }
    
    private DriverStatisticId() => Value = Guid.NewGuid();
    private DriverStatisticId(Guid value) => Value = value;

    public static DriverStatisticId Create() => new();
    public static DriverStatisticId Create(Guid value) => new(value);
    
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
