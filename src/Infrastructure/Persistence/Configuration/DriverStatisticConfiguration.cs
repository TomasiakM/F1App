using Domain.Aggregates.Drivers;
using Domain.Aggregates.Drivers.ValueObjects;
using Domain.Aggregates.DriverStatistics;
using Domain.Aggregates.DriverStatistics.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration;
internal sealed class DriverStatisticConfiguration : IEntityTypeConfiguration<DriverStatistic>
{
    public void Configure(EntityTypeBuilder<DriverStatistic> builder)
    {
        builder.ToTable("DriverStatistics");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id)
            .HasConversion(
                e => e.Value,
                e => DriverStatisticId.Create(e));

        builder.Property(e => e.DriverId)
            .ValueGeneratedNever()
            .HasConversion(
                e => e.Value,
                e => DriverId.Create(e));
        builder.HasOne<Driver>()
            .WithMany()
            .HasForeignKey(e => e.DriverId);
    }
}
