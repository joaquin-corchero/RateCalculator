using RateCalculator.Validators;
using System.Collections.Generic;
using System;

namespace RateCalculator.Models
{
    public class QuoteModel
    {
        public ValidationResult ValidationResult { get; private set; }

        public string[] Args { get; }

        public double MinimumLoan { get; }

        public double MaximumLoan { get; }

        public double MultiplesOf { get; }

        public InputModel InputModel { get; private set; }

        public List<LoanProvider> LoanProviders { get; private set; }

        public Quote Quote { get; private set; }


        public QuoteModel(string[] args, double minimumLoan = 1000, double maximumLoan = 15000, double multiplesOf = 100)
        {
            Args = args;
            MinimumLoan = minimumLoan;
            MaximumLoan = maximumLoan;
            MultiplesOf = multiplesOf;
            InputModel = new InputModel();
            LoanProviders = new List<LoanProvider>();
            ValidationResult = ValidationResult.Valid();
        }

        public void SetErrorMessage(string errorMessage)
        {
            ValidationResult = ValidationResult.Invalid(errorMessage);
        }

        internal void SetLoanProviders(List<LoanProvider> loanProviders)
        {
            LoanProviders = loanProviders;
        }
    }
}
