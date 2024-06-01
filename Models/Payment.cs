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
        Refunded
    }

    public class Payment
    {
         [Key]
        public int Id { get; set; }

        [Required]
        public DateTime PaymentDate { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Amount must be a positive number")]
        public int Amount { get; set; }

        [Required]
        public PaymentMethod Method { get; set; }

        [Required]
        public PaymentStatus Status { get; set; }

        //fk
        public int? BookingId { get; set; }
        public int? TransactionId { get; set; }

        //Navigation proprty

        [ForeignKey("BookingId")]
        public Booking? Booking { get; set; }

        // [ForeignKey("TransactionId")]
        // public Transaction? Transaction { get; set; }
    }
}
