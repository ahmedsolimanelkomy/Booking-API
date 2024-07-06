using AutoMapper;
using Booking_API.Models;
using Booking_API.Repository;
using Booking_API.Repository.IRepository;
using Booking_API.Services.IService;

namespace Booking_API.Services
{
    public class WishListService : Service<HotelWishList>, IWishListService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public WishListService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Hotel>> GetWishListHotelsAsync(int userId)
        {
            var wishList = await GetAsync(w => w.UserId == userId, new[] { "Hotels", "Hotels.Rooms", "Hotels.Rooms.RoomType", "Hotels.Photos", "Hotels.City" });
            return wishList?.Hotels ?? new List<Hotel>();
        }

        public async Task AddHotelToWishListAsync(int userId, int hotelId)
        {
            var wishList = await GetAsync(w => w.UserId == userId, new[] { "Hotels" });

            if (wishList == null)
            {
                wishList = new HotelWishList { UserId = userId };
                await AddAsync(wishList);
            }

            var hotel = await _unitOfWork.GetRepository<Hotel>().GetAsync(h => h.Id == hotelId);
            if (hotel != null && !wishList.Hotels.Contains(hotel))
            {
                wishList.Hotels.Add(hotel);
                await UpdateAsync(wishList);
            }
        }

        public async Task RemoveHotelFromWishListAsync(int userId, int hotelId)
        {
            var wishList = await GetAsync(w => w.UserId == userId, new[] { "Hotels" });
            if (wishList == null) return;

            var hotel = await _unitOfWork.GetRepository<Hotel>().GetAsync(h => h.Id == hotelId);
            if (hotel != null && wishList.Hotels.Contains(hotel))
            {
                wishList.Hotels.Remove(hotel);
                await UpdateAsync(wishList);
            }
        }

        public async Task<bool[]> CheckHotelsInUserWishListAsync(int userId, int[] hotelIds)
        {
            var wishList = await _unitOfWork.GetRepository<HotelWishList>()
                .GetAsync(w => w.UserId == userId, includeProperties: new string[] { "Hotels" });

            if (wishList == null)
            {
                return hotelIds.Select(id => false).ToArray();
            }

            var hotelIdSet = wishList.Hotels.Select(h => h.Id).ToHashSet();
            return hotelIds.Select(id => hotelIdSet.Contains(id)).ToArray();
        }
    }
}
