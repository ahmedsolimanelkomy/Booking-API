using Booking_API.Models;
using Booking_API.Repository.IRepository;

namespace Booking_API.Repository
{
    public class HotelPhotoRepository : Repository<HotelPhoto>, IHotelPhotoRepository
    {
        private readonly BookingContext _dbcontext;
        public HotelPhotoRepository(BookingContext dBcontext) : base(dBcontext)
        {
            _dbcontext = dBcontext;
        }
    }
}
