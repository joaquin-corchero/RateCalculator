using RateCalculator.Models;
using System.Collections.Generic;

namespace RateCalculator.Domain
{
    public interface ILenderSelector
    {
        LoanProvider ChooseLender(List<LoanProvider> rates, double loanAmount);
    }
}
