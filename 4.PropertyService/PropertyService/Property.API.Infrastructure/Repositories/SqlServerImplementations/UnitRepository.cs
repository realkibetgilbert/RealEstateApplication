using Microsoft.EntityFrameworkCore;
using Property.API.Domain.Domain;
using Property.API.Domain.Interfaces;
using Property.API.Infrastructure.Persistence;

namespace Property.API.Infrastructure.Repositories.SqlServerImplementations
{
    public class UnitRepository : IUnitRepository
    {
        private readonly PropertyDbContext _propertyDbContext;

        public UnitRepository(PropertyDbContext propertyDbContext)
        {
            _propertyDbContext = propertyDbContext;
        }
        public async Task<Unit> CreateUnitAsync(Unit unit)
        {
            await _propertyDbContext.Units.AddAsync(unit);

            await _propertyDbContext.SaveChangesAsync();

            return unit;
        }

        public async Task<Unit?> DeleteAsync(long id)
        {
            var existingUnit = await _propertyDbContext.Units.FindAsync(id);

            if (existingUnit == null)
            {
                return null;
            }

            await _propertyDbContext.Units
                 .Where(u => u.Id == id)
                 .ExecuteDeleteAsync();

            await _propertyDbContext.SaveChangesAsync();

            return existingUnit;
        }

        public async Task<Unit?> GetUnitByIdAsync(long id)
        {
            return await _propertyDbContext.Units
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<Unit>> GetUnitsByPropertyIdAsync(long propertyId)
        {
            return await _propertyDbContext.Units
                .Where(u => u.PropertyId == propertyId)
                .ToListAsync();
        }

        public async Task<Unit?> UpdateAsync(Unit unit)
        {
            var existingUnit = await _propertyDbContext.Units.FindAsync(unit.Id);

            if (existingUnit == null)
            {
                return null;
            }

            existingUnit.OwnerEmail = unit.OwnerEmail;
            existingUnit.Status = unit.Status;
            existingUnit.Floor = unit.Floor;
            existingUnit.UnitNumber = unit.UnitNumber;
            existingUnit.MonthlyRent = unit.MonthlyRent;
            existingUnit.Image = unit.Image;

            await _propertyDbContext.SaveChangesAsync();
            
            return existingUnit;
        }
    }
}
