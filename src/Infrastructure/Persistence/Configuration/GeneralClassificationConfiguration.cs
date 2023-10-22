using Domain.Aggregates.Drivers;
using Domain.Aggregates.GeneralClassifications;
using Domain.Aggregates.GeneralClassifications.ValueObjects;
using Domain.Aggregates.Seasons;
using Domain.Aggregates.Seasons.ValueObjects;
using Domain.Aggregates.Teams;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Aggregates.Drivers.ValueObjects;
using Domain.Aggregates.Teams.ValueObjects;

namespace Infrastructure.Persistence.Configuration;
internal sealed class GeneralClassificationConfiguration : IEntityTypeConfiguration<GeneralClassification>
{
    public void Configure(EntityTypeBuilder<GeneralClassification> builder)
    {
        builder.ToTable("GeneralClassifications");

        builder.HasKey(x => x.Id);
        builder.Property(e => e.Id)
            .ValueGeneratedNever()
            .HasConversion(
                e => e.Value,
                e => GeneralClassificationId.Create(e));

        builder.Property(e => e.SeasonId)
            .ValueGeneratedNever()
            .HasConversion(
                e => e.Value,
                e => SeasonId.Create(e));
        builder.HasOne<Season>()
            .WithMany()
            .HasForeignKey(e => e.SeasonId);

        builder.OwnsMany(e => e.Drivers, ConfigureDrivers);
        builder.Navigation(e => e.Drivers).Metadata.SetField("_drivers");
        builder.Navigation(e => e.Drivers).UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.OwnsMany(e => e.Teams, ConfigureTeams);
        builder.Navigation(e => e.Teams).Metadata.SetField("_teams");
        builder.Navigation(e => e.Teams).UsePropertyAccessMode(PropertyAccessMode.Field);
    }


    private void ConfigureDrivers(OwnedNavigationBuilder<GeneralClassification, DriverClassification> builder)
    {
        builder.ToTable("DriverClassifications");

        builder.Property(e => e.DriverId)
            .ValueGeneratedNever()
            .HasConversion(
                e => e.Value,
                e => DriverId.Create(e));
        builder.HasOne<Driver>()
            .WithMany()
            .HasForeignKey(e => e.DriverId);
    }

    private void ConfigureTeams(OwnedNavigationBuilder<GeneralClassification, TeamClassification> builder)
    {
        builder.ToTable("TeamClassifications");

        builder.Property(e => e.TeamId)
            .ValueGeneratedNever()
            .HasConversion(
                e => e.Value,
                e => TeamId.Create(e));
        builder.HasOne<Team>()
            .WithMany()
            .HasForeignKey(e => e.TeamId);
    }
}
