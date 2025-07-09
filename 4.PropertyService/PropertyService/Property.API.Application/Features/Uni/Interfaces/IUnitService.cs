using Property.API.Application.Common;
using Property.API.Application.DTOs.Property;
using Property.API.Application.DTOs.Unit;

namespace Property.API.Application.Features.Uni.Interfaces
{
    public interface IUnitService
    {
        Task<ServiceResponse<UnitToDisplayDto>> CreateUnitAsync(UnitToCreateDto unitToCreateDto);
        Task<ServiceResponse<UnitToDisplayDto>> GetUnitByIdAsync(long Id);
        Task<ServiceResponse<UnitToDisplayDto>> DeleteUnitAsync(long Id);
        Task<ServiceResponse<UnitToDisplayDto>> UpdateUnitAsync(UnitToUpdateDto unitToUpdateDto);
    }
}
