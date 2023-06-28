using Domain.Aggregates.Articles;
using Domain.Aggregates.Tags.ValueObjects;
using Domain.Interfaces;

namespace Test.Domain.Aggregates.Articles;
internal static class ArticleTestsHelpers
{
    public const string Title = "Title";
    public const string Image = "Image";
    public const string Description = "Description";
    public const string DescriptionHtml = "DescriptionHtml";
    public static readonly List<TagId> TagIds = new() { TagId.Create(), TagId.Create() };

    public static Article CreateArticle(DateTimeOffset createdAt, IDateProvider dateProvider)
    {
        return Article.Create(
            Title,
            Image,
            Description,
            DescriptionHtml,
            createdAt,
            TagIds,
            dateProvider);
    }
}
