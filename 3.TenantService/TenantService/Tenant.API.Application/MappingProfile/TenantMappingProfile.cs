using AutoMapper;
using Tenant.API.Application.DTOs.Tenant;
using Tenant.API.Domain.Entities;

namespace Tenant.API.Application.MappingProfile
{
    public class TenantMappingProfile : Profile
    {
        public TenantMappingProfile()
        {
            {
                CreateMap<CreateTenantProfileDto, TenantProfile>().ReverseMap();
                CreateMap<TenantProfile, TenantProfileToDisplayDto>().ReverseMap();
            }
        }
    }
}