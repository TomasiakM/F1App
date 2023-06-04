using Domain.Aggregates.Roles;
using Domain.Aggregates.Roles.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration;
internal sealed class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Roles");

        builder.HasKey(x => x.Id);
        builder.Property(e => e.Id)
            .HasConversion(
                e => e.Value,
                e => RoleId.Create(e));

        builder.HasData(
            Role.UserRole, 
            Role.AdminRole);
    }
}
