using Access.API.Application.DTOs.Auth;
using Access.API.Application.Features.Auth.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var response = await _authService.LoginAsync(loginDto);

            if (response.IsSuccess)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpPost]
        [Route("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshRequestDto requestDto)
        {
            var response = await _authService.RefreshTokenAsync(requestDto);

            if (response.IsSuccess)
                return Ok(response);

            return Unauthorized(response);
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var response = await _authService.RegisterAsync(registerDto);

            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response);
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var authHeader = Request.Headers["Authorization"].ToString();
            var response = await _authService.LogoutAsync(authHeader);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
    }
}
