using RateCalculator.Models;
using System;
using System.IO;

namespace RateCalculator.Handlers
{
    public class InputHandler : Handler, IHandler
    {
        public const string INVALID_LOAN_AMOUNT = "Second parameter must be a number between 1,000 and 15,000, multiple of 100";
        public const string INVALID_CSV = "First parameter must be a csv: market.csv";
        public const string INVALID_PARAMETERS = "Incorrect arguments, please follow the format: ratecalculator.exe market.csv 1500";

        public override void HandleRequest(QuoteModel quote)
        {
            if (quote.Args.Length != 2)
            {
                quote.SetErrorMessage(INVALID_PARAMETERS);
                return;
            }

            SetFileName(quote);
            if (string.IsNullOrEmpty(quote.InputModel.FileName))
            {
                quote.SetErrorMessage(INVALID_CSV);
                return;
            }

            SetAmount(quote);
            if (quote.InputModel.LoanAmount == 0)
            {
                quote.SetErrorMessage(INVALID_LOAN_AMOUNT);
                return;
            }

            successor.HandleRequest(quote);
        }

        void SetFileName(QuoteModel quote)
        {
            if (Path.GetExtension(quote.Args[0]).ToLowerInvariant() == ".csv")
                quote.InputModel.SetFileName(quote.Args[0]);
        }

        void SetAmount(QuoteModel quote)
        {
            double loanAmount = 0;
            if (!double.TryParse(quote.Args[1], out loanAmount))
                return;

            if (
                loanAmount < quote.MinimumLoan ||
                loanAmount > quote.MaximumLoan ||
                loanAmount % quote.MultiplesOf != 0)
                return;

            quote.InputModel.SetLoanAmount(loanAmount);
        }
    }
}
