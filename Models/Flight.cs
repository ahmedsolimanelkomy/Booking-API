using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Booking_API.Models
{
    public class Flight
    {
        [Key]
        public int Id { get; set; }
        public int? FlightNumber { get; set; }

        [StringLength(50)]
        public string? DepartureAirport { get; set; }

        [StringLength(50)]
        public string? ArrivalAirport { get; set; }

        [DataType(DataType.Time)]
        public DateTime? DepartureTime { get; set; }

        [DataType(DataType.Time)]
        public DateTime? ArrivalTime { get; set; }

        public int? Price { get; set; }

        public bool? AvailabilityStatus { get; set; }

        public string? Class { get; set; }

        [Range(0, 100)]
        [Display(Name = "Baggage Allowance (kg)")]
        public int? BaggageAllowance { get; set; }

        public int? StopOversNo { get; set; }

        [ForeignKey("Aireline")]
        public int? AirelineId { get; set; }

        public Aireline? Aireline { get; set; }

        // Duration ???


    }
}
