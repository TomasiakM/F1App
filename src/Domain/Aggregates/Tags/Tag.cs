using Domain.Aggregates.Tags.ValueObjects;
using Domain.DDD;
using Domain.Extensions;

namespace Domain.Aggregates.Tags;
public sealed class Tag : AggregateRoot<TagId>
{
    public string Name { get; private set; }
    public string Slug { get; private set; }

    private Tag(string name)
        : base(TagId.Create())
    {
        Name = name;
        Slug = name.ToUrlFriendly();
    }

    public static Tag Create(string name) =>
        new(name);

    #pragma warning disable CS8618
    private Tag() : base(TagId.Create()) { }
    #pragma warning restore CS8618
}
