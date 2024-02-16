using Domain.Aggregates.Comments.Entities;
using Domain.Aggregates.Users.ValueObjects;
using Domain.Interfaces;
using Moq;

namespace Test.Domain.Aggregates.Comments.Entities;
public class ReplyTests
{
    private readonly Mock<IDateProvider> _mockDateProvider;

    public ReplyTests()
    {
        _mockDateProvider = new Mock<IDateProvider>();
    }

    [Fact]
    public void Reply_Create_ShouldCreateReply()
    {
        var createdBy = UserId.Create();
        var text = "text";

        var date = new DateTimeOffset(2022, 1, 1, 1, 1, 1, TimeSpan.Zero);
        _mockDateProvider
            .Setup(e => e.UtcNow)
            .Returns(date);

        var reply = Reply.Create(createdBy, text, _mockDateProvider.Object);

        Assert.Equal(createdBy, reply.CreatedBy);
        Assert.Equal(text, reply.Text);
        Assert.Equal(date, reply.CreatedAt);
    }
}
