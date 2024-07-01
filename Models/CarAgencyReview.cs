using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Booking_API.Models
{
    public class CarAgencyReview
    {
        [Key]
        public int Id { get; set; }

        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5")]
        public int? Rating { get; set; }

        [StringLength(1000, ErrorMessage = "Comment cannot be longer than 1000 characters")]
        public string? Comment { get; set; }

        [Required]
        public DateTime ReviewDate { get; set; } = DateTime.Now;
        [ForeignKey("CarAgency")]
        public int CarAgencyId { get; set; }
        [ForeignKey("User")]
        public int ApplicationUserID { get; set; }

        public CarAgency CarAgency { get; set; }
        public ApplicationUser User { get; set; }
    }
}