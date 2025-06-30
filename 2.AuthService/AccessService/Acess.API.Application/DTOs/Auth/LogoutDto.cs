namespace Access.API.Application.DTOs.Auth
{
    public class LogoutResponseDto
    {
        public string Message { get; set; } = "Logged out successfully";
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }

}
