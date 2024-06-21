namespace Booking_API.DTOs
{
    public class Verify2FADto
    {
        public string User { get; set; }
        public string Code { get; set; } //2FA code to be verified
        public string SecretKey { get; set; } //Secret key used for verifying the TOTP code.
    }
}
