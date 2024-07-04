using Booking_API.DTOs;
using Booking_API.Models;
using Booking_API.Services;
using Booking_API.Services.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Booking_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService reviewService;

        public ReviewController(IReviewService reviewService)
        {
            this.reviewService = reviewService;
        }

        //[HttpGet]
        //public async Task<ActionResult<GeneralResponse<IEnumerable<HotelReview>>>> GetReviews([FromQuery] string[] includeProperties)
        //{
        //    var response = await reviewService.GetAllAsync(includeProperties);
        //    return Ok(new GeneralResponse<IEnumerable<HotelReview>>(true, "Reviews retrieved successfully", response));
        //}

        //[HttpGet("{id}")]
        //public async Task<ActionResult<GeneralResponse<HotelReview>>> GetReview(int id, [FromQuery] string[] includeProperties)
        //{
        //    var response = await reviewService.GetAsync(b => b.Id == id, includeProperties);
        //    if (response == null)
        //    {
        //        return NotFound(new GeneralResponse<HotelReview>(false, "Review not found", null));
        //    }
        //    return Ok(new GeneralResponse<HotelReview>(true, "Review retrieved successfully", response));
        //}

        //[HttpPost]
        //public async Task<ActionResult<GeneralResponse<HotelReview>>> PostReview(HotelReview Review)
        //{
        //    await reviewService.AddAsync(Review);
        //    return CreatedAtAction(nameof(GetReview), new { id = Review.Id }, new GeneralResponse<HotelReview>(true, "Review added successfully", Review));
        //}


        //[Authorize]
        [HttpPost("AddReview")]
        public async Task<IActionResult> AddReview([FromBody] AddHotelReviewDTO createReviewDto)
        {
            if (!ModelState.IsValid)
            {
                var response1 = new GeneralResponse<object>(false, "Invalid model state", ModelState);
                return BadRequest(response1);
            }

            var response = await reviewService.AddReviewAsync(createReviewDto);
            if (response.Success)
            {
                return Ok(response);
            }

            return StatusCode(500, response);
        }

        [HttpGet("GetAllReviews")]
        public async Task<IActionResult> GetAllReviews([FromQuery] string[] includeProperties)
        {
            var reviews = await reviewService.GetAllReviewsAsync(includeProperties);
            if (reviews == null || !reviews.Any())
            {
                return NotFound(new GeneralResponse<object>(false, "No reviews found", null));
            }

            return Ok(new GeneralResponse<IEnumerable<DisplayHotelReviewDTO>>(true, "Reviews retrieved successfully", reviews));
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<GeneralResponse<HotelReview>>> PutReview(int id, HotelReview Review)
        {
            if (id != Review.Id)
            {
                return BadRequest(new GeneralResponse<HotelReview>(false, "Review ID mismatch", null));
            }

            await reviewService.UpdateAsync(Review);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<GeneralResponse<HotelReview>>> DeleteReview(int id)
        {
            var existingReview = await reviewService.GetAsync(b => b.Id == id);
            if (existingReview == null)
            {
                return NotFound(new GeneralResponse<HotelReview>(false, "Review not found", null));
            }

            await reviewService.DeleteAsync(id);
            return Ok(new GeneralResponse<HotelReview>(true, "Review deleted successfully", existingReview));
        }

        [HttpGet("reviews/{hotelId}")]
        public async Task<ActionResult<GeneralResponse<IEnumerable<DisplayHotelReviewDTO>>>> GetAllReviewsByHotelId(int hotelId, [FromQuery] string[] includeProperties)
        {
            var reviews = await reviewService.GetAllReviewsByHotelIdAsync(hotelId, includeProperties);
            if (reviews == null || !reviews.Any())
            {
                return NotFound(new GeneralResponse<object>(false, "No reviews found for this hotel", null));
            }

            return Ok(new GeneralResponse<IEnumerable<DisplayHotelReviewDTO>>(true, "Reviews retrieved successfully", reviews));
        }

    }
}
