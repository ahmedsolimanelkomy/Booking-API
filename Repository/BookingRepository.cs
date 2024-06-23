using Booking_API.Models;
using Booking_API.Repository.IRepository;

namespace Booking_API.Repository
{
    public class BookingRepository : Repository<HotelBooking>, IBookingRepository
    {
        private readonly BookingContext _dbcontext;
        public BookingRepository(BookingContext dBcontext) : base(dBcontext)
        {
            _dbcontext = dBcontext;
        }
    }
}
