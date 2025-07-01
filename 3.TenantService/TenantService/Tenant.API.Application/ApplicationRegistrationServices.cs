using Microsoft.Extensions.DependencyInjection;
using Tenant.API.Application.Features.Tenants.Interfaces;
using Tenant.API.Application.Features.Tenants.Services;
using Tenant.API.Application.Mappers.Tenant.Interfaces;
using Tenant.API.Application.Mappers.Tenant.Services;

namespace Tenant.API.Application
{
    public static class ApplicationRegistrationServices
    {
        public static IServiceCollection AddApplicationRegistrationServices(this IServiceCollection services)
        {


            services.AddScoped<ITenantService, TenantService>();
            services.AddTransient<ITenantMapper, TenantMapper>();
            return services;
        }
    }
}
