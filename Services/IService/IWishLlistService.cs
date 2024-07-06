using Booking_API.Models;

namespace Booking_API.Services.IService
{
    public interface IWishListService : IService<HotelWishList>
    {
        public Task<IEnumerable<Hotel>> GetWishListHotelsAsync(int userId);
        public Task AddHotelToWishListAsync(int userId, int hotelId);

        public Task RemoveHotelFromWishListAsync(int userId, int hotelId);
        public Task<bool[]> CheckHotelsInUserWishListAsync(int userId, int[] hotelIds);
    }
}
