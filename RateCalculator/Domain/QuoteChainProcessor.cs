using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        IHandler _quoteHandler;
        IHandler _outputHandler;

        public QuoteChainProcessor() : this(
            new InputHandler(),
            new LoanProviderHandler(),
            new RateFinderHandler(),
            new QuoteResponseHandler(),
            new OutputHandler())
        {
        }

        public QuoteChainProcessor(
            IHandler inputHandler,
            IHandler loanProviderReaderHandler,
            IHandler rateFinderHandler,
            IHandler quoteHandler,
            IHandler outputHandler)
        {
            _inputHandler = inputHandler;
            _loanProviderReaderHandler = loanProviderReaderHandler;
            _rateFinderHandler = rateFinderHandler;
            _quoteHandler = quoteHandler;
            _outputHandler = outputHandler;
        }

        public QuoteModel Process(string[] args)
        {
            _inputHandler.SetSuccessor(_loanProviderReaderHandler);
            _loanProviderReaderHandler.SetSuccessor(_rateFinderHandler);
            _rateFinderHandler.SetSuccessor(_quoteHandler);
            _quoteHandler.SetSuccessor(_outputHandler);

            var quote = new QuoteModel(args);

            _inputHandler.HandleRequest(quote);

            return quote;
        }
    }
}
