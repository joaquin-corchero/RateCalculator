using RateCalculator.Models;
using System.IO;

namespace RateCalculator.Domain
{
    public interface IInputprocessor
    {
        Input ProcessInput(string[] args);
    }

    public class InputProcessor : IInputprocessor
    {
        public const string INVALID_LOAN_AMMOUNT = "Second parameter must be a number between 1,000 and 15,000, multiple of 100";
        public const string INVALID_CSV = "First parameter must be a csv: market.csv";
        public const string INVALID_PARAMETERS = "Incorrect arguments, please follow the format: ratecalculator.exe market.csv 1500";

        double _minimumLoan { get; }
        double  _maximumLoan { get; }
        double _multiplesOf { get; }
       
        public InputProcessor(double minimumLoan, double maximumLoan, double multiplesOf = 1)
        {
            _minimumLoan = minimumLoan;
            _maximumLoan = maximumLoan;
            _multiplesOf = multiplesOf;
        }

        public Input ProcessInput(string[] args)
        {
            var result = new Input();
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

            result.SetLoanAmmount(GetLoanAmmount(args[1]));

            if(result.LoanAmmount == 0)
            {
                result.SetErrorMessage(INVALID_LOAN_AMMOUNT);
            }

            return result;
        }


        string GetFileName(string fileName)
        {
            if (Path.GetExtension(fileName).ToLowerInvariant() == ".csv")
                return fileName;

            return null;
        }

        double GetLoanAmmount(string ammount)
        {
            double loanAmmount = 0;
            if(!double.TryParse(ammount, out loanAmmount))
                return 0;

            if(
                loanAmmount < _minimumLoan || 
                loanAmmount > _maximumLoan || 
                loanAmmount % _multiplesOf != 0)
                return 0;

            return loanAmmount;
        }
    }
}