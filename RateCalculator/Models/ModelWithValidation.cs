using RateCalculator.Validators;

namespace RateCalculator.Models
{
    public abstract class ModelWithValidation
    {
        public ValidationResult ValidationResult { get; private set; }

        public ModelWithValidation()
        {
            ValidationResult = ValidationResult.Valid();
        }

        public void SetErrorMessage(string errorMessage)
        {
            ValidationResult = ValidationResult.Invalid(errorMessage);
        }

    }
}
