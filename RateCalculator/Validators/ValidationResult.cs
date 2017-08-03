namespace RateCalculator.Validators
{
    public class ValidationResult
    {
        public string Message { get;}
        public bool IsValid { get; }

        public ValidationResult(string message = null)
        {
            Message = message;
            IsValid = string.IsNullOrEmpty(Message);
        }
    }
}