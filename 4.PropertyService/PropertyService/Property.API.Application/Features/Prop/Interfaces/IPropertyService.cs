﻿using Property.API.Application.Common;
using Property.API.Application.DTOs.Property;

namespace Property.API.Application.Features.Prop.Interfaces
{
    public interface IPropertyService
    {
        Task<ServiceResponse<PropertyToDisplayDto>> CreatePropertyAsync(PropertyToCreateDto propertyToCreateDto);
        Task<ServiceResponse<PropertyToDisplayDto>> GetPropertyByIdAsync(long Id);
        Task<ServiceResponse<PropertyToDisplayDto>> DeletePropertyAsync(long Id);
        Task<ServiceResponse<PropertyToDisplayDto>> UpdatePropertyAsync(PropertyToUpdateDto propertyToUpdateDto);
    }
}
