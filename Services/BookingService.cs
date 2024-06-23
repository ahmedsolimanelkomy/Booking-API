using Booking_API.Models;
using Booking_API.Repository.IRepository;
using Booking_API.Services.IService;
using System.Linq.Expressions;

namespace Booking_API.Services
{
    public class BookingService : Service<HotelBooking>, IBookingService
    {
        public BookingService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }

}
