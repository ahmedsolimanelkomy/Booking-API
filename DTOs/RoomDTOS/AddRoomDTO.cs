using Booking_API.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Booking_API.DTOs.RoomDTOS
{
    public class AddRoomDTO
    {

        
        public int? Id { get; set; }

        public int RoomNumber { get; set; }

        public bool AvailabilityStatus { get; set; }
        public int Capacity { get; set; }
        public View? View { get; set; }
        public bool IsBooked { get; set; }
        public int? HotelId { get; set; }
        public int? RoomTypeId { get; set; }
    }
}
