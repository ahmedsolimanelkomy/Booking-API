using Booking_API.DTOs;
using Booking_API.Models;
using Booking_API.Services.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Booking_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeatureController : ControllerBase
    {
        private readonly IFeatureService _FeatureService;

        public FeatureController(IFeatureService FeatureService)
        {
            _FeatureService = FeatureService;
        }

        [HttpGet]
        public async Task<ActionResult<GeneralResponse<IEnumerable<Feature>>>> GetFeatures([FromQuery] string[] includeProperties)
        {
            var response = await _FeatureService.GetAllAsync(includeProperties);
            return Ok(new GeneralResponse<IEnumerable<Feature>>(true, "Features retrieved successfully", response));
        }
        [HttpGet("GetHotelFeatures")]
        public async Task<ActionResult<GeneralResponse<IEnumerable<Feature>>>> GetHotelFeatures([FromQuery] string[] includeProperties)
        {
            var response = await _FeatureService.GetAllAsync(includeProperties);
            return Ok(new GeneralResponse<IEnumerable<Feature>>(true, "Features retrieved successfully", response));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GeneralResponse<Feature>>> GetFeature(int id, [FromQuery] string[] includeProperties)
        {
            var response = await _FeatureService.GetAsync(b => b.Id == id, includeProperties);
            if (response == null)
            {
                return NotFound(new GeneralResponse<Feature>(false, "Feature not found", null));
            }
            return Ok(new GeneralResponse<Feature>(true, "Feature retrieved successfully", response));
        }

        [HttpPost]
        public async Task<ActionResult<GeneralResponse<Feature>>> PostFeature(Feature Feature)
        {
            await _FeatureService.AddAsync(Feature);
            return CreatedAtAction(nameof(GetFeature), new { id = Feature.Id }, new GeneralResponse<Feature>(true, "Feature added successfully", Feature));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GeneralResponse<Feature>>> PutFeature(int id, Feature Feature)
        {
            if (id != Feature.Id)
            {
                return BadRequest(new GeneralResponse<Feature>(false, "Feature ID mismatch", null));
            }

            await _FeatureService.UpdateAsync(Feature);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<GeneralResponse<Feature>>> DeleteFeature(int id)
        {
            var existingFeature = await _FeatureService.GetAsync(b => b.Id == id);
            if (existingFeature == null)
            {
                return NotFound(new GeneralResponse<Feature>(false, "Feature not found", null));
            }

            await _FeatureService.DeleteAsync(id);
            return Ok(new GeneralResponse<Feature>(true, "Feature deleted successfully", existingFeature));
        }
    }
}

