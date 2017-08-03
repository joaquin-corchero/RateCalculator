namespace RateCalculator.Models
{
    public class Quote
    {
        public object RequestedAmmount { get; internal set; }
        public object Rate { get; internal set; }
        public object MonthlyRepayment { get; internal set; }
        public object TotalRepayment { get; internal set; }
    }
}