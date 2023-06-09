using Domain.Aggregates.Articles.ValueObjects;
using Domain.Aggregates.Comments;
using Domain.Aggregates.Comments.Entities;
using Domain.Aggregates.Comments.ValueObjects;
using Domain.Aggregates.Users.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration;
internal sealed class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.ToTable("Comments");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id)
            .ValueGeneratedNever()
            .HasConversion(
                e => e.Value,
                e => CommentId.Create(e));

        builder.Property(e => e.ArticleId)
            .HasConversion(
                e => e.Value,
                e => ArticleId.Create(e));

        builder.Property(e => e.CreatedBy)
            .HasConversion(
                e => e.Value,
                e => UserId.Create(e));

        builder.OwnsMany(e => e.Replies, ConfigureReplies);

        builder.Navigation(e => e.Replies).Metadata.SetField("_replies");
        builder.Navigation(e => e.Replies).UsePropertyAccessMode(PropertyAccessMode.Field);
    }

    private void ConfigureReplies(OwnedNavigationBuilder<Comment, Reply> builder)
    {
        builder.ToTable("Replies");

        builder.WithOwner()
            .HasForeignKey("CommentId");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id)
            .ValueGeneratedNever()
            .HasConversion(
                e => e.Value,
                e => ReplyId.Create(e));

        builder.Property(e => e.CreatedBy)
            .HasConversion(
                e => e.Value,
                e => UserId.Create(e));
    }
}
