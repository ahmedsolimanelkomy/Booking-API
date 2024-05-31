using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Booking_API.Models
{
    public class WishList
    {
        [Key]
        public int Id { get; set; }

        // Foreign keys
        [ForeignKey("User")]
        public string? UserId { get; set; }

        // Navigation Properties
        public ApplicationUser? User { get; set; }
        // m2m Hotel

    }
}
