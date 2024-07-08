using AutoMapper;
using Booking_API.DTOs;
using Booking_API.Models;
using Booking_API.Services.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Booking_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarRentalInvoiceController : ControllerBase
    {
        private readonly ICarRentalInvoiceService CarRentalInvoiceService;
        private readonly IMapper mapper;
        public CarRentalInvoiceController(ICarRentalInvoiceService CarRentalInvoiceService, IMapper mapper)
        {
            this.CarRentalInvoiceService = CarRentalInvoiceService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<GeneralResponse<IEnumerable<CarRentalInvoice>>>> GetCarRentalInvoices([FromQuery] string[] includeProperties)
        {
            var response = await CarRentalInvoiceService.GetAllAsync(includeProperties);
            return Ok(new GeneralResponse<IEnumerable<CarRentalInvoice>>(true, "CarRentalInvoices retrieved successfully", response));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GeneralResponse<CarRentalInvoice>>> GetCarRentalInvoice(int id, [FromQuery] string[] includeProperties)
        {
            var response = await CarRentalInvoiceService.GetAsync(b => b.Id == id, includeProperties);
            if (response == null)
            {
                return NotFound(new GeneralResponse<CarRentalInvoice>(false, "CarRentalInvoice not found", null));
            }
            return Ok(new GeneralResponse<CarRentalInvoice>(true, "CarRentalInvoice retrieved successfully", response));
        }
        
        [HttpGet("/GetCarUserRentalInvoiceById/{userId}")]
        public async Task<ActionResult<GeneralResponse<CarRentalInvoice>>> GetCarUserRentalInvoice(int userId, [FromQuery] string[] includeProperties)
        {
            var response = await CarRentalInvoiceService.GetAsync(b => b.UserId == userId, includeProperties);
            if (response == null)
            {
                return NotFound(new GeneralResponse<CarRentalInvoice>(false, "CarRentalInvoice not found", null));
            }
            return Ok(new GeneralResponse<CarRentalInvoice>(true, "CarRentalInvoice retrieved successfully", response));
        }

        [HttpPost]
        public async Task<ActionResult<GeneralResponse<CarRentalInvoiceDTO>>> PostCarRentalInvoice(CarRentalInvoiceDTO carRentalInvoiceDTO)
        {
            var carRentalInvoice = mapper.Map<CarRentalInvoice>(carRentalInvoiceDTO);
            await CarRentalInvoiceService.AddAsync(carRentalInvoice);
            var responseDTO = mapper.Map<CarRentalInvoiceDTO>(carRentalInvoice);
            return CreatedAtAction(nameof(GetCarRentalInvoice), new { id = carRentalInvoice.Id }, new GeneralResponse<CarRentalInvoiceDTO>(true, "CarRentalInvoice added successfully", responseDTO));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GeneralResponse<CarRentalInvoiceDTO>>> PutCarRentalInvoice(int id, CarRentalInvoiceDTO carRentalInvoiceDTO)
        {
            if (id != carRentalInvoiceDTO.Id)
            {
                return BadRequest(new GeneralResponse<CarRentalInvoiceDTO>(false, "CarRentalInvoice ID mismatch", null));
            }

            var carRentalInvoice = mapper.Map<CarRentalInvoice>(carRentalInvoiceDTO);
            await CarRentalInvoiceService.UpdateAsync(carRentalInvoice);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<GeneralResponse<CarRentalInvoice>>> DeleteCarRentalInvoice(int id)
        {
            var existingCarRentalInvoice = await CarRentalInvoiceService.GetAsync(b => b.Id == id);
            if (existingCarRentalInvoice == null)
            {
                return NotFound(new GeneralResponse<CarRentalInvoice>(false, "CarRentalInvoice not found", null));
            }

            await CarRentalInvoiceService.DeleteAsync(id);
            return Ok(new GeneralResponse<CarRentalInvoice>(true, "CarRentalInvoice deleted successfully", existingCarRentalInvoice));
        }
    }
}
