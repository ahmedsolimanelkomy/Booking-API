using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Booking_API.Models
{
    public class CarRentalInvoice
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

        [ForeignKey("CarRental")]
        public int? CarRentalId { get; set; }

        //Navigation  Properties
        public ApplicationUser? ApplicationUser { get; set; }

        public CarRental? CarRental { get; set; }
    }
}
