namespace Booking_API.Services.IService
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string subject, string body, bool isBodyHTML);
    }
}
