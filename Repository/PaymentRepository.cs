using Booking_API.Models;
using Booking_API.Repository.IRepository;

namespace Booking_API.Repository
{
    public class PaymentRepository : Repository<Payment>, IPaymentRepository
    {
        private readonly BookingContext _dbcontext;
        public PaymentRepository(BookingContext dBcontext) : base(dBcontext)
        {
            _dbcontext = dBcontext;
        }
    }
}
