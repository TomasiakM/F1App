using Domain.Aggregates.Articles.ValueObjects;
using Domain.Aggregates.Comments.Entities;
using Domain.Aggregates.Comments.ValueObjects;
using Domain.Aggregates.Users.ValueObjects;
using Domain.DDD;
using Domain.Interfaces;

namespace Domain.Aggregates.Comments;
public sealed class Comment : AggregateRoot<CommentId>
{
    private readonly List<Reply> _replies = new();

    public ArticleId ArticleId { get; init; }
    public UserId CreatedBy { get; init; }
    public string Text { get; private set; }
    public DateTimeOffset CreatedAt { get; init; }

    public IReadOnlyList<Reply> Replies => _replies.AsReadOnly();

    private Comment(ArticleId articleId, UserId createdBy, string text, IDateProvider dateProvider)
        : base(CommentId.Create())
    {
        ArticleId = articleId;
        CreatedBy = createdBy;
        Text = text;
        CreatedAt = dateProvider.UtcNow;
    }

    public static Comment Create(ArticleId articleId, UserId createdBy, string text, IDateProvider dateProvider) =>
        new(articleId, createdBy, text, dateProvider);

    public void AddReply(UserId userId, string text, IDateProvider dateProvider)
    {
        var reply = Reply.Create(userId, text, dateProvider);
        _replies.Add(reply);
    }

    #pragma warning disable CS8618
    private Comment() : base(CommentId.Create()) { }
    #pragma warning restore CS8618
}
