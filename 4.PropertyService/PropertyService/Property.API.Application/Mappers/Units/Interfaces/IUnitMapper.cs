using Property.API.Application.DTOs.Unit;
using Property.API.Domain.Domain;

namespace Property.API.Application.Mappers.Units.Interfaces
{
    public interface IUnitMapper
    {
        Task<Unit> ToDomainAsync(UnitToCreateDto unitToCreateDto);
        Task<UnitToDisplayDto> ToDomainAsync(Unit unit);
        Task<Unit> ToDomainAsync(UnitToUpdateDto unitToUpdateDto);
    }
}
