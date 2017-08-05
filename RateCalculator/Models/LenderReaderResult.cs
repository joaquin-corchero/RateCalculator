using System.Collections.Generic;

namespace RateCalculator.Models
{
    public class LenderReaderResult
    {
        public List<LoanProvider> Lenders { get; private set; }

        public void SetLenders(List<LoanProvider> lenders)
        {
            Lenders = lenders;
        }
    }
}
