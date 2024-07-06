using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Booking_API.Models
{
    public enum GearType
    {
        Manual,
        Automatic
    }
    public enum GasType
    {
        Diesel,
        BioDiesel,
        Octane95,
    }

    public class Car
    {
        [Key]
        public int Id { get; set; }

        [Range(1886, 2100, ErrorMessage = "Year must be between 1886 and 2100")]
        public int? ModelOfYear { get; set; }

        [MaxLength(50)]
        public string? Brand { get; set; }

        [Range(0, 5)]
        public int? Rating { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "RentPrice must be a positive value")]
        public decimal? RentPrice { get; set; }

        public bool? AvailabilityStatus { get; set; }

        [MaxLength(20)]
        public string? PlateNumber { get; set; }

        public bool? InsuranceIncluded { get; set; }

        public string? Description { get; set; }

        public GearType? GearType { get; set; }

        public GasType? GasType { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "NumberOfSeats must be at least 1")]
        public int? NumberOfSeats { get; set; }

        // Foreign Key
        [ForeignKey("CarAgency")]
        public int? AgencyId { get; set; }

        [ForeignKey("CarType")]
        public int? CarTypeId { get; set; }

        // Navigation Properties
        public CarAgency? CarAgency { get; set; }
        public CarType? CarType { get; set; }
        public ICollection<CarRental>? CarRentals { get; set; } = new HashSet<CarRental>();
        public ICollection<CarReview>? CarReviews { get; set; } = new HashSet<CarReview>();
        public ICollection<CarPhoto>? CarPhotos { get; set; } = new HashSet<CarPhoto>();

    }
}
