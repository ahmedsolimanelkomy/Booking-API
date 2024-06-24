using Booking_API.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Booking_API.DTOs
{
    public class HotelBookingDTO

    {
        public int Id { get; set; }
        [Required]
        public DateTime Date { get; set; }

        [Required]
        public BookingStatus Status { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int TotalPrice { get; set; }

        [StringLength(500)]
        public string Notes { get; set; }

        [Required]
        public DateTime CheckIn { get; set; }

        [Required]
        
        public DateTime CheckOut { get; set; }

        [Required]
        [StringLength(50)]
        public string UserFirstName { get; set; }



        [Required]
        [StringLength(50)]
        public string UserLastName { get; set; }

        [Required]
        [StringLength(100)]
        public string hotelName { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int RoomNumber { get; set; }

        public int UserId { get; set; }

        public int RoomId { get; set; }

        public int HotelId { get; set; }
    }
    
}
