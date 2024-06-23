using Booking_API.DTOs.HotelDTOS;
using Booking_API.Models;

namespace Booking_API.Repository.IRepository
{
    public interface IHotelRepository : IRepository<Hotel>
    {
        //Task<IEnumerable<Hotel>> GetFilteredHotelsAsync(HotelFilterDTO filter);

    }
}
