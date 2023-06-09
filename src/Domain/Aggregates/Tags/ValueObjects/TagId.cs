using Domain.DDD;

namespace Domain.Aggregates.Tags.ValueObjects;
public sealed class TagId : ValueObject
{
    public Guid Value { get; init; }

    private TagId() => Value = Guid.NewGuid();
    private TagId(Guid value) => Value = value;

    public static TagId Create() => new();
    public static TagId Create(Guid value) => new(value);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
