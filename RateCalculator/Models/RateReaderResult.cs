using RateCalculator.Validators;
using System.Collections.Generic;

namespace RateCalculator.Models
{
    public class LoanProviderResult : ModelWithValidation
    {
        public List<LoanProvider> LoanProviders { get; private set; }

        public LoanProviderResult() : base()
        {
            LoanProviders = new List<LoanProvider>();
        }

        public void SetRates(List<LoanProvider> loanProviders)
        {
            LoanProviders = loanProviders;
        }
    }
}
