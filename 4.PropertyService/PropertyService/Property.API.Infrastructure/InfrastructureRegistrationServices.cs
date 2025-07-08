using Microsoft.Extensions.DependencyInjection;
using Property.API.Domain.Interfaces;
using Property.API.Infrastructure.Repositories.SqlServerImplementations;

namespace Property.API.Infrastructure
{
    public static class InfrastructureRegistrationServices
    {
        public static IServiceCollection AddInfrastructureRegistrationServices(this IServiceCollection services)
        {
            services.AddScoped<IPropertyRepository, PropertyRepository>();
            services.AddScoped<IUnitRepository, UnitRepository>();

            return services;
        }
    }
}
