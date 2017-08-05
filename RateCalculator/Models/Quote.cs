namespace RateCalculator.Models
{
    public class Quote
    {
        public double RequestedAmount { get;}

        public double Rate { get;}

        public double MonthlyRepayment { get;}

        public double TotalRepayment { get;}

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