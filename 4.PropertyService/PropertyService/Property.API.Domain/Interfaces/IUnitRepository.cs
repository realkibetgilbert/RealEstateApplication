using Property.API.Domain.Domain;

namespace Property.API.Domain.Interfaces
{
    public interface IUnitRepository
    {
        Task<Unit> CreateUnitAsync(Unit unit);
        Task<Unit?> GetUnitByIdAsync(long id);
        Task<Unit?> UpdateAsync(Unit unit);
        Task<Unit?> DeleteAsync(long id);
        Task<IEnumerable<Unit>> GetUnitsByPropertyIdAsync(long propertyId);
    }
}
