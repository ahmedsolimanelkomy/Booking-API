using Booking_API.Models;
using Booking_API.Repository.IRepository;

namespace Booking_API.Repository
{
    public class RoomRepository : Repository<Room>, IRoomRepository
    {
        private readonly BookingContext _dbcontext;
        public RoomRepository(BookingContext dBcontext) : base(dBcontext)
        {
            _dbcontext = dBcontext;
        }
    }
}
