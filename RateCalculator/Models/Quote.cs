namespace RateCalculator.Models
{
    public class Quote
    {
        public double RequestedAmmount { get; internal set; }
        public double Rate { get; internal set; }
        public double MonthlyRepayment { get; internal set; }
        public double TotalRepayment { get; internal set; }
    }
}