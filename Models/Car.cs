using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Booking_API.Models
{
    public enum GearType
    {
        Manual,
        Automatic
    }
    public class Car
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        public string? Model { get; set; }

        [MaxLength(50)]
        public string? Brand { get; set; }

        [Range(0, double.MaxValue)]
        public decimal RentPrice { get; set; }

        public bool AvailabilityStatus { get; set; }

        [MaxLength(20)]
        public string? PlateNumber { get; set; }

        public bool InsuranceIncluded { get; set; }

        public GearType GearType { get; set; }

        public int NumberOfSeates { get; set; }

        // Foreign Key
        [ForeignKey("CarAgency")]
        public int AgencyId { get; set; }

        [ForeignKey("CarType")]
        public int CarTypeId { get; set; }

        // Navigation Properties
        public virtual CarAgency? CarAgency { get; set; }
        public CarType? CarType { get; set; }
        public ICollection<CarRental>? CarRentals { get; set; } = new HashSet<CarRental>();

    }
}
