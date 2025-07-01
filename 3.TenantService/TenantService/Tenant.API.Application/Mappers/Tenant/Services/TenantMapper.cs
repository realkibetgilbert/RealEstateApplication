using AutoMapper;
using Tenant.API.Application.DTOs.Tenant;
using Tenant.API.Application.Mappers.Tenant.Interfaces;
using Tenant.API.Domain.Entities;

namespace Tenant.API.Application.Mappers.Tenant.Services
{
    public class TenantMapper : ITenantMapper
    {
        private readonly IMapper _mapper;

        public TenantMapper(IMapper mapper)
        {
            _mapper = mapper;
        }

        public TenantProfile ToDomain(CreateTenantProfileDto dto)
        {
            return _mapper.Map<TenantProfile>(dto);
        }

        public TenantProfileToDisplayDto ToDomain(TenantProfile tenantProfile)
        {
            return _mapper.Map<TenantProfileToDisplayDto>(tenantProfile);
        }
    }
}
