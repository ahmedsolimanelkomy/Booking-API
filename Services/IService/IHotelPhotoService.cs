using Booking_API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Booking_API.Services.IService
{
    public interface IHotelPhotoService : IService<HotelPhoto>
    {
        Task<IEnumerable<HotelPhoto>> GetPhotosByHotelId(int hotelId);
    }
}
