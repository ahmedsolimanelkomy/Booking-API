using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Booking_API.Models
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }
        public int? SeatNumber { get; set; }
        public int? Price { get; set; }
        public bool? AvailabilityStatus { get; set; }

        //Foriegn Keys

        [ForeignKey("User")]
        public string? UserId { get; set; }
        [ForeignKey("Flight")]
        public int? FlightId { get; set; }

        //Navigation Properties
        public ApplicationUser? User { get; set; }
        public Flight? Flight { get; set; }
    }
}
