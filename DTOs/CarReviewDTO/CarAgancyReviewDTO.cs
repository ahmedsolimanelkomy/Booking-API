using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Booking_API.DTOs.CarReviewDTO
{
    public class CarAgancyReviewDTO
    {
        

        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5")]
        public int? Rating { get; set; }

        [StringLength(1000, ErrorMessage = "Comment cannot be longer than 1000 characters")]
        public string? Comment { get; set; }


        public DateTime? ReviewDate { get; set; } = DateTime.Now;

        
      
        public int? CarRentalId { get; set; }

       
        public int? CarAgencyId { get; set; }

        public int? UserId { get; set; }
    }
}
