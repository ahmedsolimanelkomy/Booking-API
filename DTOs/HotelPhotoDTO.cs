using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Booking_API.DTOs
{
    public class HotelPhotoDTO
    {
        [Required, MaxLength(100)]
        public string? Name { get; set; }

        public string? Description { get; set; }

        public string? PhotoUrl { get; set; }


        public int? HotelId { get; set; }

    }
}
