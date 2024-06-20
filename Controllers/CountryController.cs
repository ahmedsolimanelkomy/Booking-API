using AutoMapper;
using Booking_API.DTOs;
using Booking_API.Models;
using Booking_API.Services.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Booking_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly IService<Country> _countryService;
        private readonly IMapper _mapper;
        private readonly BookingContext bookingContext;

        public CountryController(IService<Country> countryService, IMapper mapper, BookingContext bookingContext)
        {
            _countryService = countryService;
            _mapper = mapper;
            this.bookingContext = bookingContext;
        }

        //[HttpGet]
        //public async Task<ActionResult<GeneralResponse<IEnumerable<CountryDTO>>>> GetCountries([FromQuery] string[] includeProperties = null)
        //{
        //    var countries = await _countryService.GetAllAsync(includeProperties);
        //    var countriesDTO = _mapper.Map<IEnumerable<CountryDTO>>(countries);
        //    return Ok(new GeneralResponse<IEnumerable<CountryDTO>>(true, "Countries retrieved successfully", countriesDTO));
        //}

        [HttpGet("{id}")]
        public async Task<ActionResult<GeneralResponse<CountryDTO>>> GetCountry(int id, [FromQuery] string[] includeProperties = null)
        {
            var country = await _countryService.GetAsync(c => c.Id == id, includeProperties);
            if (country == null)
            {
                return NotFound(new GeneralResponse<CountryDTO>(false, "Country not found", null));
            }
            var countryDTO = _mapper.Map<CountryDTO>(country);
            return Ok(new GeneralResponse<CountryDTO>(true, "Country retrieved successfully", countryDTO));
        }

        [HttpPost]
        public async Task<ActionResult<GeneralResponse<CountryDTO>>> PostCountry(CountryDTO countryDTO)
        {
            var country = _mapper.Map<Country>(countryDTO);
            await _countryService.AddAsync(country);
            var createdCountryDTO = _mapper.Map<CountryDTO>(country);
            return CreatedAtAction(nameof(GetCountry), new { id = country.Id }, new GeneralResponse<CountryDTO>(true, "Country added successfully", createdCountryDTO));
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<GeneralResponse<CountryDTO>>> PutCountry(int id, CountryDTO countryDTO)
        {
            if (id != countryDTO.Id)
            {
                return BadRequest(new GeneralResponse<CountryDTO>(false, "Country ID mismatch", null));
            }
            var country = _mapper.Map<Country>(countryDTO);
            await _countryService.UpdateAsync(country);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<GeneralResponse<CountryDTO>>> DeleteCountry(int id)
        {
            var existingCountry = await _countryService.GetAsync(c => c.Id == id);
            if (existingCountry == null)
            {
                return NotFound(new GeneralResponse<CountryDTO>(false, "Country not found", null));
            }
            await _countryService.DeleteAsync(id);
            var deletedCountryDTO = _mapper.Map<CountryDTO>(existingCountry);
            return Ok(new GeneralResponse<CountryDTO>(true, "Country deleted successfully", deletedCountryDTO));
        }



        [HttpGet]
        public async Task<ActionResult<PagedResult<Country>>> GetItems([FromQuery] int pageNumber = 1)
        {
            int pageSize = 3;
            var query = bookingContext.Countries.AsQueryable();

            var count = await query.CountAsync();
            var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            var result = new PagedResult<Country>(items, count, pageNumber, pageSize);

            return Ok(result);
        }

    }
}
