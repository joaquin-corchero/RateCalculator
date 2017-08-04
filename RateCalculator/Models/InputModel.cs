namespace RateCalculator.Models
{
    public class InputModel : ModelWithValidation
    {
        public string FileName { get; private set; }

        public double LoanAmount { get; private set; }

        public void SetFileName(string fileName)
        {
            FileName = fileName;
        }

        public void SetLoanAmount(double loanAmount)
        {
            LoanAmount = loanAmount;
        }
    }
}
