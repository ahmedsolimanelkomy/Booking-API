using Booking_API.Models;
using Booking_API.Repository.IRepository;

namespace Booking_API.Repository
{
    public class HotelBookingInvoiceRepository : Repository<HotelBookingInvoice>, IHotelBookingInvoiceRepository
    {
        private readonly BookingContext _dbcontext;
        public HotelBookingInvoiceRepository(BookingContext dBcontext) : base(dBcontext)
        {
            _dbcontext = dBcontext;
        }
    }
}
