using Access.API.Application.DTOs.Auth;
using Access.API.Application.Features.Auth.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Access.API.Controllers.Auth.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var response = await _authService.LoginAsync(loginDto);

            if (response.IsSuccess)
            {
                return Ok(response); 
            }

            return BadRequest(response); 
        }
    }
}
