using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Booking_API.Models
{
    public class CarAgency
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(100)]
        public string? Name { get; set; }

        [MaxLength(200)]
        public string? Address { get; set; }

        [Url]
        public string? LogoURL { get; set; }

        [Phone]
        public string? PhoneNumber { get; set; }

        [Url]
        public string? WebsiteURL { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        // Foreign Key
        [ForeignKey("City")]
        public int CityId { get; set; }

        // Navigation Properties
        public City? City { get; set; }
        public ICollection<Car>? Cars { get; set; } = new HashSet<Car>();
    }
}
