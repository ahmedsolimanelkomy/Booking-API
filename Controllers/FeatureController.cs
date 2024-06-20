using AutoMapper;
using Booking_API.DTOs;
using Booking_API.DTOs.FeatureDTOS;
using Booking_API.DTOs.HotelDTOS;
using Booking_API.Models;
using Booking_API.Services;
using Booking_API.Services.IService;
using Microsoft.AspNetCore.Mvc;

namespace Booking_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeatureController : ControllerBase
    {
        private readonly IFeatureService _featureService;
        private readonly IHotelService _hotelService;
        private readonly IMapper _mapper;

        public FeatureController(IFeatureService featureService, IHotelService hotelService, IMapper mapper)
        {
            _featureService = featureService;
            _hotelService = hotelService;
            _mapper = mapper;
        }

        [HttpGet("/api/GetFeatures")]
        public async Task<ActionResult<GeneralResponse<IEnumerable<FeatureDTO>>>> GetFeatures([FromQuery] string[] includeProperties)
        {
            try
            {
                var response = await _featureService.GetAllAsync(includeProperties);
                var features = _mapper.Map<IEnumerable<FeatureDTO>>(response);

                return Ok(new GeneralResponse<IEnumerable<FeatureDTO>>(true, "Features retrieved successfully", features));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new GeneralResponse<IEnumerable<FeatureDTO>>(false, ex.Message, null));
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GeneralResponse<FeatureDTO>>> GetFeature(int id, [FromQuery] string[] includeProperties)
        {
            try
            {
                var response = await _featureService.GetAsync(f => f.Id == id, includeProperties);
                if (response == null)
                {
                    return NotFound(new GeneralResponse<FeatureDTO>(false, "Feature not found", null));
                }

                var feature = _mapper.Map<FeatureDTO>(response);
                return Ok(new GeneralResponse<FeatureDTO>(true, "Feature retrieved successfully", feature));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new GeneralResponse<FeatureDTO>(false, ex.Message, null));
            }
        }

        [HttpPost]
        public async Task<ActionResult<GeneralResponse<FeatureDTO>>> PostFeature(FeatureDTO featureDTO)
        {
            try
            {
                var feature = _mapper.Map<Feature>(featureDTO);
                await _featureService.AddAsync(feature);
                return Ok(new GeneralResponse<FeatureDTO>(true, "Feature added successfully", featureDTO));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new GeneralResponse<FeatureDTO>(false, ex.Message, null));
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GeneralResponse<FeatureDTO>>> PutFeature(int id, FeatureDTO featureDTO)
        {
            if (id != featureDTO.Id)
            {
                return BadRequest(new GeneralResponse<FeatureDTO>(false, "Feature ID mismatch", null));
            }

            try
            {
                var featuredb = await _featureService.GetAsync(f => f.Id == id);

                if (featuredb == null)
                {
                    return NotFound(new GeneralResponse<FeatureDTO>(false, "Feature not found", null));
                }

                _mapper.Map(featureDTO, featuredb);
                await _featureService.UpdateAsync(featuredb);

                return Ok(new GeneralResponse<FeatureDTO>(true, "Feature updated successfully", featureDTO));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new GeneralResponse<FeatureDTO>(false, ex.Message, null));
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<GeneralResponse<FeatureDTO>>> DeleteFeature(int id)
        {
            try
            {
                var existingFeature = await _featureService.GetAsync(f => f.Id == id);
                if (existingFeature == null)
                {
                    return NotFound(new GeneralResponse<FeatureDTO>(false, "Feature not found", null));
                }

                await _featureService.DeleteAsync(id);
                return Ok(new GeneralResponse<FeatureDTO>(true, "Feature deleted successfully", null));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new GeneralResponse<FeatureDTO>(false, ex.Message, null));
            }
        }

    }
}
