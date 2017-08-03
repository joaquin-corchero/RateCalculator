namespace RateCalculator.Validators
{
    public class ValidationResult
    {
        public string Message { get;}
        public bool IsValid { get; }

        private ValidationResult(string message = null)
        {
            Message = message;
            IsValid = string.IsNullOrEmpty(Message);
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