using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RateCalculator.Models;
using RateCalculator.Validators;

namespace RateCalculator.Domain
{
    public interface IQuoteOrchestrator
    {
        QuoteResponse GetQuote(string[] args);
    }

    public class QuoteOrchestrator : IQuoteOrchestrator
    {
        public const string NO_LOAN_PROVIDER_FOUND = "Sorry, is not possible to provide a quote at this time";

        readonly IInputprocessor _inputValidator;
        readonly IRateFileReader _rateFileReader;
        readonly ILenderSelector _lenderSelector;
        readonly IQuoteCalculator _quoteCalculator;

        public QuoteOrchestrator(
            IInputprocessor inputValidator,
            IRateFileReader rateFileReader,
            ILenderSelector lenderSelector,
            IQuoteCalculator quoteCalculator
        )
        {
            _inputValidator = inputValidator;
            _rateFileReader = rateFileReader;
            _lenderSelector = lenderSelector;
            _quoteCalculator = quoteCalculator;
        }

        public QuoteOrchestrator() : this(
            new InputProcessor(1000, 15000, 100),
            new RateFileReader(),
            new LenderSelector(),
            new CompoundingQuoteCalculator(12, 3))
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

            var loanProvider = _lenderSelector.ChooseLender(readerResult.LoanProviders, input.LoanAmmount);
            if(loanProvider == null)
            {
                result.SetErrorMessage(NO_LOAN_PROVIDER_FOUND);
                return result;
            }

            result.SetQuote(_quoteCalculator.GetQuote(input.LoanAmmount, loanProvider.Rate));

            return result;
        }
    }
}
