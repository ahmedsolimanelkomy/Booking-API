using System.ComponentModel.DataAnnotations;

namespace Booking_API.Validations
{
    public class CheckOutDateAttribute : ValidationAttribute
    {
        private readonly string _comparisonProperty;
        public CheckOutDateAttribute(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var comparisonProperty = validationContext.ObjectType.GetProperty(_comparisonProperty);
            if (comparisonProperty == null)
                throw new ArgumentException("Property with this name not found");

            var comparisonValue = comparisonProperty.GetValue(validationContext.ObjectInstance);

            if (value is DateTime endDate && comparisonValue is DateTime startDate)
            {
                if (endDate <= startDate)
                {
                    return new ValidationResult(ErrorMessage);
                }
            }

            return ValidationResult.Success;
        }
    }
}
