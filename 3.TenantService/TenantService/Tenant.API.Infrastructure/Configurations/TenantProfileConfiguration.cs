using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tenant.API.Domain.Entities;

namespace Tenant.API.Infrastructure.Configurations
{
    public class TenantProfileConfiguration : IEntityTypeConfiguration<TenantProfile>
    {
        public void Configure(EntityTypeBuilder<TenantProfile> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.FullName)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(t => t.PhoneNumber)
                   .IsRequired()
                   .HasMaxLength(20);

            builder.Property(t => t.IdNumber)
                   .IsRequired()
                   .HasMaxLength(30);

            builder.Property(t => t.PreferredLocation)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(t => t.Gender)
                   .IsRequired()
                   .HasMaxLength(10);

            builder.Property(t => t.Email)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.HasIndex(t => t.Email)
                   .IsUnique();

            builder.Property(t => t.CreatedAt)
                   .HasDefaultValueSql("GETUTCDATE()");
        }
    }
}
