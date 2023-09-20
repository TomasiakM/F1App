using Domain.Aggregates.Tracks;
using Domain.Aggregates.Tracks.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration;
internal sealed class TrackConfiguration : IEntityTypeConfiguration<Track>
{
    public void Configure(EntityTypeBuilder<Track> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id)
            .HasConversion(
                e => e.Value,
                e => TrackId.Create(e));
        
        builder.HasIndex(e => e.Slug)
            .IsUnique();
    }

}
