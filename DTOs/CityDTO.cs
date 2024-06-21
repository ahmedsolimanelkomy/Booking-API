using System.ComponentModel.DataAnnotations;

namespace Booking_API.DTOs
{
    public class CityDTO
    {
        public int Id { get; set; }
        [Required,MaxLength(50)]
        public string? Name { get; set; }
        [Required]
        public string? Code { get; set; }
        [Required]
        public int CountryId { get; set; }
    }
}
