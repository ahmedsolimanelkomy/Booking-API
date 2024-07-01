using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Booking_API.Models
{
    public class CarRentalInvoice
    {
        [Key]
        public int Id { get; set; }
        public int Number { get; set; }
        public DateTime Data { get; set; }
        public decimal Amount { get; set; }
        public InvoiceStatus Status { get; set; }
        public string PaymentMethod { get; set; }

        [ForeignKey("CarRental")]
        public int CarRentalId { get; set; }
        [ForeignKey("User")]
        public int ApplicationUserID { get; set; }

        public CarRental CarRental { get; set; }
        public ApplicationUser User { get; set; }
    }
}
