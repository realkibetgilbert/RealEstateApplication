using Property.API.Application.DTOs.Property;
using Property.API.Application.Mappers.Prop.Interfaces;
using Property.API.Domain.Domain;

namespace Property.API.Application.Mappers.Prop.Services
{
    public class PropertyMapper : IPropertyMapper
    {
        public async Task<Properties> ToDomain(PropertyToCreateDto dto)
        {
            byte[]? imageBytes = null;

            if (dto.Image != null)
            {
                using var ms = new MemoryStream();
                await dto.Image.CopyToAsync(ms);
                imageBytes = ms.ToArray();
            }
            return new Properties
            {
                Name = dto.Name,
                Location = dto.Location,
                Type = dto.Type,
                OwnerEmail = dto.OwnerEmail,
                Image = imageBytes
            };
        }

        public async Task<PropertyToDisplayDto> ToDomain(Properties property)
        {
            var dto = new PropertyToDisplayDto
            {
                Id = property.Id,
                Name = property.Name,
                Location = property.Location,
                Type = property.Type,
                OwnerEmail = property.OwnerEmail,
                ImageBase64 = property.Image != null
                ? $"data:image/jpeg;base64,{Convert.ToBase64String(property.Image)}"
                : null
            };

            return await Task.FromResult(dto);
        }

        public async Task<Properties> ToDomain(PropertyToUpdateDto dto)
        {
            byte[]? imageBytes = null;

            if (dto.Image != null)
            {
                using var ms = new MemoryStream();
                dto.Image.CopyTo(ms);
                imageBytes = ms.ToArray();
            }
            return await Task.FromResult(new Properties
            {
                Id = dto.Id,
                Name = dto.Name,
                Location = dto.Location,
                Type = dto.Type,
                OwnerEmail = dto.OwnerEmail,
                Image = imageBytes
            });
        }

    }
}
