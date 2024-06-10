using Booking_API.DTOs;
using Booking_API.Models;

namespace Booking_API.Services.IService
{
    public interface IRoomService : IService<Room>
    {
        Task<Room> AddDTOAsync(RoomDTO entity);
        Task<Room> UpdateDTOAsync(RoomDTO entity);
    }
}
