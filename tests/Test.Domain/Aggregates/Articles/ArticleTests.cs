using Domain.Aggregates.Articles.Exceptions;
using Domain.Aggregates.Tags.ValueObjects;
using Domain.Aggregates.Users.ValueObjects;
using Domain.Interfaces;
using Moq;

namespace Test.Domain.Aggregates.Articles;
public class ArticleTests
{
    private readonly Mock<IDateProvider> _mockDateProvider;

    public ArticleTests()
    {
        _mockDateProvider = new Mock<IDateProvider>();
    }

    [Fact]
    public void Article_Create_ShouldCreateArticle()
    {
        var publishedAt = new DateTimeOffset(2000, 1, 1, 0, 0, 20, TimeSpan.Zero);

        _mockDateProvider
            .Setup(e => e.UtcNow)
            .Returns(new DateTimeOffset(2000, 1, 1, 0, 0, 30, TimeSpan.Zero));

        var article = ArticleTestsHelpers.CreateArticle(
            publishedAt,
            _mockDateProvider.Object);

        Assert.Equal(ArticleTestsHelpers.Title, article.Title);
        Assert.Equal(ArticleTestsHelpers.Image, article.Image);
        Assert.Equal(ArticleTestsHelpers.Description, article.Description);
        Assert.Equal(ArticleTestsHelpers.DescriptionHtml, article.DescriptionHtml);
        Assert.Equal(ArticleTestsHelpers.TagIds, article.TagIds);

        Assert.Equal(article.PublishedAt, publishedAt);
    }

    [Fact]
    public void Article_Create_ShouldThrowException()
    {
        var publishedAt = new DateTimeOffset(2000, 1, 1, 0, 0, 19, TimeSpan.Zero);

        _mockDateProvider
            .Setup(e => e.UtcNow)
            .Returns(new DateTimeOffset(2000, 1, 1, 0, 0, 30, TimeSpan.Zero));

        Assert.Throws<PublishDateCannotBeSetToPastException>(() => ArticleTestsHelpers.CreateArticle(publishedAt, _mockDateProvider.Object));
    }

    [Fact]
    public void Article_Update_ShouldUpdateArticle()
    {
        var publishedAt = new DateTimeOffset(2000, 1, 1, 0, 0, 40, TimeSpan.Zero);

        _mockDateProvider
            .Setup(e => e.UtcNow)
            .Returns(new DateTimeOffset(2000, 1, 1, 0, 0, 30, TimeSpan.Zero));

        var article = ArticleTestsHelpers.CreateArticle(publishedAt, _mockDateProvider.Object);

        var title = "New title";
        var image = "New image";
        var description = "New description";
        var descriptionHtml = "New descriptionHtml";
        var publichedAt = new DateTimeOffset(2000, 1, 1, 0, 1, 0, TimeSpan.Zero);
        var tagIds = new List<TagId>() { TagId.Create() };

        article.Update(title, image, description, descriptionHtml, publichedAt, tagIds, _mockDateProvider.Object);

        Assert.Equal(title, article.Title);
        Assert.Equal(image, article.Image);
        Assert.Equal(description, article.Description);
        Assert.Equal(descriptionHtml, article.DescriptionHtml);
        Assert.Equal(publichedAt, article.PublishedAt);
        Assert.Equal(tagIds, article.TagIds);
    }

    [Fact]
    public void Article_Update_ShouldThrowException()
    {
        var publishedAt = new DateTimeOffset(2000, 1, 1, 0, 0, 40, TimeSpan.Zero);

        _mockDateProvider
            .Setup(e => e.UtcNow)
            .Returns(new DateTimeOffset(2000, 1, 1, 0, 0, 30, TimeSpan.Zero));

        var article = ArticleTestsHelpers.CreateArticle(publishedAt, _mockDateProvider.Object);

        var title = "New title";
        var image = "New image";
        var description = "New description";
        var descriptionHtml = "New descriptionHtml";
        var publichedAt = new DateTimeOffset(2000, 1, 1, 0, 1, 0, TimeSpan.Zero);
        var tagIds = new List<TagId>() { TagId.Create() };

        _mockDateProvider
            .Setup(e => e.UtcNow)
            .Returns(new DateTimeOffset(2000, 1, 1, 0, 1, 30, TimeSpan.Zero));

        Assert.Throws<PublishedDateCannotBeChangedException>(
            () => article.Update(title, image, description, descriptionHtml, publichedAt, tagIds, _mockDateProvider.Object));
    }

    [Fact]
    public void Article_AddLike_ShouldAddLikeToList()
    {
        var publishedAt = new DateTimeOffset(2000, 1, 1, 0, 0, 40, TimeSpan.Zero);

        _mockDateProvider
            .Setup(e => e.UtcNow)
            .Returns(new DateTimeOffset(2000, 1, 1, 0, 0, 30, TimeSpan.Zero));

        var article = ArticleTestsHelpers.CreateArticle(publishedAt, _mockDateProvider.Object);

        var userId = UserId.Create();

        article.AddLike(userId, _mockDateProvider.Object);

        Assert.Equal(1, article.Likes.Count);
        Assert.Contains(article.Likes, e => e.UserId == userId);
    }

    [Fact]
    public void Article_AddLike_ShouldDoNothingWhenUserCurrentlyLike()
    {
        var publishedAt = new DateTimeOffset(2000, 1, 1, 0, 0, 40, TimeSpan.Zero);

        _mockDateProvider
            .Setup(e => e.UtcNow)
            .Returns(new DateTimeOffset(2000, 1, 1, 0, 0, 30, TimeSpan.Zero));

        var article = ArticleTestsHelpers.CreateArticle(publishedAt, _mockDateProvider.Object);

        var userId = UserId.Create();

        article.AddLike(userId, _mockDateProvider.Object);

        Assert.Equal(1, article.Likes.Count);
        Assert.Contains(article.Likes, e => e.UserId == userId);

        article.AddLike(userId, _mockDateProvider.Object);

        Assert.Equal(1, article.Likes.Count);
        Assert.Contains(article.Likes, e => e.UserId == userId);
    }
}
