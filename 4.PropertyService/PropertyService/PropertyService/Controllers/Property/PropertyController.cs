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
    }
}