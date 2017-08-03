using System.IO;

namespace RateCalculator.Validators
{
    public interface IInputValidator
    {
        ValidationResult Validate(string[] args);
    }

    public class InputValidator : IInputValidator
    {
        public const string INVALID_LOAN_AMMOUNT = "Second parameter must be a number between 1,000 and 15,000, multiple of 100";
        public const string INVALID_CSV = "First parameter must be a csv: market.csv";
        public const string INVALID_PARAMETERS = "Incorrect arguments, please follow the format: ratecalculator.exe market.csv 1500";

        double _minimumLoan { get; }
        double  _maximumLoan { get; }
        double _multiplesOf { get; }
       
        public InputValidator(double minimumLoan, double maximumLoan, double multiplesOf = 1)
        {
            _minimumLoan = minimumLoan;
            _maximumLoan = maximumLoan;
            _multiplesOf = multiplesOf;
        }

        public ValidationResult Validate(string[] args)
        {
            if (args.Length != 2)
            {
                return ValidationResult.Invalid(INVALID_PARAMETERS);
            }

            var fileValidator = IsFileNameValid(args[0]);

            if (!fileValidator.IsValid)
            {
                return fileValidator;
            }

            var loanAmmountValidator = IsAmmountValid(args[1]);

            if (!loanAmmountValidator.IsValid)
            {
                return loanAmmountValidator;
            }

            return ValidationResult.Valid();
        }


        ValidationResult IsFileNameValid(string fileName)
        {
            if (Path.GetExtension(fileName).ToLowerInvariant() != ".csv")
            {
                return ValidationResult.Invalid(INVALID_CSV);
            }

            return ValidationResult.Valid();
        }

        ValidationResult IsAmmountValid(string ammount)
        {
            double loanAmmount = 0;
            if(!double.TryParse(ammount, out loanAmmount))
            {
                return ValidationResult.Invalid(INVALID_LOAN_AMMOUNT);
            }

            if(
                loanAmmount < _minimumLoan || 
                loanAmmount > _maximumLoan || 
                loanAmmount % _multiplesOf != 0)
            {
                return ValidationResult.Invalid(INVALID_LOAN_AMMOUNT);
            }

            return ValidationResult.Valid();
        }
    }
}