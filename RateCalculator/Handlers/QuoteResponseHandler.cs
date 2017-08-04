using RateCalculator.Models;
using System;

namespace RateCalculator.Handlers
{
    class QuoteResponseHandler : Handler, IHandler
    {
        public override void HandleRequest(QuoteModel quote)
        {
            if (quote.ValidationResult.IsValid)
            {
                successor.HandleRequest(quote);
            }
        }
    }
}
