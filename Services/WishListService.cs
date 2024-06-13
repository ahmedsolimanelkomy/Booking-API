using Booking_API.Models;
using Booking_API.Repository.IRepository;
using Booking_API.Services.IService;

namespace Booking_API.Services
{
    public class WishListService : Service<WishList>, IWishListService
    {
        public WishListService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
