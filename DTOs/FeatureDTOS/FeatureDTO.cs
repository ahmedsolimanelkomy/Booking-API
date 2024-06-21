using System.ComponentModel.DataAnnotations;

namespace Booking_API.DTOs.FeatureDTOS
{
    public class FeatureDTO
    {
        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string? Name { get; set; }
    }
}
