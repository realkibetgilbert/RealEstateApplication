using Access.API.Application.Common;
using Access.API.Application.DTOs.Auth;
using System.Threading.Tasks;

namespace Access.API.Application.Features.Auth.Interfaces
{
    public interface IAuthService
    {
        Task<ServiceResponse<LoginResponseDto>> LoginAsync(LoginDto loginDto);
        Task<ServiceResponse<LoginResponseDto>> RefreshTokenAsync(RefreshRequestDto requestDto);
        Task<ServiceResponse<LoginResponseDto>> RegisterAsync(RegisterDto registerDto);
        Task<ServiceResponse<LogoutResponseDto>> LogoutAsync(string authHeader);

    }
}
