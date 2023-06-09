using Domain.DDD;

namespace Domain.Aggregates.Articles.ValueObjects;
public sealed class ArticleId : ValueObject
{
    public Guid Value { get; init; }

    private ArticleId(Guid value) => Value = value;

    public static ArticleId Create() => new(Guid.NewGuid());
    public static ArticleId Create(Guid value) => new(value);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
