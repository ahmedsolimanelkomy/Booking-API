using Booking_API.Models;
using Booking_API.Repository.IRepository;

namespace Booking_API.Repository
{
    public class HotelRepository : Repository<Hotel>, IHotelRepository
    {
        private readonly BookingContext _dbcontext;
        public HotelRepository(BookingContext dBcontext) : base(dBcontext)
        {
            _dbcontext = dBcontext;
        }
    }
}
