using System.Collections.Generic;
using System.Threading.Tasks;
using Booking_API.DTOs;

namespace Booking_API.Services.IService
{
    public interface IHotelBookingService
    {
        Task<IEnumerable<HotelBookingDTO>> GetAllBookingsAsync();
        Task<HotelBookingDTO> GetBookingByIdAsync(int id);
        Task<HotelBookingDTO> CreateBookingAsync(HotelBookingDTO bookingDTO);
        Task<HotelBookingDTO> UpdateBookingAsync(int id, HotelBookingDTO bookingDTO);
        Task DeleteBookingAsync(int id);
    }
}
