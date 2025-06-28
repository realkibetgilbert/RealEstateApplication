namespace Property.API.Domain.Domain
{
    public class Properties
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Type { get; set; }
        public Guid? DocumentId { get; set; }
        public ICollection<Unit?> Units { get; set; } = new List<Unit>();
    }
}
