namespace Access.API.Domain.Entities
{
    public class RegisteredApplication
    {
        public long Id { get; set; }
        public Guid ApplicationId { get; set; }
        public string ApplicationName { get; set; }
    }
}
