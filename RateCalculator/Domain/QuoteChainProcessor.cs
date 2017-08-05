using RateCalculator.Handlers;
using RateCalculator.Models;

namespace RateCalculator.Domain
{
    public interface IQuoteChainProcessor
    {
        QuoteModel Process(string[] args);
    }

    public class QuoteChainProcessor : IQuoteChainProcessor
    {
        IHandler _inputHandler;
        IHandler _loanProviderReaderHandler;
        IHandler _rateFinderHandler;
        IHandler _quoteCalculatorHandler;

        public QuoteChainProcessor() : this(
            new InputHandler(),
            new LoanProviderHandler(),
            new RateFinderHandler(),
            new CompoundCalculatorHandler())
        {
        }

        public QuoteChainProcessor(
            IHandler inputHandler,
            IHandler loanProviderReaderHandler,
            IHandler rateFinderHandler,
            IHandler quoteCalculatorHandler)
        {
            _inputHandler = inputHandler;
            _loanProviderReaderHandler = loanProviderReaderHandler;
            _rateFinderHandler = rateFinderHandler;
            _quoteCalculatorHandler = quoteCalculatorHandler;
        }

        public QuoteModel Process(string[] args)
        {
            _inputHandler.SetSuccessor(_loanProviderReaderHandler);
            _loanProviderReaderHandler.SetSuccessor(_rateFinderHandler);
            _rateFinderHandler.SetSuccessor(_quoteCalculatorHandler);

            var quote = QuoteModel.Create(args);

            _inputHandler.HandleRequest(quote);

            return quote;
        }
    }
}
