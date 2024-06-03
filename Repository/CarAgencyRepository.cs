using Booking_API.Models;
using Booking_API.Repository.IRepository;

namespace Booking_API.Repository
{
    public class CarAgencyRepository : Repository<CarAgency>, ICarAgencyRepository
    {
        private readonly BookingContext _dbcontext;
        public CarAgencyRepository(BookingContext dBcontext) : base(dBcontext)
        {
            _dbcontext = dBcontext;
        }
    }
}
