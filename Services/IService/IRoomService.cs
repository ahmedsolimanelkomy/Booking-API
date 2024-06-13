using Booking_API.DTOs.RoomDTOS;
using Booking_API.Models;

namespace Booking_API.Services.IService
{
    public interface IRoomService : IService<Room>
    {
        Task<Room> AddDTOAsync(AddRoomDTO entity);
        Task<Room> UpdateDTOAsync(AddRoomDTO entity);
    }
}
