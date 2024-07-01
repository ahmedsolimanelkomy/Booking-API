using Booking_API.Models;
using Booking_API.Repository.IRepository;

namespace Booking_API.Repository
{
    public class CarRentalInvoiceRepository : Repository<CarRentalInvoice>, ICarRentalInvoiceRepository
    {
        private readonly BookingContext _dbcontext;
        public CarRentalInvoiceRepository(BookingContext dBcontext) : base(dBcontext)
        {
            _dbcontext = dBcontext;
        }
    }
}
