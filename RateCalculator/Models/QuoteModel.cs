using System.Collections.Generic;

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

        public double MinimumLendingRateAvailable { get; private set; }

        public int Years { get; private set; }

        public int RecalculationInterval { get; private set; }
      
        QuoteModel(string[] args, double minimumLoan, double maximumLoan, double multiplesOf, int years, int recalculationInterval)
        {
            Args = args;
            MinimumLoan = minimumLoan;
            MaximumLoan = maximumLoan;
            MultiplesOf = multiplesOf;
            Years = years;
            RecalculationInterval = recalculationInterval;
            InputModel = new InputModel();
            LoanProviders = new List<LoanProvider>();
            ValidationResult = ValidationResult.Valid();
        }

        public static QuoteModel Create(string[] args, double minimumLoan = 1000, double maximumLoan = 15000, double multiplesOf = 100, int years =3, int recalculationInterval = 12)
        {
            return new QuoteModel(args, minimumLoan, maximumLoan, multiplesOf, years, recalculationInterval);
        }

        public void SetErrorMessage(string errorMessage)
        {
            ValidationResult = ValidationResult.Invalid(errorMessage);
        }

        public void SetLoanProviders(List<LoanProvider> loanProviders)
        {
            LoanProviders = loanProviders;
        }

        public void SetMinimumAvailableRate(double rate)
        {
            MinimumLendingRateAvailable = rate;
        }

        public void SetQuote(Quote quote)
        {
            Quote = quote;
        }
    }
}
