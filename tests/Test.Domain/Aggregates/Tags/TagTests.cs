using Domain.Aggregates.Tags;
using Domain.Extensions;

namespace Test.Domain.Aggregates.Tags;
public class TagTests
{
    [Fact]
    public void Tag_Create_ShouldCreateTag()
    {
        var name = "Tag name";

        var tag = Tag.Create(name);

        Assert.Equal(name, tag.Name);
        Assert.Equal(name.ToUrlFriendly(), tag.Slug);
    }
}
