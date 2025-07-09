using Microsoft.EntityFrameworkCore;
using Property.API.Domain.Domain;

namespace Property.API.Infrastructure.Persistence
{
    public class PropertyDbContext : DbContext
    {
        public PropertyDbContext(DbContextOptions<PropertyDbContext> options) : base(options)
        {
        }
        public DbSet<Properties> Properties { get; set; }
        public DbSet<Unit> Units { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PropertyDbContext).Assembly);
        }
    }
}
