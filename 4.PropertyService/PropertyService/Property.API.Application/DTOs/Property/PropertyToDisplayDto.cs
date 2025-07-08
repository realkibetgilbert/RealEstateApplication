namespace Property.API.Application.DTOs.Property
{
    public class PropertyToDisplayDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Type { get; set; }
        public string OwnerEmail { get; set; }
        public string? ImageBase64 { get; set; }
    }
}
