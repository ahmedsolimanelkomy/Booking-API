using Booking_API.DTOs;
using Booking_API.Models;
using Booking_API.Services.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Booking_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PassportController : ControllerBase
    {


        private readonly IPassportService passportService;

        public PassportController(IPassportService passportService)
        {
            this.passportService = passportService;
        }

        [HttpGet]
        public async Task<ActionResult<GeneralResponse<IEnumerable<Passport>>>> GetPassports([FromQuery] string[] includeProperties)
        {
            var response = await passportService.GetAllAsync(includeProperties);
            return Ok(new GeneralResponse<IEnumerable<Passport>>(true, "Passports retrieved successfully", response));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GeneralResponse<Passport>>> GetPassport(int id, [FromQuery] string[] includeProperties)
        {
            var response = await passportService.GetAsync(b => b.Id == id, includeProperties);
            if (response == null)
            {
                return NotFound(new GeneralResponse<Passport>(false, "Passport not found", null));
            }
            return Ok(new GeneralResponse<Passport>(true, "Passport retrieved successfully", response));
        }

        [HttpPost]
        public async Task<ActionResult<GeneralResponse<Passport>>> PostPassport(Passport Passport)
        {
            await passportService.AddAsync(Passport);
            return CreatedAtAction(nameof(GetPassport), new { id = Passport.Id }, new GeneralResponse<Passport>(true, "Passport added successfully", Passport));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GeneralResponse<Passport>>> PutPassport(int id, Passport Passport)
        {
            if (id != Passport.Id)
            {
                return BadRequest(new GeneralResponse<Passport>(false, "Passport ID mismatch", null));
            }

            await passportService.UpdateAsync(Passport);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<GeneralResponse<Passport>>> DeletePassport(int id)
        {
            var existingPassport = await passportService.GetAsync(b => b.Id == id);
            if (existingPassport == null)
            {
                return NotFound(new GeneralResponse<Passport>(false, "Passport not found", null));
            }

            await passportService.DeleteAsync(id);
            return Ok(new GeneralResponse<Passport>(true, "Passport deleted successfully", existingPassport));
        }


    }
}
