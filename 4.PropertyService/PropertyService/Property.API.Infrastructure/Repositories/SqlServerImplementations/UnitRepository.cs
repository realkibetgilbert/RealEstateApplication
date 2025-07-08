using Property.API.Domain.Domain;
using Property.API.Domain.Interfaces;

namespace Property.API.Infrastructure.Repositories.SqlServerImplementations
{
    public class UnitRepository : IUnitRepository
    {
        public UnitRepository()
        {
            
        }
        public Task<Unit> CreateUnitAsync(Unit unit)
        {
            throw new NotImplementedException();
        }

        public Task<Unit?> DeleteAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<Unit?> GetUnitByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Unit>> GetUnitsByPropertyIdAsync(long propertyId)
        {
            throw new NotImplementedException();
        }

        public Task<Unit?> UpdateAsync(Unit unit)
        {
            throw new NotImplementedException();
        }
    }
}
