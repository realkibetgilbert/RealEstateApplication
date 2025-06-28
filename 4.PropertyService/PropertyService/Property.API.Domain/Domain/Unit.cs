namespace Property.API.Domain.Domain
{
    public class Unit
    {
        public long Id { get; set; }
        public decimal Price { get; set; }
        public string Type { get; set; }
        public Guid? DocumentId { get; set; }
        public long PropertyId { get; set; }
        public Properties Property { get; set; }
    }
}
