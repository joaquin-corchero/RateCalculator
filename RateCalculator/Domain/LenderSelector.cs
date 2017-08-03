using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RateCalculator.Models;

namespace RateCalculator.Domain
{
    public interface ILenderSelector
    {
        LoanProvider ChooseLender(List<LoanProvider> rates, double loanAmmount);
    }

    public class LenderSelector : ILenderSelector
    {
        public LoanProvider ChooseLender(List<LoanProvider> rates, double loanAmmount)
        {
            throw new NotImplementedException();
        }
    }
}
