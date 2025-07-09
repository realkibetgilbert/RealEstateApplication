using Property.API.Application.Common;
using Property.API.Application.DTOs.Property;
using Property.API.Application.DTOs.Unit;
using Property.API.Application.Features.Uni.Interfaces;
using Property.API.Application.Mappers.Units.Interfaces;
using Property.API.Domain.Interfaces;

namespace Property.API.Application.Features.Uni.Services
{
    public class UnitService : IUnitService
    {
        private readonly IUnitRepository _unitRepository;
        private readonly IUnitMapper _unitMapper;

        public UnitService(IUnitRepository unitRepository, IUnitMapper unitMapper)
        {
            _unitRepository = unitRepository;
            _unitMapper = unitMapper;
        }
        public async Task<ServiceResponse<UnitToDisplayDto>> CreateUnitAsync(UnitToCreateDto unitToCreateDto)
        {
            try
            {

                var unitDomain = await _unitMapper.ToDomainAsync(unitToCreateDto);

                var createdUnit = await _unitRepository.CreateUnitAsync(unitDomain);

                var unitToDisplay = await _unitMapper.ToDomainAsync(createdUnit);

                return ServiceResponse<UnitToDisplayDto>.Success(unitToDisplay);

            }
            catch (Exception)
            {
                //TODO: log here...
                return ServiceResponse<UnitToDisplayDto>.Failure();
            }
        }

        public async Task<ServiceResponse<UnitToDisplayDto>> DeleteUnitAsync(long Id)
        {
            try
            {
                var existingUnit = await _unitRepository.GetUnitByIdAsync(Id);

                if (existingUnit == null)
                {
                    return ServiceResponse<UnitToDisplayDto>.Failure();
                }

                await _unitRepository.DeleteAsync(Id);

                var unitToDisplay = await _unitMapper.ToDomainAsync(existingUnit);

                return ServiceResponse<UnitToDisplayDto>.Success(unitToDisplay);

            }
            catch (Exception)
            {
                //TODO:log here......
                return ServiceResponse<UnitToDisplayDto>.Failure();
            }
        }

        public async Task<ServiceResponse<UnitToDisplayDto>> GetUnitByIdAsync(long Id)
        {
            try
            {
                var property = await _unitRepository.GetUnitByIdAsync(Id);

                if (property == null)
                {
                    return ServiceResponse<UnitToDisplayDto>.Failure();
                }

                var unitDisplayDto = await _unitMapper.ToDomainAsync(property);

                return ServiceResponse<UnitToDisplayDto>.Success(unitDisplayDto);
            }
            catch (Exception)
            {
                //logging
                return ServiceResponse<UnitToDisplayDto>.Failure();

            }
        }

        public async Task<ServiceResponse<UnitToDisplayDto>> UpdateUnitAsync(UnitToUpdateDto unitToUpdateDto)
        {
            try
            {
                var updatedUnitDomain = await _unitMapper.ToDomainAsync(unitToUpdateDto);

                var updatedUnit = await _unitRepository.UpdateAsync(updatedUnitDomain);

                if (updatedUnit == null)
                {
                    return ServiceResponse<UnitToDisplayDto>.Failure();
                }

                var unitToDisplay = await _unitMapper.ToDomainAsync(updatedUnit);

                return ServiceResponse<UnitToDisplayDto>.Success(unitToDisplay);


            }
            catch (Exception)
            {
                //TODO:log here...
                return ServiceResponse<UnitToDisplayDto>.Failure();
            }
        }
    }
}
