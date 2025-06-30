namespace Access.API.Application.DTOs.Auth
{
    public class LoginResponseDto
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public List<string> Roles { get; set; }
    }
}
