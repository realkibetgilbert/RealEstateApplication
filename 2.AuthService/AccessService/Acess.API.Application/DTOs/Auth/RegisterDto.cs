namespace Access.API.Application.DTOs.Auth
{
    public class RegisterDto
    {
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}
