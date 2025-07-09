using Microsoft.Extensions.DependencyInjection;
using Property.API.Application.Features.Prop.Interfaces;
using Property.API.Application.Features.Prop.Services;
using Property.API.Application.Features.Uni.Interfaces;
using Property.API.Application.Features.Uni.Services;
using Property.API.Application.Mappers.Prop.Interfaces;
using Property.API.Application.Mappers.Prop.Services;
using Property.API.Application.Mappers.Units.Interfaces;
using Property.API.Application.Mappers.Units.Services;

namespace Property.API.Application
{
    public static class ApplicationRegistrationServices
    {
        public static IServiceCollection AddApplicationRegistrationServices(this IServiceCollection services)
        {

            services.AddScoped<IPropertyService, PropertyService>();
            services.AddTransient<IPropertyMapper, PropertyMapper>();
            services.AddTransient<IUnitService, UnitService>();
            services.AddTransient<IUnitMapper, UnitMapper>();

            return services;
        }
    }
}
