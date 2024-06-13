using Booking_API.Models;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Collections.Generic;
using Booking_API.DTOs;

namespace Booking_API.Repository.IRepository
{
    public interface IHotelPhotoRepository : IRepository<HotelPhoto>
    {
        Task AddAsync(HotelPhoto photo);
        Task UpdateAsync(HotelPhoto photo);
        Task DeleteAsync(int id);
        Task<IEnumerable<HotelPhotoDTO>> GetByHotelIdAsync(int hotelId);
    }
}
