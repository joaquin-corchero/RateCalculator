using System;
using System.IO;

namespace RateCalculator.Validators
{
    public interface IInputValidator
    {
        ValidationResult Validate(string[] args);
    }

    public class InputValidator : IInputValidator
    { 
        public ValidationResult Validate(string[] args)
        {
            if (args == null || args.Length != 2)
            {
                return new ValidationResult("Incorrect arguments, please follow the format: ratecalculator.exe market.csv 1500");
            }

            var fileValidator = IsFileNameValid(args[0]);

            if (!fileValidator.IsValid)
            {
                return fileValidator;
            }

            return new ValidationResult();
        }

        ValidationResult IsFileNameValid(string fileName)
        {
            if(Path.GetExtension(fileName).ToLowerInvariant() != "csv")
            {
                return new ValidationResult("First parameter must be a csv: market.csv");
            }

            return new ValidationResult("");
        }
    }
}