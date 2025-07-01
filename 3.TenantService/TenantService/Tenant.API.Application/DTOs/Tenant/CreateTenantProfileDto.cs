namespace Tenant.API.Application.DTOs.Tenant
{
    public class CreateTenantProfileDto
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string IdNumber { get; set; }
        public string PreferredLocation { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
    }
}
