using Booking_API.DTOs;
using Booking_API.DTOs.HotelDTOS;
using Booking_API.Models;
using System.Linq.Expressions;

namespace Booking_API.Services.IService
{
    public interface IHotelService : IService<Hotel>
    {
        Task<Hotel> AddDTOAsync(HotelDTO entity);
        Task<Hotel> UpdateDTOAsync(HotelDTO entity);
        Task<IEnumerable<FilteredHotelDTO>> GetFilteredHotelsAsync(HotelFilterDTO filter);

    }
}
