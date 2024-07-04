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

        public int Amount { get; set; }

        public PaymentStatus paymentStatus { get; set; }

        public int TransactionId { get; set; }

        public PaymentMethod paymentMethod { get; set; }

        //Foreign Keys

        [ForeignKey("ApplicationUser")]
        public int UserId { get; set; }

        [ForeignKey("CarRental")]
        public int CarRentalId { get; set; }

        //Navigation  Properties
        public ApplicationUser? ApplicationUser { get; set; }

        public CarRental? CarRental { get; set; }
    }
}
