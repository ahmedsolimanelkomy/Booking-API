using System.Collections.Generic;
using System.Threading.Tasks;
using Booking_API.Models;

namespace Booking_API.Repository.IRepository
{
    public interface IHotelBookingRepository
    {
        Task<IEnumerable<HotelBooking>> GetAllBookingsAsync();
        Task<HotelBooking> GetBookingByIdAsync(int id);
        Task<HotelBooking> CreateBookingAsync(HotelBooking booking);
        Task<HotelBooking> UpdateBookingAsync(int id, HotelBooking booking);
        Task DeleteBookingAsync(int id);
    }
}
