using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Booking_API.Models;

namespace Booking_API.DTOs
{
    public class RoomDTO
    {
        [Key]
        public int Id { get; set; }

        public bool AvailabilityStatus { get; set; }

        public int Capacity { get; set; }

        public View? View { get; set; }

        
        public int? HotelId { get; set; }

        
        public int? RoomTypeId { get; set; }
    }
}
