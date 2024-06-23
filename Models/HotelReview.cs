using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Booking_API.Models
{
    public class HotelReview
    {
        [Key]
        public int Id { get; set; }

        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5")]
        public int? Rating { get; set; }

        [StringLength(1000, ErrorMessage = "Comment cannot be longer than 1000 characters")]
        public string? Comment { get; set; }

        [Required]
        public DateTime ReviewDate { get; set; } = DateTime.Now;

        //Foreign Keys
        [ForeignKey("HotelBooking")]
        public int HotelBookingId { get; set; }

        //Navigation Properties
        public HotelBooking? HotelBooking { get; set; }
    }
}
