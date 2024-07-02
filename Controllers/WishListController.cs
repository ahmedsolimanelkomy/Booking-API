using AutoMapper;
using Booking_API.DTOs;
using Booking_API.Models;
using Booking_API.Services.IService;
using Microsoft.AspNetCore.Mvc;

namespace Booking_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishListController : ControllerBase
    {
        private readonly IWishListService wishListService;
        private readonly IMapper _mapper;

        public WishListController(IWishListService wishListService, IMapper mapper)
        {
            this.wishListService = wishListService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<GeneralResponse<IEnumerable<HotelWishListDTO>>>> GetWishLists([FromQuery] string[] includeProperties = null)
        {
            var wishLists = await wishListService.GetAllAsync(includeProperties);
            var wishListDTOs = _mapper.Map<IEnumerable<HotelWishListDTO>>(wishLists);
            return Ok(new GeneralResponse<IEnumerable<HotelWishListDTO>>(true, "WishLists retrieved successfully", wishListDTOs));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GeneralResponse<HotelWishListDTO>>> GetWishList(int id, [FromQuery] string[] includeProperties = null)
        {
            var wishList = await wishListService.GetAsync(b => b.Id == id, includeProperties);
            if (wishList == null)
            {
                return NotFound(new GeneralResponse<HotelWishListDTO>(false, "WishList not found", null));
            }
            var wishListDTO = _mapper.Map<HotelWishListDTO>(wishList);
            return Ok(new GeneralResponse<HotelWishListDTO>(true, "WishList retrieved successfully", wishListDTO));
        }

        [HttpPost]
        public async Task<ActionResult<GeneralResponse<HotelWishListDTO>>> PostWishList(HotelWishListDTO wishListDTO)
        {
            var wishList = _mapper.Map<HotelWishList>(wishListDTO);
            await wishListService.AddAsync(wishList);
            var createdWishListDTO = _mapper.Map<HotelWishListDTO>(wishList);
            return CreatedAtAction(nameof(GetWishList), new { id = wishList.Id }, new GeneralResponse<HotelWishListDTO>(true, "WishList added successfully", createdWishListDTO));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GeneralResponse<HotelWishListDTO>>> PutWishList(int id, HotelWishListDTO wishListDTO)
        {
            if (id != wishListDTO.Id)
            {
                return BadRequest(new GeneralResponse<HotelWishListDTO>(false, "WishList ID mismatch", null));
            }

            var wishList = _mapper.Map<HotelWishList>(wishListDTO);
            await wishListService.UpdateAsync(wishList);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<GeneralResponse<HotelWishListDTO>>> DeleteWishList(int id)
        {
            var existingWishList = await wishListService.GetAsync(b => b.Id == id);
            if (existingWishList == null)
            {
                return NotFound(new GeneralResponse<HotelWishListDTO>(false, "WishList not found", null));
            }

            await wishListService.DeleteAsync(id);
            var deletedWishListDTO = _mapper.Map<HotelWishListDTO>(existingWishList);
            return Ok(new GeneralResponse<HotelWishListDTO>(true, "WishList deleted successfully", deletedWishListDTO));
        }
    }
}
