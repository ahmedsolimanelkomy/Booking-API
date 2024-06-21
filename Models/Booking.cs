using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

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

        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime BookingDate { get; set; } = DateTime.Now;

        [Required]
        public BookingStatus Status { get; set; } = BookingStatus.Pending;

        [Required]
        [Range(0, double.MaxValue), DataType(DataType.Currency, ErrorMessage = "Total Price must be a positive number")]
        public decimal TotalPrice { get; set; }

        //Foreign Keys
        [ForeignKey("ApplicationUser")]
        public int? UserId { get; set; }

        [ForeignKey("Car")]
        public int? CarId { get; set; }

        [ForeignKey("Room")]
        public int? RoomId { get; set; }

        [ForeignKey("Flight")]
        public int? FlightId { get; set; }

        //Navigation  Properties
        public ApplicationUser? ApplicationUser { get; set; }
        public Room? Room { get; set; }
        public Car? Car { get; set; }
        public Review? ReviewList { get; set; }
        public Payment? PaymentList { get; set; }
    }
}
