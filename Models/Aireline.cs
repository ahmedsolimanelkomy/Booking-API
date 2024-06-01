using System.ComponentModel.DataAnnotations;

namespace Booking_API.Models
{
    public class Aireline
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string? Name { get; set; }

        public string? LogoUrl { get; set; }

        
        [EmailAddress]
        public string? Email { get; set; }

        public string? WebsiteURL { get; set; }

        public int? PhoneNumber { get; set; }

        // CountryNumber ????

        // mt1 City
    }
}
