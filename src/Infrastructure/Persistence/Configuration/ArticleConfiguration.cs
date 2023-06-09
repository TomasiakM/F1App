using Domain.Aggregates.Articles;
using Domain.Aggregates.Articles.Entities;
using Domain.Aggregates.Articles.ValueObjects;
using Domain.Aggregates.Tags.ValueObjects;
using Domain.Aggregates.Users.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration;
internal sealed class ArticleConfiguration : IEntityTypeConfiguration<Article>
{
    public void Configure(EntityTypeBuilder<Article> builder)
    {
        builder.ToTable("Articles");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id)
            .ValueGeneratedNever()
            .HasConversion(
                e => e.Value,
                e => ArticleId.Create(e));

        builder.HasIndex(e => e.Slug)
            .IsUnique();

        builder.OwnsMany(e => e.Likes, ConfigureLikes);
        builder.Metadata.FindNavigation(nameof(Article.Likes))!.
            SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.OwnsMany(e => e.TagIds, ConfigureTagIds);
        builder.Metadata.FindNavigation(nameof(Article.TagIds))!.
            SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private void ConfigureTagIds(OwnedNavigationBuilder<Article, TagId> builder)
    {
        builder.ToTable("Article_Tag");

        builder.WithOwner()
            .HasForeignKey("ArticleId");

        builder.Property(e => e.Value)
            .HasColumnName("TagId");
    }

    private void ConfigureLikes(OwnedNavigationBuilder<Article, Like> builder)
    {
        builder.ToTable("Likes");

        builder.WithOwner()
            .HasForeignKey("ArticleId");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        builder.Property(e => e.UserId)
            .HasConversion(
                e => e.Value,
                e => UserId.Create(e));
    }
}
