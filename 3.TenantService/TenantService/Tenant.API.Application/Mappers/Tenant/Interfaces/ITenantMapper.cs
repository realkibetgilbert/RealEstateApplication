using Tenant.API.Application.DTOs.Tenant;
using Tenant.API.Domain.Entities;

namespace Tenant.API.Application.Mappers.Tenant.Interfaces
{
    public interface ITenantMapper
    {
      TenantProfile  ToDomain(CreateTenantProfileDto createTenantDto);
      TenantProfileToDisplayDto  ToDomain(TenantProfile tenantProfile);
    }
}
