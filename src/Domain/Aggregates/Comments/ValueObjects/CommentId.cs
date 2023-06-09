using Domain.DDD;

namespace Domain.Aggregates.Comments.ValueObjects;
public sealed class CommentId : ValueObject
{
    public Guid Value { get; init; }

    private CommentId() => Value = Guid.NewGuid();
    private CommentId(Guid value) => Value = value;

    public static CommentId Create() => new();
    public static CommentId Create(Guid value) => new(value);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}