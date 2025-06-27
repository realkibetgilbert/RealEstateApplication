using Access.API.Domain.Interfaces;
using Access.API.Infrastructure.Repositories.SqlServerImplementations;
using Microsoft.Extensions.DependencyInjection;

namespace Access.API.Infrastructure
{
    public static class InfrastructureRegistrationServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenRepository, TokenRepository>();

            return services;
        }
    }
}
