using Domain.DDD;

namespace Domain.Aggregates.GeneralClassifications.ValueObjects;
public sealed class GeneralClassificationId : ValueObject
{
    public Guid Value { get; private set; }

    private GeneralClassificationId() => Value = Guid.NewGuid();
    private GeneralClassificationId(Guid value) => Value = value;

    public static GeneralClassificationId Create() => new();
    public static GeneralClassificationId Create(Guid value) => new(value);

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
