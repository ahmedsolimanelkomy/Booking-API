using Booking_API.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Booking_API.DTOs.HotelBookingDTOS
{
    public class HotelBookingViewDTO

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
        public string? Notes { get; set; } = " ";

        [Required]
        public DateTime CheckIn { get; set; }

        [Required]
        public DateTime CheckOut { get; set; }

        [Required]
        [StringLength(50)]
        public string? UserFirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string? UserLastName { get; set; }

        [Required]
        [StringLength(100)]
        public string? HotelName { get; set; }

        [Required]
        public List<int> RoomNumbers { get; set; } = new List<int>();

        public int UserId { get; set; }

        public int HotelId { get; set; }
    }

}
