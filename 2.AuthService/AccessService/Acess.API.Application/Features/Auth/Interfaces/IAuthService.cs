using Access.API.Application.Common;
using Access.API.Application.DTOs.Auth;

namespace Access.API.Application.Features.Auth.Interfaces
{
    public interface IAuthService
    {
        Task<ServiceResponse<LoginResponseDto>> LoginAsync(LoginDto loginDto);
        Task<ServiceResponse<LoginResponseDto>> RefreshTokenAsync(RefreshRequestDto requestDto);
    }
}
