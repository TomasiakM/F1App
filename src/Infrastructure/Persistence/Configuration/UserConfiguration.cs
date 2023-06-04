using Domain.Aggregates.Users;
using Domain.Aggregates.Users.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration;
internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id)
            .ValueGeneratedNever()
            .HasConversion(
                e => e.Value,
                e => UserId.Create(e));

        builder.HasIndex(e => e.Username)
            .IsUnique();

        builder.HasIndex(e => e.Email)
            .IsUnique();

        builder.Metadata.FindNavigation(nameof(User.RoleIds))!.
            SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.OwnsMany(u => u.RoleIds, rb =>
        {
            rb.ToTable("User_Role");

            rb.Property(e => e.Value)
                .HasColumnName("RoleId");

            rb.WithOwner()
                .HasForeignKey("UserId");
        });

        builder.OwnsMany(u => u.Bans, bb =>
        {
            bb.ToTable("Bans");

            bb.HasKey("Id");

            bb.WithOwner()
                .HasForeignKey("UserId");
        });
    }
}
