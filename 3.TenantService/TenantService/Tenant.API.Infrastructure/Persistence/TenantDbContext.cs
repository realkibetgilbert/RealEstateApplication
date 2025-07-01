using Microsoft.EntityFrameworkCore;
using Tenant.API.Domain.Entities;

namespace Tenant.API.Infrastructure.Persistence
{
    public class TenantDbContext : DbContext
    {
        public TenantDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<TenantProfile> TenantProfiles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TenantDbContext).Assembly);
        }
    }
}