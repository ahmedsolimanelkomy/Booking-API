using Booking_API.Models;
using System.ComponentModel.DataAnnotations;

namespace Booking_API.DTOs.InvoiceDTOS
{
    public class ViewInvoiceDTO
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
    }
}
