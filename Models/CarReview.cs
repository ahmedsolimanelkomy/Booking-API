using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Booking_API.Models
{
    public class CarReview
    {
        [Key]
        public int Id { get; set; }

        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5")]
        public int? Rating { get; set; }

        [StringLength(1000, ErrorMessage = "Comment cannot be longer than 1000 characters")]
        public string? Comment { get; set; }

        
        public DateTime? ReviewDate { get; set; } = DateTime.Now;

        //Foreign Keys
        [ForeignKey("CarRental")]
        public int? CarRentalId { get; set; }

        [ForeignKey("Car")]
        public int? CarId { get; set; }

        [ForeignKey("ApplicationUser")]
        public int? UserId { get; set; }


        //Navigation Properties
        public CarRental? CarRental { get; set; }

        public Car? Car { get; set; }

        public ApplicationUser? ApplicationUser { get; set; }
    }
}
