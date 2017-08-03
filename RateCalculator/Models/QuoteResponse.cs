namespace RateCalculator.Models
{
    public class QuoteResponse : ModelWithValidation
    {
        public Quote Quote { get; private set; }

        public QuoteResponse() : base()
        {
            Quote = null;
        }

        public void SetQuote(Quote quote)
        {
            Quote = quote;
        }

    }
}
