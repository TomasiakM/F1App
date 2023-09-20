using Domain.Aggregates.Articles;
using Domain.Aggregates.Drivers;
using Domain.Aggregates.Drivers.ValueObjects;
using Domain.Aggregates.RaceWeeks;
using Domain.Aggregates.RaceWeeks.Entities;
using Domain.Aggregates.RaceWeeks.ValueObjects;
using Domain.Aggregates.RaceWeeks.ValueObjects.SessionResults;
using Domain.Aggregates.Seasons;
using Domain.Aggregates.Seasons.ValueObjects;
using Domain.Aggregates.Tracks;
using Domain.Aggregates.Tracks.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration;
internal sealed class RaceWeekConfiguration : IEntityTypeConfiguration<RaceWeek>
{
    public void Configure(EntityTypeBuilder<RaceWeek> builder)
    {
        builder.ToTable("RaceWeeks");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id)
            .HasConversion(
                e => e.Value,
                e => RaceWeekId.Create(e));

        builder.HasIndex(e => new{ e.TrackId, e.SeasonId })
            .IsUnique();

        builder.HasIndex(e => new { e.Slug, e.SeasonId })
            .IsUnique();

        builder.Property(e => e.TrackId)
            .HasConversion(
                e => e.Value,
                e => TrackId.Create(e));
        builder.HasOne<Track>()
            .WithMany()
            .HasForeignKey(e => e.TrackId);

        builder.Property(e => e.SeasonId)
            .HasConversion(
                e => e.Value,
                e => SeasonId.Create(e));
        builder.HasOne<Season>()
            .WithMany()
            .HasForeignKey(e => e.SeasonId);

        builder.OwnsOne(e => e.FP1, ConfigureFP1);
        builder.OwnsOne(e => e.FP2, ConfigureFP2);
        builder.OwnsOne(e => e.FP3, ConfigureFP3);

        builder.OwnsOne(e => e.SprintQualifications, ConfigureSprintQualifications);
        builder.OwnsOne(e => e.Sprint, ConfigureSprint);

        builder.OwnsOne(e => e.RaceQualifications, ConfigureRaceQualifications);
        builder.OwnsOne(e => e.Race, ConfigureRace);
    }

    private void ConfigureFP1(OwnedNavigationBuilder<RaceWeek, Session<FP1Result>> builder)
    {
        builder.ToTable("FP1");
        builder.WithOwner()
            .HasForeignKey("RaceWeekId");

        builder.OwnsMany(e => e.SessionResults, srb =>
        {
            srb.ToTable("FP1Results");
            srb.WithOwner()
                .HasForeignKey("SessionId");

            srb.Property(e => e.DriverId)
                .HasConversion(
                    e => e.Value,
                    e => DriverId.Create(e));
            srb.HasOne<Driver>()
                .WithMany()
                .HasForeignKey(e => e.DriverId);
        });

        builder.Navigation(e => e.SessionResults).Metadata.SetField("_sessionResults");
        builder.Navigation(e => e.SessionResults).UsePropertyAccessMode(PropertyAccessMode.Field);
    }

    private void ConfigureFP2(OwnedNavigationBuilder<RaceWeek, Session<FP2Result>> builder)
    {
        builder.ToTable("FP2");
        builder.WithOwner()
            .HasForeignKey("RaceWeekId");

        builder.OwnsMany(e => e.SessionResults, srb =>
        {
            srb.ToTable("FP2Results");
            srb.WithOwner()
                .HasForeignKey("SessionId");

            srb.Property(e => e.DriverId)
                .HasConversion(
                    e => e.Value,
                    e => DriverId.Create(e));
            srb.HasOne<Driver>()
                .WithMany()
                .HasForeignKey(e => e.DriverId);
        });

        builder.Navigation(e => e.SessionResults).Metadata.SetField("_sessionResults");
        builder.Navigation(e => e.SessionResults).UsePropertyAccessMode(PropertyAccessMode.Field);
    }

    private void ConfigureFP3(OwnedNavigationBuilder<RaceWeek, Session<FP3Result>> builder)
    {
        builder.ToTable("FP3");
        builder.WithOwner()
            .HasForeignKey("RaceWeekId");

        builder.OwnsMany(e => e.SessionResults, srb =>
        {
            srb.ToTable("FP3Results");
            srb.WithOwner()
                .HasForeignKey("SessionId");

            srb.Property(e => e.DriverId)
                .HasConversion(
                    e => e.Value,
                    e => DriverId.Create(e));
            srb.HasOne<Driver>()
                .WithMany()
                .HasForeignKey(e => e.DriverId);
        });

        builder.Navigation(e => e.SessionResults).Metadata.SetField("_sessionResults");
        builder.Navigation(e => e.SessionResults).UsePropertyAccessMode(PropertyAccessMode.Field);
    }

    private void ConfigureSprintQualifications(OwnedNavigationBuilder<RaceWeek, Session<SprintQualificationResult>> builder)
    {
        builder.ToTable("SprintQualifications");
        builder.WithOwner()
            .HasForeignKey("RaceWeekId");

        builder.OwnsMany(e => e.SessionResults, srb =>
        {
            srb.ToTable("SprintQualificationResults");
            srb.WithOwner()
                .HasForeignKey("SessionId");

            srb.Property(e => e.DriverId)
                .HasConversion(
                    e => e.Value,
                    e => DriverId.Create(e));
            srb.HasOne<Driver>()
                .WithMany()
                .HasForeignKey(e => e.DriverId);
        });

        builder.Navigation(e => e.SessionResults).Metadata.SetField("_sessionResults");
        builder.Navigation(e => e.SessionResults).UsePropertyAccessMode(PropertyAccessMode.Field);
    }

    private void ConfigureSprint(OwnedNavigationBuilder<RaceWeek, Session<SprintResult>> builder)
    {
        builder.ToTable("Sprints");
        builder.WithOwner()
            .HasForeignKey("RaceWeekId");

        builder.OwnsMany(e => e.SessionResults, srb =>
        {
            srb.ToTable("SprintResults");
            srb.WithOwner()
                .HasForeignKey("SessionId");

            srb.Property(e => e.DriverId)
                .HasConversion(
                    e => e.Value,
                    e => DriverId.Create(e));
            srb.HasOne<Driver>()
                .WithMany()
                .HasForeignKey(e => e.DriverId);
        });

        builder.Navigation(e => e.SessionResults).Metadata.SetField("_sessionResults");
        builder.Navigation(e => e.SessionResults).UsePropertyAccessMode(PropertyAccessMode.Field);
    }

    private void ConfigureRaceQualifications(OwnedNavigationBuilder<RaceWeek, Session<RaceQualificationResult>> builder)
    {
        builder.ToTable("RaceQualifications");
        builder.WithOwner()
            .HasForeignKey("RaceWeekId");

        builder.OwnsMany(e => e.SessionResults, srb =>
        {
            srb.ToTable("RaceQualificationsResults");
            srb.WithOwner()
                .HasForeignKey("SessionId");

            srb.Property(e => e.DriverId)
                .HasConversion(
                    e => e.Value,
                    e => DriverId.Create(e));
            srb.HasOne<Driver>()
                .WithMany()
                .HasForeignKey(e => e.DriverId);
        });

        builder.Navigation(e => e.SessionResults).Metadata.SetField("_sessionResults");
        builder.Navigation(e => e.SessionResults).UsePropertyAccessMode(PropertyAccessMode.Field);
    }

    private void ConfigureRace(OwnedNavigationBuilder<RaceWeek, Session<RaceResult>> builder)
    {
        builder.ToTable("Races");
        builder.WithOwner()
            .HasForeignKey("RaceWeekId");

        builder.OwnsMany(e => e.SessionResults, srb =>
        {
            srb.ToTable("RaceResults");
            srb.WithOwner()
                .HasForeignKey("SessionId");

            srb.Property(e => e.DriverId)
                .HasConversion(
                    e => e.Value,
                    e => DriverId.Create(e));
            srb.HasOne<Driver>()
                .WithMany()
                .HasForeignKey(e => e.DriverId);
        });

        builder.Navigation(e => e.SessionResults).Metadata.SetField("_sessionResults");
        builder.Navigation(e => e.SessionResults).UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
