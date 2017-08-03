namespace RateCalculator.Validators
{
    public class ValidationResult
    {
        public string ErrorMessage { get;}
        public bool IsValid { get; }

        private ValidationResult(string message = null)
        {
            ErrorMessage = message;
            IsValid = string.IsNullOrEmpty(ErrorMessage);
        }

        public static ValidationResult Invalid(string message)
        {
            return new ValidationResult(message);
        }

        public static ValidationResult Valid()
        {
            return new ValidationResult();
        }
    }
}