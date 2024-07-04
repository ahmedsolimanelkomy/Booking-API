using Booking_API.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Booking_API.DTOs
{
    public class HotelBookingInvoiceDTO
    {
        public int Id { get; set; }
        public int Number { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        public int Amount { get; set; }

        public PaymentStatus paymentStatus { get; set; }

        public int TransactionId { get; set; }

        public PaymentMethod paymentMethod { get; set; }

        //Foreign Keys
        
        public int UserId { get; set; }

        public int HotelBookingId { get; set; }


        


    }
}
