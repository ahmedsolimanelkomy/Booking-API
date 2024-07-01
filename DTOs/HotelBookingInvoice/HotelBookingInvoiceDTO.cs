using Booking_API.Models;

namespace Booking_API.DTOs.HotelBookingInvoice
{
    public class HotelBookingInvoiceDTO
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public InvoiceStatus Status { get; set; }
        public string PaymentMethod { get; set; }
        public string TransactionId { get; set; }
        public int HotelBookingId { get; set; }
        public int ApplicationUserId { get; set; }
    }
}
