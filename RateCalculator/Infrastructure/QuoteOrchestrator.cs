using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RateCalculator.Models;
using RateCalculator.Validators;

namespace RateCalculator.Infrastructure
{
    public interface IQuoteOrchestrator
    {
        QuoteResponse GetQuote(string[] args);
    }

    public class QuoteOrchestrator : IQuoteOrchestrator
    {
        readonly IInputValidator _inputValidator;

        public QuoteOrchestrator(IInputValidator inputValidator)
        {
            _inputValidator = inputValidator;
        }

        public QuoteOrchestrator() : this(new InputValidator(1000, 15000, 100))
   {
        }

        public QuoteResponse GetQuote(string[] args)
        {
            QuoteResponse response = new QuoteResponse();
            response.SetValidationResult(_inputValidator.Validate(args));

            return response;
        }
    }
}
