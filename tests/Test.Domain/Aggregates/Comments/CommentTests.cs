using Domain.Aggregates.Articles.ValueObjects;
using Domain.Aggregates.Comments;
using Domain.Aggregates.Users.ValueObjects;
using Domain.Interfaces;
using Moq;

namespace Test.Domain.Aggregates.Comments;
public class CommentTests
{
    private readonly Mock<IDateProvider> _dateProvider;

    public CommentTests()
    {
        _dateProvider = new Mock<IDateProvider>();
    }

    [Fact]
    public void Comment_Create_ShouldCreateComment()
    {
        var articleId = ArticleId.Create();
        var userId = UserId.Create();
        var text = "Text";

        var now = new DateTimeOffset(2000, 1, 1, 0, 0, 30, TimeSpan.Zero);
        _dateProvider
            .Setup(e => e.UtcNow)
            .Returns(now);

        var comment = Comment.Create(articleId, userId, text, _dateProvider.Object);

        Assert.Equal(articleId, comment.ArticleId);
        Assert.Equal(userId, comment.CreatedBy);
        Assert.Equal(text, comment.Text);
        Assert.Equal(now, comment.CreatedAt);

        Assert.Empty(comment.Replies);
    }

    [Fact]
    public void Comment_AddReply_ShouldAddReply()
    {
        var articleId = ArticleId.Create();
        var userId = UserId.Create();
        var text = "Text";

        var now = new DateTimeOffset(2000, 1, 1, 0, 0, 30, TimeSpan.Zero);
        _dateProvider
            .Setup(e => e.UtcNow)
            .Returns(now);

        var comment = Comment.Create(articleId, userId, text, _dateProvider.Object);

        var userId2 = UserId.Create();
        var text2 = "Text";

        var now2 = new DateTimeOffset(2000, 1, 1, 0, 0, 40, TimeSpan.Zero);
        _dateProvider
            .Setup(e => e.UtcNow)
            .Returns(now2);

        comment.AddReply(userId2, text2, _dateProvider.Object);

        Assert.Equal(1, comment.Replies.Count);
        Assert.Equal(comment.Replies.First().CreatedBy, userId2);
        Assert.Equal(comment.Replies.First().Text, text2);
        Assert.Equal(comment.Replies.First().CreatedAt, now2);
    }
}
