using Property.API.Application.DTOs.Unit;
using Property.API.Application.Mappers.Units.Interfaces;
using Property.API.Domain.Domain;

namespace Property.API.Application.Mappers.Units.Services
{
    internal class UnitMapper : IUnitMapper
    {
        public async Task<Unit> ToDomainAsync(UnitToCreateDto unitToCreateDto)
        {
            byte[]? imageBytes = null;

            if (unitToCreateDto.Image != null)
            {
                using var ms = new MemoryStream();
                await unitToCreateDto.Image.CopyToAsync(ms);
                imageBytes = ms.ToArray();
            }
            return new Unit
            {
                UnitNumber = unitToCreateDto.UnitNumber,
                Floor = unitToCreateDto.Floor,
                Status = unitToCreateDto.Status,
                MonthlyRent = unitToCreateDto.MonthlyRent,
                OwnerEmail = unitToCreateDto.OwnerEmail,
                PropertyId = unitToCreateDto.PropertyId,
                Image = imageBytes
            };
        }

        public async Task<UnitToDisplayDto> ToDomainAsync(Unit unit)
        {
            var dto = new UnitToDisplayDto
            {
                Id = unit.Id,
                UnitNumber = unit.UnitNumber,
                Floor = unit.Floor,
                Status = unit.Status,
                MonthlyRent = unit.MonthlyRent,
                OwnerEmail = unit.OwnerEmail,
                PropertyId = unit.PropertyId,
                ImageBase64 = unit.Image != null
    ? $"data:image/jpeg;base64,{Convert.ToBase64String(unit.Image)}"
    : null
            };

            return await Task.FromResult(dto);
        }

        public async Task<Unit> ToDomainAsync(UnitToUpdateDto unitToUpdateDto)
        {
            byte[]? imageBytes = null;

            if (unitToUpdateDto.Image != null)
            {
                using var ms = new MemoryStream();
                unitToUpdateDto.Image.CopyTo(ms);
                imageBytes = ms.ToArray();
            }
            return await Task.FromResult(new Unit
            {
                Id = unitToUpdateDto.Id,
                UnitNumber = unitToUpdateDto.UnitNumber,
                Floor = unitToUpdateDto.Floor,
                Status = unitToUpdateDto.Status,
                MonthlyRent = unitToUpdateDto.MonthlyRent,
                OwnerEmail = unitToUpdateDto.OwnerEmail,
                PropertyId = unitToUpdateDto.PropertyId,
                Image = imageBytes
            });
        }
    }
}
