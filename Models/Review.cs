using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Booking_API.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5")]
        public int Rating { get; set; }

        [StringLength(1000, ErrorMessage = "Comment cannot be longer than 1000 characters")]
        public string Comment { get; set; }

        [Required]
        public DateTime ReviewDate { get; set; }
        //fk
        public int? BookingId { get; set; }

        //Navigation proprty

        [ForeignKey("BookingId")]
        public Booking? Booking { get; set; }
    }
}
