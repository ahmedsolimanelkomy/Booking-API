using Booking_API.DTOs;
using Booking_API.Models;
using Booking_API.Services;
using Booking_API.Services.IService;
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

        [HttpGet]
        public async Task<ActionResult<GeneralResponse<IEnumerable<Review>>>> GetReviews([FromQuery] string[] includeProperties)
        {
            var response = await reviewService.GetAllAsync(includeProperties);
            return Ok(new GeneralResponse<IEnumerable<Review>>(true, "Reviews retrieved successfully", response));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GeneralResponse<Review>>> GetReview(int id, [FromQuery] string[] includeProperties)
        {
            var response = await reviewService.GetAsync(b => b.Id == id, includeProperties);
            if (response == null)
            {
                return NotFound(new GeneralResponse<Review>(false, "Review not found", null));
            }
            return Ok(new GeneralResponse<Review>(true, "Review retrieved successfully", response));
        }

        [HttpPost]
        public async Task<ActionResult<GeneralResponse<Review>>> PostReview(Review Review)
        {
            await reviewService.AddAsync(Review);
            return CreatedAtAction(nameof(GetReview), new { id = Review.Id }, new GeneralResponse<Review>(true, "Review added successfully", Review));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GeneralResponse<Review>>> PutReview(int id, Review Review)
        {
            if (id != Review.Id)
            {
                return BadRequest(new GeneralResponse<Review>(false, "Review ID mismatch", null));
            }

            await reviewService.UpdateAsync(Review);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<GeneralResponse<Review>>> DeleteReview(int id)
        {
            var existingReview = await reviewService.GetAsync(b => b.Id == id);
            if (existingReview == null)
            {
                return NotFound(new GeneralResponse<Review>(false, "Review not found", null));
            }

            await reviewService.DeleteAsync(id);
            return Ok(new GeneralResponse<Review>(true, "Review deleted successfully", existingReview));
        }
    }
}
