using RateCalculator.Models;
using System;

namespace RateCalculator.Handlers
{
    public class InputHandler : Handler, IHandler
    {
        public const string INVALID_PARAMETERS = "Incorrect arguments, please follow the format: ratecalculator.exe market.csv 1500";

        public override void HandleRequest(QuoteModel quote)
        {
            if (quote.ValidationResult.IsValid)
            {
                successor.HandleRequest(quote);
            }
        }
    }
}
