using System;

namespace RateCalculator.Validators
{
    public interface IInputValidator
    {
        ValidationResult Validate(string[] args);
    }

    public class InputValidator : IInputValidator
    {
        public InputValidator()
        {
        }

        public ValidationResult Validate(string[] args)
        {
            return new ValidationResult("Incorrect argumenst, please try something like: ratecalculator.exe market.csv 1500");
        }
    }
}