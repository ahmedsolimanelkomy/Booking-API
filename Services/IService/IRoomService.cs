using Booking_API.DTOs.RoomDTOS;
using Booking_API.Models;

namespace Booking_API.Services.IService
{
    public interface IRoomService : IService<Room>
    {
        Task<Room> AddDTOAsync(RoomViewDTO entity);
        Task<Room> UpdateDTOAsync(RoomViewDTO entity);
    }
}
