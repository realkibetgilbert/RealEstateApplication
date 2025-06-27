namespace Access.API.Infrastructure.Data.Seed.Models
{
    public class SeederSettings
    {
        public string[] DefaultRoles { get; set; }
        public List<SeederUser> DefaultUsers { get; set; }
    }
}
