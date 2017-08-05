using RateCalculator.Models;
using System;
using System.Collections.Generic;

namespace RateCalculator
{
    public interface IOutputFormatter
    {
        List<string> GenerateOutput(QuoteModel quote);
    }

    public class ConsoleFormatter : IOutputFormatter
    {
        public List<string> GenerateOutput(QuoteModel quote)
        {
            if(!quote.ValidationResult.IsValid)
            {
                return new List<string> { quote.ValidationResult.ErrorMessage };
            }

            return new List<string> {
                $"Requested amount: £{quote.Quote.RequestedAmount}",
                $"Rate: {Math.Round(quote.Quote.Rate * 100, 1).ToString("0.0")}%",
                $"Monthly repayment: £{Math.Round(quote.Quote.MonthlyRepayment, 2).ToString("0.00")}",
                $"Total repayment: £{Math.Round(quote.Quote.TotalRepayment, 2).ToString("0.00")}"
            };
        }
    }
}