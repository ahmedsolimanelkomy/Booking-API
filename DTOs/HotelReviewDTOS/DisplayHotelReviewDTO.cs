using System.ComponentModel.DataAnnotations;

namespace Booking_API.DTOs
{
    public class DisplayHotelReviewDTO
    {
        public string UserName { get; set; }

        public string? PhotoUrl { get; set; }

        [StringLength(1000, ErrorMessage = "Comment cannot be longer than 1000 characters")]
        public string? Comment { get; set; }

    }
}
