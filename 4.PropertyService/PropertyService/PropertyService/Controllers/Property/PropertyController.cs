using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Property.API.Application.Common;
using Property.API.Application.DTOs.Property;
using Property.API.Application.Features.Prop.Interfaces;

namespace Property.API.Controllers.Property
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        private readonly IPropertyService _propertyService;

        public PropertyController(IPropertyService propertyService)
        {
            _propertyService = propertyService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProperty([FromForm] PropertyToCreateDto propertyToCreateDto)
        {
            var response = await _propertyService.CreatePropertyAsync(propertyToCreateDto);

            return response.IsSuccess
                ? Ok(response)
                : BadRequest(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPropertyById(long id)
        {
            var response = await _propertyService.GetPropertyByIdAsync(id);

            return response.IsSuccess
                ? Ok(response)
                : NotFound(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePropertyById(long id)
        {
            var response = await _propertyService.DeletePropertyAsync(id);

            return response.IsSuccess
                ? Ok(response)
                : NotFound(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProperty([FromForm] PropertyToUpdateDto propertyToUpdateDto)
        {
            var response = await _propertyService.UpdatePropertyAsync(propertyToUpdateDto);

            return response.IsSuccess
                ? Ok(response)
                : BadRequest(response);
        }

    }
}