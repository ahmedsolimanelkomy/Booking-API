using Booking_API.DTOs.HotelDTOS;
using Booking_API.DTOs.RoomDTOs;
using Booking_API.DTOs.RoomDTOS;
using Booking_API.Models;

namespace Booking_API.Services.IService
{
    public interface IRoomService : IService<Room>
    {
        Task<Room> AddDTOAsync(AddRoomDTO entity);
        Task<Room> UpdateDTOAsync(AddRoomDTO entity);

        Task<IEnumerable<FilteredRoomDTO>> GetFilteredRoomsAsync(HotelFilterDTO filter);
    }
}
