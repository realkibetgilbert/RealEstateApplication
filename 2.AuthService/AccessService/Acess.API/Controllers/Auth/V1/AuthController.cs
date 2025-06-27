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
        [Route("register")]
        public async Task<IActionResult> RegisterUser(UserRegistrationDto expressWayPosUserDto)
        {
            var (isSuccess, result) = await _authService.RegisterUserAsync(expressWayPosUserDto);

            if (isSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var (isSuccess, result) = await _authService.LoginAsync(loginDto);

            if (isSuccess)
            {

                var jsonRes = JsonConvert.SerializeObject(result);

                return Content(jsonRes, "application/json");
            }

            return BadRequest(result);
        }
    }
}
