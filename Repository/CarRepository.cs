using Booking_API.Models;
using Booking_API.Repository.IRepository;

namespace Booking_API.Repository
{
    public class CarRepository : Repository<Car>, ICarRepository
    {
        private readonly BookingContext _dbcontext;
        public CarRepository(BookingContext dBcontext) : base(dBcontext)
        {
            _dbcontext = dBcontext;
        }
    }
}
