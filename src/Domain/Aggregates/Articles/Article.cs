using Domain.Aggregates.Articles.Entities;
using Domain.Aggregates.Articles.Exceptions;
using Domain.Aggregates.Articles.ValueObjects;
using Domain.Aggregates.Tags.ValueObjects;
using Domain.Aggregates.Users.ValueObjects;
using Domain.DDD;
using Domain.Extensions;
using Domain.Interfaces;

namespace Domain.Aggregates.Articles;
public sealed class Article : AggregateRoot<ArticleId>
{
    private readonly List<Like> _likes = new();
    private readonly List<TagId> _tagIds = new();

    public string Title { get; private set; }
    public string Image { get; private set; }
    public string Slug { get; private set; }
    public string Description { get; private set; }
    public string DescriptionHtml { get; private set; }
    public DateTimeOffset PublishedAt { get; private set; }
    
    public IReadOnlyList<Like> Likes => _likes.AsReadOnly();
    public IReadOnlyList<TagId> TagIds => _tagIds.AsReadOnly();

    private Article(string title, string image, string description, string descriptionHtml, DateTimeOffset publishedAt, List<TagId> tagIds, IDateProvider dateProvider) 
        : base(ArticleId.Create())
    {
        ValidateDate(publishedAt, dateProvider);

        Title = title;
        Image = image;
        Slug = title.ToUrlFriendly();
        Description = description;
        DescriptionHtml = descriptionHtml;
        PublishedAt = publishedAt;

        _tagIds = tagIds;
    }
    

    public static Article Create(string title, string image, string description, string descriptionHtml, DateTimeOffset publishedAt, List<TagId> tagIds, IDateProvider dateProvider) =>
        new(title, image, description, descriptionHtml, publishedAt, tagIds, dateProvider);

    public void Update(string title, string image, string description, string descriptionHtml, DateTimeOffset publishedAt, List<TagId> tagIds, IDateProvider dateProvider)
    {
        ValidateDate(publishedAt, dateProvider);

        if(publishedAt != PublishedAt && PublishedAt < dateProvider.UtcNow)
        {
            throw new PublishedDateCannotBeChangedException();
        }

        Title = title;
        Description = description;
        DescriptionHtml = descriptionHtml;
        PublishedAt = publishedAt;

        _tagIds.Clear();
        _tagIds.AddRange(tagIds);
    }

    public void AddLike(UserId userId, IDateProvider dateProvider)
    {
        var like = _likes.FirstOrDefault(e => e.UserId == userId);
        if(like is null)
        {
            _likes.Add(Like.Create(userId, dateProvider));
        }
    }

    private static void ValidateDate(DateTimeOffset publishedAt, IDateProvider dateProvider)
    {
        if (publishedAt.AddSeconds(-10) < dateProvider.UtcNow)
        {
            throw new PublishDateCannotBeSetToPastException();
        }
    }

    #pragma warning disable CS8618
    private Article() : base(ArticleId.Create()) { }
    #pragma warning restore CS8618
}
