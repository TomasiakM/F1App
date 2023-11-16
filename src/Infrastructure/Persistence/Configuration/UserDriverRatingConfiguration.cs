using Domain.Aggregates.Drivers;
using Domain.Aggregates.Drivers.ValueObjects;
using Domain.Aggregates.Ratings;
using Domain.Aggregates.Ratings.ValueObjects;
using Domain.Aggregates.UserDriverRatings;
using Domain.Aggregates.UserDriverRatings.ValueObjects;
using Domain.Aggregates.Users;
using Domain.Aggregates.Users.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration;
internal sealed class UserDriverRatingConfiguration : IEntityTypeConfiguration<UserDriverRating>
{
    public void Configure(EntityTypeBuilder<UserDriverRating> builder)
    {
        builder.ToTable("UserDriverRatings");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id)
            .ValueGeneratedNever()
            .HasConversion(
                e => e.Value,
                e => UserDriverRatingId.Create(e));

        builder.Property(e => e.RatingId)
            .ValueGeneratedNever()
            .HasConversion(
                e => e.Value,
                e => RatingId.Create(e));
        builder.HasOne<Rating>()
            .WithMany()
            .HasForeignKey(e => e.RatingId);


        builder.Property(e => e.DriverId)
            .ValueGeneratedNever()
            .HasConversion(
                e => e.Value,
                e => DriverId.Create(e));

        /*
          need to be fexed
          builder.HasOne<Driver>()
            .WithMany()
            .HasForeignKey(e => e.DriverId);*/

        builder.Property(e => e.UserId)
            .ValueGeneratedNever()
            .HasConversion(
                e => e.Value,
                e => UserId.Create(e));
        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(e => e.UserId);
    }
}
