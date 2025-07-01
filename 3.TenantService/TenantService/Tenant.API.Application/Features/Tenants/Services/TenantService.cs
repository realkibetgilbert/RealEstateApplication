using Tenant.API.Application.Common;
using Tenant.API.Application.DTOs.Tenant;
using Tenant.API.Application.Features.Tenants.Interfaces;
using Tenant.API.Application.Mappers.Tenant.Interfaces;
using Tenant.API.Domain.Interfaces;

namespace Tenant.API.Application.Features.Tenants.Services
{
    public class TenantService : ITenantService
    {
        private readonly ITenantRepository _tenantRepository;
        private readonly ITenantMapper _tenantMapper;

        public TenantService(ITenantRepository tenantRepository, ITenantMapper tenantMapper)
        {
            _tenantRepository = tenantRepository;
            _tenantMapper = tenantMapper;
        }
        public async Task<ServiceResponse<TenantProfileToDisplayDto>> CreateAsync(CreateTenantProfileDto dto)
        {
            try
            {
                var tenantProfile = _tenantMapper.ToDomain(dto);
               
                var createdTenant = await _tenantRepository.CreateProfileAsync(tenantProfile);

                var result = _tenantMapper.ToDomain(createdTenant);

                return ServiceResponse<TenantProfileToDisplayDto>.Success(result);
            }
            catch (Exception ex)
            {
                return ServiceResponse<TenantProfileToDisplayDto>.Failure();
            }
        }

        public async Task<ServiceResponse<TenantProfileToDisplayDto>> GetTenantByEmailAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return ServiceResponse<TenantProfileToDisplayDto>.Failure();
            }

            try
            {
                var profile = await _tenantRepository.GetTenantProfileByEmailAsync(email);

                if (profile == null)
                {
                    // Profile not found is not a failure, return success with null
                    return ServiceResponse<TenantProfileToDisplayDto>.Success(null);
                }

                var dto = _tenantMapper.ToDomain(profile);
                return ServiceResponse<TenantProfileToDisplayDto>.Success(dto);
            }
            catch (Exception)
            {
                return ServiceResponse<TenantProfileToDisplayDto>.Failure();
            }
        }

        public async Task<ServiceResponse<TenantProfileToDisplayDto>> UpdateAsync(CreateTenantProfileDto dto)
        {
            try
            {
                var existingProfile = await _tenantRepository.GetTenantProfileByEmailAsync(dto.Email);

                if (existingProfile == null)
                    return ServiceResponse<TenantProfileToDisplayDto>.Failure();

                var updatedProfileData = _tenantMapper.ToDomain(dto);

                var updatedProfile = await _tenantRepository.UpdateProfileAsync(updatedProfileData);
                var result = _tenantMapper.ToDomain(updatedProfile);

                return ServiceResponse<TenantProfileToDisplayDto>.Success(result);
            }
            catch (Exception)
            {
                return ServiceResponse<TenantProfileToDisplayDto>.Failure();
            }
        }
    }
}
