using RateCalculator.Models;
using System.IO;

namespace RateCalculator.Domain
{
    public interface IInputprocessor
    {
        InputModel ProcessInput(string[] args);
    }

    public class InputProcessor : IInputprocessor
    {
        public const string INVALID_LOAN_AMOUNT = "Second parameter must be a number between 1,000 and 15,000, multiple of 100";
        public const string INVALID_CSV = "First parameter must be a csv: market.csv";
        public const string INVALID_PARAMETERS = "Incorrect arguments, please follow the format: ratecalculator.exe market.csv 1500";

        double _minimumLoan { get; }
        double _maximumLoan { get; }
        double _multiplesOf { get; }
       
        public InputProcessor(double minimumLoan, double maximumLoan, double multiplesOf = 1)
        {
            _minimumLoan = minimumLoan;
            _maximumLoan = maximumLoan;
            _multiplesOf = multiplesOf;
        }

        public InputModel ProcessInput(string[] args)
        {
            var result = new InputModel();
            if (args.Length != 2)
            {
                result.SetErrorMessage(INVALID_PARAMETERS);
                return result;
            }

            result.SetFileName(GetFileName(args[0]));

            if (string.IsNullOrEmpty(result.FileName))
            {
                result.SetErrorMessage(INVALID_CSV);
                return result;
            }

            result.SetLoanAmount(GetLoanAmount(args[1]));

            if(result.LoanAmount == 0)
            {
                result.SetErrorMessage(INVALID_LOAN_AMOUNT);
            }

            return result;
        }


        string GetFileName(string fileName)
        {
            if (Path.GetExtension(fileName).ToLowerInvariant() == ".csv")
                return fileName;

            return null;
        }

        double GetLoanAmount(string amount)
        {
            double loanAmount = 0;
            if(!double.TryParse(amount, out loanAmount))
                return 0;

            if(
                loanAmount < _minimumLoan || 
                loanAmount > _maximumLoan || 
                loanAmount % _multiplesOf != 0)
                return 0;

            return loanAmount;
        }
    }
}