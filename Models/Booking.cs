using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Booking_API.Models
{
    public enum BookingStatus
    {
        Pending,
        Confirmed,
        Cancelled,
        Completed
    }

    public class Booking
    {

        // Constructor
        public Booking()
        {
            BookingDate = DateTime.Now;
            Status = BookingStatus.Pending;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime BookingDate { get; set; }

        [Required]
        public BookingStatus Status { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Total Price must be a positive number")]
        public int TotalPrice { get; set; }

        //FK
        public string? UserId { get; set; }
        public int? RoomId { get; set; }
        public int? FlightId { get; set; }
        public int? CarRentalId { get; set; }

        //Navigation  proprty
        [ForeignKey("UserId")]
        public ApplicationUser? User { get; set; }

        public ICollection<Review>? ReviewList { get; set; }
        public ICollection<Payment>? PaymentList { get; set; }

        //the next lines are commented until the models are done 

        //[ForeignKey("RoomId")]
        //public Room Room { get; set; }

        //[ForeignKey("FlightId")]
        //public Flight Flight { get; set; }

        //[ForeignKey("CarRentalId")]
        //public CarRental CarRental { get; set; }

        //Extra QA //add Duration prop
    }
}
