using Access.API.Application.Common;
using Access.API.Application.DTOs.Auth;
using Access.API.Application.Features.Auth.Interfaces;
using Access.API.Domain.Entities;
using Access.API.Domain.Interfaces;
using Access.API.Infrastructure.Caching;
using Access.API.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace Access.API.Application.Features.Auth.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AuthUser> _userManager;
        private readonly RoleManager<IdentityRole<long>> _roleManager;
        private readonly AuthDbContext _context;
        private readonly ITokenRepository _tokenRepository;
        private readonly IDistributedCache _cache;

        public AuthService(
            UserManager<AuthUser> userManager,
            RoleManager<IdentityRole<long>> roleManager,
            AuthDbContext context,
            ITokenRepository tokenRepository,
            IDistributedCache cache)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _tokenRepository = tokenRepository;
            _cache = cache;
        }

       
        public async Task<ServiceResponse<LoginResponseDto>> LoginAsync(LoginDto loginDto)
        {
            var loginUser = await _context.Users.FirstOrDefaultAsync(e => e.Email == loginDto.UserName);

            if (loginUser == null)
            {
                return ServiceResponse<LoginResponseDto>.Failure();
                
            }

            bool isPasswordValid = await _userManager.CheckPasswordAsync(loginUser, loginDto.Password);

            if (!isPasswordValid)
            {
                return ServiceResponse<LoginResponseDto>.Failure();
            }

            var redisKey= $"auth_login_response:{loginUser.Id}";

            if(_cache.TryGetValue<LoginResponseDto>(redisKey,out var cachedResponse) && cachedResponse is not null)
            {
                return ServiceResponse<LoginResponseDto>.Success(cachedResponse);   
            }

            var roles = await _userManager.GetRolesAsync(loginUser);

            var jwtToken = _tokenRepository.CreateJwtToken(loginUser, roles.ToList());

            var loginResponse = new LoginResponseDto
            {
                Token = jwtToken,
                Email = loginDto.UserName,
                Roles = roles.ToList()
            };

            await _cache.SetAsync(redisKey, loginResponse, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow=TimeSpan.FromMinutes(15)
            });

            return ServiceResponse<LoginResponseDto>.Success(loginResponse);
        }
    }
}
