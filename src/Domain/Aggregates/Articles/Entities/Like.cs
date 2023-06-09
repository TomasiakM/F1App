using Domain.Aggregates.Users.ValueObjects;
using Domain.DDD;
using Domain.Interfaces;

namespace Domain.Aggregates.Articles.Entities;
public sealed class Like : Entity<int>
{
    public UserId UserId { get; init; }
    public DateTimeOffset CreatedAt { get; init; }

    private Like(UserId userId, IDateProvider dateProvider) 
        : base(0)
    {
        UserId = userId;
        CreatedAt = dateProvider.UtcNow;
    }

    public static Like Create(UserId userId, IDateProvider dateProvider) =>
        new(userId, dateProvider);

    #pragma warning disable CS8618
    private Like() : base(0) { }
    #pragma warning restore CS8618
}
