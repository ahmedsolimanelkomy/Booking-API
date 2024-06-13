using Booking_API.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Booking_API.DTOs.RoomDTOS
{
    public class AddRoomDTO
    {
<<<<<<< HEAD:DTOs/RoomDTO.cs
        
        public int? Id { get; set; }

=======
        public int Id { get; set; }
>>>>>>> 09a3c385fd57a783794dd72847543597556a32cb:DTOs/RoomDTOS/AddRoomDTO.cs
        public bool AvailabilityStatus { get; set; }
        public int Capacity { get; set; }
        public View? View { get; set; }
        public bool IsBooked { get; set; }
        public int? HotelId { get; set; }
        public int? RoomTypeId { get; set; }
    }
}
