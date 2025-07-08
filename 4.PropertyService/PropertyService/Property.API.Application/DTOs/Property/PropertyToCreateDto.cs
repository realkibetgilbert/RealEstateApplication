using Microsoft.AspNetCore.Http;

namespace Property.API.Application.DTOs.Property
{
    public class PropertyToCreateDto
    {
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string OwnerEmail { get; set; } = string.Empty;
        public IFormFile? Image { get; set; }
    }
}
