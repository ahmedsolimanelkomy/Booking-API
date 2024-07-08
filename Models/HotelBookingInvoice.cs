using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Booking_API.Models
{

    public enum PaymentMethod
    {
        CreditCard,
        DebitCard,
        PayPal,
        BankTransfer,
        Cash
    }

    public enum PaymentStatus
    {
        Pending,
        Completed,
        Failed,
        Refunded,
        Confirmed
    }
    public class HotelBookingInvoice
    {
        [Key]
        public int Id { get; set; }
        public int Number { get; set; }

        [Required]
        public DateTime Date { get; set; } = DateTime.Now;

        public decimal Amount { get; set; }

        public PaymentStatus PaymentStatus { get; set; }

        public string? TransactionId { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        //Foreign Keys
        [ForeignKey("ApplicationUser")]
        public int UserId { get; set; }

        [ForeignKey("HotelBooking")]
        public int? HotelBookingId { get; set; }


        //Navigation  Properties
        public ApplicationUser? ApplicationUser { get; set; }
        
        public HotelBooking? HotelBooking { get; set; }
    }
}
