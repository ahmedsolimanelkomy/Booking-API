using Booking_API.DTOs;
using Booking_API.Models;
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

        public WishListController(IWishListService wishListService)
        {
            this.wishListService = wishListService;
        }

        [HttpGet]
        public async Task<ActionResult<GeneralResponse<IEnumerable<WishList>>>> GetWishLists([FromQuery] string[] includeProperties)
        {
            var response = await wishListService.GetAllAsync(includeProperties);
            return Ok(new GeneralResponse<IEnumerable<WishList>>(true, "WishLists retrieved successfully", response));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GeneralResponse<WishList>>> GetWishList(int id, [FromQuery] string[] includeProperties)
        {
            var response = await wishListService.GetAsync(b => b.Id == id, includeProperties);
            if (response == null)
            {
                return NotFound(new GeneralResponse<WishList>(false, "WishList not found", null));
            }
            return Ok(new GeneralResponse<WishList>(true, "WishList retrieved successfully", response));
        }

        [HttpPost]
        public async Task<ActionResult<GeneralResponse<WishList>>> PostWishList(WishList WishList)
        {
            await wishListService.AddAsync(WishList);
            return CreatedAtAction(nameof(GetWishList), new { id = WishList.Id }, new GeneralResponse<WishList>(true, "WishList added successfully", WishList));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GeneralResponse<WishList>>> PutWishList(int id, WishList WishList)
        {
            if (id != WishList.Id)
            {
                return BadRequest(new GeneralResponse<WishList>(false, "WishList ID mismatch", null));
            }

            await wishListService.UpdateAsync(WishList);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<GeneralResponse<WishList>>> DeleteWishList(int id)
        {
            var existingWishList = await wishListService.GetAsync(b => b.Id == id);
            if (existingWishList == null)
            {
                return NotFound(new GeneralResponse<WishList>(false, "WishList not found", null));
            }

            await wishListService.DeleteAsync(id);
            return Ok(new GeneralResponse<WishList>(true, "WishList deleted successfully", existingWishList));
        }


    }
}
