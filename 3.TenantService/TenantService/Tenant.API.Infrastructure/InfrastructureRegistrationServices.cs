using Microsoft.Extensions.DependencyInjection;
using Tenant.API.Domain.Interfaces;
using Tenant.API.Infrastructure.Repositories.SqlServerImplementations;

namespace Tenant.API.Infrastructure
{
    public static class InfrastructureRegistrationServices
    {
        public static IServiceCollection AddInfrastructureRegistrationServices(this IServiceCollection services)
        {
            services.AddScoped<ITenantRepository, TenantRepository>();

            return services;
        }
    }
}
