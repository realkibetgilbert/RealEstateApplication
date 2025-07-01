using Tenant.API.Domain.Entities;

namespace Tenant.API.Domain.Interfaces
{
    public interface ITenantRepository
    {
        Task<TenantProfile> CreateProfileAsync(TenantProfile tenantProfile);
        Task<TenantProfile> GetTenantProfileByEmailAsync(string email);
        Task<TenantProfile> UpdateProfileAsync(TenantProfile tenantProfile);
    }
}
