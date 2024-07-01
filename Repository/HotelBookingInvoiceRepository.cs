using Booking_API.Models;
using Booking_API.Repository.IRepository;

namespace Booking_API.Repository
{
    public class HotelBookingInvoiceRepository : Repository<HotelBookingInvoice>, IhotelBookingInvoiceRepository
    {
        public HotelBookingInvoiceRepository(BookingContext context) : base(context)
        {
        }
    }
}
