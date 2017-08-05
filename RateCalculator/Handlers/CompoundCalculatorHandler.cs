using RateCalculator.Models;
using System;

namespace RateCalculator.Handlers
{
    public class CompoundCalculatorHandler : Handler, IHandler
    {
        public override void HandleRequest(QuoteModel quote)
        {
            var a = 1 + (quote.MinimumLendingRateAvailable / quote.RecalculationInterval);
            var c = Math.Pow(a, (quote.Years * quote.RecalculationInterval));

            var totalAmountToPay = quote.InputModel.LoanAmount * c;

            quote.SetQuote(Quote.Create(quote.InputModel.LoanAmount, quote.MinimumLendingRateAvailable, totalAmountToPay, totalAmountToPay / (quote.Years * quote.RecalculationInterval)));
        }
    }
}
