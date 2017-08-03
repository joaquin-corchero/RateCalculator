using RateCalculator.Validators;

namespace RateCalculator.Models
{
    public class QuoteResponse
    {
        public ValidationResult ValidationResult { get; private set; }

        public Quote Quote { get; private set; }

        public QuoteResponse()
        {
            ValidationResult = ValidationResult.Valid();
        }

        public void SetValidationResult(ValidationResult validation)
        {
            ValidationResult = validation;
        }
    }
}
