using Property.API.Domain.Domain;

namespace Property.API.Domain.Interfaces
{
    public interface IPropertyRepository
    {
        Task<Properties> CreateAsync(Properties property);

        Task<Properties?> GetByIdAsync(long id);

        Task<Properties?> UpdateAsync(Properties property);

        Task<Properties?> DeleteAsync(long id);
    }
}
