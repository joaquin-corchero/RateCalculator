using RateCalculator.Models;

namespace RateCalculator.Domain
{
    public interface IQuoteCalculator
    {
        Quote GetQuote(double loanAmount, double rate);
    }
}
