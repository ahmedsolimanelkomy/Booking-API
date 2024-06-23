using Booking_API.DTOs.HotelDTOS;
using Booking_API.Models;
using Booking_API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Booking_API.Repository
{
    public class HotelRepository : Repository<Hotel>, IHotelRepository
    {
        private readonly BookingContext _dbcontext;
        public HotelRepository(BookingContext dBcontext) : base(dBcontext)
        {
            _dbcontext = dBcontext;
        }

        //public async Task<IEnumerable<Hotel>> GetFilteredHotelsAsync(HotelFilterDTO filter)
        //{
        //    var query = _dbcontext.Hotels
        //        .Include(h => h.Rooms).ThenInclude(r => r.HotelBooking)
        //        .Include(h => h.Rooms).ThenInclude(r => r.RoomType)
        //        .Include(h => h.Features)
        //        .Include(h => h.Photos)
        //        .AsQueryable();

        //    if (filter.CityId.HasValue)
        //    {
        //        query = query.Where(h => h.CityId == filter.CityId.Value);
        //    }

        //    if (filter.CheckInDate.HasValue && filter.CheckOutDate.HasValue)
        //    {
        //        query = query.Where(h => h.Rooms.Any(r => r.HotelBooking == null || 
        //                            r.HotelBooking.CheckOutDate <= filter.CheckInDate || 
        //                            r.HotelBooking.CheckInDate >= filter.CheckOutDate)
        //        );
        //    }

        //    if (filter.MinPrice.HasValue)
        //    {
        //        query = query.Where(h => h.Rooms.Any(r => r.RoomType.PricePerNight >= filter.MinPrice.Value));
        //    }

        //    if (filter.MaxPrice.HasValue)
        //    {
        //        query = query.Where(h => h.Rooms.Any(r => r.RoomType.PricePerNight <= filter.MaxPrice.Value));
        //    }

        //    if (filter.RoomView.HasValue)
        //    {
        //        query = query.Where(h => h.Rooms.Any(r => r.View == filter.RoomView));
        //    }

        //    if (filter.FeatureIds != null && filter.FeatureIds.Any())
        //    {
        //        query = query.Where(h => filter.FeatureIds.All(id => h.Features.Any(f => f.Id == id)));
        //    }

        //    if (filter.RoomTypeId.HasValue)
        //    {
        //        query = query.Where(h => h.Rooms.Any(r => r.RoomTypeId == filter.RoomTypeId.Value));
        //    }

        //    return await query.ToListAsync();
        //}
    }
}
