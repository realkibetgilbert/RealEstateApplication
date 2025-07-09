namespace Property.API.Application.DTOs.Unit
{
    public class UnitToDisplayDto
    {
        public long Id { get; set; }
        public string UnitNumber { get; set; }
        public int Floor { get; set; }
        public string Status { get; set; }
        public decimal MonthlyRent { get; set; }
        public string OwnerEmail { get; set; }
        public long PropertyId { get; set; }
        public string? ImageBase64 { get; set; }
    }
}
