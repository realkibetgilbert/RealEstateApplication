using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tenant.API.Application.DTOs.Tenant;
using Tenant.API.Application.Features.Tenants.Interfaces;

namespace Tenant.API.Controllers.Tenant
{
    [Route("api/[controller]")]
    [ApiController]
    public class TenantController : ControllerBase
    {
        private readonly ITenantService _tenantService;

        public TenantController(ITenantService tenantService)
        {
            _tenantService = tenantService;
        }


        [HttpPost]
        [Route("tenant-profile")]
        public async Task<IActionResult> Create([FromBody] CreateTenantProfileDto tenantProfileDto)
        {
            var response = await _tenantService.CreateAsync(tenantProfileDto);

            return response.IsSuccess
                ? Ok(response)           
                : BadRequest(response);   
        }

        [HttpGet("tenant-profile/{email}")]
        public async Task<IActionResult> GetByEmail( string email)
        {
            var result = await _tenantService.GetTenantByEmailAsync(email);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }

        [HttpPut("tenant-profile")]
        public async Task<IActionResult> Update([FromBody] CreateTenantProfileDto dto)
        {
            var response = await _tenantService.UpdateAsync(dto);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

    }
}
