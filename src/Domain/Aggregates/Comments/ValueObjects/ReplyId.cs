using Domain.DDD;

namespace Domain.Aggregates.Comments.ValueObjects;
public sealed class ReplyId : ValueObject
{
    public Guid Value { get; init; }

    private ReplyId() => Value = Guid.NewGuid();
    private ReplyId(Guid value) => Value = value;

    public static ReplyId Create() => new();
    public static ReplyId Create(Guid value) => new(value);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
