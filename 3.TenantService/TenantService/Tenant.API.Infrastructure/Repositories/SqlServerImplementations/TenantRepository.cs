using Microsoft.EntityFrameworkCore;
using Tenant.API.Domain.Entities;
using Tenant.API.Domain.Interfaces;
using Tenant.API.Infrastructure.Persistence;

namespace Tenant.API.Infrastructure.Repositories.SqlServerImplementations
{
    public class TenantRepository : ITenantRepository
    {
        private readonly TenantDbContext _tenantDbContext;

        public TenantRepository(TenantDbContext tenantDbContext)
        {
            _tenantDbContext = tenantDbContext;
        }
        public async Task<TenantProfile> CreateProfileAsync(TenantProfile tenantProfile)
        {
            await _tenantDbContext.TenantProfiles.AddAsync(tenantProfile);

            await _tenantDbContext.SaveChangesAsync();

            return tenantProfile;
        }

        public async Task<TenantProfile> GetTenantProfileByEmailAsync(string email)
        {
            return await _tenantDbContext.TenantProfiles
                .FirstOrDefaultAsync(p => p.Email.ToLower() == email.ToLower());
        }

        public async Task<TenantProfile> UpdateProfileAsync(TenantProfile tenantProfile)
        {
            var existing = await _tenantDbContext.TenantProfiles
                .FirstOrDefaultAsync(x => x.Email.ToLower() == tenantProfile.Email.ToLower());

            if (existing == null)
                return null;

            existing.FullName = tenantProfile.FullName;
            existing.PhoneNumber = tenantProfile.PhoneNumber;
            existing.IdNumber = tenantProfile.IdNumber;
            existing.PreferredLocation = tenantProfile.PreferredLocation;
            existing.Gender = tenantProfile.Gender;

            await _tenantDbContext.SaveChangesAsync();

            return existing;
        }


    }
}
