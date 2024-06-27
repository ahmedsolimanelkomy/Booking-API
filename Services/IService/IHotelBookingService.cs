using System.Collections.Generic;
using System.Threading.Tasks;
using Booking_API.DTOs;
using Booking_API.DTOs.HotelBookingDTOS;
using Booking_API.Models;

namespace Booking_API.Services.IService
{
    public interface IHotelBookingService : IService<HotelBooking>
    {
        //Task<IEnumerable<HotelBookingDTO>> GetAllBookingsAsync(string[] includeProperties);
        //Task<HotelBookingDTO> GetBookingByIdAsync(int id, string[] includeProperties);
        //Task<HotelBookingDto> CreateBookingAsync(HotelBookingDto bookingDTO);
        //Task<HotelBookingDTO> UpdateBookingAsync(int id, HotelBookingDTO bookingDTO);
        //Task DeleteBookingAsync(int id);
    }
}
