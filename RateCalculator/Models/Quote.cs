namespace RateCalculator.Models
{
    public class Quote
    {
        public double RequestedAmount { get; set; }
        public double Rate { get; set; }
        public double MonthlyRepayment { get; set; }
        public double TotalRepayment { get; set; }

        Quote(double requestedAmount, double interestRate, double totalAmountToPay, double monthlyRepayment)
        {
            RequestedAmount = requestedAmount;
            Rate = interestRate;
            TotalRepayment = totalAmountToPay;
            MonthlyRepayment = monthlyRepayment;
        }

        public static Quote Create(double loanAmount, double interestRate, double totalAmountToPay, double monthlyRepayment)
        {
            return new Quote(loanAmount, interestRate, totalAmountToPay, monthlyRepayment);
        }
    }
}