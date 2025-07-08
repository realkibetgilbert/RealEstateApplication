using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Property.API.Domain.Domain;
using Property.API.Domain.Interfaces;
using Property.API.Infrastructure.Persistence;

namespace Property.API.Infrastructure.Repositories.SqlServerImplementations
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly PropertyDbContext _context;

        public PropertyRepository(PropertyDbContext context)
        {
            _context = context;
        }
        public async Task<Properties> CreateAsync(Properties property)
        {
            await _context.Properties.AddAsync(property);

            await _context.SaveChangesAsync();

            return property;
        }

        public async Task<Properties?> DeleteAsync(long id)
        {
            var property = await _context.Properties.FirstOrDefaultAsync(p => p.Id == id);
            if (property == null)
            {
                return null;
            }
            _context.Properties.Remove(property);
            await _context.SaveChangesAsync();
            return property;
        }

        public async Task<Properties?> GetByIdAsync(long id)
        {
            return await _context.Properties.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Properties?> UpdateAsync(Properties property)
        {
            var existingProperty = await _context.Properties
                  .FirstOrDefaultAsync(p => p.Id== property.Id);

            if (existingProperty == null)
                return null;

            existingProperty.Name = property.Name;
            existingProperty.Location = property.Location ;
            existingProperty.Type = property.Type;
            existingProperty.Image = property.Image;
            existingProperty.OwnerEmail = property.OwnerEmail;

            await _context.SaveChangesAsync();

            return existingProperty;
        }
    }
}
