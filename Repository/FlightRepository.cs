using Booking_API.Models;
using Booking_API.Repository.IRepository;

namespace Booking_API.Repository
{
    public class FlightRepository : Repository<Flight>, IFlightRepository
    {
        private readonly BookingContext _dbcontext;
        public FlightRepository(BookingContext dBcontext) : base(dBcontext)
        {
            _dbcontext = dBcontext;
        }
    }
}
