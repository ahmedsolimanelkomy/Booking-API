using System.ComponentModel.DataAnnotations;

namespace Booking_API.Validations
{
    public class CheckInDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is DateTime date)
            {
                return date >= DateTime.Now.Date;
            }
            return false;
        }
    }
}
