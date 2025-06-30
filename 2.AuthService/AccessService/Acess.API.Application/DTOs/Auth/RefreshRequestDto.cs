namespace Access.API.Application.DTOs.Auth
{
    public class RefreshRequestDto
    {
        public string Email { get; set; } = default!;
        public string RefreshToken { get; set; } = default!;
    }
}
