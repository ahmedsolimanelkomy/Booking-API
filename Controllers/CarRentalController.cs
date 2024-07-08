using AutoMapper;
using Booking_API.DTOs;
using Booking_API.DTOs.CarDTOS;
using Booking_API.DTOs.CarRental;
using Booking_API.Models;
using Booking_API.Services;
using Booking_API.Services.IService;
using Braintree;
using Microsoft.AspNetCore.Mvc;
using BraintreeService = Booking_API.Services.BraintreeService;
using PaymentMethod = Booking_API.Models.PaymentMethod;

namespace Booking_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarRentalController : ControllerBase
    {
        private readonly ICarRentalService _carRentalService;
        private readonly ICarRentalInvoiceService _carRentalInvoiceService;
        private readonly BraintreeService _braintreeService;
        private readonly IMapper _mapper;

        public CarRentalController(ICarRentalService carRentalService, IMapper mapper, ICarRentalInvoiceService carRentalInvoiceService, BraintreeService braintreeService)
        {
            _carRentalService = carRentalService;
            _carRentalInvoiceService = carRentalInvoiceService;
            _braintreeService = braintreeService;
            _mapper = mapper;
        }

        #region Filteration

        [HttpPost("GetFilteredCarRentals")]
        public async Task<ActionResult<GeneralResponse<IEnumerable<CarRentalViewDTO>>>> GetFilteredCarRentals([FromQuery] CarRentalFilterationDTO filter)
        {
            try
            {
                var rentalDTOs = await _carRentalService.GetFilteredCarRentals(filter);
                if (rentalDTOs == null)
                {
                    return NotFound(new GeneralResponse<IEnumerable<CarRentalViewDTO>>(false, "No rentals found for the specified filter criteria", null));
                }
                return Ok(new GeneralResponse<IEnumerable<CarRentalViewDTO>>(true, "Filtered rentals retrieved successfully", rentalDTOs));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new GeneralResponse<IEnumerable<CarRentalViewDTO>>(false, ex.Message, null));
            }
        }

        [HttpGet("GetFilteredUserCarRents")]
        public async Task<ActionResult<GeneralResponse<IEnumerable<CarRentalViewDTO>>>> GetFilteredUserCarRents([FromQuery] UserCarRentalFilterDTO filter)
        {
            try
            {
                var rentalDTOs = await _carRentalService.GetFilteredUserCarRents(filter);
                if (rentalDTOs == null || !rentalDTOs.Any())
                {
                    return NotFound(new GeneralResponse<IEnumerable<CarRentalViewDTO>>(false, "No rentals found for the specified filter criteria", null));
                }
                return Ok(new GeneralResponse<IEnumerable<CarRentalViewDTO>>(true, "Filtered rentals retrieved successfully", rentalDTOs));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new GeneralResponse<IEnumerable<CarRentalViewDTO>>(false, ex.Message, null));
            }
        }

        #endregion

        // GET: api/CarRental
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarRentalDTO>>> GetCarRentals()
        {
            var carRentals = await _carRentalService.GetAllAsync();
            var carRentalDTOs = _mapper.Map<IEnumerable<CarRentalDTO>>(carRentals);
            return Ok(new GeneralResponse<IEnumerable<CarRentalDTO>>(true, "Car rentals retrieved successfully", carRentalDTOs));
        }

        // GET: api/CarRental/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<CarRentalDTO>> GetCarRentalById(int id)
        {
            var carRental = await _carRentalService.GetAsync(cr => cr.Id == id);
            if (carRental == null)
            {
                return NotFound(new GeneralResponse<CarRentalDTO>(false, "Car rental not found", null));
            }

            var carRentalDTO = _mapper.Map<CarRentalDTO>(carRental);
            return Ok(new GeneralResponse<CarRentalDTO>(true, "Car rental retrieved successfully", carRentalDTO));
        }

        // POST: api/CarRental
        [HttpPost]
        public async Task<ActionResult<CarRentalDTO>> CreateCarRental(CarRentalDTO carRentalDTO)
        {
            if (carRentalDTO == null)
            {
                return BadRequest(new GeneralResponse<CarRentalDTO>(false, "Invalid car rental data", null));
            }

            var carRental = _mapper.Map<CarRental>(carRentalDTO);
            await _carRentalService.AddAsync(carRental);
            var createdCarRentalDTO = _mapper.Map<CarRentalDTO>(carRental);
            return CreatedAtAction(nameof(GetCarRentalById), new { id = createdCarRentalDTO.Id }, new GeneralResponse<CarRentalDTO>(true, "Car rental created successfully", createdCarRentalDTO));
        }

        // PUT: api/CarRental/{id}
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateCarRental(int id, CarRentalDTO carRentalDTO)
        {
            if (id != carRentalDTO.Id || carRentalDTO == null)
            {
                return BadRequest(new GeneralResponse<CarRentalDTO>(false, "Car rental ID mismatch or invalid data", null));
            }

            var existingCarRental = await _carRentalService.GetAsync(cr => cr.Id == id);
            if (existingCarRental == null)
            {
                return NotFound(new GeneralResponse<CarRentalDTO>(false, "Car rental not found", null));
            }

            _mapper.Map(carRentalDTO, existingCarRental);
            await _carRentalService.UpdateAsync(existingCarRental);
            return CreatedAtAction(nameof(UpdateCarRental), new { id = existingCarRental.Id }, new GeneralResponse<CarRentalDTO>(true, "Car rental created successfully", carRentalDTO));
        }

        // DELETE: api/CarRental/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarRental(int id)
        {
            var carRental = await _carRentalService.GetAsync(cr => cr.Id == id);
            if (carRental == null)
            {
                return NotFound(new GeneralResponse<CarRentalDTO>(false, "Car rental not found", null));
            }

            await _carRentalService.DeleteAsync(id);
            return CreatedAtAction(nameof(DeleteCarRental), new { id = carRental.Id }, new GeneralResponse<CarRentalDTO>(true, "Car rental created successfully", null));
        }

        #region Payment

        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout([FromBody] CarRentalPaymentRequest paymentRequest)
        {
            if (paymentRequest == null || paymentRequest.RentalData == null)
            {
                return BadRequest(new GeneralResponse<CreateCarRentDTO>(false, "Invalid data", null));
            }

            var rentalDto = paymentRequest.RentalData;

            if (!ModelState.IsValid)
            {
                return BadRequest(new GeneralResponse<CreateCarRentDTO>(false, "Invalid rental data", rentalDto));
            }

            // Process the payment
            var paymentResult = await _braintreeService.MakePaymentAsync(paymentRequest.Nonce, paymentRequest.Amount);

            if (!paymentResult.IsSuccess())
            {
                // Payment failed, return error
                return BadRequest(new { Errors = paymentResult.Errors.DeepAll() });
            }

            // Payment succeeded, proceed with rental
            rentalDto.Status = BookingStatus.Confirmed;
            var rentalResponse = await _carRentalService.CreateCarRentAsync(rentalDto);

            if (!rentalResponse.Success)
            {
                // Optionally, handle failure to create rental (rollback payment if necessary)
                return BadRequest(new GeneralResponse<CreateCarRentDTO>(false, "Rental creation failed", rentalDto));
            }

            // Create the invoice
            var invoiceResponse = await _carRentalService.CreateInvoiceAsync(
                rentalResponse.Data,
                paymentRequest.Amount,
                rentalDto.UserId,
                PaymentMethod.PayPal
                );
            if (!invoiceResponse.Success)
            {
                // Handle failure to create invoice by deleting the rental
                var rental = await _carRentalService.GetAsync(r => r.Id == rentalResponse.Data.Id);
                await _carRentalService.DeleteAsync(rental.Id);

                return BadRequest(new GeneralResponse<CreateCarRentDTO>(false, "Invoice creation failed, rental deleted", rentalDto));
            }

            return Ok(new
            {
                Message = "Rental and payment successful"
            });
        }

        [HttpPost("checkout/cash")]
        public async Task<IActionResult> CheckoutCash([FromBody] CarRentalPaymentRequest paymentRequest)
        {
            if (paymentRequest == null || paymentRequest.RentalData == null)
            {
                return BadRequest(new GeneralResponse<CreateCarRentDTO>(false, "Invalid data", null));
            }

            var rentalDto = paymentRequest.RentalData;

            if (!ModelState.IsValid)
            {
                return BadRequest(new GeneralResponse<CreateCarRentDTO>(false, "Invalid rental data", rentalDto));
            }

            // Proceed with rental
            rentalDto.Status = BookingStatus.Confirmed;
            var rentalResponse = await _carRentalService.CreateCarRentAsync(rentalDto);

            if (!rentalResponse.Success)
            {
                return BadRequest(new GeneralResponse<CreateCarRentDTO>(false, "Rental creation failed", rentalDto));
            }

            // Create the invoice
            var invoiceResponse = await _carRentalService.CreateInvoiceAsync(
                rentalResponse.Data,
                paymentRequest.Amount,
                rentalDto.UserId,
                PaymentMethod.Cash
            );

            if (!invoiceResponse.Success)
            {
                var rental = await _carRentalService.GetAsync(r => r.Id == rentalResponse.Data.Id);
                await _carRentalService.DeleteAsync(rental.Id);

                return BadRequest(new GeneralResponse<CreateCarRentDTO>(false, "Invoice creation failed, rental deleted", rentalDto));
            }

            return Ok(new
            {
                Message = "Rental successful with cash payment"
            });
        }

        [HttpGet("client_token")]
        public async Task<IActionResult> GetClientToken()
        {
            var clientToken = await _braintreeService.GetClientTokenAsync();
            return Ok(new { clientToken });
        }

        #endregion

        public class CarRentalPaymentRequest
        {
            public string? Nonce { get; set; }
            public decimal Amount { get; set; }
            public CreateCarRentDTO RentalData { get; set; }
        }
    }
}
