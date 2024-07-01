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

        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string? Email { get; set; }

        public double? Longitude { get; set; }

        public double? Latitude { get; set; }

        // Foreign Key
        [ForeignKey("City")]
        public int? CityId { get; set; }

        [ForeignKey("CarRental")]
        public int? CarRentalId { get; set; }

        // Navigation Properties
        public City? City { get; set; }
        public CarRental? CarRental { get; set; }
        public ICollection<Car>? Cars { get; set; } = new HashSet<Car>();
        public ICollection<CarAgencyReview>? CarAgencyReviews { get; set; } = new HashSet<CarAgencyReview>();
    }
}
