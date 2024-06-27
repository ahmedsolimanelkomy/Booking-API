using Braintree;

namespace Booking_API.Services
{
    public class BraintreeService
    {
        private readonly BraintreeGateway _gateway;

        public BraintreeService(IConfiguration configuration)
        {
            var environment = configuration["Braintree:Environment"];
            var merchantId = configuration["Braintree:MerchantId"];
            var publicKey = configuration["Braintree:PublicKey"];
            var privateKey = configuration["Braintree:PrivateKey"];

            _gateway = new BraintreeGateway(environment, merchantId, publicKey, privateKey);
        }

        public async Task<Result<Transaction>> MakePaymentAsync(string nonce, decimal amount)
        {
            var request = new TransactionRequest
            {
                Amount = amount,
                PaymentMethodNonce = nonce,
                Options = new TransactionOptionsRequest
                {
                    SubmitForSettlement = true
                }
            };

            var result = await _gateway.Transaction.SaleAsync(request);
            return result;
        }
        public async Task<string> GetClientTokenAsync()
        {
            return await _gateway.ClientToken.GenerateAsync();
        }
    }
}
