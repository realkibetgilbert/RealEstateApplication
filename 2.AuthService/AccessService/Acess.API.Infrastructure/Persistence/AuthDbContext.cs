using Access.API.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Access.API.Infrastructure.Persistence
{
    public class AuthDbContext : IdentityDbContext<AuthUser, IdentityRole<long>, long>
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options)
            : base(options)
        {
        }
        public DbSet<RegisteredApplication> RegisteredApplications { get; set; }

    }
}