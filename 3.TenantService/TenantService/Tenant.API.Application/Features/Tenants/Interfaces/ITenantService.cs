using Tenant.API.Application.Common;
using Tenant.API.Application.DTOs.Tenant;

namespace Tenant.API.Application.Features.Tenants.Interfaces
{
    public interface ITenantService
    {
        Task<ServiceResponse<TenantProfileToDisplayDto>> CreateAsync(CreateTenantProfileDto dto);
        Task<ServiceResponse<TenantProfileToDisplayDto>> GetTenantByEmailAsync(string email);
        Task<ServiceResponse<TenantProfileToDisplayDto>> UpdateAsync(CreateTenantProfileDto dto);

    }
}
