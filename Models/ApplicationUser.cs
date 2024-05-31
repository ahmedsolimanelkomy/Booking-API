using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Booking_API.Models
{
    public class ApplicationUser : IdentityUser
    {
        [StringLength(100)]
        public string? FullName { get; set; }

        [StringLength(10)]
        public string? Gender { get; set; }

        [StringLength(250)]
        public string? Address { get; set; }
        public string? PhotoUrl { get; set; }

        [DataType(DataType.Date)]
        public DateOnly? BirthDate { get; set; }

        // Foreign keys
        [ForeignKey("WishList")]
        public int WishListId { get; set; }
        [ForeignKey("Passport")]
        public int PassportId { get; set; }
        // public int CityId { get; set; }

        // Navigation Properties
        public WishList? WishList { get; set; }
        public Passport? Passport { get; set; }
        // public City City { get; set; } 
        // mt1 Ticket
        // 1tm Booking
    }
}
