using RateCalculator.Models;
using System;

namespace RateCalculator.Handlers
{
    public class InputHandler : Handler, IHandler
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
