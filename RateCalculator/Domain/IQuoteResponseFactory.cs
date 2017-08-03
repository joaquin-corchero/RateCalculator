using RateCalculator.Models;

namespace RateCalculator.Domain
{
    public interface IQuoteResponseFactory
    {
        QuoteResponse GetQuote(string[] args);
    }
}
