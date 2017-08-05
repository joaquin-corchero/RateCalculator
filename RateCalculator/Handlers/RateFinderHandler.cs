using RateCalculator.Models;
using System;
using System.Linq;

namespace RateCalculator.Handlers
{
    public class RateFinderHandler : Handler, IHandler
    {
        public const string NO_RATE_AVAILABLE = "No rates available for the loan amount requested";

        public override void HandleRequest(QuoteModel quote)
        {
            quote.SetMinimumAvailableRate(quote.LoanProviders
                    .Where(r => r.Available >= quote.InputModel.LoanAmount)
                    .OrderBy(r => r.Rate)
                    .Select(r => r.Rate)
                    .FirstOrDefault());

            if (quote.MinimumLendingRateAvailable == 0)
            {
                quote.SetErrorMessage(NO_RATE_AVAILABLE);
                return;
            }

            successor.HandleRequest(quote);
        }
    }
}
