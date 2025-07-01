namespace Tenant.API.Domain.Entities
{
    public class TenantProfile
    {
        public  long Id { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string IdNumber { get; set; }
        public string PreferredLocation { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
