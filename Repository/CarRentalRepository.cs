using Booking_API.Models;
using Booking_API.Repository.IRepository;

namespace Booking_API.Repository
{
    public class CarRentalRepository : Repository<CarRental>, ICarRentalRepository
    {
        private readonly BookingContext _dbcontext;
        public CarRentalRepository(BookingContext dBcontext) : base(dBcontext)
        {
            _dbcontext = dBcontext;
        }
    }
}
