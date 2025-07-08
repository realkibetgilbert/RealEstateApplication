using Microsoft.AspNetCore.Http;

namespace Property.API.Application.DTOs.Property
{
    public class PropertyToUpdateDto
    {
        public long Id { get; set; }
        public string Name { get; set; } 
        public string Location { get; set; } 
        public string Type { get; set; }
        public string OwnerEmail { get; set; } 
        public IFormFile? Image { get; set; }
    }
}
