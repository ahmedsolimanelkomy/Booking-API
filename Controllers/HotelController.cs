using AutoMapper;
using AutoMapper.Features;
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
    public class HotelController : ControllerBase
    {
        private readonly IHotelService _HotelService;
        private readonly IFeatureService _FeatureService;
        private readonly IMapper mapper;

        public HotelController(IHotelService HotelService,IFeatureService FeatureService, IMapper mapper)
        {
            _HotelService = HotelService;
            _FeatureService = FeatureService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<GeneralResponse<IEnumerable<HotelViewDTO>>>> GetAllHotels([FromQuery] string[] includeProperties)
        {
            var response = await _HotelService.GetAllAsync(includeProperties);
            var res = mapper.Map<IEnumerable<HotelViewDTO>>(response);
            return Ok(new GeneralResponse<IEnumerable<HotelViewDTO>>(true, "Hotels retrieved successfully", res));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GeneralResponse<Hotel>>> GetHotel(int id, [FromQuery] string[] includeProperties)
        {
            var response = await _HotelService.GetAsync(b => b.Id == id, includeProperties);
            if (response == null)
            {
                return NotFound(new GeneralResponse<Hotel>(false, "Hotel not found", null));
            }
            return Ok(new GeneralResponse<Hotel>(true, "Hotel retrieved successfully", response));
        }

        [HttpPost]
        public async Task<ActionResult<GeneralResponse<Hotel>>> PostHotel(HotelDTO hotelDTO)
        {
            var Hotel = await _HotelService.AddDTOAsync(hotelDTO);
            return CreatedAtAction(nameof(GetHotel), new { id = Hotel.Id }, new GeneralResponse<Hotel>(true, "Hotel added successfully", Hotel));
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<GeneralResponse<Hotel>>> PatchHotel(int id, HotelDTO hotelDTO)
        {
            if (id != hotelDTO.Id)
            {
                return BadRequest(new GeneralResponse<Hotel>(false, "Hotel ID mismatch", null));
            }
            await _HotelService.UpdateDTOAsync(hotelDTO);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<GeneralResponse<Hotel>>> DeleteHotel(int id)
        {
            var existingHotel = await _HotelService.GetAsync(b => b.Id == id, ["Features","Rooms","Photos"]);
            if (existingHotel == null)
            {
                return NotFound(new GeneralResponse<Hotel>(false, "Hotel not found", null));
            }

            await _HotelService.DeleteAsync(id);
            return Ok(new GeneralResponse<Hotel>(true, "Hotel deleted successfully", existingHotel));
        }

        #region HotelFeatureOperations
        [HttpGet("/api/GetHotelFeatures/{hotelId}")]
        public async Task<ActionResult<GeneralResponse<IEnumerable<HotelFeatureDTO>>>> GetHotelFeatures(int hotelId)
        {
            var hotel = await _HotelService.GetAsync(h => h.Id == hotelId, ["Features"]);

            if (hotel == null)
            {
                return NotFound(new GeneralResponse<IEnumerable<HotelFeatureDTO>>(false, "Hotel not found", null));
            }

            var features = hotel.Features;

            if (features == null)
            {
                return NotFound(new GeneralResponse<IEnumerable<HotelFeatureDTO>>(false, "Hotel has no features", null));
            }

            IList<HotelFeatureDTO> hotelFeatureDTO = new List<HotelFeatureDTO>();

            mapper.Map(features, hotelFeatureDTO);

            return Ok(new GeneralResponse<IEnumerable<HotelFeatureDTO>>(true, "Hotel features retrieved successfully", hotelFeatureDTO));
        }

        [HttpGet("/api/GetHotelFeature/{hotelId}/{featureId}")]
        public async Task<ActionResult<GeneralResponse<HotelFeatureDTO>>> GetHotelFeature(int hotelId, int featureId)
        {
            var hotel = await _HotelService.GetAsync(h => h.Id == hotelId, ["Features"]);

            if (hotel == null)
            {
                return NotFound(new GeneralResponse<HotelFeatureDTO>(false, "Hotel not found", null));
            }

            var feature = hotel.Features.Where(f => f.Id == featureId);

            if (feature == null)
            {
                return NotFound(new GeneralResponse<IEnumerable<HotelFeatureDTO>>(false, "Hotel dosent have this feature", null));
            }

            HotelFeatureDTO hotelFeatureDTO = new HotelFeatureDTO();

            mapper.Map(feature, hotelFeatureDTO);

            return Ok(new GeneralResponse<HotelFeatureDTO>(true, "Hotel feature retrieved successfully", hotelFeatureDTO));
        }


        [HttpPost("/api/AddFeature/{hotelId}")]
        public async Task<ActionResult<GeneralResponse<HotelFeatureDTO>>> AddFeature(int hotelId, HotelFeatureDTO hotelFeatureDTO)
        {
            var hotel = await _HotelService.GetAsync(h => h.Id == hotelId, ["Features"]);
            if (hotel == null)
            {
                return NotFound(new GeneralResponse<string>(false, "Hotel not found", null));
            }

            var feature = await _FeatureService.GetAsync(f => f.Id == hotelFeatureDTO.Id);
            if (feature == null)
            {
                return NotFound(new GeneralResponse<string>(false, "Feature not found", null));
            }

            if (hotel.Features == null)
            {
                hotel.Features = new List<Feature>();
            }

            if (!hotel.Features.Any(f => f.Id == feature.Id))
            {
                hotel.Features.Add(feature);
                await _HotelService.UpdateAsync(hotel);
            }
            else
            {
                return BadRequest(new GeneralResponse<string>(false, "Feature already exists in hotel", null));
            }

            return CreatedAtAction(nameof(AddFeature), new GeneralResponse<string>(true, "Feature added successfully", null));
        }

        [HttpDelete("/api/DeleteFeature/{hotelId}/{featureId}")]
        public async Task<ActionResult<GeneralResponse<HotelFeatureDTO>>> DeleteFeature(int hotelId, int featureId)
        {
            var hotel = await _HotelService.GetAsync(h => h.Id == hotelId, ["Features"]);

            if (hotel == null)
            {
                return NotFound(new GeneralResponse<string>(false, "Hotel not found", null));
            }

            var feature = hotel.Features.FirstOrDefault(f => f.Id == featureId);

            if (feature == null)
            {
                return NotFound(new GeneralResponse<string>(false, "Feature not found", null));
            }

            hotel.Features.Remove(feature);
            await _HotelService.UpdateAsync(hotel);

            return Ok(new GeneralResponse<string>(true, "Feature deleted successfully", null));
        }
        #endregion

        #region HotelFilteration
        [HttpGet("/api/GetFilteredHotels")]
        public async Task<IActionResult> GetFilteredHotels([FromQuery] HotelFilterDTO filter)
        {
            var hotels = await _HotelService.GetFilteredHotelsAsync(filter);

            return Ok(new GeneralResponse<IEnumerable<FilteredHotelDTO>>(true, "Hotels Retrieved successfully", hotels));
        }
        #endregion

    }
}

