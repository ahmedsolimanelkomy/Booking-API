using AutoMapper;
using Booking_API.DTOs;
using Booking_API.DTOs.CarReviewDTO;
using Booking_API.Models;
using Booking_API.Services.IService;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Booking_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarAgencyReviewController : ControllerBase
    {
        private readonly ICarAgencyReviewService _carAgencyReviewService;
        private readonly IMapper _mapper;

        public CarAgencyReviewController(ICarAgencyReviewService carAgencyReviewService, IMapper mapper)
        {
            _carAgencyReviewService = carAgencyReviewService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var reviews = await _carAgencyReviewService.GetAllAsync();

            if (reviews == null || !reviews.Any())
            {
                return NotFound(new GeneralResponse<IEnumerable<CarAgancyReviewDTO>>(false, "No reviews found for any car agency.", null));
            }

            var reviewDtos = _mapper.Map<IEnumerable<CarAgancyReviewDTO>>(reviews);
            return Ok(new GeneralResponse<IEnumerable<CarAgancyReviewDTO>>(true, "Reviews retrieved successfully.", reviewDtos));
        }

        [HttpGet("{carAgencyId}")]
        public async Task<IActionResult> GetReviewsByCarAgencyId(int carAgencyId)
        {
            var reviews = await _carAgencyReviewService.GetListAsync(r => r.CarAgencyId == carAgencyId);
            if (reviews == null)
            {
                return NotFound(new GeneralResponse<IEnumerable<CarAgancyReviewDTO>>(false, "No reviews found for this car agency.", null));
            }

            var reviewDtos = _mapper.Map<IEnumerable<CarAgancyReviewDTO>>(reviews);
            return Ok(new GeneralResponse<IEnumerable<CarAgancyReviewDTO>>(true, "Reviews retrieved successfully.", reviewDtos));
        }

        [HttpPost]
        public async Task<IActionResult> AddReview([FromBody] CarAgancyReviewDTO reviewDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new GeneralResponse<CarAgancyReviewDTO>(false, "Invalid model state.", null));
            }

            var review = _mapper.Map<CarAgencyReview>(reviewDto);
            await _carAgencyReviewService.AddAsync(review);
            var addedReviewDto = _mapper.Map<CarAgancyReviewDTO>(review);

            return CreatedAtAction(nameof(GetReviewsByCarAgencyId), new { carAgencyId = addedReviewDto.CarAgencyId }, new GeneralResponse<CarAgancyReviewDTO>(true, "Review added successfully.", addedReviewDto));
        }

        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateReview(int id, [FromBody] CarAgancyReviewDTO reviewDto)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(new GeneralResponse<CarAgancyReviewDTO>(false, "Invalid model state.", null));
        //    }

        //    var existingReview = await _carAgencyReviewService.GetAsync(r => r.Id == id);
        //    if (existingReview == null)
        //    {
        //        return NotFound(new GeneralResponse<CarAgancyReviewDTO>(false, "Review not found.", null));
        //    }

        //    _mapper.Map(reviewDto, existingReview);

        //    await _carAgencyReviewService.UpdateAsync(existingReview);

        //    var updatedReviewDto = _mapper.Map<CarAgancyReviewDTO>(existingReview);

        //    return Ok(new GeneralResponse<CarAgancyReviewDTO>(true, "Review updated successfully.", updatedReviewDto));
        //}

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var review = await _carAgencyReviewService.GetAsync(r => r.Id == id);
            if (review == null)
            {
                return NotFound(new GeneralResponse<CarAgancyReviewDTO>(false, "Review not found.", null));
            }

            await _carAgencyReviewService.DeleteAsync(id);
            return Ok(new GeneralResponse<bool>(true, "Review deleted successfully.", true));
        }
    }
}
