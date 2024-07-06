using AutoMapper;
using Booking_API.DTOs;
using Booking_API.DTOs.HotelDTOS;
using Booking_API.Models;
using Booking_API.Services;
using Booking_API.Services.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Booking_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishListController : ControllerBase
    {

        private readonly IWishListService wishListService;
        private readonly IMapper mapper;

        public WishListController(IWishListService wishListService, IMapper mapper)
        {
            this.wishListService = wishListService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<GeneralResponse<IEnumerable<HotelWishList>>>> GetWishLists([FromQuery] string[] includeProperties)
        {
            var response = await wishListService.GetAllAsync(includeProperties);
            return Ok(new GeneralResponse<IEnumerable<HotelWishList>>(true, "WishLists retrieved successfully", response));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GeneralResponse<HotelWishList>>> GetWishList(int id, [FromQuery] string[] includeProperties)
        {
            var response = await wishListService.GetAsync(b => b.Id == id, includeProperties);
            if (response == null)
            {
                return NotFound(new GeneralResponse<HotelWishList>(false, "WishList not found", null));
            }
            return Ok(new GeneralResponse<HotelWishList>(true, "WishList retrieved successfully", response));
        }

        [HttpPost]
        public async Task<ActionResult<GeneralResponse<HotelWishList>>> PostWishList(HotelWishListDTO wishListDTO)
        {
            // Map DTO to entity model
            var wishList = mapper.Map<HotelWishList>(wishListDTO);

            // Save the new wishlist
            await wishListService.AddAsync(wishList);

            // Return the created wishlist
            return CreatedAtAction(nameof(GetWishList), new { id = wishList.Id }, new GeneralResponse<HotelWishList>(true, "WishList added successfully", wishList));
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<GeneralResponse<HotelWishList>>> PutWishList(int id, HotelWishList WishList)
        {
            if (id != WishList.Id)
            {
                return BadRequest(new GeneralResponse<HotelWishList>(false, "WishList ID mismatch", null));
            }

            await wishListService.UpdateAsync(WishList);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<GeneralResponse<HotelWishList>>> DeleteWishList(int id)
        {
            var existingWishList = await wishListService.GetAsync(b => b.Id == id);
            if (existingWishList == null)
            {
                return NotFound(new GeneralResponse<HotelWishList>(false, "WishList not found", null));
            }

            await wishListService.DeleteAsync(id);
            return Ok(new GeneralResponse<HotelWishList>(true, "WishList deleted successfully", existingWishList));
        }


        [HttpGet("user/{userId}")]
        public async Task<ActionResult<GeneralResponse<ICollection<WishlistHotelDTO>>>> GetWishListHotels(int userId)
        {
            var hotels = await wishListService.GetWishListHotelsAsync(userId);
            var wishlistHotelDTOs = mapper.Map<ICollection<WishlistHotelDTO>>(hotels);

            return Ok(new GeneralResponse<ICollection<WishlistHotelDTO>>(true, "WishList retrieved successfully", wishlistHotelDTOs));
        }

        [HttpPost("user/{userId}/{hotelId}")]
        public async Task<ActionResult<GeneralResponse<string>>> AddHotelToWishList(int userId, int hotelId)
        {
            await wishListService.AddHotelToWishListAsync(userId, hotelId);
            return Ok(new GeneralResponse<string>(true, "Hotel added to wishlist successfully", null));
        }

        [HttpDelete("user/{userId}/{hotelId}")]
        public async Task<ActionResult<GeneralResponse<string>>> RemoveHotelFromWishList(int userId, int hotelId)
        {
            await wishListService.RemoveHotelFromWishListAsync(userId, hotelId);
            return Ok(new GeneralResponse<string>(true, "Hotel removed from wishlist successfully", null));
        }


        [HttpPost("user/{userId}")]
        public async Task<ActionResult<GeneralResponse<bool[]>>> CheckHotelsInUserWishList(int userId, [FromBody] int[] hotelIds)
        {
            var results = await wishListService.CheckHotelsInUserWishListAsync(userId, hotelIds);
            return Ok(new GeneralResponse<bool[]>(true, "Check completed successfully", results));
        }


    }
}
