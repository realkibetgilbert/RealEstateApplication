using Property.API.Application.Common;
using Property.API.Application.DTOs.Property;
using Property.API.Application.Features.Prop.Interfaces;
using Property.API.Application.Mappers.Prop.Interfaces;
using Property.API.Domain.Interfaces;

namespace Property.API.Application.Features.Prop.Services
{
    public class PropertyService : IPropertyService
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IPropertyMapper _propertyMapper;

        public PropertyService(IPropertyRepository propertyRepository, IPropertyMapper propertyMapper)
        {
            _propertyRepository = propertyRepository;
            _propertyMapper = propertyMapper;
        }
        public async Task<ServiceResponse<PropertyToDisplayDto>> CreatePropertyAsync(PropertyToCreateDto propertyToCreateDto)
        {
            try
            {

                var propertyDomain = await _propertyMapper.ToDomain(propertyToCreateDto);

                var createdProperty = await _propertyRepository.CreateAsync(propertyDomain);

                var propertyToDisplayDto = await _propertyMapper.ToDomain(createdProperty);

                return ServiceResponse<PropertyToDisplayDto>.Success(propertyToDisplayDto);

            }
            catch (Exception)
            {

                return ServiceResponse<PropertyToDisplayDto>.Failure();
            }
        }
    }
}
