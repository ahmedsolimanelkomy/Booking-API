//using Booking_API.DTOs;
//using Booking_API.Models;
//using Booking_API.Services;
//using Booking_API.Services.IService;
//using Braintree;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using BraintreeService = Booking_API.Services.BraintreeService;

//namespace Booking_API.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class PaymentController : ControllerBase
//    {
//        private readonly BraintreeService _braintreeService;
//        private readonly IPaymentService paymentService;
//        public PaymentController(IPaymentService paymentService, BraintreeService braintreeService)
//        {
//            this.paymentService = paymentService;
//            _braintreeService = braintreeService;
//        }

//        [HttpGet]
//        public async Task<ActionResult<GeneralResponse<IEnumerable<Payment>>>> GetPayments([FromQuery] string[] includeProperties)
//        {
//            var response = await paymentService.GetAllAsync(includeProperties);
//            return Ok(new GeneralResponse<IEnumerable<Payment>>(true, "Payments retrieved successfully", response));
//        }

//        [HttpGet("{id}")]
//        public async Task<ActionResult<GeneralResponse<Payment>>> GetPayment(int id, [FromQuery] string[] includeProperties)
//        {
//            var response = await paymentService.GetAsync(b => b.Id == id, includeProperties);
//            if (response == null)
//            {
//                return NotFound(new GeneralResponse<Payment>(false, "Payment not found", null));
//            }
//            return Ok(new GeneralResponse<Payment>(true, "Payment retrieved successfully", response));
//        }

//        [HttpPost]
//        public async Task<ActionResult<GeneralResponse<Payment>>> PostPayment(Payment Payment)
//        {
//            await paymentService.AddAsync(Payment);
//            return CreatedAtAction(nameof(GetPayment), new { id = Payment.Id }, new GeneralResponse<Payment>(true, "Payment added successfully", Payment));
//        }

//        [HttpPut("{id}")]
//        public async Task<ActionResult<GeneralResponse<Payment>>> PutPayment(int id, Payment Payment)
//        {
//            if (id != Payment.Id)
//            {
//                return BadRequest(new GeneralResponse<Payment>(false, "Payment ID mismatch", null));
//            }

//            await paymentService.UpdateAsync(Payment);
//            return NoContent();
//        }

//        [HttpDelete("{id}")]
//        public async Task<ActionResult<GeneralResponse<Payment>>> DeletePayment(int id)
//        {
//            var existingPayment = await paymentService.GetAsync(b => b.Id == id);
//            if (existingPayment == null)
//            {
//                return NotFound(new GeneralResponse<Payment>(false, "Payment not found", null));
//            }

//            await paymentService.DeleteAsync(id);
//            return Ok(new GeneralResponse<Payment>(true, "Payment deleted successfully", existingPayment));
//        }

//        [HttpPost("checkout")]
//        public async Task<IActionResult> Checkout([FromBody] PaymentRequest paymentRequest)
//        {
//            var result = await _braintreeService.MakePaymentAsync(paymentRequest.Nonce, paymentRequest.Amount);
//            if (result.IsSuccess())
//            {
//                return Ok(new { result.Transaction?.Status});
//            }
//            else
//            {
//                return BadRequest(new { Errors = result.Errors.DeepAll() });
//            }
//        }

//        [HttpGet("client_token")]
//        public async Task<IActionResult> GetClientToken()
//        {
//            var clientToken = await _braintreeService.GetClientTokenAsync();
//            return Ok(new { clientToken });
//        }
       

//    }

//    public class PaymentRequest
//    {
//        public string Nonce { get; set; }
//        public decimal Amount { get; set; }
//    }

//}

