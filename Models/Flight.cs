using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Booking_API.Models
{
    public enum Class
    {
        FirstClass,
        Economy,
        Business
    }
    public class Flight
    {
        [Key]
        public int Id { get; set; }

        public int? FlightNumber { get; set; }

        [StringLength(50)]
        public string? DepartureAirport { get; set; }

        [StringLength(50)]
        public string? ArrivalAirport { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DepartureTime { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? ArrivalTime { get; set; }
        public Class? Class { get; set; }

        [Range(0, 100)]
        [Display(Name = "Baggage Allowance (kg)")]
        public int? BaggageAllowance { get; set; }

        public int? StopOversNo { get; set; }

        [DataType(DataType.Duration)]
        public int Duration { get; set; }

        // Foreign Keys
        [ForeignKey("Airline")]
        public int? AirlineId { get; set; }

        // Navigation Properties
        public Airline? Airline { get; set; }
        public ICollection<Booking>? Bookings { get; set; } = new HashSet<Booking>();


    }
}
