using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Property.API.Domain.Domain;

namespace Property.API.Infrastructure.Configurations.UnitConfigs;

public class UnitConfigurations : IEntityTypeConfiguration<Unit>
{
    public void Configure(EntityTypeBuilder<Unit> builder)
    {
        builder.HasIndex(u => u.UnitNumber).IsUnique();

        builder.Property(u => u.UnitNumber).IsRequired();
        builder.Property(u => u.Floor).IsRequired();
        builder.Property(u => u.Status).IsRequired();
        builder.Property(u => u.MonthlyRent).HasColumnType("decimal(18,2)");

        builder.HasOne(u => u.Property)
               .WithMany(p => p.Units)
               .HasForeignKey(u => u.PropertyId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}


