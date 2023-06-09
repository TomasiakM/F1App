using Domain.Aggregates.Comments.ValueObjects;
using Domain.Aggregates.Users.ValueObjects;
using Domain.DDD;
using Domain.Interfaces;

namespace Domain.Aggregates.Comments.Entities;
public sealed class Reply : Entity<ReplyId>
{
    public UserId CreatedBy { get; init; }
    public string Text { get; private set; }
    public DateTimeOffset CreatedAt { get; init; }

    private Reply(UserId createdBy, string text, IDateProvider dateProvider)
        : base(ReplyId.Create())
    {
        CreatedBy = createdBy;
        Text = text;
        CreatedAt = dateProvider.UtcNow;
    }

    public static Reply Create(UserId createdBy, string text, IDateProvider dateProvider) =>
        new Reply(createdBy, text, dateProvider);


    #pragma warning disable CS8618
    private Reply() : base(ReplyId.Create()) { }
    #pragma warning restore CS8618
}

