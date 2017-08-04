using System.Collections.Generic;

namespace RateCalculator.Models
{
    public class LenderReaderResult : ModelWithValidation
    {
        public List<LoanProvider> Lenders { get; private set; }

        public LenderReaderResult() : base()
        {
            Lenders = new List<LoanProvider>();
        }

        public void SetLenders(List<LoanProvider> lenders)
        {
            Lenders = lenders;
        }
    }
}
