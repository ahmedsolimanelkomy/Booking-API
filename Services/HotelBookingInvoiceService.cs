using Booking_API.Models;
using Booking_API.Repository.IRepository;
using Booking_API.Services.IService;

namespace Booking_API.Services
{
    public class HotelBookingInvoiceService : Service<HotelBookingInvoice>, IHotelBookingInvoiceService
    {
        public HotelBookingInvoiceService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
