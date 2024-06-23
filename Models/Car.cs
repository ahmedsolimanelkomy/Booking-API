using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Booking_API.Models
{
    public class Car
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        public string? Model { get; set; }

        [MaxLength(50)]
        public string? Type { get; set; }

        [Range(0, double.MaxValue)]
        public decimal RentPrice { get; set; }

        public bool AvailabilityStatus { get; set; }

        [MaxLength(100)]
        public string? PickupLocation { get; set; }

        [MaxLength(100)]
        public string? DropOffLocation { get; set; }

        public DateTime PickupDate { get; set; }

        public DateTime DropOffDate { get; set; }

        [MaxLength(20)]
        public string? PlateNumber { get; set; }
        public bool InsuranceIncluded { get; set; }

        public bool IsBooked { get; set; }

        [MaxLength(30)]
        public string? Color { get; set; }


        // Foreign Key
        [ForeignKey("CarAgency")]
        public int AgencyId { get; set; }

        // Navigation Properties
        public virtual CarAgency? CarAgency { get; set; }
        public virtual ICollection<HotelBooking>? Bookings { get; set; } = new HashSet<HotelBooking>();

    }
}
