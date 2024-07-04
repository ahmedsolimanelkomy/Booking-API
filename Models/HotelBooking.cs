using Booking_API.Validations;
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

    public class HotelBooking
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
        public string? Notes { get; set; }
        [CheckInDate]
        public DateTime CheckInDate { get; set; }
        [CheckOutDate("CheckInDate", ErrorMessage = "Check-out date must be after the check-in date.")]
        public DateTime CheckOutDate { get; set; }

        //Foreign Keys
        [ForeignKey("ApplicationUser")]
        public int UserId { get; set; }

        [ForeignKey("Room")]
        public int RoomId { get; set; }
        [ForeignKey("Hotel")]
        public int HotelId { get; set; }

        //Navigation  Properties
        public ApplicationUser? ApplicationUser { get; set; }
        public ICollection<Room>? Rooms { get; set; } = new HashSet<Room>();
        public Hotel? Hotel { get; set; }
        public HotelReview? ReviewList { get; set; }
        public HotelBookingInvoice? HotelBookingInvoice { get; set; }
    }
}
