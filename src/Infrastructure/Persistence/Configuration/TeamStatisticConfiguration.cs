using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.Aggregates.TeamStatistics;
using Domain.Aggregates.TeamStatistics.ValueObjects;
using Domain.Aggregates.Teams.ValueObjects;
using Domain.Aggregates.Teams;


namespace Infrastructure.Persistence.Configuration;
internal sealed class TeamStatisticConfiguration : IEntityTypeConfiguration<TeamStatistic>
{
    public void Configure(EntityTypeBuilder<TeamStatistic> builder)
    {
        builder.ToTable("TeamStatistics");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id)
            .HasConversion(
                e => e.Value,
                e => TeamStatisticId.Create(e));

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