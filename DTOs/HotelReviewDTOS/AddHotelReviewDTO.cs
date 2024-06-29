using System.ComponentModel.DataAnnotations;

namespace Booking_API.DTOs
{
    public class AddHotelReviewDTO
    {
        public int UserId { get; set; }

        [StringLength(1000, ErrorMessage = "Comment cannot be longer than 1000 characters")]
        public string? Comment { get; set; }

        public int? HotelId { get; set; }

        [Required]
        public DateTime ReviewDate { get; set; } = DateTime.Now;

    }
}
