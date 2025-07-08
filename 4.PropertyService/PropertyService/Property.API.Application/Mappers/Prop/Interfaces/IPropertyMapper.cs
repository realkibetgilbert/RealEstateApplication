using Property.API.Application.DTOs.Property;
using Property.API.Domain.Domain;

namespace Property.API.Application.Mappers.Prop.Interfaces
{
    public interface IPropertyMapper
    {
        Task<Properties> ToDomain(PropertyToCreateDto propertyToCreateDto);
        Task<PropertyToDisplayDto> ToDomain(Properties property);
    }
}
