using Microsoft.AspNetCore.Mvc;
using Property.API.Application.DTOs.Unit;
using Property.API.Application.Features.Uni.Interfaces;

namespace Property.API.Controllers.Unit
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitController : ControllerBase
    {
        private readonly IUnitService _unitService;

        public UnitController(IUnitService unitService)
        {
            _unitService = unitService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] UnitToCreateDto unitToCreateDto)
        {
            var response = await _unitService.CreateUnitAsync(unitToCreateDto);

            return response.IsSuccess
                ? Ok(response)
                : BadRequest(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(long id)
        {
            var response = await _unitService.GetUnitByIdAsync(id);

            return response.IsSuccess
                ? Ok(response)
                : NotFound(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteByIdAsync(long id)
        {
            var response = await _unitService.DeleteUnitAsync(id);

            return response.IsSuccess
                ? Ok(response)
                : NotFound(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromForm] UnitToUpdateDto unitToUpdateDto)
        {
            var response = await _unitService.UpdateUnitAsync(unitToUpdateDto);

            return response.IsSuccess
                ? Ok(response)
                : BadRequest(response);
        }

    }
}
