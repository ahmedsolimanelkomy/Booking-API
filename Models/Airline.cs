using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Booking_API.Models
{
    public class Airline
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string? Name { get; set; }

        [Url]
        public string? LogoUrl { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [Url]
        public string? WebsiteURL { get; set; }

        [Phone]
        public string? PhoneNumber { get; set; }

        // Foreign Keys
        [ForeignKey("City")]
        public int CityId { get; set; }

        // Navigation Properties
        public City? City { get; set; }

    }
}
