using System.ComponentModel.DataAnnotations;

namespace Access.API.Application.DTOs.Auth
{
    public class UserRegistrationDto
    {
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string[] Roles { get; set; }
    }
}
