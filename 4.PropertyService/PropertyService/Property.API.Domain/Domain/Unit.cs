namespace Property.API.Domain.Domain
{
    public class Unit
    {
        public long Id { get; set; }
        public string UnitNumber { get; set; }
        public int Floor { get; set; }
        public string Status { get; set; }
        public decimal MonthlyRent { get; set; }
        public string OwnerEmail { get; set; }
        public long PropertyId { get; set; }
        public Properties Property { get; set; }
        public byte[]? Image { get; set; }
    }
}
