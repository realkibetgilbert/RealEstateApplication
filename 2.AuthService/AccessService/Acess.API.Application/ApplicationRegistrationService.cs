using Access.API.Application.Features.Auth.Interfaces;
using Access.API.Application.Features.Auth.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Access.API.Application
{
    public static class ApplicationRegistrationService
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            return services;
        }
    }
}
