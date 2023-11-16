using Domain.Aggregates.Drivers;
using Domain.Aggregates.Drivers.ValueObjects;
using Domain.Aggregates.RaceWeeks;
using Domain.Aggregates.RaceWeeks.ValueObjects;
using Domain.Aggregates.Ratings;
using Domain.Aggregates.Ratings.Entities;
using Domain.Aggregates.Ratings.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration;
internal sealed class RatingConfiguration : IEntityTypeConfiguration<Rating>
{
    public void Configure(EntityTypeBuilder<Rating> builder)
    {
        builder.ToTable("Ratings");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id)
            .ValueGeneratedNever()
            .HasConversion(
                e => e.Value,
                e => RatingId.Create(e));

        builder.Property(e => e.RaceWeekId)
            .ValueGeneratedNever()
            .HasConversion(
                e => e.Value,
                e => RaceWeekId.Create(e));
        builder.HasOne<RaceWeek>()
            .WithOne()
            .HasForeignKey<Rating>(e => e.RaceWeekId);

        builder.OwnsMany(e => e.DriverIds, ConfigureDriverIds);
        builder.Metadata.FindNavigation(nameof(Rating.DriverIds))!.
            SetPropertyAccessMode(PropertyAccessMode.Field);


        builder.OwnsMany(e => e.DriverRatings, ConfigureDriverRating);
        builder.Metadata.FindNavigation(nameof(Rating.DriverRatings))!.
            SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private void ConfigureDriverIds(OwnedNavigationBuilder<Rating, DriverId> builder)
    {
        builder.ToTable("RatingDrivers");

        builder.WithOwner()
            .HasForeignKey("RatingId");

        builder.Property(e => e.Value)
            .HasColumnName("DriverId");
    }

    private void ConfigureDriverRating(OwnedNavigationBuilder<Rating, DriverRating> builder)
    {
        builder.ToTable("DriverRatings");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        builder.WithOwner()
            .HasForeignKey("RatingId");

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
    }
}
