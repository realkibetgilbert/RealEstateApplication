using Access.API.Application.Common;
using Access.API.Application.DTOs.Auth;
using Access.API.Application.Features.Auth.Interfaces;
using Access.API.Domain.Entities;
using Access.API.Domain.Interfaces;
using Access.API.Infrastructure.Caching;
using Access.API.Infrastructure.Data.Seed.Models;
using Access.API.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;

namespace Access.API.Application.Features.Auth.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AuthUser> _userManager;
        private readonly RoleManager<IdentityRole<long>> _roleManager;
        private readonly AuthDbContext _context;
        private readonly ITokenRepository _tokenRepository;
        private readonly IDistributedCache _cache;
        private readonly IOptions<SeederSettings> _options;

        public AuthService(
            UserManager<AuthUser> userManager,
            RoleManager<IdentityRole<long>> roleManager,
            AuthDbContext context,
            ITokenRepository tokenRepository,
            IDistributedCache cache,
            IOptions<SeederSettings> options)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _tokenRepository = tokenRepository;
            _cache = cache;
            _options = options;
        }


        public async Task<ServiceResponse<LoginResponseDto>> LoginAsync(LoginDto loginDto)
        {
            var loginUser = await _context.Users.FirstOrDefaultAsync(e => e.Email == loginDto.UserName);

            if (loginUser == null || !await _userManager.CheckPasswordAsync(loginUser, loginDto.Password))
            {
                return ServiceResponse<LoginResponseDto>.Failure();

            }

            var redisKey = $"auth_login_response:{loginUser.Id}";

            if (_cache.TryGetValue<LoginResponseDto>(redisKey, out var cachedResponse) && cachedResponse is not null)
            {
                return ServiceResponse<LoginResponseDto>.Success(cachedResponse);
            }

            var roles = await _userManager.GetRolesAsync(loginUser);

            var jwtToken = _tokenRepository.CreateJwtToken(loginUser, roles.ToList());

            var refreshToken = _tokenRepository.GenerateRefreshToken();

            await _cache.SetAsync($"refresh_token:{loginUser.Id}", refreshToken, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(7)
            });

            var loginResponse = new LoginResponseDto
            {
                Token = jwtToken,
                RefreshToken = refreshToken,
                Email = loginDto.UserName,
                Roles = roles.ToList()
            };

            await _cache.SetAsync(redisKey, loginResponse, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(15)
            });

            return ServiceResponse<LoginResponseDto>.Success(loginResponse);
        }

        public async Task<ServiceResponse<LoginResponseDto>> RefreshTokenAsync(RefreshRequestDto requestDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == requestDto.Email);

            if (user == null)
            {
                return ServiceResponse<LoginResponseDto>.Failure();
            }

            var redisKey = $"refresh_token:{user.Id}";

            if (!_cache.TryGetValue<string>(redisKey, out var savedToken) || savedToken != requestDto.RefreshToken)
                return ServiceResponse<LoginResponseDto>.Failure();

            var roles = await _userManager.GetRolesAsync(user);
            var newJwtToken = _tokenRepository.CreateJwtToken(user, roles.ToList());
            var newRefreshToken = _tokenRepository.GenerateRefreshToken();


            await _cache.SetAsync($"refresh_token:{user.Id}", newRefreshToken, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(7)
            });

            var response = new LoginResponseDto
            {
                Token = newJwtToken,
                RefreshToken = newRefreshToken,
                Email = user.Email,
                Roles = roles.ToList()
            };

            await _cache.SetAsync($"auth_login_response:{user.Id}", response, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(15)
            });

            return ServiceResponse<LoginResponseDto>.Success(response);
        }

        public async Task<ServiceResponse<LoginResponseDto>> RegisterAsync(RegisterDto registerDto)
        {
            var existingUser = await _userManager.FindByEmailAsync(registerDto.Email);

            if (existingUser != null)
                return ServiceResponse<LoginResponseDto>.Failure();

            var newUser = new AuthUser
            {
                UserName = registerDto.Email,
                Email = registerDto.Email
            };

            var result = await _userManager.CreateAsync(newUser, registerDto.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return ServiceResponse<LoginResponseDto>.Failure();
            }

            var defaultRole = _options.Value.DefaultRoles.FirstOrDefault(r => r == "Tenant") ?? "Tenant";

            if (!await _roleManager.RoleExistsAsync(defaultRole))
            {
                await _userManager.DeleteAsync(newUser);
                return ServiceResponse<LoginResponseDto>.Failure();
            }

            var roleResult = await _userManager.AddToRoleAsync(newUser, defaultRole);

            if (!roleResult.Succeeded)
            {
                await _userManager.DeleteAsync(newUser);
                var errors = string.Join(", ", roleResult.Errors.Select(e => e.Description));
                return ServiceResponse<LoginResponseDto>.Failure();
            }

            var roles = await _userManager.GetRolesAsync(newUser);
            var jwtToken = _tokenRepository.CreateJwtToken(newUser, roles.ToList());
            var refreshToken = _tokenRepository.GenerateRefreshToken();

            await _cache.SetAsync($"refresh_token:{newUser.Id}", refreshToken, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(7)
            });

            var response = new LoginResponseDto
            {
                Email = newUser.Email,
                Token = jwtToken,
                RefreshToken = refreshToken,
                Roles = roles.ToList()
            };

            await _cache.SetAsync($"auth_login_response:{newUser.Id}", response, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(15)
            });

            return ServiceResponse<LoginResponseDto>.Success(response);
        }
        public async Task<ServiceResponse<LogoutResponseDto>> LogoutAsync(string authHeader)
        {
            if (string.IsNullOrWhiteSpace(authHeader) || !authHeader.StartsWith("Bearer "))
            {
                return ServiceResponse<LogoutResponseDto>.Failure();
            }

            var token = authHeader["Bearer ".Length..].Trim();

            long userId;

            try
            {
                userId = _tokenRepository.GetUserIdToken(token);
            }
            catch
            {
                return ServiceResponse<LogoutResponseDto>.Failure();
            }

            await _cache.RemoveAsync($"refresh_token:{userId}");
            await _cache.RemoveAsync($"auth_login_response:{userId}");
            return ServiceResponse<LogoutResponseDto>.Success(new LogoutResponseDto
            {
            });
        }
    }
}
