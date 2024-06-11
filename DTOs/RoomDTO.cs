using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Booking_API.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Booking_API.DTOs
{
    public class RoomDTO
    {
        public int Id { get; set; }
        public bool AvailabilityStatus { get; set; }
        public int Capacity { get; set; }
        public Models.View? View { get; set; }
        public string? HotelName { get; set; }
        public string? TypeName { get; set; }
        public decimal PricePerNight { get; set; }
    }
}
