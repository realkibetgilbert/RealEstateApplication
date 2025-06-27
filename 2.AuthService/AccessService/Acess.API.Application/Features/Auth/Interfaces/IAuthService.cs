using Access.API.Application.DTOs.Auth;

namespace Access.API.Application.Features.Auth.Interfaces
{
    public interface IAuthService
    {
        Task<(bool isSuccess, object result)> RegisterUserAsync(UserRegistrationDto dto);
        Task<(bool isSuccess, object result)> LoginAsync(LoginDto loginDto);
    }
}
