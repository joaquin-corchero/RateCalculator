using RateCalculator.Models;
using System.Collections.Generic;
using System.Linq;

namespace RateCalculator.Domain
{
    public class LenderSelector : ILenderSelector
    {
        public LoanProvider ChooseLender(List<LoanProvider> loanProviders, double loandAmount)
        {
            return loanProviders
                .Where(r => r.Available >= loandAmount)
                .OrderBy(r => r.Rate)
                .FirstOrDefault();
        }
    }
}
