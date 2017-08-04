using RateCalculator.Models;

namespace RateCalculator.Domain
{
    public class QuoteResponseFactory : IQuoteResponseFactory
    {
        public const string NO_LOAN_PROVIDER_FOUND = "Sorry, is not possible to provide a quote at this time";

        readonly IInputprocessor _inputValidator;
        readonly ILendersFileReader _rateFileReader;
        readonly ILenderSelector _lenderSelector;
        readonly IQuoteCalculator _quoteCalculator;

        public QuoteResponseFactory(
            IInputprocessor inputValidator,
            ILendersFileReader rateFileReader,
            ILenderSelector lenderSelector,
            IQuoteCalculator quoteCalculator
        )
        {
            _inputValidator = inputValidator;
            _rateFileReader = rateFileReader;
            _lenderSelector = lenderSelector;
            _quoteCalculator = quoteCalculator;
        }

        public QuoteResponseFactory() : this(
            new InputProcessor(1000, 15000, 100),
            new LendersFileReader(),
            new LenderSelector(),
            new CompoundQuoteCalculator(12, 3))
        { }

        public QuoteResponse GetQuote(string[] args)
        {
            QuoteResponse result = new QuoteResponse();

            var input = _inputValidator.ProcessInput(args);
            if(!input.ValidationResult.IsValid)
            {
                result.SetErrorMessage(input.ValidationResult.ErrorMessage);
                return result;
            }
           
            var readerResult = _rateFileReader.Read(input.FileName);
            if (!readerResult.ValidationResult.IsValid)
            {
                result.SetErrorMessage(readerResult.ValidationResult.ErrorMessage);
                return result;
            }

            var loanProvider = _lenderSelector.ChooseLender(readerResult.Lenders, input.LoanAmount);
            if(loanProvider == null)
            {
                result.SetErrorMessage(NO_LOAN_PROVIDER_FOUND);
                return result;
            }

            result.SetQuote(_quoteCalculator.GetQuote(input.LoanAmount, loanProvider.Rate));

            return result;
        }
    }
}
