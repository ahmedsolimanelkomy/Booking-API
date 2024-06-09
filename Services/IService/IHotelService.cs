using Booking_API.DTOs;
using Booking_API.Models;

namespace Booking_API.Services.IService
{
    public interface IHotelService : IService<Hotel>
    {
        Task<Hotel> AddAsync(HotelDTO entity);
        Task<Hotel> UpdateAsync(HotelDTO entity);
    }
}
